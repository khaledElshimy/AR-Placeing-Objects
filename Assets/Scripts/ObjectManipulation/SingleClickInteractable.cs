using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace arplace.ObjectManipulation
{
    public class SingleClickInteractable : ARBaseGestureInteractable
    {
        [SerializeField]
        private UnityEvent singleClickAction;

        //private bool isObjectSelected;

        protected override bool CanStartManipulationForGesture(TapGesture gesture)
        {
            // Check if the gesture is targeting this GameObject
            if (gesture.targetObject == gameObject)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
        }

        /// <inheritdoc />
        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
        }

        // This method is called when the gesture ends
        protected override void OnEndManipulation(TapGesture gesture)
        {
            if (gesture.isCanceled)
            {
                return; // Gesture was cancelled, do nothing
            }

            if (gesture.targetObject == gameObject)
            {
                OnSingleClick();
            }
        }

        private void OnSingleClick()
        {          
            singleClickAction?.Invoke();
        }
    }
}
