using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private Image whiteScreen;
    [SerializeField] private float fadeOutTime;
    public float transitionFadeInTime;
    public CapsuleCollider enableObjectOnFadeInComplete;
    public float objectEnableAdditionalWait;
    float transitionFadeOutTime;
    float haltAtStartTime;
    float haltAtEndTime;

    // Start is called before the first frame update
    void Start()
    {
        whiteScreen = whiteScreen.GetComponent<Image>();
        GameObject.Find("ObjectPickupLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("ObjectTossLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("ObjectExamineLocation").GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject.Find("ObjectInInventoryPosition").GetComponentInChildren<MeshRenderer>().enabled = false;

        if (whiteScreen.color.a == 1)
        {
            enableObjectOnFadeInComplete.enabled = false;
            StartCoroutine(TransitionFadeIn());
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

    public void InitialiseTransitionFadeOut(string sceneName)
    {
        StartCoroutine(TransitionFadeOut(sceneName));
    }

    public void EndGameEvent()
    {
        StartCoroutine(EndGame());
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
        enableObjectOnFadeInComplete.enabled = true;

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

        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    IEnumerator EndGame()
    {
        GameObject.Find("FPEPlayerController(Clone)").GetComponent<Whilefun.FPEKit.FPEFirstPersonController>().playerFrozen = true;

        Color fixedColor = whiteScreen.color;
        fixedColor.a = 1;
        whiteScreen.color = fixedColor;
        whiteScreen.CrossFadeAlpha(0f, 0f, true);
        whiteScreen.CrossFadeAlpha(1, fadeOutTime, false);

        yield return new WaitForSeconds(fadeOutTime);
        whiteScreen.color = Color.black;
    }
}
