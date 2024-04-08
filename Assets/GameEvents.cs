using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private Image whiteScreen;
    [SerializeField] private float fadeOutTime;

    // Start is called before the first frame update
    void Start()
    {
        whiteScreen = whiteScreen.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGameEvent()
    {
        StartCoroutine(EndGame());
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
