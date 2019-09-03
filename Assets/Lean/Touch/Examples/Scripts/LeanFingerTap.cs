using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Lean.Touch
{
    /// <summary>This script calls the OnFingerTap event when a finger taps the screen.</summary>
    [HelpURL(LeanTouch.HelpUrlPrefix + "LeanFingerTap")]
    public class LeanFingerTap : MonoBehaviour
    {
        [Tooltip("Ignore fingers with OverGui?")]
        public bool IgnoreIsOverGui;

        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreStartedOverGui = true;

        [FormerlySerializedAs("OnTap")] [SerializeField]
        private LeanFingerEvent onTap;

        [Tooltip("Do nothing if this LeanSelectable isn't selected?")]
        public LeanSelectable RequiredSelectable;

        [Tooltip(
            "How many times must this finger tap before OnTap gets called? (0 = every time) Keep in mind OnTap will only be called once if you use this.")]
        public int RequiredTapCount;

        [Tooltip(
            "How many times repeating must this finger tap before OnTap gets called? (0 = every time) (e.g. a setting of 2 means OnTap will get called when you tap 2 times, 4 times, 6, 8, 10, etc)")]
        public int RequiredTapInterval;

        public LeanFingerEvent OnTap
        {
            get
            {
                if (onTap == null) onTap = new LeanFingerEvent();
                return onTap;
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
            LeanTouch.OnFingerTap += FingerTap;
        }

        protected virtual void OnDisable()
        {
            // Unhook events
            LeanTouch.OnFingerTap -= FingerTap;
        }

        private void FingerTap(LeanFinger finger)
        {
            // Ignore?
            if (IgnoreStartedOverGui && finger.StartedOverGui) return;

            if (IgnoreIsOverGui && finger.IsOverGui) return;

            if (RequiredTapCount > 0 && finger.TapCount != RequiredTapCount) return;

            if (RequiredTapInterval > 0 && finger.TapCount % RequiredTapInterval != 0) return;

            if (RequiredSelectable != null && RequiredSelectable.IsSelected == false) return;

            // Call event
            if (onTap != null) onTap.Invoke(finger);
        }

        // Event signature
        [Serializable]
        public class LeanFingerEvent : UnityEvent<LeanFinger>
        {
        }
    }
}