using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Factory : WorldBlock
{
    public float multiplier = 1.0f;
    private List<Factory> neighborFactories = new List<Factory>();

    private void Start()
    {
        if (!isShadow) {
            GetNeighbors();
            foreach (Factory neighbor in neighborFactories)
            {
                neighbor.GetNeighbors();
            }
        }
    }

    public void GetNeighbors()
    {
        neighborFactories = new List<Factory>();
        Collider[] neighbors = Physics.OverlapSphere(transform.position, 0.55f);
        foreach (Collider neighbor in neighbors)
        {
            Factory neighborFactory = neighbor.GetComponentInParent<Factory>();
            if (neighborFactory != null && neighborFactory.transform != transform && !neighborFactory.isDestroyed)
            {
                Vector3 relativePos = neighborFactory.transform.rotation * (neighborFactory.transform.position - transform.position);
                neighborFactories.Add(neighborFactory);
            }
        }
    }

    protected override void GetDestroyed()
    {
        foreach (Factory neighbor in neighborFactories)
        {
            neighbor.GetNeighbors();
        }
    }

    protected void FillOutputBuffers()
    {

    }
    
    protected void lee()
    {

    }
}