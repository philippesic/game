using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Factory : WorldBlock
{
    public Dictionary<string, Factory> neighborFactories = new();

    public void Start()
    {
        if (!isShadow)
        {
            GetNeighbors();
            foreach (Factory neighbor in neighborFactories.Values)
            {
                neighbor.GetNeighbors();
            }
            SetupFactory();
        }
    }

    public virtual void SetupFactory() { }

    public void GetNeighbors()
    {
        neighborFactories = new Dictionary<string, Factory>();
        Collider[] neighbors = Physics.OverlapBox(transform.position, new Vector3(0.9f, 0.9f, 2.9f));
        neighbors = neighbors.Concat(Physics.OverlapBox(transform.position, new Vector3(0.9f, 2.9f, 0.9f))).ToArray();
        neighbors = neighbors.Concat(Physics.OverlapBox(transform.position, new Vector3(2.9f, 0.9f, 0.9f))).ToArray();
        foreach (Collider neighbor in neighbors)
        {
            Factory neighborFactory = neighbor.GetComponentInParent<Factory>();
            if (neighborFactory != null && neighborFactory.transform != transform && !neighborFactory.isDestroyed)
            {
                Vector3 relativePos = Quaternion.Inverse(transform.rotation) * (neighborFactory.transform.position - transform.position);
                if (!neighborFactories.ContainsKey(relativePos.ToString("F0")))
                {
                    neighborFactories.Add(relativePos.ToString("F0"), neighborFactory);
                }
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

    public virtual void GeneralUpdate() { }

    public virtual void PreTick() { }

    public virtual void Tick() { }

    public void PrintDict(Dictionary<string, Factory> dict)
    {
        foreach (KeyValuePair<string, Factory> pair in dict)
        {
            Debug.Log(pair.Key + pair.Value.ToString());
        }
    }
}