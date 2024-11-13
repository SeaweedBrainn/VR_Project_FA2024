using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class SnapToDirectInteractorManager : MonoBehaviour
{
    public XRDirectInteractor leftDirectInteractor;
    private XRInteractionManager interactionManager;

    private void Awake()
    {
        interactionManager = FindObjectOfType<XRInteractionManager>();

        // Register select events for all existing XRGrabInteractables
        RegisterInteractablesOnSceneLoad();
    }

    private void RegisterInteractablesOnSceneLoad()
    {
        foreach (var interactable in FindObjectsOfType<XRGrabInteractable>(true)) // 'true' to include inactive objects
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    private void OnDestroy()
    {
        foreach (var interactable in FindObjectsOfType<XRGrabInteractable>())
        {
            if (interactable != null)
                interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Check if the selecting interactor is a Ray Interactor
        if (args.interactorObject is XRRayInteractor)
        {
            // Try to switch to the left direct interactor
            TrySwitchToLeftDirectInteractor(args.interactableObject as XRGrabInteractable);
        }
    }

    private void TrySwitchToLeftDirectInteractor(XRGrabInteractable grabInteractable)
    {
        if (leftDirectInteractor != null && !leftDirectInteractor.hasSelection)
        {
            // Deselect from the ray interactor
            interactionManager.SelectExit(grabInteractable.interactorsSelecting[0], grabInteractable);

            // Select the object with the left-hand direct interactor
            interactionManager.SelectEnter(leftDirectInteractor, grabInteractable);
        }
    }
}