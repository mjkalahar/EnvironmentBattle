using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadResource : Resource
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 0;
        SetupHealthBar();
    }

    // Update is called once per frame
    public override void Update()
    {
        UpdateHealthBar();
    }
}