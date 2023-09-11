using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int itemId;
    public int multiplier;

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Drill")) {
            Drill script = other.gameObject.GetComponent<Drill>();
            script.timeSec /= multiplier;
            script.itemsMade = itemId;
            print(script.itemsMade);    
            Debug.Log("eibvi");
        }
    }
}

//This is something? No clue how you plan to implement actual drills so just replace these assignments with whatever you need
//Node should tell drill which to generate and affect speed with multiplier variable
