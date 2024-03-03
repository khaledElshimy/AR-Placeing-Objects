using UnityEngine;

namespace arplace.Data
{
    /// <summary>
    /// Defines the data structure for AR objects within the application.
    /// This ScriptableObject holds information necessary to represent and instantiate AR objects in the scene.
    /// </summary>
    [CreateAssetMenu(fileName = "Data/NewObjectData", menuName = "arplace/Object Data", order = 0)]
    public class ObjectData : ScriptableObject
    {
        // The name of the AR object.
        public string objectName;
        // The icon associated with the AR object, used for UI representation.
        public Sprite objectIcon;
        // The prefab of the AR object that can be instantiated in the scene.
        public GameObject objectPrefab;
    }
}
