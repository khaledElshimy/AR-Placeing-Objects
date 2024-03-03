using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Manages object manipulation , including selection, movement, scaling, and rotation.
    /// Handles long press and double tap gestures for triggering specific actions.
    /// </summary>
    public class ObjectManipulationManager : MonoBehaviour
    {
        private Camera arCamera; // Reference to the AR camera.
        private ARRaycastManager arRaycastManager; // Reference to the AR raycast manager.

        [SerializeField] private GameObject selectionVisuals; // Visuals indicating object selection.
        private ISelectable selectable; // Interface for object selection.
        private IMovable movable; // Interface for object movement.
        private IScalable scalable; // Interface for object scaling.
        private IRotatable rotatable; // Interface for object rotation.

        // Long press variables
        private float touchStartTime; // Time when touch begins.
        private bool isLongPress = false; // Flag indicating long press.
        private float longPressThreshold = 0.5f; // Duration in seconds to qualify as a long press.
        private const float moveThreshold = 10f; // Movement threshold in screen pixels.
        private Vector2 initialTouchPosition; // Initial touch position.

        // Double-tap variables
        private float lastTapTime = 0f; // Time of the last tap.
        private float doubleTapThreshold = 0.3f; // Time in seconds between taps.
        private int tapCount = 0; // Number of taps.

        // Scaling
        private Vector3 targetScale; // Target scale for scaling operation.

        [SerializeField] private UnityEvent onLongPress; // Event invoked on long press.
        [SerializeField] private UnityEvent onDoubleTap; // Event invoked on double tap.


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

        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                // Get the touch input  cast a ray from the AR camera
                Touch touch = Input.GetTouch(0);
                // cast a ray from the AR camera
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Switch statement to handle different touch phases
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        // Check if the touch began on this object
                        initialTouchPosition = touch.position;
                        if (Physics.Raycast(ray, out hit))
                        {
                            // If the raycast hits this object, select it
                            if (hit.collider != null && hit.collider.gameObject == gameObject)
                            {
                                selectable.Select();
                                touchStartTime = Time.time;
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        // Handle object movement and rotation
                        if (Physics.Raycast(ray, out hit) && selectable.IsSelected)
                        {
                            // If the object is selected and the touch moved, handle movement and rotation
                            if (hit.collider != null && hit.collider.gameObject == gameObject)
                            {
                                if (!selectable.IsSelected)
                                {
                                    selectable.Select();
                                    return; // ensure object whill not move, or rotate when selecting
                                }

                                if (rotatable.IsRotating)
                                {
                                    // If the object is rotating, continue rotation
                                    rotatable.Rotate(transform);
                                    movable.IsMoving = false;
                                    isLongPress = false;
                                    return;
                                }

                                // Check if movement exceeds threshold to consider it a move
                                // This line to detect the user holds or moves the objects
                                // Check if touch exceeds moveThreshold to consider it a a move.
                                if (!movable.IsMoving && Vector2.Distance(touch.position, initialTouchPosition) > moveThreshold)
                                {
                                    isLongPress = false; // Cancel long press if it started moving
                                    movable.IsMoving = true;
                                }
                            }
                            else
                            {
                                // If the touch out of the object bounds , rotate the object
                                rotatable.Rotate(transform);
                            }

                            if (movable.IsMoving)
                            {
                                // If the object is moving, continue movement
                                rotatable.IsRotating = false;
                                isLongPress = false;
                                movable.Move(transform, arRaycastManager);
                                return;
                            }
                        }
                        break;
                    case TouchPhase.Stationary:
                        // Check if duration exceeds long press time threshold to consider it a long press
                        if (!movable.IsMoving && selectable.IsSelected
                            && Time.time - touchStartTime > longPressThreshold)
                        {
                            isLongPress = true;
                        }
                        break;
                    case TouchPhase.Ended:
                        // Handle touch end phase
                        if (selectable.IsSelected && isLongPress)
                        {
                            // If it was a long press, invoke the long press event
                            onLongPress?.Invoke();
                        }

                        if (Physics.Raycast(ray, out hit))
                        {
                            // Check for object deselection or double tap
                            if (hit.collider.gameObject != gameObject)
                            {
                                if (!movable.IsMoving)
                                {
                                    // Deselect the object if it's not moving
                                    selectable.Deselect();
                                }
                                if (!rotatable.IsRotating)
                                {
                                    // Deselect the object if it's not rotating
                                    selectable.Deselect();
                                }
                            }
                            else
                            {
                                // Handle double tap detection
                                float currentTime = Time.time;
                                if (currentTime - lastTapTime < doubleTapThreshold)
                                {
                                    // Detected a double tap
                                    tapCount++;

                                    if (tapCount == 2)
                                    {
                                        // Double tap confirmed, invoke the double tap event
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

                        // Reset movement and rotation flags
                        movable.IsMoving = false;
                        rotatable.IsRotating = false;
                        isLongPress = false;
                        break;
                }
            }
            if (Input.touchCount == 2)
            {
                // Two-finger touch detected, attempting to scale.
                if (selectable.IsSelected)
                {
                    scalable.Scale(transform, arCamera, Input.GetTouch(0), Input.GetTouch(1));
                }
            }
        }
    }
}
