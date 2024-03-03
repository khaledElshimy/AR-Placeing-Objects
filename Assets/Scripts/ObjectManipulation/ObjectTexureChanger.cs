using UnityEngine;

namespace arplace.ObjectManipulation
{
    /// <summary>
    /// Implements the IDoubleClickAction interface to change the object's texture upon a double-click.
    /// Holds an array of materials to cycle through each double-click event.
    /// </summary>
    public class ObjectTexureChanger : MonoBehaviour, IDoubleClickAction
    {
        public Material[] materials; // Array of materials to apply to the object.
        private int currentMaterialIndex = 0; // Tracks the current index of the applied material.

        public void OnDoubleClick()
        {
            // Increment the material index to change the texture.
            currentMaterialIndex++;
            // Loop back to the first material if the end of the array is reached.
            if (currentMaterialIndex >= materials.Length)
            {
                currentMaterialIndex = 0;
            }

            // Apply the new material to the object's Renderer component.
            GetComponentInChildren<Renderer>().material = materials[currentMaterialIndex];
        }
    }
}
