using UnityEngine;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Implements the ISelectable interface to provide selection functionality to objects.
    /// Allows for objects to be visually indicated as selected or deselected.
    /// </summary>
    public class SelectableObject : ISelectable
    {
        public bool IsSelected { get; set; } // Tracks the selection state of the object.

        public GameObject SelectionVisuals { get; set; } // Visual indicator for selection.

        /// <summary>
        /// Activates the selection visuals and marks the object as selected.
        /// </summary>
        public void Select()
        {
            // Ensure there are visuals set to indicate selection.
            if (SelectionVisuals != null)
            {
                SelectionVisuals.SetActive(true); // Show selection visuals.
            }
            else
            {
                Debug.LogWarning("SelectionVisuals not set for SelectableObject.");
            }
            IsSelected = true; // Update selection state.
        }

        /// <summary>
        /// Deactivates the selection visuals and marks the object as deselected.
        /// </summary>
        public void Deselect()
        {
            // Ensure there are visuals set to remove selection indication.
            if (SelectionVisuals != null)
            {
                SelectionVisuals.SetActive(false); // Hide selection visuals.
            }
            else
            {
                Debug.LogWarning("SelectionVisuals not set for SelectableObject, cannot deselect visually.");
            }
            IsSelected = false; // Update selection state.
        }
    }
}
