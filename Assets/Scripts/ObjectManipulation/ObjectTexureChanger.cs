using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public class ObjectTexureChanger : MonoBehaviour, IDoubleClickAction
    {
        // Array to hold your textures
        public Material[] materials;
        private int currentMaterialIndex = 0;

        public void OnDoubleClick()
        {
            // Change to the next texture in the array
            currentMaterialIndex++;
            if (currentMaterialIndex >= materials.Length)
            {
                currentMaterialIndex = 0; // Loop back to the first texture if we've gone through all
            }

            // Apply the new texture to the material of the GameObject
            GetComponentInChildren<Renderer>().material= materials[currentMaterialIndex];
        }
    }
}
