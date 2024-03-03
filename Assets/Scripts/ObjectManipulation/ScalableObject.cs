using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public class ScalableObject : MonoBehaviour
    {
        private float initialDistance;
        private Vector3 initialScale;
        private Camera cam;

        [SerializeField]
        private float minScale;
        [SerializeField]
        private float maxScale;
        private float damping = 5f; // Adjust this value to control the smoothness

        private Vector3 targetScale;

        void Start()
        {
            // Initialize targetScale to the current local scale
            targetScale = transform.localScale;
            cam = Camera.main;

        }

        void Update()
        {
            // Check if there are exactly two touches on the screen
            if (Input.touchCount == 2)
            {
                
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Ray ray = cam.ScreenPointToRay(touchZero.position);
                RaycastHit hit;

                if (touchZero.phase == TouchPhase.Began)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider == null || hit.collider.gameObject != gameObject)
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

                // Find the difference in distances between each frame
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If this is the first frame where touches are detected, initialize initial distance and scale
                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialDistance = touchDeltaMag;
                    initialScale = transform.localScale;
                }
                else
                {
                    // Calculate scale factor based on the change in distance between the touches
                    float scaleFactor = touchDeltaMag / initialDistance;
                    scaleFactor = Mathf.Clamp(scaleFactor, minScale, maxScale);

                    // Set the scale of the object
                    targetScale = initialScale * scaleFactor;
                    // Smoothly interpolate to the target scale
                    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, damping * Time.deltaTime);
                }
            }
           
        }
    }
}