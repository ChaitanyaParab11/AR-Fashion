using UnityEngine;

public class CityGraph : MonoBehaviour
{
    // This array will store the transforms of all the cities.
    public Transform[] cityTransforms;

    // Current city number where the car is located.
    private int currentCityNumber;

    // Start is called before the first frame update
    void Start()
    {
        // Assume the car starts in City 0
        currentCityNumber = 0;
        // Move the car to the starting city (City 0)
        MoveToCity(currentCityNumber);
    }

    // Call this method when you want the car to move to a new city
    public void MoveToCity(int destinationCityNumber)
    {
        if (IsValidMove(destinationCityNumber))
        {
            // Update the current city number
            currentCityNumber = destinationCityNumber;
            // Move the car to the destination city
            transform.position = cityTransforms[destinationCityNumber].position;
        }
        else
        {
            Debug.LogWarning("Invalid move! City " + destinationCityNumber + " is not connected to City " + currentCityNumber);
        }
    }

    // Helper method to check if the move is valid
    private bool IsValidMove(int destinationCityNumber)
    {
        // Check if the destination city is connected to the current city
        // You may need to set up a proper data structure or define connections between cities
        // For simplicity, I assume city 0 is connected to cities 1, 3, and 5.
        if (currentCityNumber == 0)
        {
            return destinationCityNumber == 1 || destinationCityNumber == 3 || destinationCityNumber == 5;
        }

        // You should add more cases for other cities and their connections.

        // Return false if the destination city is not connected to the current city
        return false;
    }
}
