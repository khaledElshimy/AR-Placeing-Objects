using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        GameObject SelectionVisuals { get; set; }
        void Select();
        void Deselect();

    }
}

