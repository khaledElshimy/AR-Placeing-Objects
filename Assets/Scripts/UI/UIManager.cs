using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using arplace.Data;
using arplace.Events;

namespace arplace.UI
{
    /// <summary>
    /// Manages the user interface for the application, handling interactions and displaying UI elements accordingly.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // Button to trigger the browsing of available AR objects.
        [SerializeField]
        private Button browseButton;

        // Button to confirm the selection of an AR object.
        [SerializeField]
        private Button selectButton;

        // Panel that displays the selection of AR objects.
        [SerializeField]
        private GameObject objectsSelectionPanel;

        // The content area where UI items for AR objects will be dynamically added.
        [SerializeField]
        private Transform objectsUIListContent;

        // Prefab for creating UI items representing each AR object.
        [SerializeField]
        private GameObject ObjectUIItemPrefab;

        // Stores references to the UI items for quick access.
        private Dictionary<string, ObjectUIItem> objectIconsList = new Dictionary<string, ObjectUIItem>();

        // Event manager to handle application events.
        private IEventManager eventManager;

        // Data manager to get access to AR objects data.
        private IDataManager dataManager;

        // Reference to the ARPlacementManager to manage AR object placement.
        private ARPlacementManager aRPlacementManager;

        /// <summary>
        /// Initializes the UIManager with necessary managers and sets up button listeners.
        /// </summary>
        public void Setup(IEventManager eventManager, IDataManager dataManager, ARPlacementManager aRPlacementManager)
        {
            this.aRPlacementManager = aRPlacementManager;
            this.eventManager = eventManager;
            this.dataManager = dataManager;
            browseButton.onClick.AddListener(OnBrowseButtonPressed);
            selectButton.onClick.AddListener(OnSelectButtonPressed);
            PopulatesObjectUIList();
        }

        /// <summary>
        /// Handles the browse button press, showing the objects selection panel.
        /// </summary>
        private void OnBrowseButtonPressed()
        {
            browseButton.gameObject.SetActive(false);
            objectsSelectionPanel.gameObject.SetActive(true);

            // Notifies the system that the user wishes to browse available objects.
            eventManager.BrowseObjects();
        }

        /// <summary>
        /// Handles the select button press, hiding the objects selection panel.
        /// </summary>
        private void OnSelectButtonPressed()
        {
            browseButton.gameObject.SetActive(true);
            objectsSelectionPanel.gameObject.SetActive(false);

            // Notifies the system that the user has finished browsing and has potentially selected an object.
            eventManager.SelectObject();
        }

        /// <summary>
        /// Populates the UI list with items for each AR object available in the application.
        /// </summary>
        private void PopulatesObjectUIList()
        {
            // Clears existing list items before repopulating.
            foreach (ObjectData objectData in dataManager.ObjectsData.Values)
            {
                GameObject objectUIItemGameObject = Instantiate(ObjectUIItemPrefab, objectsUIListContent);
                ObjectUIItem objectUIItem = objectUIItemGameObject.GetComponent<ObjectUIItem>();

                // Sets up each UI item with the object's name, icon, and a callback for when it's selected.
                objectUIItem.SetupView(objectData.objectName, objectData.objectIcon, () => { eventManager.SwitchObject(objectData.objectPrefab); });
            }
        }
    }
}
