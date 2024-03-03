using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace arplace.UI
{
    public class ObjectUIItem : MonoBehaviour
    {
        [SerializeField] private Text objectName;
        [SerializeField] private Image objectIcon;
        [SerializeField] private Button objectButton;

        public void SetupView(String objectName, Sprite objectIcon, UnityAction butttonActionCallback)
        {
            this.objectName.text = objectName;
            this.objectIcon.sprite = objectIcon;
            this.objectButton.onClick.AddListener(butttonActionCallback);
        }
    }
}