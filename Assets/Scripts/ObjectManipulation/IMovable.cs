using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace arplace.ObjectManipulation
{
    public interface IMovable 
    {
        bool IsMoving { get; set; }
        float Damping { get; set; }
        void Move(Transform transform, ARRaycastManager raycastManager);
    }
}