using System.Collections;
using System.Security.Principal;
using UnityEngine;

public class Drill : Factory
{
    public GameObject node;
    public int genid;

    void OnTriggerEnter(Collider other)
    {
        node = other.gameObject;
        NodeID nodeID = node.GetComponent<NodeID>();
        if (nodeID != null)
        {
            genid = nodeID.id;
            StartCoroutine(Generate(1, genid, 1));
        }
    }

    private IEnumerator Generate(float sec, int id, int mult)
    {
        yield return new WaitForSeconds(sec);
        Player.instance.inv.Add(id, mult);
    }
}
