using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsHandler : MonoBehaviour
{
    [HideInInspector] public static EventsHandler instance;

    public event EventHandler TestEvent;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TestEvent += test_;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            TestEvent?.Invoke(this, EventArgs.Empty); // invokes the method if it is not null
        }
    }

    private void test_(object sender, EventArgs e)
    {
        Debug.Log("Clicked B!");
    }
}
