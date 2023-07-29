using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    public Camera playerCamera;
    public Transform targetObject;
    public float moveSpeed = 5f;
    public float stoppingDistance = 2f;

    private bool isMoving = false;

    void Update()
    {
        // Check if the player camera is looking at the target object
        if (IsCameraLookingAtTarget())
        {
            if (!isMoving)
            {
                // Start moving towards the target object
                isMoving = true;
            }

            // Move towards the target object
            MoveToTarget();
        }
        else
        {
            // Stop moving if the camera is not looking at the target object
            isMoving = false;
        }
    }

    private bool IsCameraLookingAtTarget()
    {
        // Make sure both playerCamera and targetObject are assigned
        if (playerCamera == null || targetObject == null)
            return false;

        // Calculate the vector from the camera to the target
        Vector3 cameraToTarget = targetObject.position - playerCamera.transform.position;

        // Calculate the angle between the camera's forward direction and the vector to the target
        float angle = Vector3.Angle(playerCamera.transform.forward, cameraToTarget);

        // Return true if the angle is less than a threshold value (e.g., 30 degrees) to consider it "looking at" the target
        return angle < 30f;
    }

    private void MoveToTarget()
    {
        // Calculate the distance to the target object
        float distanceToTarget = Vector3.Distance(transform.position, targetObject.position);

        // If the player is already within the stopping distance, no need to move
        if (distanceToTarget <= stoppingDistance)
        {
            isMoving = false;
            return;
        }

        // Calculate the direction to the target
        Vector3 directionToTarget = (targetObject.position - transform.position).normalized;

        // Move towards the target with a smooth step
        transform.position += directionToTarget * moveSpeed * Time.deltaTime;
    }
}
