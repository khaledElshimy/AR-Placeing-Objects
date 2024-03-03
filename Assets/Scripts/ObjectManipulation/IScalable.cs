using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public interface IScalable
    {
        float MinScaleLimit { get; set; }
        Vector3 InitialScale { get; set; }

        float MaxScaleLimit { get; set; }
        float Damping { get; set; }

        void Scale(Transform transform, Camera arCamera, Touch touchZero, Touch touchOne);
    }
}