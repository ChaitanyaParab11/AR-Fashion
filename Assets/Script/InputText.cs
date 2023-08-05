using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    public float maxLookTime = 2.0f; // The time to look at an object to trigger interaction
    public Image loadCircle; // Reference to the UI Image for the progress circle

    public float gvrTimer = 0.0f; // Timer for looking at objects
    public string combinedNames = ""; // String to store combined object names

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Text"))
            {
                gvrTimer += Time.deltaTime;
                float fillAmount = gvrTimer / maxLookTime;
                loadCircle.fillAmount = fillAmount;

                if (gvrTimer >= maxLookTime)
                {
                    AddObjectName(hit.collider.gameObject.name);
                }
            }
            else
            {
                gvrTimer = 0.0f;
                loadCircle.fillAmount = 0.0f;
            }
        }
        else
        {
            gvrTimer = 0.0f;
            loadCircle.fillAmount = 0.0f;
        }
    }

    private void AddObjectName(string name)
    {
        if (!combinedNames.Contains(name))
        {
            if (combinedNames != "")
            {
                combinedNames += ", ";
            }
            combinedNames += name;
        }
    }
}
