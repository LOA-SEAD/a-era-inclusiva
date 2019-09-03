using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Lean.Touch
{
    /// <summary>This script calls the OnFingerSet event while a finger is touching the screen.</summary>
    [HelpURL(LeanTouch.HelpUrlPrefix + "LeanFingerSet")]
    public class LeanFingerSet : MonoBehaviour
    {
        public enum DeltaCoordinatesType
        {
            Screen,
            Scaled
        }

        [Tooltip("The coordinate space of the OnSetDelta values")]
        public DeltaCoordinatesType DeltaCoordinates;

        [Tooltip("If the finger didn't move, ignore it?")]
        public bool IgnoreIfStatic;

        [Tooltip("Ignore fingers with IsOverGui?")]
        public bool IgnoreIsOverGui;

        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreStartedOverGui = true;

        [FormerlySerializedAs("OnSet")] [SerializeField]
        private LeanFingerEvent onSet;

        [FormerlySerializedAs("OnSetDelta")] [SerializeField]
        private Vector2Event onSetDelta;

        [Tooltip("If RequiredSelectable.IsSelected is false, ignore?")]
        public LeanSelectable RequiredSelectable;

        public LeanFingerEvent OnSet
        {
            get
            {
                if (onSet == null) onSet = new LeanFingerEvent();
                return onSet;
            }
        }

        public Vector2Event OnSetDelta
        {
            get
            {
                if (onSetDelta == null) onSetDelta = new Vector2Event();
                return onSetDelta;
            }
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            Start();
        }
#endif

        protected virtual void Start()
        {
            if (RequiredSelectable == null) RequiredSelectable = GetComponent<LeanSelectable>();
        }

        protected virtual void OnEnable()
        {
            // Hook events
            LeanTouch.OnFingerSet += FingerSet;
        }

        protected virtual void OnDisable()
        {
            // Unhook events
            LeanTouch.OnFingerSet -= FingerSet;
        }

        private void FingerSet(LeanFinger finger)
        {
            // Get delta
            var delta = finger.ScreenDelta;

            // Ignore?
            if (IgnoreStartedOverGui && finger.StartedOverGui) return;

            if (IgnoreIsOverGui && finger.IsOverGui) return;

            if (IgnoreIfStatic && finger.ScreenDelta.magnitude <= 0.0f) return;

            if (RequiredSelectable != null && RequiredSelectable.IsSelected == false) return;

            // Scale?
            if (DeltaCoordinates == DeltaCoordinatesType.Scaled) delta *= LeanTouch.ScalingFactor;

            // Call event
            if (onSet != null) onSet.Invoke(finger);

            if (onSetDelta != null) onSetDelta.Invoke(delta);
        }

        // Event signature
        [Serializable]
        public class LeanFingerEvent : UnityEvent<LeanFinger>
        {
        }

        [Serializable]
        public class Vector2Event : UnityEvent<Vector2>
        {
        }
    }
}