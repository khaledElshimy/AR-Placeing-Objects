using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Implements the IMovable interface to provide movement functionality to objects.
    /// Allows for objects to be moved based on user touch input within AR environments.
    /// </summary>
    public class MovableObject : IMovable
    {
        private bool isMoving = false; // Tracks the moving state of the object.

        public bool IsMoving { get => isMoving; set => isMoving = value; } // Public access to the moving state.
        public float Damping { get; set; } // Damping factor to smooth the movement effect.

        /// <summary>
        /// Moves the object based on user touch input, utilizing ARRaycastManager to determine the new position on detected planes.
        /// </summary>
        /// <param name="transform">The transform of the object to move.</param>
        /// <param name="raycastManager">The ARRaycastManager instance used for raycasting to find surfaces.</param>
        public void Move(Transform transform, ARRaycastManager raycastManager)
        {
            IsMoving = true; // Indicate that movement has started.
            Touch touch = Input.GetTouch(0); // Get the first touch input.

            // Check if the touch is moving to update the object's position.
            if (touch.phase == TouchPhase.Moved)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                // Perform raycasting to find a suitable position on detected AR planes.
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    // Calculate new position based on the hit pose and apply damping.
                    Vector3 newPos = Vector3.Lerp(transform.localPosition, hitPose.position, Damping * Time.deltaTime);
                    transform.localPosition = newPos;
                }
                else
                {
                    Debug.Log("Raycast did not hit an AR plane.");
                }
            }
            // Check if the touch has ended or been canceled to stop moving.
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isMoving = false; // Indicate that movement has stopped.
            }
        }
    }
}
