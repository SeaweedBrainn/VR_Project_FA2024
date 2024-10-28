using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class SnapToDirectInteractorManager : MonoBehaviour
{
    // Specify left-hand direct interactor here
    public XRDirectInteractor leftDirectInteractor;

    private XRInteractionManager interactionManager;
    private List<XRGrabInteractable> interactables = new List<XRGrabInteractable>();

    private void Awake()
    {
        // Find or assign the XRInteractionManager in the scene
        interactionManager = FindObjectOfType<XRInteractionManager>();

        // Get all XRGrabInteractables in the scene
        foreach (var interactable in FindObjectsOfType<XRGrabInteractable>())
        {
            interactables.Add(interactable);
            interactable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    private void OnDestroy()
    {
        // Remove event listeners to prevent memory leaks
        foreach (var interactable in interactables)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Check if the interactor is a Ray Interactor
        if (args.interactorObject is XRRayInteractor)
        {
            // Try to switch to the left direct interactor
            TrySwitchToLeftDirectInteractor(args.interactableObject as XRGrabInteractable);
        }
    }

    private void TrySwitchToLeftDirectInteractor(XRGrabInteractable grabInteractable)
    {
        if (leftDirectInteractor != null && leftDirectInteractor.hasSelection == false)
        {
            // Deselect from the ray interactor
            interactionManager.SelectExit(grabInteractable.interactorsSelecting[0], grabInteractable);

            // Select the object with the left-hand direct interactor
            interactionManager.SelectEnter(leftDirectInteractor, grabInteractable);
        }
    }
}