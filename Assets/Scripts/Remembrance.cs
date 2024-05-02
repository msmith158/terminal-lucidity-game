using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Remembrance : MonoBehaviour
{
    [SerializeField] private UnityEvent startingEvent;

    // Start is called before the first frame update
    void Start()
    {
        startingEvent.Invoke();
    }
}
