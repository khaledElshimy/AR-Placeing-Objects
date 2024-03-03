using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Defines the contract for movable objects within the application.
    /// This interface specifies the properties and methods required to implement moving behavior in AR.
    /// </summary>
    public interface IMovable
    {
        // Indicates whether the object is currently being moved.
        bool IsMoving { get; set; }
        // The damping factor to smooth the moving effect.
        float Damping { get; set; }

        /// <summary>
        /// Initiates the movement of the object using ARRaycastManager to determine the new position based on user input.
        /// </summary>
        /// <param name="transform">The transform of the object to move.</param>
        /// <param name="raycastManager">The ARRaycastManager instance used for raycasting to find surfaces.</param>
        void Move(Transform transform, ARRaycastManager raycastManager);
    }
}
