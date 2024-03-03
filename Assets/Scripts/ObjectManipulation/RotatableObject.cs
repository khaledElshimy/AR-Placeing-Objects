using UnityEngine;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Implements the IRotatable interface to provide rotation functionality to objects.
    /// Allows for objects to be rotated based on user touch input.
    /// </summary>
    public class RotatableObject : IRotatable
    {
        private bool isRotating = false; // Tracks the rotation state of the object.
        public float Damping { get; set; } // Damping factor to smooth the rotation.

        // Property to get or set the object's rotating state.
        public bool IsMoving { get => isRotating; set => isRotating = value; }

        // Explicit interface property implementation for rotation state.
        bool IRotatable.IsRotating { get => isRotating; set => isRotating = value; }

        /// <summary>
        /// Rotates the object based on user touch movement.
        /// </summary>
        /// <param name="transform">The transform of the object to apply rotation to.</param>
        public void Rotate(Transform transform)
        {
            isRotating = true; // Indicate that rotation has started.
            Touch touch = Input.GetTouch(0); // Get the first touch input.

            // Check if the touch is moving to apply rotation.
            if (touch.phase == TouchPhase.Moved)
            {
                // Calculate rotation amount based on touch movement and damping.
                float rotationAmount = -touch.deltaPosition.x * Damping;
                // Apply rotation to the transform.
                transform.Rotate(0f, rotationAmount, 0f, Space.World);
            }
            // Check if the touch has ended or been canceled to stop rotation.
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isRotating = false; // Indicate that rotation has stopped.
            }
        }
    }
}
