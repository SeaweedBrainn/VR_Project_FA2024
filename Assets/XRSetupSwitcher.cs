using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
public class XRSetupSwitcher : MonoBehaviour
{
    public GameObject oldXRSetup; // Drag your old XR setup GameObject here
    public GameObject newXRSetup; // Drag your new XR setup GameObject here
    public InputActionAsset newSetupInputActions; // Drag the Input Action Asset used by the new setup here

    public void SwitchToNewSetup()
    {
        // Step 1: Disable the old XR setup
        if (oldXRSetup != null)
        {
            oldXRSetup.SetActive(false);
        }

        // Step 2: Enable the new XR setup
        if (newXRSetup != null)
        {
            newXRSetup.SetActive(true);
        }

        // Step 3: Enable Input Actions for the new setup
        if (newSetupInputActions != null)
        {
            EnableNewSetupActions();
        }

        // Step 4: Restart the XR Input Subsystem to ensure proper tracking
        RestartXRSubsystem();

        // Step 5: Recenter the tracking to ensure the new setup starts aligned
        UnityEngine.XR.InputTracking.Recenter();

        // Step 6: Refresh XR controllers and interactors in the new setup
        RefreshControllersAndInteractors(newXRSetup);

        Debug.Log("Switched to the new XR setup.");
    }

    private void EnableNewSetupActions()
    {
        foreach (var action in newSetupInputActions)
        {
            if (!action.enabled)
            {
                action.Enable();
            }
        }

        Debug.Log("Enabled all Input Actions for the new setup.");
    }

    private void RestartXRSubsystem()
    {
        // Get all XR Input Subsystems
        var inputSubsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances(inputSubsystems);

        // Restart each subsystem
        foreach (var subsystem in inputSubsystems)
        {
            subsystem.Stop();
            subsystem.Start();
        }
        
    }

    private void RefreshControllersAndInteractors(GameObject xrSetup)
    {
        // Refresh XR controllers
        var controllers = xrSetup.GetComponentsInChildren<XRController>(true);
        foreach (var controller in controllers)
        {
            controller.enabled = false;
            controller.enabled = true; // Refresh state
        }

        // Refresh XR interactors
        var interactors = xrSetup.GetComponentsInChildren<XRBaseControllerInteractor>(true);
        foreach (var interactor in interactors)
        {
            interactor.enabled = false;
            interactor.enabled = true; // Refresh state
        }
        
    }
}
