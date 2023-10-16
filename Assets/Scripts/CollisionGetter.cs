using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGetter : MonoBehaviour
{
    void OnCollisionEnter(Collision collision){
        GetComponentInParent<WorldBlock>().OnCollisionEnter(collision);
    }
}
