using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace arplace.ObjectManipulation
{
    public class SelectableObject : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectionVisuals;
        public bool IsSelected { get; set; }
        private Camera arCamera;

        private void Start()
        {
            arCamera = Camera.main;
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        if (hit.collider != null && hit.collider.gameObject == gameObject)
                        {
                            ShowSelectionVisuals();
                        }
                        else
                        {
                            HideSelectionVisuals();
                        }
                    }
                    else
                    {
                        HideSelectionVisuals();
                    }
                }
            }
        }
        private void ShowSelectionVisuals()
        {
            selectionVisuals.gameObject.SetActive(true);
            IsSelected = true;
        }

        private void HideSelectionVisuals()
        {
            selectionVisuals.gameObject.SetActive(false);
            IsSelected = false;
        }
    }
}