using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class ObjectSocketAware : XRGrabInteractable
{
    public UnityEvent OnPlacedInSocket;
    /*public gameObject beacon;*/
    
    // This method is called when the object is selected (grabbed or placed in a socket)
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Check if the interactor is an XRSocketInteractor
        if (args.interactorObject is XRSocketInteractor)
        {
            Debug.Log("I have been placed into a socket!");

            // Add any custom behavior here for when the object is placed into the socket
            OnPlacedInSocket.Invoke();
        }
    }

    // Custom behavior for when the object enters a socket
    /*private void OnPlacedInSocket()
    {
        // Example: Change color to green when placed in the socket
        gameObject.SetActive(false);
        beacon.SetActive(false);
    }*/
}