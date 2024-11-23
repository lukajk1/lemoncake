using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private void Update()
    {
        if (playerCamera != null)
        {
            transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                             playerCamera.transform.rotation * Vector3.up);
        }
    }
}