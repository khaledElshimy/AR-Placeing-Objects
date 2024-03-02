using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace
{
    public interface IARPlacementManager
    {
        GameObject SpawnableObjectPrefab { get; set; }
        void SwitchObject(GameObject spawnableObject);
        void PlaceObject(GameObject spawanablePrefab, Pose placePose);
    }
}