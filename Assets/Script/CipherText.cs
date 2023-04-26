using UnityEngine;
using UnityEngine.UI;

public class CipherText : MonoBehaviour
{
    public Text inputField;
    public Text outputText;
    public int key = 3; // number of letters to shift

    public void EncryptText()
    {
        string input = inputField.text.ToUpper(); // convert input to uppercase
        string output = "";

        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                char encryptedChar = (char)(((int)c + key - 65) % 26 + 65); // apply Caesar Cipher
                output += encryptedChar;
            }
            else
            {
                output += c; // leave non-letter characters unchanged
            }
        }

        outputText.text = output; // display encrypted text in output Text UI
    }
}