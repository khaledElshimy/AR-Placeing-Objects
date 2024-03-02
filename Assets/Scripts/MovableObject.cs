using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace arplace.ObjectManipulation
{
    public class MovableObject : MonoBehaviour
    {
        private Camera cam;
        private Vector3 offset;
        private bool isDragging = false;
        private float dampening = 5;
            private ARRaycastManager raycastManager;

        void Start()
        {
            raycastManager = FindObjectOfType<ARRaycastManager>();
            cam = Camera.main;
        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (touch.phase == TouchPhase.Began)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                            isDragging = true;
                            offset = gameObject.transform.position - hit.point;
                        }
                    }
                }

                if (touch.phase == TouchPhase.Moved && isDragging)
                {
                    List<ARRaycastHit> hits = new List<ARRaycastHit>();

                    if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                    {
                        Debug.Log("raycastManager hits");
                        Pose hitPose = hits[0].pose;
                        Vector3 newPos = Vector3.Lerp(transform.localPosition, hitPose.position, dampening * Time.deltaTime);
                       transform.localPosition = newPos;

                    }                                
                }
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                }
            }
        }
    }
}
