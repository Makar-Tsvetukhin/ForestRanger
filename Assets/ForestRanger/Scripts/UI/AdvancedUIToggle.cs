using UnityEngine;
using System.Collections.Generic;

public class AdvancedUIToggle2D : MonoBehaviour
{
	[field: SerializeField] private List<GameObject> uiElementsToDeactivate = new List<GameObject>();
	[field: SerializeField] private List<GameObject> elementsToActivate = new List<GameObject>();
	[field: SerializeField] private GameObject parentWithColliders;

    private List<bool> originalUIStates = new List<bool>();
    private List<Collider2D> childColliders = new List<Collider2D>();
    private List<bool> originalColliderStates = new List<bool>();
    private bool isToggled = false;

    private void Start()
    {
        foreach (GameObject uiElement in uiElementsToDeactivate)
        {
            if (uiElement != null)
            {
                originalUIStates.Add(uiElement.activeSelf);
            }
        }

        if (parentWithColliders != null)
        {
            childColliders.AddRange(parentWithColliders.GetComponentsInChildren<Collider2D>());
            foreach (Collider2D col in childColliders)
            {
                originalColliderStates.Add(col.enabled);
            }
        }
    }

    public void ToggleUI()
    {
        if (!isToggled)
        {
            for (int i = 0; i < uiElementsToDeactivate.Count; i++)
            {
                if (uiElementsToDeactivate[i] != null)
                {
                    uiElementsToDeactivate[i].SetActive(false);
                }
            }

            foreach (GameObject element in elementsToActivate)
            {
                if (element != null)
                {
                    element.SetActive(true);
                }
            }

            for (int i = 0; i < childColliders.Count; i++)
            {
                childColliders[i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < uiElementsToDeactivate.Count; i++)
            {
                if (uiElementsToDeactivate[i] != null)
                {
                    uiElementsToDeactivate[i].SetActive(originalUIStates[i]);
                }
            }

            foreach (GameObject element in elementsToActivate)
            {
                if (element != null)
                {
                    element.SetActive(false);
                }
            }

            for (int i = 0; i < childColliders.Count; i++)
            {
                childColliders[i].enabled = originalColliderStates[i];
            }
        }

        isToggled = !isToggled;
    }
}