using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Lean.Touch
{
    /// <summary>This script calls the OnUp event when a finger stops touching the screen.</summary>
    [HelpURL(LeanTouch.HelpUrlPrefix + "LeanFingerUp")]
    public class LeanFingerUp : MonoBehaviour
    {
        [Tooltip("Ignore fingers with IsOverGui?")]
        public bool IgnoreIsOverGui;

        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreStartedOverGui = true;

        [FormerlySerializedAs("OnUp")] [SerializeField]
        private LeanFingerEvent onUp;

        [Tooltip("Do nothing if this LeanSelectable isn't selected?")]
        public LeanSelectable RequiredSelectable;

        public LeanFingerEvent OnUp
        {
            get
            {
                if (onUp == null) onUp = new LeanFingerEvent();
                return onUp;
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
            LeanTouch.OnFingerUp += FingerUp;
        }

        protected virtual void OnDisable()
        {
            // Unhook events
            LeanTouch.OnFingerUp -= FingerUp;
        }

        private void FingerUp(LeanFinger finger)
        {
            // Ignore?
            if (IgnoreStartedOverGui && finger.StartedOverGui) return;

            if (IgnoreIsOverGui && finger.IsOverGui) return;

            if (RequiredSelectable != null && RequiredSelectable.IsSelected == false) return;

            // Call event
            if (onUp != null) onUp.Invoke(finger);
        }

        // Event signature
        [Serializable]
        public class LeanFingerEvent : UnityEvent<LeanFinger>
        {
        }
    }
}