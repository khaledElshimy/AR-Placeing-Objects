using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace arplace.ArInteractableExtentions
{
    public class DoubleClickInteractable : ARBaseGestureInteractable
    {
        [SerializeField]
        private float maxTimeBetweenClicks = 0.5f; // Time window for double click

        private float lastTapTime = 0f;
        private int tapCount = 0;

        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            // Check if the gesture is targeting this GameObject
            if (gesture.targetObject == gameObject)
            {
                return true;
            }
            return false;
        }

        protected override void OnEndManipulation(TapGesture gesture)
        {
            if (gesture.isCanceled)
            {
                return; // Gesture was cancelled, do nothing
            }

            // Ensure the gesture has no target or that the target is this object
            if (gesture.targetObject == gameObject || gesture.targetObject == null)
            {
                float currentTime = Time.time;
                if (currentTime - lastTapTime < maxTimeBetweenClicks)
                {
                    // Detected a double tap
                    tapCount++;

                    if (tapCount == 2)
                    {
                        // Double tap confirmed
                        OnDoubleClick();
                        tapCount = 0; // Reset tap count after a double tap
                    }
                }
                else
                {
                    tapCount = 1; // Reset to 1 as we're starting a new tap sequence
                }

                lastTapTime = currentTime;
            }
        }

        private void OnDoubleClick()
        {
            // Double click action, for example, change color or invoke a custom event
            Debug.Log("Double Click Detected on " + gameObject.name);
            // Implement your double click logic here
        }
    }
}