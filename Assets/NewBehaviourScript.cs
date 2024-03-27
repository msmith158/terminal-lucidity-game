using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    private TextMeshProUGUI displayText;

    private void Start()
    {
        displayText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        int objectCount = UnityStats.vboTotal;
        Debug.Log("Object count: " + objectCount);
        displayText.text = objectCount + " Objects";
    }
}
