using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NewDebugCanvas : MonoBehaviour
{
    private TextMeshProUGUI debugText;

    // Dictionary to store bools by their names
    private Dictionary<string, bool> boolDictionary = new Dictionary<string, bool>();

    List<string> debugList = new List<string>();

    // Example bools (you can add more)
    public bool enableFPSCounter;
    public bool enableFrameTiming;
    public bool enableCPUMetrics;
    public bool enableGPUMetrics;
    public bool enableLevelName;

    // String to modify
    private string resultString = "";

    // String dictionary
    private string framerateString = "";
    private string frameTimingString = "";
    private string CPUString = "";
    private string GPUString = "";
    private string levelNameString = "";

    // Framerate values
    [HideInInspector] public int fpsCounter_avgFrameRate;
    [Tooltip("Adjust how often the average framerate metre updates")] public float fpsCounter_updateInterval = 0.5F;
    private double fpsCounter_lastInterval;
    private int fpsCounter_frames;
    private float fpsCounter_fps;

    private void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        CheckBools();

        // Reset dictionary per draw to allow for adding fresh stats
        boolDictionary.Clear();

        // Add the strings with their respective bools here
        boolDictionary.Add(framerateString, enableFPSCounter);
        boolDictionary.Add(frameTimingString, enableFrameTiming);
        boolDictionary.Add(CPUString, enableCPUMetrics);
        boolDictionary.Add(GPUString, enableGPUMetrics);
        boolDictionary.Add(levelNameString, enableLevelName);

        // Reset list per draw to get fresh stats
        debugList.Clear();

        foreach (var kvp in boolDictionary)
        {
            if (kvp.Value)
            {
                debugList.Add(kvp.Key);
            }
        }

        // Reset string per draw to fix an overflow issue
        resultString = "";
        resultString += "/// Debug Metrics /// \n \n";

        for (int i = 0; i < debugList.Count; i++)
        {
            if (i != (debugList.Count + 1))
            {
                resultString += debugList[i] + " \n";
            }
            else
            {
                break;
            }
        }

        debugText.text = resultString;
    }

    // This big list of switch statements allows optimisation by only allowing functions that are needed to run.
    private void CheckBools()
    {
        switch (enableFPSCounter)
        {
            case true:
                Framerate();
                break;

            case false:
                break;
        }

        switch (enableFrameTiming)
        {
            case true:
                FrameTiming();
                break;

            case false:
                break;
        }

        switch (enableCPUMetrics)
        {
            case true:
                CPUMetrics();
                break;

            case false:
                break;
        }

        switch (enableGPUMetrics)
        {
            case true:
                GPUMetrics();
                break;

            case false:
                break;
        }

        switch (enableLevelName)
        {
            case true:
                LevelName();
                break;

            case false:
                break;
        }
    }

    private void Framerate()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        fpsCounter_avgFrameRate = (int)current;
        framerateString = "FPS: " + fpsCounter_avgFrameRate.ToString() + " (" + fpsCounter_fps.ToString() + " avg)";

        ++fpsCounter_frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > fpsCounter_lastInterval + fpsCounter_updateInterval)
        {
            fpsCounter_fps = Mathf.Ceil((float)(fpsCounter_frames / (timeNow - fpsCounter_lastInterval))); // Mathf.Ceil will round the outputted integer up to the nearest whole number
            fpsCounter_frames = 0;
            fpsCounter_lastInterval = timeNow;
        }
    }

    private void FrameTiming()
    {
       float currentFrameTiming = Mathf.Ceil(Time.deltaTime * 1000);
       frameTimingString = "Timing: " + currentFrameTiming.ToString() + " ms / ";
    }

    private void CPUMetrics()
    {
        ProfilerRecorder cpuFrameTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "CPU Total Frame Time");
        var frameTime = cpuFrameTimeRecorder.LastValue;
        CPUString = frameTime.ToString();
    }

    private void GPUMetrics()
    {
        // Code here.
    }

    private void LevelName()
    {
        Scene scene = SceneManager.GetActiveScene();
        levelNameString = "Active scene: \"" + scene.name + "\"";
    }
}