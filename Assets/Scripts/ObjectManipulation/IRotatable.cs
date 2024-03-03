using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public interface IRotatable
    {
        bool IsRotating { get; set; }
        float Damping { get; set; }
        void Rotate(Transform transform);
    }
}