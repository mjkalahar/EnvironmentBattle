using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTree : MonoBehaviour
{
    public float health = 100;
    // Start is called before the first frame update
    public void TakeDamage()
    {
        health = health - 10;
        if( health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
