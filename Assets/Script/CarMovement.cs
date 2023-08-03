using UnityEngine;
using System.Collections.Generic;

public class CarMovement : MonoBehaviour
{

    public LayerMask buildingLayer;
    public Transform building0;
    public Transform building1;
    public Transform building2;
    public Transform building3;
    public Transform building4;
    public Transform building5;
    public Transform building6;

    public GameObject car;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private Camera mainCamera;
    private bool movingTowardsBuilding = false;
    private Transform targetBuilding;
    private List<Transform> visitedBuildings = new List<Transform>();
    private Dictionary<Transform, List<Transform>> buildingConnections = new Dictionary<Transform, List<Transform>>();

    void Start()
    {
        mainCamera = Camera.main;

        // Set up the building connections
        buildingConnections.Add(building0, new List<Transform> { building1, building2 });
        buildingConnections.Add(building1, new List<Transform> { building3, building4, building0 });
        buildingConnections.Add(building2, new List<Transform> { building3, building6, building0 });
        buildingConnections.Add(building3, new List<Transform> { building4, building5, building6, building1, building2 });
        buildingConnections.Add(building4, new List<Transform> { building1, building3, building5 });
        buildingConnections.Add(building5, new List<Transform> { building3, building4, building6 });
        buildingConnections.Add(building6, new List<Transform> { building2, building3, building5 });

        // Start the car at building0
        targetBuilding = building0;
        visitedBuildings.Add(building0);
    }

    void Update()
    {
        if (movingTowardsBuilding)
        {
            return;
        }

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayer))
        {
            if (hit.transform == building0 || hit.transform == building1 || hit.transform == building2 ||
                hit.transform == building3 || hit.transform == building4 || hit.transform == building5 ||
                hit.transform == building6)
            {
                if (!visitedBuildings.Contains(hit.transform))
                {
                    Vector3 targetDir = hit.point - car.transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                    car.transform.rotation = Quaternion.Lerp(car.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    if (Quaternion.Angle(car.transform.rotation, targetRotation) < 1f)
                    {
                        movingTowardsBuilding = true;
                        targetBuilding = hit.transform;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (movingTowardsBuilding)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            car.transform.position = Vector3.MoveTowards(car.transform.position, targetBuilding.position, step);

            if (car.transform.position == targetBuilding.position)
            {
                visitedBuildings.Add(targetBuilding);
                movingTowardsBuilding = false;

                // Find the next building to move towards based on connections
                List<Transform> connections = buildingConnections[targetBuilding];
                foreach (Transform connection in connections)
                {
                    if (!visitedBuildings.Contains(connection))
                    {
                        targetBuilding = connection;
                        break;
                    }
                }
            }
        }
    }
}