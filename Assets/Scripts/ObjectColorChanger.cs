using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace arplace.UI
{
    public class ObjectColorChanger : MonoBehaviour
    {
        [SerializeField] private Slider colorSlider;

        private void Start()
        {
            colorSlider.onValueChanged.AddListener(ChangeColor);
        }
        public void ChangeColor(float hueValue)
        {
            Renderer objectRenderer = GetComponentInChildren<Renderer>();

            // Convert hue slider value to color and apply it to the object
            Color newColor = Color.HSVToRGB(hueValue, 1f, 1f); // Full saturation and brightness
            objectRenderer.material.color = newColor;
        }
    }
}