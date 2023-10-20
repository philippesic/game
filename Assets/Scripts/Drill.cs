using System.Collections;
using UnityEngine;

public class Drill : Factory
{
    private int genid;
    void Awake()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NodeID>() != null)
        {
            genid = other.gameObject.GetComponent<NodeID>().id;
            StartCoroutine(Generate(1, genid, 1));
        }
    }

    private IEnumerator Generate(float sec, int id, int mult)
    {
        yield return new WaitForSeconds(sec);
        Player.instance.inv.Add(id, mult);
    }
}
