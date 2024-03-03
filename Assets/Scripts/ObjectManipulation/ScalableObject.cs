using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public class ScalableObject : IScalable
    {
        private float initialDistance;

        public Vector3 InitialScale { get; set; }
        public float MinScaleLimit { get; set; }
        public float MaxScaleLimit { get; set; }
        public float Damping { get; set; }

        public void Scale(Transform transform, Camera arCamera, Touch touchZero, Touch touchOne)
        {
            
            Ray ray = arCamera.ScreenPointToRay(touchZero.position);
            RaycastHit hit;

            if (touchZero.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == null || hit.collider.gameObject != transform.gameObject)
                    {
                        return;
                    }
                }
            }

            // Get the position of the touches in the previous frame
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (distance) between the touches in each frame
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

         

            // If this is the first frame where touches are detected, initialize initial distance and scale
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = touchDeltaMag;
            }
            else
            {
                // Calculate scale factor based on the change in distance between the touches
                float scaleFactor = touchDeltaMag / initialDistance;
                scaleFactor = Mathf.Clamp(scaleFactor, MinScaleLimit, MaxScaleLimit);

                // Set the scale of the object
                Vector3 targetScale = InitialScale * scaleFactor;
                // Smoothly interpolate to the target scale
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Damping * Time.deltaTime);
            }
        }
    }
}