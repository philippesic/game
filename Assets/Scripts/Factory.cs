using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : WorldBlock
{
    public float speed = 1.0f; //In seconds
    public int itemId;
    public int multiplier = 1;
    protected bool isRunning = false;

    private void Start()
    {
        StartFactory(); 
    }

    public abstract GameObject Create();

    protected virtual void ExecuteFactory()
    {
        if (!isRunning)
        {
            StartCoroutine(FactoryLoop());
        }
    }

    protected IEnumerator FactoryLoop()
    {
        isRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(speed);
            Generate(itemId, multiplier);
        }
    }

    public void StartFactory()
    {
        ExecuteFactory();
    }

    public void StopFactory()
    {
        isRunning = false;
        StopCoroutine(FactoryLoop());
    }

    public void Generate(int id, int count) {
        Player.instance.inv.Add(id, count);
        //Adds to player inventory temporarily. Replace with factory inventory later
    }
}