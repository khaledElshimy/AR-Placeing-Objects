using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace arplace
{
    public class EventManager : IEventManager
    {
        private UnityEvent<GameObject> arplace_onPlaceObject = new UnityEvent<GameObject>();
        private UnityEvent<GameObject> arplace_onSwitchObject = new UnityEvent<GameObject>();
        private UnityEvent arplace_onBrowseObjects = new UnityEvent();
        private UnityEvent arplace_onSelectObject = new UnityEvent();


        public void PlaceObject(GameObject gameObject)
        {
            arplace_onPlaceObject?.Invoke(gameObject);
        }

        public void AddPlaceObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onPlaceObject.AddListener(listener);
        }

        public void RemovePlaceObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onPlaceObject.RemoveListener(listener);
        }

        public void SwitchObject(GameObject newObjectPrefab)
        {
            arplace_onSwitchObject?.Invoke(newObjectPrefab);
        }

        public void AddSwitchObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onSwitchObject.AddListener(listener);
        }

        public void RemoveSwitchObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onSwitchObject.RemoveListener(listener);
        }

        public void BrowseObjects()
        {
            arplace_onBrowseObjects?.Invoke();
        }       

        public void AddBrowsObjectsListener(UnityAction listener)
        {
            arplace_onBrowseObjects.AddListener(listener);
        }

        public void RemoveBrowsObjectsListener(UnityAction listener)
        {
            arplace_onBrowseObjects.RemoveListener(listener);
        }

        public void SelectObject()
        {
            arplace_onSelectObject?.Invoke();
        }

        public void AddSelectObjectListener(UnityAction listener)
        {
            arplace_onSelectObject.AddListener(listener);
        }

        public void RemoveSelectObjectListener(UnityAction listener)
        {
            arplace_onSelectObject.RemoveListener(listener);
        }
    }
}
