using UnityEngine;
using TMPro;

public class TriggerTextUpdater : MonoBehaviour
{
    public SaveData sd;
    public float ValueToAdd = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player (you can change the tag accordingly)
        {
            sd.totalCost += ValueToAdd;
        }
    }
}