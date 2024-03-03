using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace arplace
{
    public class ARPlacementManager : MonoBehaviour, IARPlacementManager
    {
        private GameObject spawanableObjectPrefab;
        public GameObject SpawnableObjectPrefab { get => spawanableObjectPrefab; set => spawanableObjectPrefab = value; }

        [SerializeField]
        private ARRaycastManager arRaycastManager;

        [SerializeField]
        private ARPlaneManager arPlaneManager;

        private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();


        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    bool collision = arRaycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);
                    if (collision && spawanableObjectPrefab)
                    {
                        PlaceObject(spawanableObjectPrefab, raycastHits[0].pose);
                    }        
                }
            }
        }

        public void PlaceObject(GameObject spawanablePrefab , Pose placePose)
        {
            GameObject spawnedGameObject = Instantiate(spawanablePrefab);
            spawnedGameObject.transform.position = placePose.position;
            spawnedGameObject.transform.rotation = placePose.rotation;
            enabled = false;

        }

        public void SwitchObject(GameObject spawanablePrefab)
        {
            this.spawanableObjectPrefab = spawanablePrefab;
        }
    }
}
