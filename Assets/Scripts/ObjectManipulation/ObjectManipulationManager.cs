using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace arplace.ObjectManipulation
{
    public class ObjectManipulationManager : MonoBehaviour
    {
        private Camera arCamera;
        private ARRaycastManager arRaycastManager;
        private Vector3 targetScale;

        [SerializeField]
        private GameObject selectionVisuals;
        private ISelectable selectable;
        private IMovable movable;
        private IScalable scalable;
        private IRotatable rotatable;

        // Start is called before the first frame update
        void Start()
        {
            arRaycastManager = FindObjectOfType<ARRaycastManager>();
            arCamera = Camera.main;

            selectable = new SelectableObject();
            movable = new MovableObject();
            scalable = new ScalableObject();
            rotatable = new RotatableObject();

            // Selection Setup
            selectable.SelectionVisuals = selectionVisuals;

            // Moving Setup
            movable.Damping = 5f;

            // Scaling Setup
            scalable.InitialScale = transform.localScale;
            scalable.Damping = 12f;
            scalable.MinScaleLimit = 0.75f;
            scalable.MaxScaleLimit = 1.5f;

            // Setup Rotation
            rotatable.Damping = 0.1f;

            // Initialize targetScale to the current local scale
            targetScale = transform.localScale;

            Debug.Log("ObjectManipulationManager initialized.");
        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (touch.phase == TouchPhase.Began)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log($"Raycast hit {hit.collider.gameObject.name}");
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                          
                            selectable.Select();
                        }                            
                    }
                }
                else if(touch.phase == TouchPhase.Moved)
                {
                    if (Physics.Raycast(ray, out hit) && selectable.IsSelected)
                    {
                        Debug.Log($"Raycast hit {hit.collider.gameObject.name}");
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                            Debug.Log($"Raycast hit {hit.collider.gameObject.name}");
                            if (!selectable.IsSelected)
                            {
                                selectable.Select();
                                return;
                            }

                            if (rotatable.IsRotating)
                            {
                                rotatable.Rotate(transform);
                                movable.IsMoving = false;
                                return;
                            }
                            Debug.Log("Object is selected, attempting to move.");
                            movable.Move(transform, arRaycastManager);
                            
                        }
                        else
                        {
                            if (movable.IsMoving)
                            {
                                movable.Move(transform, arRaycastManager);
                                rotatable.IsRotating = false;
                                return;
                            }
                            Debug.Log("Object is selected, attempting to rotate.");
                            rotatable.Rotate(transform);
                            
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                 
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log($"Raycast hit {hit.collider.gameObject.name}");
                        if (hit.collider.gameObject != gameObject)
                        {                          
               
                            if (!movable.IsMoving)
                            {
                                selectable.Deselect();
                            }
                            if (!rotatable.IsRotating)
                            {
                                selectable.Deselect();
                            }
                        }
                    }

                    movable.IsMoving = false;
                    rotatable.IsRotating = false;
                }
            }
            if (Input.touchCount == 2)
            {
                Debug.Log("Two-finger touch detected, attempting to scale.");
                if (selectable.IsSelected)
                {
                    scalable.Scale(transform, arCamera, Input.GetTouch(0), Input.GetTouch(1));
                }
            }
        }
    }
}
