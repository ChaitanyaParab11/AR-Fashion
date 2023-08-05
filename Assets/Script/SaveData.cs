using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SaveData : MonoBehaviour
{
    public TextMeshProUGUI textMesh; 

    public float totalCost= 0;
    private void Update() // Remove this Update Function use Normal Function
    {
        textMesh.text = totalCost.ToString();
    }



}
