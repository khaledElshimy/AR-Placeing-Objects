using UnityEngine;

namespace arplace
{
    /// <summary>
    /// Defines the contract for AR object placement management within the application.
    /// This interface specifies the methods necessary for placing and switching AR objects in the environment.
    /// </summary>
    public interface IARPlacementManager
    {
        // Property to get or set the current prefab that can be spawned in the AR environment.
        GameObject SpawnableObjectPrefab { get; set; }

        /// <summary>
        /// Switches the currently selected AR object to a new one.
        /// </summary>
        /// <param name="spawnableObject">The new AR object prefab to switch to.</param>
        void SwitchObject(GameObject spawnableObject);

        /// <summary>
        /// Places the specified AR object at the given pose in the AR environment.
        /// </summary>
        /// <param name="spawnablePrefab">The prefab of the AR object to place.</param>
        /// <param name="placePose">The pose where the AR object should be placed.</param>
        void PlaceObject(GameObject spawnablePrefab, Pose placePose);
    }
}
