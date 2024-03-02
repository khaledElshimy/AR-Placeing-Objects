using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using UnityEngine.XR.Interaction.Toolkit.AR;
using arplace.Data;
using arplace.Events;

namespace arplace.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Button browseButton;

        [SerializeField]
        private Button selectButton;

        [SerializeField]
        private GameObject objectsSelectionPanel;
        [SerializeField]
        private Transform objectsUIListwContent;
        [SerializeField]
        private GameObject ObjectUIItemPrefab;


        private Dictionary<string, ObjectUIITem> objectIconsList = new Dictionary<string, ObjectUIITem>();
        private IEventManager eventManager;
        private IDataManager dataManager;

        private ARPlacementManager aRPlacementManager;

        public void Setup(IEventManager eventManager, IDataManager dataManager, ARPlacementManager aRPlacementManager)
        {
            this.aRPlacementManager = aRPlacementManager;
            this.eventManager = eventManager;
            this.dataManager = dataManager;
            browseButton.onClick.AddListener(OnBrowseButtonPressed);
            selectButton.onClick.AddListener(OnSelectButtonPressed);
            publishObjectUIList();
        }

        private void OnBrowseButtonPressed()
        {
            browseButton.gameObject.SetActive(false);
            objectsSelectionPanel.gameObject.SetActive(true);

            // Fire browse objects event
            eventManager.BrowseObjects();
        }

        private void OnSelectButtonPressed()
        {
            browseButton.gameObject.SetActive(true);
            objectsSelectionPanel.gameObject.SetActive(false);

            // Fire select object event
            eventManager.SelectObject();
        }

        private void publishObjectUIList()
        {
            foreach (ObjectData objectData in dataManager.ObjectsData.Values)
            {
                GameObject objectUItemGameObject = Instantiate(ObjectUIItemPrefab , objectsUIListwContent);
                ObjectUIITem objectUIITem = objectUItemGameObject.GetComponent<ObjectUIITem>();

                objectUIITem.SetupView(objectData.objectName, objectData.objectIcon, () => { eventManager.SwitchObject(objectData.objectPrefab); });
            }
        }
    }
}