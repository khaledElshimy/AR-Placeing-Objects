using System.Collections.Generic;
using UnityEngine;

namespace arplace.Data
{
    /// <summary>
    /// Manages the data related to AR objects within the application, implementing the IDataManager interface.
    /// It loads and manipulates the data saved in scriptable objects for AR objects.
    /// </summary>
    public class DataManager : IDataManager
    {
        // A dictionary to store ObjectData by their names for efficient access.
        public Dictionary<string, ObjectData> ObjectsData { get; private set; }

        /// <summary>
        /// Constructs the DataManager and initializes the ObjectsData dictionary with AR objects data loaded from resources.
        /// </summary>
        public DataManager()
        {
            ObjectsData = new Dictionary<string, ObjectData>();
            // Load all ObjectData assets from the Resources/Data folder.
            ObjectData[] objectsDataArray = Resources.LoadAll<ObjectData>("Data");
            foreach (ObjectData data in objectsDataArray)
            {
                // Populate the dictionary with loaded ObjectData.
                ObjectsData.Add(data.objectName, data);
            }
        }

        /// <summary>
        /// Adds a new ObjectData entry to the ObjectsData dictionary, if not already present.
        /// </summary>
        /// <param name="objectData">The ObjectData to add.</param>
        public void AddObjectData(ObjectData objectData)
        {
            // Check if the ObjectData is already present to avoid duplicates.
            if (ObjectsData.ContainsKey(objectData.objectName))
            {
                return; // Do nothing if the object is already in the dictionary.
            }

            // Add the new ObjectData to the dictionary.
            ObjectsData.Add(objectData.objectName, objectData);
        }

        /// <summary>
        /// Retrieves ObjectData for a given object name.
        /// </summary>
        /// <param name="objectName">The name of the object to retrieve data for.</param>
        /// <returns>The ObjectData associated with the given name, or null if not found.</returns>
        public ObjectData GetObjectData(string objectName)
        {
            ObjectData objectData;

            // Attempt to get the ObjectData from the dictionary.
            if (!ObjectsData.TryGetValue(objectName, out objectData))
            {
                Debug.Log("Object Not Found"); // Log a message if the object is not found.
            }

            return objectData; // Return the found ObjectData or null.
        }
    }
}
