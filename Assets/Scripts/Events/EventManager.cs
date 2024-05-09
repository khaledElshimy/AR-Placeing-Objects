using UnityEngine;
using UnityEngine.Events;
using arplace.Events.interfaces;

namespace arplace.Events
{
    /// <summary>
    /// Manages events within the application, facilitating communication between components
    /// via UnityEvents. Allows for subscribing to and triggering specific application events.
    /// </summary>
    public class EventManager : IEventManager
    {
        // Event triggered when an object is placed into AR scene.
        private UnityEvent<GameObject> arplace_onPlaceObject = new UnityEvent<GameObject>();
        // Event triggered when switching to a different AR object.
        private UnityEvent<GameObject> arplace_onSwitchObject = new UnityEvent<GameObject>();
        // Event triggered to browse through available AR objects.
        private UnityEvent arplace_onBrowseObjects = new UnityEvent();
        // Event triggered when an AR object is selected.
        private UnityEvent arplace_onSelectObject = new UnityEvent();

        // Invokes the place object event with the specified GameObject.
        public void PlaceObject(GameObject gameObject)
        {
            arplace_onPlaceObject?.Invoke(gameObject);
        }

        // Adds a listener to the place object event.
        public void AddPlaceObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onPlaceObject.AddListener(listener);
        }

        // Removes a listener from the place object event.
        public void RemovePlaceObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onPlaceObject.RemoveListener(listener);
        }

        // Invokes the switch object event with the specified GameObject.
        public void SwitchObject(GameObject newObjectPrefab)
        {
            arplace_onSwitchObject?.Invoke(newObjectPrefab);
        }

        // Adds a listener to the switch object event.
        public void AddSwitchObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onSwitchObject.AddListener(listener);
        }

        // Removes a listener from the switch object event.
        public void RemoveSwitchObjectListener(UnityAction<GameObject> listener)
        {
            arplace_onSwitchObject.RemoveListener(listener);
        }

        // Invokes the browse objects event.
        public void BrowseObjects()
        {
            arplace_onBrowseObjects?.Invoke();
        }

        // Adds a listener to the browse objects event.
        public void AddBrowsObjectsListener(UnityAction listener)
        {
            arplace_onBrowseObjects.AddListener(listener);
        }

        // Removes a listener from the browse objects event.
        public void RemoveBrowsObjectsListener(UnityAction listener)
        {
            arplace_onBrowseObjects.RemoveListener(listener);
        }

        // Invokes the select object event.
        public void SelectObject()
        {
            arplace_onSelectObject?.Invoke();
        }

        // Adds a listener to the select object event.
        public void AddSelectObjectListener(UnityAction listener)
        {
            arplace_onSelectObject.AddListener(listener);
        }

        // Removes a listener from the select object event.
        public void RemoveSelectObjectListener(UnityAction listener)
        {
            arplace_onSelectObject.RemoveListener(listener);
        }
    }
}
