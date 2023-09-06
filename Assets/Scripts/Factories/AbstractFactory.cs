using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : WorldBlock
{
    public float speed = 1.0f; //In seconds
    public Item item;
    public GameObject prefab;
    
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
            Generate(item);
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

    public void Generate(Item item) {
        InventoryManager.Instance.Add(item);
        //Adds to player inventory temporarily. Replace with factory inventory later
    }
}
