using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Factory : WorldBlock
{
    public float multiplier = 1.0f;
    protected Dictionary<string, Factory> neighborFactories = new Dictionary<string, Factory>();

    private void Start()
    {
        if (!isShadow) {
            GetNeighbors();
            foreach (Factory neighbor in neighborFactories.Values)
            {
                neighbor.GetNeighbors();
            }
            printDict(neighborFactories);
        }
    }

    public void printDict(Dictionary<string, Factory> dict)
    {
        foreach (KeyValuePair<string, Factory> pair in dict)
        {
            Debug.Log(pair.Key + pair.Value.ToString());
        }
    }

    public void GetNeighbors()
    {
        neighborFactories = new Dictionary<string, Factory>();
        Collider[] neighbors = Physics.OverlapSphere(transform.position, 0.55f);
        foreach (Collider neighbor in neighbors)
        {
            Factory neighborFactory = neighbor.GetComponentInParent<Factory>();
            if (neighborFactory != null && neighborFactory.transform != transform && !neighborFactory.isDestroyed)
            {
                Vector3 relativePos = neighborFactory.transform.rotation * (neighborFactory.transform.position - transform.position);
                neighborFactories.Add(relativePos.ToString("F0"), neighborFactory);
            }
        }
    }

    protected override void GetDestroyed()
    {
        foreach (Factory neighbor in neighborFactories.Values)
        {
            neighbor.GetNeighbors();
        }
    }

    public virtual void Tick() {}
}