using UnityEngine;
using TMPro;

public class EscapeRoom : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Assign the TextMeshProUGUI component in the Inspector

    // Call this function to remove the last entered object or letter from displayText
    public void RemoveLastEntry()
    {
        if (!string.IsNullOrEmpty(displayText.text))
        {
            int lastNewlineIndex = displayText.text.LastIndexOf("\n");
            if (lastNewlineIndex >= 0)
            {
                displayText.text = displayText.text.Substring(0, lastNewlineIndex);
            }
            else
            {
                displayText.text = displayText.text.Substring(0, displayText.text.Length - 1);
            }
        }
    }
}
