using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public bool isMemorySceneTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void EnableMemoryTrigger()
    {
        isMemorySceneTriggered = true;
        Debug.Log("THE ONE PIECE IS REAL");
    }
}
