using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Navigation : UIControllable
{
    [SerializeField] private UnityEvent onStart;

    private void Start()
    {
        Application.targetFrameRate = 90;
        onStart.Invoke();
    }
}
