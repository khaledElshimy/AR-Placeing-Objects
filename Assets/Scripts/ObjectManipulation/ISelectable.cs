using UnityEngine;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Defines the contract for selectable objects within the application.
    /// This interface specifies the properties and methods required to implement selection behavior.
    /// </summary>
    public interface ISelectable
    {
        // Indicates whether the object is currently selected.
        bool IsSelected { get; set; }
        // The visual indicator of selection, such as a highlight or outline.
        GameObject SelectionVisuals { get; set; }

        /// <summary>
        /// Marks the object as selected and activates any selection visuals.
        /// </summary>
        void Select();

        /// <summary>
        /// Deselects the object, deactivating any selection visuals.
        /// </summary>
        void Deselect();
    }
}
