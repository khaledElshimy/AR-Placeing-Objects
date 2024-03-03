using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public class RotatableObject : IRotatable
    {
        private bool isRotating = false;
        public float Damping { get; set; }

        public bool IsMoving { get => isRotating; set => isRotating = value; }

        bool IRotatable.IsRotating { get => isRotating; set => isRotating = value; }

        public void Rotate(Transform transform)
        {
            isRotating = true;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch Phaes Moved for rotation.");

                float rotationAmount = -touch.deltaPosition.x * Damping;
                transform.Rotate(0f, rotationAmount, 0f, Space.World);
        
                Debug.Log($"Rotating object: {rotationAmount} degrees.");
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isRotating = false;
                Debug.Log("Rotation ended.");
            }
        }
    }
}
