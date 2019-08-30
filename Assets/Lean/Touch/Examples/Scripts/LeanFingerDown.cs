using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Lean.Touch
{
    /// <summary>This script calls the OnFingerDown event when a finger touches the screen.</summary>
    [HelpURL(LeanTouch.HelpUrlPrefix + "LeanFingerDown")]
    public class LeanFingerDown : MonoBehaviour
    {
        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreStartedOverGui = true;

        [FormerlySerializedAs("OnDown")] [SerializeField]
        private LeanFingerEvent onDown;

        [Tooltip("Do nothing if this LeanSelectable isn't selected?")]
        public LeanSelectable RequiredSelectable;

        public LeanFingerEvent OnDown
        {
            get
            {
                if (onDown == null) onDown = new LeanFingerEvent();
                return onDown;
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
            LeanTouch.OnFingerDown += FingerDown;
        }

        protected virtual void OnDisable()
        {
            // Unhook events
            LeanTouch.OnFingerDown -= FingerDown;
        }

        private void FingerDown(LeanFinger finger)
        {
            // Ignore?
            if (IgnoreStartedOverGui && finger.IsOverGui) return;

            if (RequiredSelectable != null && RequiredSelectable.IsSelected == false) return;

            // Call event
            onDown.Invoke(finger);
        }

        // Event signature
        [Serializable]
        public class LeanFingerEvent : UnityEvent<LeanFinger>
        {
        }
    }
}