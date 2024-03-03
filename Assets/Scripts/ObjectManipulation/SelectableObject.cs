using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public class SelectableObject : ISelectable
    {
        public bool IsSelected { get; set; }

        public GameObject SelectionVisuals { get; set; }

        public void Select()
        {
            Debug.Log("Select.");

            if (SelectionVisuals != null)
            {
                SelectionVisuals.SetActive(true);
                Debug.Log("Object selected.");
            }
            else
            {
                Debug.LogWarning("SelectionVisuals not set for SelectableObject.");
            }
            IsSelected = true;
        }

        public void Deselect()
        {
            if (SelectionVisuals != null)
            {
                SelectionVisuals.SetActive(false);
                Debug.Log("Object deselected.");
            }
            else
            {
                Debug.LogWarning("SelectionVisuals not set for SelectableObject, cannot deselect visually.");
            }
            IsSelected = false;
        }
    }
}
