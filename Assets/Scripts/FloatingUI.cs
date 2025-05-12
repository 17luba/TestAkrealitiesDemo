using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public Transform cameraTransform; // la cam�ra XR
    public float distanceFromCamera = 1.5f;
    public Vector3 offset = Vector3.zero;
    public bool followRotation = true;

    void LateUpdate()
    {
        if (cameraTransform == null)
            return;

        // Position du canvas devant la cam�ra
        Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera + offset;
        transform.position = targetPosition;

        // Orientation du canvas (optionnel)
        if (followRotation)
        {
            Vector3 lookDirection = transform.position - cameraTransform.position;
            lookDirection.y = 0; // Ne pas suivre la t�te verticalement si tu veux un comportement plus stable
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
