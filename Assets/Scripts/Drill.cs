using System.Collections;
using UnityEngine;

public class Drill : Factory
{
    void Update() {
        Generate(speed, itemId, multiplier);
    }

    private IEnumerator Generate(float sec, int id, int mult) {
        yield return new WaitForSeconds(sec);
        Player.instance.inv.Add(id, mult);
    }
}
