using UnityEngine;
using arplace.UI;
using arplace.Data;
using arplace.Events;

namespace arplace
{
    /// <summary>
    /// A central unit manages the app workflow, coordinating interactions between UI, data, and AR object placement.
    /// </summary>
    public class AppManager : MonoBehaviour
    {
        #region Private Members
        // Manages event-driven communication across the application.
        private IEventManager eventManager;
        // Handles data operations like loading and manipulate AR object information.
        private IDataManager dataManager;
        // Responsible for AR object placement into the scene.
        [SerializeField] private ARPlacementManager arPlacementManger;
        // Manages user interface elements and interactions.
        [SerializeField] private UIManager uIManager;
        #endregion

        #region Unity Methods
        void Awake()
        {
            // Called when the script instance is being loaded.
            // Initializes managers and sets up dependencies and event listeners to manage the application's lifecycle.
            eventManager = new EventManager();
            dataManager = new DataManager();
            // Configure UIManager with dependencies.
            uIManager.Setup(eventManager, dataManager, arPlacementManger);
            // Subscribe to events with respective handlers.
            eventManager.AddSwitchObjectListener(OnSwitchObject);
            eventManager.AddPlaceObjectListener(OnPlaceObject);
            eventManager.AddBrowsObjectsListener(OnBrowseObjects);
            eventManager.AddSelectObjectListener(OnSelectObject);
        }

        private void OnDestroy()
        {
            // Cleans up event listeners to avoid memory leaks and ensure clean application shutdown.
            if (eventManager != null)
            {
                eventManager.RemoveSwitchObjectListener(OnSwitchObject);
                eventManager.RemovePlaceObjectListener(OnPlaceObject);
                eventManager.RemoveBrowsObjectsListener(OnBrowseObjects);
                eventManager.RemoveSelectObjectListener(OnSelectObject);
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Invoked when there's a request to switch the currently placed AR object with a new one.
        /// </summary>
        /// <param name="newPlacedObject"></param>
        private void OnSwitchObject(GameObject newPlacedObject)
        {
            // Calls ARPlacementManager's method to update the AR scene with the new object.
            arPlacementManger.SwitchObject(newPlacedObject);
        }

        /// <summary>
        /// Placeholder method for handling the placement of an AR object.
        /// </summary>
        private void OnPlaceObject(GameObject placedObject)
        {
            // Intended to be implemented with logic to place the specified object in the AR environment.
        }

        /// <summary>
        ///  Handles the event when the user wants to browse through available AR objects.
        /// </summary>
        private void OnBrowseObjects()
        {
            // Disables the ARPlacementManager to prevent object placement during browsing.
            arPlacementManger.enabled = false;
        }

        /// <summary>
        ///  Handles the event when the user selects an AR object to place.
        /// </summary>
        private void OnSelectObject()
        {
            // Enables the ARPlacementManager, allowing the user to place the selected object in the AR environment.
            arPlacementManger.enabled = true;
        }
        #endregion
    }
}
