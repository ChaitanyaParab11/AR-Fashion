using UnityEngine;

public class MoveTowardsLook : MonoBehaviour
{
    public Transform[] locations; // Array of locations to move towards
    public float moveSpeed = 5f; // Speed at which the player moves
    public float stopDistance = 1f; // Stopping distance from the target object

    private Transform currentTarget;
    private bool isMoving = false;

    private void Start()
    {
        // Initialize with the first target from the array
        if (locations.Length > 0)
        {
            currentTarget = locations[0];
        }
    }

    private void Update()
    {
        bool isLookingAtLocation = false;

        // Check if the player is looking at a target
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Transform hitTransform = hit.transform;
            if (ArrayContainsLocation(hitTransform))
            {
                currentTarget = hitTransform;
                isMoving = true;
                isLookingAtLocation = true;
            }
        }

        // Stop moving if not looking at a location
        if (!isLookingAtLocation)
        {
            isMoving = false;
        }

        // Move towards the current target
        if (isMoving && currentTarget != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 targetPosition = currentTarget.position;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Move towards the target if the distance is greater than the stopping distance
        if (distanceToTarget > stopDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }

    private bool ArrayContainsLocation(Transform target)
    {
        foreach (Transform location in locations)
        {
            if (location == target)
            {
                return true;
            }
        }
        return false;
    }
}
