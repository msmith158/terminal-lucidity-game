using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SubtitleSystem : MonoBehaviour
{
    public TextMeshProUGUI subtitleTextElement;
    [Range(0, 1)] public float subtitleFadeTime;
    public float subtitleHangTime;
    Color fixedColor;
    string subtitleTextGlobal;
    string newTextGlobal;
    bool isActivated;
    int subtitleQueuePos;
    int waitQueuePos;
    float pauseTimeGlobal;
    [HideInInspector] public List<string> subtitleQueue = new();
    [HideInInspector] public List<float> waitQueue = new();

    // Start is called before the first frame update
    void Start()
    {
        subtitleTextElement = subtitleTextElement.GetComponent<TextMeshProUGUI>();
        subtitleTextElement.color = new Vector4(1, 1, 1, 0);
    }

    public void InitialiseSubtitles(string subtitleText)
    {
        if (!isActivated)
        {
            subtitleTextGlobal = subtitleText;
            StartCoroutine(DisplaySubtitles());
        }
        else
        {
            SubtitleExit(subtitleText);
        }
    }

    public void AddToWaitQueue(float pauseTime)
    {
        waitQueue.Add(pauseTime);
    }

    public void SubtitleExit(string newText)
    {
        StartCoroutine(SubtitleExit2(newText)); 
    }

    public void AddToSubtitleQueue(string subtitleTextToQueue) 
    {
        subtitleQueue.Add(subtitleTextToQueue);
    }

    public void InitialiseSubtitlesMulti()
    {
        if (subtitleQueuePos < subtitleQueue.Count)
        {
            subtitleTextGlobal = subtitleQueue[subtitleQueuePos];
            subtitleQueuePos++;
            StartCoroutine(DisplaySubtitlesMulti());
        }
        else if (subtitleQueuePos >= subtitleQueue.Count)
        {
            subtitleQueue.Clear();
            subtitleQueuePos = 0;
        }
    }

    IEnumerator DisplaySubtitlesMulti()
    {
        subtitleTextElement.text = subtitleTextGlobal;

        fixedColor = subtitleTextElement.color;
        fixedColor.a = 1;
        subtitleTextElement.color = fixedColor;
        subtitleTextElement.CrossFadeAlpha(0f, 0f, true);
        subtitleTextElement.CrossFadeAlpha(1, subtitleFadeTime, false);

        yield return new WaitForSeconds(subtitleHangTime);

        fixedColor.a = 0;
        subtitleTextElement.CrossFadeAlpha(0, subtitleFadeTime, false);

        yield return new WaitForSeconds(subtitleFadeTime);

        if (waitQueuePos < waitQueue.Count)
        {
            pauseTimeGlobal = waitQueue[waitQueuePos];
            waitQueuePos++;
            yield return new WaitForSeconds(pauseTimeGlobal);
        }
        
        InitialiseSubtitlesMulti();
    }

    IEnumerator DisplaySubtitles()
    {
        subtitleTextElement.text = subtitleTextGlobal;

        fixedColor = subtitleTextElement.color;
        fixedColor.a = 1;
        subtitleTextElement.color = fixedColor;
        subtitleTextElement.CrossFadeAlpha(0f, 0f, true);
        subtitleTextElement.CrossFadeAlpha(1, subtitleFadeTime, false);
        isActivated = true;

        yield return new WaitForSeconds(subtitleHangTime);

        SubtitleExit(newTextGlobal);
    }

    IEnumerator SubtitleExit2(string newText2)
    {
        fixedColor.a = 0;
        subtitleTextElement.CrossFadeAlpha(0, subtitleFadeTime, false);
        isActivated = false;

        yield return new WaitForSeconds(subtitleFadeTime);

        if (newText2 != null)
        {
            subtitleTextGlobal = newText2;
            InitialiseSubtitles(subtitleTextGlobal);
        }
        else yield return null;
    }
}