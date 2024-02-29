using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace arplace.Modification
{
    public class ObjectColorChanger : MonoBehaviour, ISingleClickAction
    {
        public void OnSingleClick()
        {
            Renderer objectRenderer = GetComponentInChildren<Renderer>();

            // Change the color of the object to a random hue.
            objectRenderer.material.color = Random.ColorHSV();
        }
    }
}