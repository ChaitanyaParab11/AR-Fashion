using UnityEngine;
using TMPro;

public class CaesarCipherGame : MonoBehaviour
{
    public string randomText;
    public string encrypted;
    public int randomShift;
    public TMP_Text originalText;
    public TMP_Text encryptedText;
    public TMP_Text randomshifttext;

    public TMP_Text InputText;
    public TimerController tc;


    private void Update()
    {
        if(randomText == InputText.text && tc.currentTime >1)
        {
            tc.WinStatus = 2;
        }

    }


    private void Start()
    {
        GenerateRandomText();
    }

    public void GenerateRandomText()
    {
        randomText = GenerateRandomString();
        originalText.text = randomText;

        randomShift = Random.Range(1, 26); // Random shift between 1 and 25
        encrypted = EncryptCaesarCipher(randomText, randomShift);
        encryptedText.text = encrypted;
        randomshifttext.text = randomShift.ToString();
    }

    private string GenerateRandomString()
    {
        const string characters = "abcdefghijklmnopqrstuvwxyz";
        int length = Random.Range(4, 8); // Random length between 5 and 20 characters
        char[] randomChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomChars[i] = characters[Random.Range(0, characters.Length)];
        }

        return new string(randomChars);
    }

    private string EncryptCaesarCipher(string text, int shift)
    {
        string encrypted = "";

        foreach (char character in text)
        {
            if (char.IsLetter(character))
            {
                char shifted = (char)(character + shift);
                if ((char.IsLower(character) && shifted > 'z') || (char.IsUpper(character) && shifted > 'Z'))
                {
                    shifted = (char)(character - (26 - shift));
                }
                encrypted += shifted;
            }
            else
            {
                encrypted += character;
            }
        }

        return encrypted;
    }

}
