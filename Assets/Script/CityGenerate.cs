using UnityEngine;
using System.Collections.Generic;

public class CityGenerate : MonoBehaviour
{
    public Transform Car;
    public int currentLocation = 0;
    public Transform[] cityTransforms; // An array to store all city transforms
    private HashSet<int> visitedCities = new HashSet<int>(); // Keep track of visited cities

    private void Start()
    {
        MoveCar(); // Move the car to the starting city at the beginning
    }

    private void Update()
    {
        // Example usage: Move to the next city when the user presses a key, like "N".
        if (Input.GetKeyDown(KeyCode.N))
        {
            MoveToNextCity();
        }
    }

    public void MoveToNextCity()
    {
        int nextCity = GetNextConnectedCity();
        if (nextCity != -1)
        {
            currentLocation = nextCity;
            visitedCities.Add(currentLocation);
            MoveCar();
        }
        else
        {
            Debug.LogWarning("No valid connected city from City " + currentLocation);
        }
    }

    private int GetNextConnectedCity()
    {
        // Define the connections between cities based on the provided connections.
        // The car can only go to each city once.

        switch (currentLocation)
        {
            case 0:
                // City 0 is connected to cities 1 and 2.
                if (!visitedCities.Contains(1)) return 1;
                if (!visitedCities.Contains(2)) return 2;
                break;

            case 1:
                // City 1 is connected to cities 4 and 3.
                if (!visitedCities.Contains(4)) return 4;
                if (!visitedCities.Contains(3)) return 3;
                break;

            case 2:
                // City 2 is connected to cities 3 and 6.
                if (!visitedCities.Contains(3)) return 3;
                if (!visitedCities.Contains(6)) return 6;
                break;

            case 3:
                // City 3 is connected to cities 4, 5, and 6.
                if (!visitedCities.Contains(4)) return 4;
                if (!visitedCities.Contains(5)) return 5;
                if (!visitedCities.Contains(6)) return 6;
                break;

            case 4:
                // City 4 is connected to cities 5 and 1.
                if (!visitedCities.Contains(5)) return 5;
                if (!visitedCities.Contains(1)) return 1;
                break;

            case 5:
                // City 5 is connected to cities 4, 3, and 6.
                if (!visitedCities.Contains(4)) return 4;
                if (!visitedCities.Contains(3)) return 3;
                if (!visitedCities.Contains(6)) return 6;
                break;

            case 6:
                // City 6 is connected to cities 2, 3, and 5.
                if (!visitedCities.Contains(2)) return 2;
                if (!visitedCities.Contains(3)) return 3;
                if (!visitedCities.Contains(5)) return 5;
                break;
        }

        return -1; // Return -1 if there are no valid unvisited connected cities from the current city.
    }

    public void MoveCar()
    {
        Car.transform.position = cityTransforms[currentLocation].position;
    }
}
