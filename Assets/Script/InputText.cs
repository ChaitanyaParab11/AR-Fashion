using UnityEngine;
using TMPro;

public class InputText : MonoBehaviour
{
    public LayerMask targetLayer; // Layer to consider for the GameObject
    public TextMeshProUGUI displayText; // Assign the TextMeshProUGUI component in the Inspector
    public float lookDuration = 1.0f; // Time in seconds to look at the object

    private GameObject currentTarget;
    private bool hasAddedName;

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
        {
            if (hit.collider.gameObject != currentTarget)
            {
                currentTarget = hit.collider.gameObject;
                hasAddedName = false;
            }

            if (!hasAddedName)
            {
                if (Time.time >= lookDuration)
                {
                    DisplayObjectName(currentTarget.name);
                    hasAddedName = true;
                }
            }
        }
        else
        {
            currentTarget = null;
            hasAddedName = false;
        }
    }

    private void DisplayObjectName(string name)
    {
        displayText.text +=name;
    }
}
