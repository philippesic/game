using System.Collections;
using System.Security.Principal;
using UnityEngine;

public class Drill : Factory
{
    public GameObject node;
    public int genid;

    public override void SetupFactory()
    {
        Collider[] others = Physics.OverlapBox(transform.position, new Vector3(0.9f, 0.9f, 0.9f));
        foreach (Collider other in others)
        {
            Debug.Log(other.gameObject.tag);
            node = other.gameObject;
            NodeID nodeID = node.GetComponent<NodeID>();
            if (nodeID != null)
            {
                genid = nodeID.id;
                StartCoroutine(Generate(1, genid, 1));
                break;
            }
        }
    }

    private IEnumerator Generate(float sec, int id, int mult)
    {
        while (true)
        {
            yield return new WaitForSeconds(sec);
            Player.instance.inv.Add(id, mult);
        }
    }
}
