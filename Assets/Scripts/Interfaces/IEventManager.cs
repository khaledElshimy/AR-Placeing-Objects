using UnityEngine;
using UnityEngine.Events;

namespace arplace.Events
{
    /// <summary>
    /// Defines the contract for an event management system in the application.
    /// This interface outlines methods for triggering actions related to AR object manipulation and UI interaction.
    /// </summary>
    public interface IEventManager
    {
        // Triggers placing an AR object in the scene.
        void PlaceObject(GameObject gameObject);
        // Switches the current AR object with a new one.
        void SwitchObject(GameObject newObjectPrefab);
        // Initiates browsing available AR objects.
        void BrowseObjects();
        // Signals the selection of an AR object.
        void SelectObject();

        // Adds/removes listeners for the object placement event.
        void AddPlaceObjectListener(UnityAction<GameObject> listener);
        void RemovePlaceObjectListener(UnityAction<GameObject> listener);

        // Adds/removes listeners for the object switching event.
        void AddSwitchObjectListener(UnityAction<GameObject> listener);
        void RemoveSwitchObjectListener(UnityAction<GameObject> listener);

        // Adds/removes listeners for browsing objects event.
        void AddBrowsObjectsListener(UnityAction listener);
        void RemoveBrowsObjectsListener(UnityAction listener);

        // Adds/removes listeners for object selection event.
        void AddSelectObjectListener(UnityAction listener);
        void RemoveSelectObjectListener(UnityAction listener);
    }
}
