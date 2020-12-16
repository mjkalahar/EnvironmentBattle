
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : Target
{
    bool rotationOn = false;
    float rotationAmount = 360f;
    float currentRotationAmount = 0f;

    Vector3 rotation;

    float time;
    // Update is called once per frame
    void Update()
    {
        if (rotationOn)
        {
            if(currentRotationAmount < rotationAmount)
            {
                time = Time.deltaTime;
                target.transform.Rotate(rotation * time);
                currentRotationAmount += rotationAmount * time;
                
            }
            else
            {
                rotationOn = false;
                target.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void Rotate(float degrees)
    {
        if (!rotationOn)
        {
            Debug.Log("Initializing rotation for: " + target.name);
            rotationOn = true;
            rotationAmount = degrees;
            currentRotationAmount = 0f;
            rotation = new Vector3(0, degrees, 0);
        }
    }

    public override void Process (RaycastHit hit)
    {
        Debug.Log("In Process, hit: " + gameObject.tag);
        if(gameObject.tag == "Target")
        {
            Rotate(rotationAmount);
        }

        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
    }
}
