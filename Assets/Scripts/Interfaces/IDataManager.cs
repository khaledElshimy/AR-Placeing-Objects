using System.Collections;
using System.Collections.Generic;
using arplace.Data;
using UnityEngine;

namespace arplace.Data
{
    public interface IDataManager
    {
        Dictionary<string, ObjectData> ObjectsData { get; }
        void AddObjectData(ObjectData objectData);
        ObjectData GetObjectData(string objectName);

    }
}