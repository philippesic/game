using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : WorldBlock
{
    public float speed = 1.0f; //In seconds
    public int itemId;
    public int multiplier = 1;
    public GameObject prefab;
    protected bool isRunning = false;

    private void Start()
    {
        StartFactory(); 
    }

    protected virtual void ExecuteFactory()
    {
        if (!isRunning)
        {
            //
        }
    }

    public void StartFactory()
    {
        ExecuteFactory();
    }

    public void StopFactory()
    {
        //
    }
}