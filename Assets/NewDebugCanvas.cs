// Broken script, pls fix later

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NewDebugCanvas : MonoBehaviour
{
    private TextMeshProUGUI debugText;

    // Dictionary to store bools by their names
    private Dictionary<string, bool> boolDictionary = new Dictionary<string, bool>();

    // Example bools (you can add more)
    public bool enableFPSCounter;
    public bool hasKey;
    public bool isQuestCompleted;

    // String to modify
    private string resultString = "";

    // String dictionary
    private string framerateString = "";

    // Framerate values
    public int fpsCounter_avgFrameRate;
    public float fpsCounter_updateInterval = 0.5F;
    private double fpsCounter_lastInterval;
    private int fpsCounter_frames;
    private float fpsCounter_fps;

    private void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
        debugText.text = resultString;

        // Populate the dictionary with bools
        boolDictionary.Add(framerateString, enableFPSCounter);
        boolDictionary.Add("HasKey", hasKey);
        boolDictionary.Add("QuestCompleted", isQuestCompleted);

        // Check bool states and modify the string
        foreach (var kvp in boolDictionary)
        {
            Debug.Log(kvp.Value);

            if (kvp.Value)
            {
                resultString += kvp.Key + " \n"; // Modify this part as needed
            }

            Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
        }

        // Example: Log the modified string
        Debug.Log("Modified String: " + resultString);
    }

    private void Update()
    {
        StandardFramerate();
        InterpFramerate();

        framerateString = "FPS: " + fpsCounter_avgFrameRate.ToString() + " (" + fpsCounter_fps.ToString() + " avg)";
    }

    private void StandardFramerate()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        fpsCounter_avgFrameRate = (int)current;
        //framerateString.text = avgFrameRate.ToString() + " FPS";
    }

    private void InterpFramerate()
    {
        ++fpsCounter_frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > fpsCounter_lastInterval + fpsCounter_updateInterval)
        {
            fpsCounter_fps = Mathf.Ceil((float)(fpsCounter_frames / (timeNow - fpsCounter_lastInterval))); // Mathf.Ceil will round the outputted integer up to the nearest whole number
            fpsCounter_frames = 0;
            fpsCounter_lastInterval = timeNow;
        }
    }
}