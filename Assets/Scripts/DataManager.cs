using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using arplace.Data;

namespace arplace.Data
{
    public class DataManager : IDataManager
    {
        public Dictionary<string, ObjectData> ObjectsData { get; private set; }

        public DataManager()
        {
            ObjectsData = new Dictionary<string, ObjectData>();
            ObjectData[] objectsDataArray = Resources.LoadAll<ObjectData>("Data");
            foreach(ObjectData data in objectsDataArray)
            {
                ObjectsData.Add(data.objectName, data);
            }
        }

        public void AddObjectData(ObjectData objectData)
        {
            if(ObjectsData.ContainsKey(objectData.name))
            {
                return;
            }

            ObjectsData.Add(objectData.objectName, objectData);
        }

        public ObjectData GetObjectData(string objectName)
        {
            ObjectData objectData;

            if (!ObjectsData.TryGetValue(objectName, out objectData))
            {
                Debug.Log("Object Not Found");
            }

            return objectData;
        }
    }
}