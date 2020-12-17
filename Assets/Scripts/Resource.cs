using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Character
{
    public DeadResource deadResource;

    // Start is called before the first frame update
    void Start()
    {
        SetupHealthBar();
    }

    // Update is called once per frame
    public override void Update()
    {
        UpdateHealthBar();
    }

    public override void Die()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Destroy(gameObject);
        Instantiate(deadResource, pos, rot);
    }
}
