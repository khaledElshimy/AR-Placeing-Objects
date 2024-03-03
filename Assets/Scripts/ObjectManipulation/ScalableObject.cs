using UnityEngine;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Allows objects to be scaled up or down through pinch gestures.
    /// Implements the IScalable interface to define scaling behavior.
    /// </summary>
    public class ScalableObject : IScalable
    {
        private float initialDistance; // Stores the initial distance between two touch points.

        public Vector3 InitialScale { get; set; } // Initial scale of the object, used for calculating scale adjustments.
        public float MinScaleLimit { get; set; } // Minimum allowed scale factor.
        public float MaxScaleLimit { get; set; } // Maximum allowed scale factor.
        public float Damping { get; set; } // Damping factor for smooth scaling transition.

        /// <summary>
        /// Scales the object based on the distance between two touch points.
        /// </summary>
        /// <param name="transform">Transform of the object to scale.</param>
        /// <param name="arCamera">AR camera used for raycasting.</param>
        /// <param name="touchZero">First touch point.</param>
        /// <param name="touchOne">Second touch point.</param>
        public void Scale(Transform transform, Camera arCamera, Touch touchZero, Touch touchOne)
        {
            // Perform raycast from camera to ensure the object is being touched.
            Ray ray = arCamera.ScreenPointToRay(touchZero.position);
            RaycastHit hit;

            // Initial touch detection.
            if (touchZero.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the touched object is not the target object.
                    if (hit.collider == null || hit.collider.gameObject != transform.gameObject)
                    {
                        return; // Exit if the touched object is not this object.
                    }
                }
            }

            // Calculate the distance between touches for the current and previous frame.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Initialize scale calculation on the first touch.
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = touchDeltaMag;
            }
            else
            {
                // Calculate and apply scaling factor.
                float scaleFactor = touchDeltaMag / initialDistance;
                scaleFactor = Mathf.Clamp(scaleFactor, MinScaleLimit, MaxScaleLimit);

                Vector3 targetScale = InitialScale * scaleFactor;
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Damping * Time.deltaTime);
            }
        }
    }
}
