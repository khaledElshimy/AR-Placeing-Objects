using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        // Long press variables
        private float touchStartTime;
        private bool isLongPress = false;
        private float longPressThreshold = 0.5f; // Duration in seconds to qualify as a long press
        private const float moveThreshold = 10f; // Movement threshold in screen pixels
        private Vector2 initialTouchPosition;

        // Double-tap variables
        private float lastTapTime = 0f;
        private float doubleTapThreshold = 0.3f; // Time in seconds between taps
        private int tapCount = 0;
        [SerializeField]
        private UnityEvent onLongPress;

        [SerializeField]
        private UnityEvent onDoubleTap;

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
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        initialTouchPosition = touch.position;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log($"Raycast hit {hit.collider.gameObject.name}");
                            if (hit.collider != null && hit.collider.gameObject == gameObject)
                            {
                                selectable.Select();
                                touchStartTime = Time.time;
                            }
                        }
                        break;
                    case TouchPhase.Moved:
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
                                    isLongPress = false;

                                    return;
                                }

                                // Check if movement exceeds threshold to consider it a move
                                if (!movable.IsMoving && Vector2.Distance(touch.position, initialTouchPosition) > moveThreshold)
                                {
                                    isLongPress = false; // Cancel long press if it started moving
                                    movable.IsMoving = true;
                                }
                             
                            }
                            else
                            {
                                Debug.Log("Object is selected, attempting to rotate.");
                                rotatable.Rotate(transform);
                            }
                            if (movable.IsMoving)
                            {
                                rotatable.IsRotating = false;
                                isLongPress = false;
                                movable.Move(transform, arRaycastManager);
                                return;
                            }
                        }
                        break;
                    case TouchPhase.Stationary:
                        // Check if duration exceeds threshold to consider it a long press
                        if (!movable.IsMoving && selectable.IsSelected 
                            && Time.time - touchStartTime > longPressThreshold)
                        {
                            isLongPress = true;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (selectable.IsSelected && isLongPress)
                        {
                            onLongPress?.Invoke();
                        }


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
                            else
                            {
                                float currentTime = Time.time;
                                if (currentTime - lastTapTime < doubleTapThreshold)
                                {
                                    // Detected a double tap
                                    tapCount++;

                                    if (tapCount == 2)
                                    {
                                        // Double tap confirmed
                                        onDoubleTap?.Invoke();
                                        tapCount = 0; // Reset tap count after a double tap
                                    }
                                }
                                else
                                {
                                    tapCount = 1; // Reset to 1 as we're starting a new tap sequence
                                }

                                lastTapTime = currentTime;
                            } 
                        }

                        movable.IsMoving = false;
                        rotatable.IsRotating = false;
                        isLongPress = false;
                        break;
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
