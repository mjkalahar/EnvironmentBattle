using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Target
{
    public override void Process(RaycastHit hit)
    {
        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
        Destroy(target);
    }
}
