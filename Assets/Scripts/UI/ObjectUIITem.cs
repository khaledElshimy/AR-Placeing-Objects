using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace arplace.UI
{
    /// <summary>
    /// Represents a UI element for an AR object within the application.
    /// This class is responsible for setting up the visual representation of the AR object in the UI.
    /// </summary>
    public class ObjectUIItem : MonoBehaviour
    {
        // The text component displaying the name of the AR object.
        [SerializeField] private Text objectName;
        // The image component displaying the icon of the AR object.
        [SerializeField] private Image objectIcon;
        // The button component that users interact with to select the AR object.
        [SerializeField] private Button objectButton;

        /// <summary>
        /// Configures the UI item with the AR object's name, icon, and a callback action for when the object is selected.
        /// </summary>
        /// <param name="objectName">The name of the AR object.</param>
        /// <param name="objectIcon">The icon representing the AR object.</param>
        /// <param name="buttonActionCallback">The action to execute when the AR object is selected.</param>
        public void SetupView(String objectName, Sprite objectIcon, UnityAction buttonActionCallback)
        {
            this.objectName.text = objectName; // Set the text component to display the AR object's name.
            this.objectIcon.sprite = objectIcon; // Set the image component to display the AR object's icon.
            this.objectButton.onClick.AddListener(buttonActionCallback); // Attach the provided callback action to the button's onClick event.
        }
    }
}
