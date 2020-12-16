using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float range = 100f;

    public ParticleSystem muzzleFlash;
    private Camera fpsCamera;
    private float nextTimeToFire;

    void Start()
    {
        fpsCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        nextTimeToFire = 0.0f;
    }

    void Update()
    {
        bool ready = Time.time >= nextTimeToFire;
        if (ready && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null
                
                 
                )
            {
                target.Process(hit);
            }
        }

        nextTimeToFire = Time.time + 0.33f;
    }
}
