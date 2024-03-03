using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace arplace
{
    /// <summary>
    /// Manages the placement of AR objects onto detected planes in an AR environment.
    /// Implements the IARPlacementManager interface.
    /// </summary>
    public class ARPlacementManager : MonoBehaviour, IARPlacementManager
    {
        // The prefab of the object to spawn in the AR world.
        private GameObject spawanableObjectPrefab;
        public GameObject SpawnableObjectPrefab { get => spawanableObjectPrefab; set => spawanableObjectPrefab = value; }

        // AR Foundation components for raycasting and plane detection.
        [SerializeField]
        private ARRaycastManager arRaycastManager;
        [SerializeField]
        private ARPlaneManager arPlaneManager;

        // A list to store raycast hits.
        private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

        // Update is called once per frame to detect touch inputs.
        private void Update()
        {
            // Check for touch input.
            if (Input.touchCount > 0)
            {
                // On initial touch, perform raycast to detect planes.
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    bool collision = arRaycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);
                    // If a plane is hit, place the object at the hit position.
                    if (collision && spawanableObjectPrefab)
                    {
                        PlaceObject(spawanableObjectPrefab, raycastHits[0].pose);
                    }
                }
            }
        }

        /// <summary>
        /// Places the object at the specified pose.
        /// </summary>
        /// <param name="spawanablePrefab">The prefab to spawn.</param>
        /// <param name="placePose">The pose at which to place the object.</param>
        public void PlaceObject(GameObject spawanablePrefab, Pose placePose)
        {
            // Instantiate the object at the pose.
            GameObject spawnedGameObject = Instantiate(spawanablePrefab);
            spawnedGameObject.transform.position = placePose.position;
            spawnedGameObject.transform.rotation = placePose.rotation;
            // disable ARPlaneManager if only one object is to be placed.
            enabled = false;
        }

        /// <summary>
        /// Switches the current object prefab to a new prefab.
        /// </summary>
        /// <param name="spawanablePrefab">The new prefab to switch to.</param>
        public void SwitchObject(GameObject spawanablePrefab)
        {
            this.spawanableObjectPrefab = spawanablePrefab;
        }
    }
}
