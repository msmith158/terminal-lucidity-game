using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private Image whiteScreen;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private GameObject wheelchair;
    [SerializeField] private Transform wheelchairPos;
    [SerializeField] private UnityEvent fadeEndEvent;
    [SerializeField] private UnityEvent introEvent;
    [SerializeField] private UnityEvent outroEvent;
    [Header("Audio Stuff")]
    [SerializeField] private float audioFadeStartDelay;
    public float transitionFadeInTime;
    public CapsuleCollider enableObjectOnFadeInComplete;
    public float objectEnableAdditionalWait;
    float transitionFadeOutTime;
    float endFadeOutTime;
    float haltAtStartTime;
    float haltAtEndTime;
    bool playerDocked;
    bool memoryTrigger;
    Holder holder;
    float waitBeforeLevelChange;

    // Start is called before the first frame update
    void Start()
    {
        whiteScreen = whiteScreen.GetComponent<Image>();

        if (SceneManager.GetActiveScene().name == "1_Bedroom")
        {
            GameObject.Find("FPEPlayerController(Clone)").transform.position = GameObject.Find("PlayerStart").transform.position;
            GameObject.Find("FPEPlayerController(Clone)").transform.rotation = GameObject.Find("PlayerStart").transform.rotation;
        }

        if (SceneManager.GetActiveScene().name == "4_Bedroom2") holder = GameObject.Find("Holder").GetComponent<Holder>();

        Debug.Log("1");
        if (holder != null)
        {
            memoryTrigger = holder.isMemorySceneTriggered;
            Debug.Log("2");

            switch (memoryTrigger)
            {
                case true:
                    Debug.Log("3");
                    StartCoroutine(AltStart());
                    break;
                case false:
                    Debug.Log("4");
                    GameObject.Find("ObjectPickupLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
                    GameObject.Find("ObjectTossLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
                    GameObject.Find("ObjectExamineLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
                    GameObject.Find("ObjectInInventoryPosition").GetComponentInChildren<MeshRenderer>().enabled = false;
                    break;
            }
        }
        /*else if (SceneManager.GetActiveScene().name != "0_Intro")
        {
            Debug.Log("Holder is null");
            GameObject.Find("ObjectPickupLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
            GameObject.Find("ObjectTossLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
            GameObject.Find("ObjectExamineLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
            GameObject.Find("ObjectInInventoryPosition").GetComponentInChildren<MeshRenderer>().enabled = false;
        }*/

        if (whiteScreen.color.a == 1 && (SceneManager.GetActiveScene().name != "0_Intro" || SceneManager.GetActiveScene().name == "5_Outro"))
        {
            if (enableObjectOnFadeInComplete != null)
            {
                enableObjectOnFadeInComplete.enabled = false;
            }
            if (SceneManager.GetActiveScene().name != "3_Remembrance") StartCoroutine(TransitionFadeIn());
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "0_Intro":
                introEvent.Invoke();
                break;
            case "5_Outro":
                outroEvent.Invoke();
                break;
        }
    }

    public void SetFadeOut(float fadeOutTime)
    {
        transitionFadeOutTime = fadeOutTime;
    }

    public void SetStartHaltTime(float startHaltTime)
    {
        haltAtStartTime = startHaltTime;
    }

    public void SetEndHaltTime(float endHaltTime)
    {
        haltAtEndTime = endHaltTime;
    }

    public void SetEndFadeTime(float endGameFadeTime)
    {
        endFadeOutTime = endGameFadeTime;
    }

    public void SetLevelChangeHalt(float waitTime)
    {
        waitBeforeLevelChange = waitTime;
    }

    public void InitialiseTransitionFadeOut(string sceneName)
    {
        StartCoroutine(TransitionFadeOut(sceneName));
    }

    public void EndGameEvent(string sceneName)
    {
        StartCoroutine(EndGame(sceneName)); 
    }

    public void TimedChangeLevel(string sceneName)
    {
        StartCoroutine(ChangeLevel(sceneName));
    }

    IEnumerator AltStart()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("5");
        GameObject.Find("ObjectPickupLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("ObjectTossLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("ObjectExamineLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("ObjectInInventoryPosition").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("FPEPlayerController(Clone)").GetComponent<CharacterController>().enabled = false;
        if (wheelchairPos != null) GameObject.Find("FPEPlayerController(Clone)").transform.parent = wheelchairPos;
        GameObject.Find("FPEPlayerController(Clone)").transform.position = wheelchairPos.position;
        GameObject.Find("MainCamera").transform.rotation = new Quaternion(30, 0, 0, 0);
        wheelchair.GetComponent<Wheelchair>().enabled = true;
        GameObject.Find("FPEPlayerController(Clone)").GetComponent<Whilefun.FPEKit.FPEFirstPersonController>().playerDocked = true;
        GameObject.Find("FPEPlayerController(Clone)").GetComponent<Whilefun.FPEKit.FPEMouseLook>().MinimumX = -30;
        GameObject.Find("FPEPlayerController(Clone)").GetComponent<Whilefun.FPEKit.FPEMouseLook>().MaximumX = 30;
        Debug.Log("THE ONE PIECE IS REAL!!!");

        StartCoroutine(TransitionFadeIn());
    }

    IEnumerator TransitionFadeIn()
    {
        Color fixedColor = whiteScreen.color;
        fixedColor.a = 1;
        whiteScreen.color = fixedColor;
        whiteScreen.CrossFadeAlpha(1f, 0f, true);
        whiteScreen.CrossFadeAlpha(0, transitionFadeInTime, false);

        GameObject.Find("FPEPlayerController(Clone)").GetComponent<Whilefun.FPEKit.FPEFirstPersonController>().playerFrozen = false;

        yield return new WaitForSeconds(transitionFadeInTime);
        yield return new WaitForSeconds(objectEnableAdditionalWait);
        if (enableObjectOnFadeInComplete != null) enableObjectOnFadeInComplete.enabled = true;
        if (fadeEndEvent != null) fadeEndEvent.Invoke();
    }

    IEnumerator TransitionFadeOut(string scene)
    {
        yield return new WaitForSeconds(haltAtStartTime);

        GameObject.Find("FPEPlayerController(Clone)").GetComponent<Whilefun.FPEKit.FPEFirstPersonController>().playerFrozen = true;

        Color fixedColor = whiteScreen.color;
        fixedColor.a = 1;
        whiteScreen.color = fixedColor;
        whiteScreen.CrossFadeAlpha(0f, 0f, true);
        whiteScreen.CrossFadeAlpha(1, transitionFadeOutTime, false);

        yield return new WaitForSeconds(haltAtEndTime);

        GameObject.Find("FPEPlayerController(Clone)").transform.parent = null;
        DontDestroyOnLoad(GameObject.Find("FPEPlayerController(Clone)"));
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    IEnumerator ChangeLevel(string scene)
    {
        yield return new WaitForSeconds(waitBeforeLevelChange);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    IEnumerator EndGame(string scene)
    {
        GameObject.Find("FPEPlayerController(Clone)").GetComponent<Whilefun.FPEKit.FPEFirstPersonController>().playerFrozen = true;
        wheelchair.GetComponent<Wheelchair>().enabled = false;
        GameObject.Find("FPEDefaultHUD(Clone)").GetComponentInChildren<Image>().CrossFadeAlpha(0, 0.1f, false);
        GameObject.Find("FPEDefaultHUD(Clone)").GetComponentInChildren<Text>().CrossFadeAlpha(0, 0.1f, false);

        yield return new WaitForSeconds(haltAtStartTime);

        Color fixedColor = new Color(0,0,0);
        fixedColor.a = 1;
        whiteScreen.color = fixedColor;
        whiteScreen.CrossFadeAlpha(0f, 0f, true);
        whiteScreen.CrossFadeAlpha(1, endFadeOutTime, false);

        yield return new WaitForSeconds(haltAtEndTime);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
