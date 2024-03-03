using UnityEngine;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Defines the contract for scalable objects within the application.
    /// This interface specifies properties and methods required for implementing scalable behavior in AR.
    /// </summary>
    public interface IScalable
    {
        // Minimum scale limit to prevent objects from being scaled down too much.
        float MinScaleLimit { get; set; }
        // The initial scale of the object, used as a reference for scaling operations.
        Vector3 InitialScale { get; set; }

        // Maximum scale limit to prevent objects from being scaled up excessively.
        float MaxScaleLimit { get; set; }
        // The damping factor to smooth the scaling effect, making it more natural.
        float Damping { get; set; }

        /// <summary>
        /// Scales the object based on user input, typically pinching gestures, with respect to the camera view.
        /// </summary>
        /// <param name="transform">The transform of the object to scale.</param>
        /// <param name="arCamera">The AR camera used for scaling in relation to the user's viewpoint.</param>
        /// <param name="touchZero">The first touch point used for scaling.</param>
        /// <param name="touchOne">The second touch point used for scaling.</param>
        void Scale(Transform transform, Camera arCamera, Touch touchZero, Touch touchOne);
    }
}
