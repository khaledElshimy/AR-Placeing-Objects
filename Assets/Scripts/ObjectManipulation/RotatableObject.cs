using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public class RotatableObject : MonoBehaviour
    {
        private bool isRotating = false;
        [SerializeField]
        private float rotationRateDegreesDrag = 0.1f; // Adjust this value to control rotation sensitivity

        void Update()
        {
            if (Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0);
                if (screenTouch.phase == TouchPhase.Moved)
                {
                    transform.Rotate(0f, -screenTouch.deltaPosition.x * rotationRateDegreesDrag, 0f, Space.World);

                    isRotating = true;
                }
                if (screenTouch.phase == TouchPhase.Ended)
                {
                    isRotating = false;
                }
            }
        }
    }
}
