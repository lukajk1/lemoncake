using UnityEngine;
using static UnityEngine.UI.Image;

public class CheckForInteractable : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;

    private float range = 5.5f;
    private RaycastHit hit;
    private InteractPrompt interactPrompt;
    bool currentInteractState;
    private void Start()
    {
        interactPrompt = FindFirstObjectByType<InteractPrompt>();
    }
    private void Update()
    {
        bool isInteractable = false;

        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
        {
            isInteractable = hit.collider.CompareTag("Interact");
        }

        if (isInteractable != currentInteractState)
        {
            currentInteractState = isInteractable;
            interactPrompt.SetInteract(currentInteractState);
        }
    }
}
