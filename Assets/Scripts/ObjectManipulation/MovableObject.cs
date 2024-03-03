using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace arplace.ObjectManipulation
{
    public class MovableObject : IMovable
    {
        private bool isMoving = false;

        public bool IsMoving { get => isMoving; set => isMoving = value; }
        public float Damping { get; set; }

        public void Move(Transform transform, ARRaycastManager raycastManager)
        {
            Debug.Log("Move.");
            IsMoving = true;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                Debug.Log("Touch Phaes Moved for movement.");

                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    Vector3 newPos = Vector3.Lerp(transform.localPosition, hitPose.position, Damping * Time.deltaTime);
                    transform.localPosition = newPos;
                    Debug.Log($"Moving to new position: {newPos}");
                }
                else
                {
                    Debug.Log("Raycast did not hit an AR plane.");
                }
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isMoving = false;
                Debug.Log("Movement ended.");
            }
        }
    }
}
