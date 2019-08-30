using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

namespace Lean.Common.Examples
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LeanMarker))]
    public class LeanMarker_Inspector : LeanInspector<LeanMarker>
    {
        protected override void DrawInspector()
        {
            BeginError(Any(t => t.Target == null));
            Draw("target");
            EndError();
        }
    }
}
#endif

namespace Lean.Common.Examples
{
	/// <summary>
	///     This component marks the Target object using the current GameObject name.
	///     This allows you to quickly find and access it from anywhere using the LeanMarker.Reference component.
	/// </summary>
	[ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Lean/Common/Lean Marker")]
    public class LeanMarker : MonoBehaviour
    {
        /// <summary>This stores all active an enables LeanMarker instances by their GameObject name.</summary>
        private static readonly Dictionary<string, LeanMarker> instances = new Dictionary<string, LeanMarker>();

        [NonSerialized] private string registeredName;

        /// <summary>The marker is pointing to this Object.</summary>
        [field: SerializeField]
        public Object Target { set; get; }

        protected virtual void OnEnable()
        {
            registeredName = name;

            instances.Add(registeredName, this);
        }

        protected virtual void OnDisable()
        {
            instances.Remove(registeredName);
        }
#if UNITY_EDITOR
        protected virtual void Reset()
        {
            Target = gameObject;
        }
#endif
	    /// <summary>
	    ///     This struct can be added to your custom components, allowing you to quickly find and efficiently access a
	    ///     marked GameObject.
	    /// </summary>
	    public class Reference<T>
            where T : Object
        {
            protected bool cached;

            protected T instance;

            protected string name;

            public Reference(string newName)
            {
                if (string.IsNullOrEmpty(newName))
                    throw new ArgumentException("Cannot reference a null or empty marker!");

                name = newName;
            }

            public T Instance
            {
                get
                {
                    if (cached == false) Find();

                    return instance;
                }
            }

            protected virtual void Build(LeanMarker marker)
            {
                if (typeof(T) == typeof(GameObject))
                {
                    if (marker.Target != null)
                    {
                        if (marker.Target is GameObject)
                        {
                            instance = (T) marker.Target;
                            return;
                        }

                        if (marker.Target is Component)
                        {
                            instance = (T) (Object) ((Component) marker.Target).gameObject;
                            return;
                        }
                    }
                    else
                    {
                        instance = (T) (Object) marker.gameObject;
                        return;
                    }
                }
                else if (typeof(T).IsSubclassOf(typeof(Component)))
                {
                    if (marker.Target != null)
                    {
                        if (marker.Target is T)
                        {
                            instance = (T) marker.Target;
                            return;
                        }

                        if (marker.Target is GameObject)
                        {
                            var component = ((GameObject) marker.Target).GetComponent<T>();

                            if (component != null)
                            {
                                instance = component;
                                return;
                            }
                        }
                        else if (marker.Target is Component)
                        {
                            var component = ((Component) marker.Target).GetComponent<T>();

                            if (component != null)
                            {
                                instance = component;
                                return;
                            }
                        }
                    }
                    else
                    {
                        var component = marker.gameObject.GetComponent<T>();

                        if (component != null)
                        {
                            instance = component;
                            return;
                        }
                    }
                }
                else if (marker.Target != null && marker.Target is T)
                {
                    instance = (T) marker.Target;
                    return;
                }

                throw new MissingMemberException();
            }

            protected void Find()
            {
                var marker = default(LeanMarker);

                if (instances.TryGetValue(name, out marker))
                {
                    Build(marker);

                    return;
                }

                var markers = FindObjectsOfType<LeanMarker>();

                for (var i = markers.Length - 1; i >= 0; i--)
                {
                    marker = markers[i];

                    if (marker.name == name)
                    {
                        Build(marker);

                        return;
                    }
                }

                throw new NullReferenceException("Failed to find LeanMarker in scene with name: " + name);
            }
        }
    }
}