using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomText : MonoBehaviour
{
    public TMP_Text randomTextOutput;
    public TMP_Text reverseCipherOutput;
    public TMP_Text caesarCipherOutput;
    public TMP_Text customCaesarCipherOutput;

    public int textLength = 10;
    public int caesarShiftAmount = 3; // The default shift amount for the Caesar cipher
    public int customShiftAmount = 5; // The custom shift amount for the custom Caesar cipher

    private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public void GenerateRandomText()
    {
        string randomText = GenerateRandomString(textLength);
        randomTextOutput.text = randomText;
    }

    private string GenerateRandomString(int length)
    {
        string result = "";
        for (int i = 0; i < length; i++)
        {
            int randomIndex = Random.Range(0, chars.Length);
            result += chars[randomIndex];
        }
        return result;
    }

    public void ConvertToReverseCipher()
    {
        string originalText = randomTextOutput.text;
        string reverseCipherText = ReverseCipher(originalText);
        reverseCipherOutput.text = reverseCipherText;
    }

    public void ConvertToCaesarCipher()
    {
        string originalText = randomTextOutput.text;
        string caesarCipherText = CaesarCipher(originalText, caesarShiftAmount);
        caesarCipherOutput.text = caesarCipherText;
    }

    public void ConvertToCustomCaesarCipher()
    {
        string originalText = randomTextOutput.text;
        string customCaesarCipherText = CaesarCipher(originalText, customShiftAmount);
        customCaesarCipherOutput.text = customCaesarCipherText;
    }

    private string ReverseCipher(string inputText)
    {
        char[] charArray = inputText.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }

    private string CaesarCipher(string inputText, int shift)
    {
        string result = "";
        foreach (char c in inputText)
        {
            char encryptedChar = c;

            if (char.IsLetter(c))
            {
                char offset = char.IsUpper(c) ? 'A' : 'a';
                encryptedChar = (char)(((c + shift) - offset) % 26 + offset);
            }
            else if (char.IsDigit(c))
            {
                encryptedChar = (char)(((c - '0' + shift) % 10) + '0');
            }

            result += encryptedChar;
        }
        return result;
    }
}
