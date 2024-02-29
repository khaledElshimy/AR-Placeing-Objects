using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.Data
{
    [CreateAssetMenu(fileName = "Data/NewObjectData", menuName = "arplace/Object Data", order = 0)]
    public class ObjectData : ScriptableObject
    {
        public string objectName;
        public Sprite objectIcon;
        public GameObject objectPrefab;
    }
}
