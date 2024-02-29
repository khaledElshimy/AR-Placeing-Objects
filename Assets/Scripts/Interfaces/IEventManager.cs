using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace arplace
{
    public interface IEventManager
    {
        void PlaceObject(GameObject gameObject);
        void SwitchObject(GameObject newObjectPrefab);
        void BrowseObjects();
        void SelectObject();


        void AddPlaceObjectListener(UnityAction<GameObject> listener);
        void RemovePlaceObjectListener(UnityAction<GameObject> listener);

        void AddSwitchObjectListener(UnityAction<GameObject> listener);
        void RemoveSwitchObjectListener(UnityAction<GameObject> listener);

        void AddBrowsObjectsListener(UnityAction listener);
        void RemoveBrowsObjectsListener(UnityAction listener);

        void AddSelectObjectListener(UnityAction listener);
        void RemoveSelectObjectListener(UnityAction listener);
    }
}
