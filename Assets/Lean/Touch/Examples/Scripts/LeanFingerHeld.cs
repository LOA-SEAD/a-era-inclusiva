using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Lean.Touch
{
    /// <summary>This script fires events if a finger has been held for a certain amount of time without moving.</summary>
    [HelpURL(LeanTouch.HelpUrlPrefix + "LeanFingerHeld")]
    public class LeanFingerHeld : MonoBehaviour
    {
        [Tooltip("Ignore fingers with IsOverGui?")]
        public bool IgnoreIsOverGui;

        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreStartedOverGui = true;

        // This stores all finger links
        private readonly List<Link> links = new List<Link>();

        [Tooltip("The finger cannot move more than this many pixels relative to the reference DPI")]
        public float MaximumMovement = 5.0f;

        [Tooltip("The finger must be held for this many seconds")]
        public float MinimumAge = 1.0f;

        [FormerlySerializedAs("OnHeldDown")] [SerializeField]
        private LeanFingerEvent onHeldDown;

        [FormerlySerializedAs("OnHeldSet")] [SerializeField]
        private LeanFingerEvent onHeldSet;

        [FormerlySerializedAs("OnHeldUp")] [SerializeField]
        private LeanFingerEvent onHeldUp;

        [Tooltip("Do nothing if this LeanSelectable isn't selected?")]
        public LeanSelectable RequiredSelectable;

        /// <summary>Called on the first frame the conditions are met.</summary>
        public LeanFingerEvent OnHeldDown
        {
            get
            {
                if (onHeldDown == null) onHeldDown = new LeanFingerEvent();
                return onHeldDown;
            }
        }

        /// <summary>Called on every frame the conditions are met.</summary>
        public LeanFingerEvent OnHeldSet
        {
            get
            {
                if (onHeldSet == null) onHeldSet = new LeanFingerEvent();
                return onHeldSet;
            }
        }

        /// <summary>Called on the last frame the conditions are met.</summary>
        public LeanFingerEvent OnSelect
        {
            get
            {
                if (onHeldUp == null) onHeldUp = new LeanFingerEvent();
                return onHeldUp;
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
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerSet += OnFingerSet;
            LeanTouch.OnFingerUp += OnFingerUp;
        }

        protected virtual void OnDisable()
        {
            // Unhook events
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerSet -= OnFingerSet;
            LeanTouch.OnFingerUp -= OnFingerUp;
        }

        private void OnFingerDown(LeanFinger finger)
        {
            // Ignore?
            if (IgnoreStartedOverGui && finger.StartedOverGui) return;
            if (IgnoreIsOverGui && finger.IsOverGui) return;

            if (RequiredSelectable != null && RequiredSelectable.IsSelected == false) return;

            // Get link for this finger and reset
            var link = FindLink(finger, true);

            link.LastSet = false;
            link.TotalScaledDelta = Vector2.zero;
        }

        private void OnFingerSet(LeanFinger finger)
        {
            // Try and find the link for this finger
            var link = FindLink(finger, false);

            if (link != null)
            {
                // Has this finger been held for more than MinimumAge without moving more than MaximumMovement?
                var set = finger.Age >= MinimumAge && link.TotalScaledDelta.magnitude < MaximumMovement;

                link.TotalScaledDelta += finger.ScaledDelta;

                if (set && link.LastSet == false)
                    if (onHeldDown != null)
                        onHeldDown.Invoke(finger);

                if (set)
                    if (onHeldSet != null)
                        onHeldSet.Invoke(finger);

                if (set == false && link.LastSet)
                    if (onHeldUp != null)
                        onHeldUp.Invoke(finger);

                // Store last value
                link.LastSet = set;
            }
        }

        private void OnFingerUp(LeanFinger finger)
        {
            // Find link for this finger, and clear it
            var link = FindLink(finger, false);

            if (link != null)
            {
                links.Remove(link);

                if (link.LastSet)
                    if (onHeldUp != null)
                        onHeldUp.Invoke(finger);
            }
        }

        private Link FindLink(LeanFinger finger, bool createIfNull)
        {
            // Find existing link?
            for (var i = 0; i < links.Count; i++)
            {
                var link = links[i];

                if (link.Finger == finger) return link;
            }

            // Make new link?
            if (createIfNull)
            {
                var link = new Link();

                link.Finger = finger;

                links.Add(link);

                return link;
            }

            return null;
        }

        // Event signature
        [Serializable]
        public class LeanFingerEvent : UnityEvent<LeanFinger>
        {
        }

        // This class will store extra Finger data
        [Serializable]
        public class Link
        {
            public LeanFinger Finger; // The finger associated with this link
            public bool LastSet; // Was this finger held?
            public Vector2 TotalScaledDelta; // The total movement so we can ignore it if it gets too high
        }
    }
}