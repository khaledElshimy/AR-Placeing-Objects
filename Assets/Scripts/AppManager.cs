using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using arplace.UI;
using arplace.Data;
using arplace.Events;

using System;

namespace arplace
{
    public class AppManager : MonoBehaviour
    {
        #region Private Members
        private IEventManager eventManager;
        private IDataManager dataManager;
        [SerializeField] private ARPlacementManager arPlacementManger;
        [SerializeField] private UIManager uIManager;

        #endregion
        #region UnityMethods
        void Awake()
        {
            Debug.Log("AppManager");
            // Initialize and setup Managers
            eventManager = new EventManager();
            dataManager = new DataManager();

            uIManager.Setup(eventManager, dataManager, arPlacementManger);
            // Subsribe listeners to EventManager events
            eventManager.AddSwitchObjectListener(OnSwitchObject);
            eventManager.AddPlaceObjectListener(OnPlaceObject);
            eventManager.AddBrowsObjectsListener(OnBrowseObjects);
            eventManager.AddSelectObjectListener(OnSelectObject);

        }

        private void OnBrowseObjects()
        {
            arPlacementManger.enabled = false;
        }

        private void OnSelectObject()
        {
            arPlacementManger.enabled = true;
        }

        private void OnDestroy()
        {
            // Remove listeners
            if (eventManager != null)
            {
                eventManager.RemoveSwitchObjectListener(OnSwitchObject);
            }
        }
        #endregion

        #region Private Method

        private void OnSwitchObject(GameObject newplacedObject)
        {
            arPlacementManger.SwitchObject(newplacedObject);
        }

        private void OnPlaceObject(GameObject placedObject)
        {
           // arPlacementManger.PlaceObject(placedObject);
        }
        #endregion
    }
}