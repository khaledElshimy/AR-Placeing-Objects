using UnityEngine;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Defines the contract for rotatable objects within the application.
    /// This interface specifies the properties and methods required to implement rotation behavior.
    /// </summary>
    public interface IRotatable
    {
        // Indicates whether the object is currently being rotated.
        bool IsRotating { get; set; }
        // The damping factor to smooth the rotation effect.
        float Damping { get; set; }
        /// <summary>
        /// Initiates the rotation of the object.
        /// </summary>
        /// <param name="transform">The transform of the object to rotate.</param>
        void Rotate(Transform transform);
    }
}
