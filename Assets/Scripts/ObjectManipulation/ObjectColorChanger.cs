using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace arplace.ObjectManipulation
{
    public class ObjectColorChanger : MonoBehaviour, ILongPressAction
    {
        public void OnlongPress()
        {
            Renderer objectRenderer = GetComponentInChildren<Renderer>();

            // Change the color of the object to a random hue.
            objectRenderer.material.color = Random.ColorHSV();
        }
    }
}