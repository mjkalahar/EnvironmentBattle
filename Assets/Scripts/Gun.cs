using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;

    public ParticleSystem muzzleFlash;
    private float nextTimeToFire;
    private float lastFire;

    public float damage = 5.3f;
    public float timePerShot = .2f;


    void Start()
    {
        nextTimeToFire = 0.0f;
        lastFire = 0.0f;
    }

    public float GetDamage()
    {
        return damage;
    }

    public bool CanFire()
    {
        return Time.time >= nextTimeToFire;
    }

    public float GetCooldownTime()
    {
        float howLong = Time.time - lastFire;
        if(howLong > timePerShot)
        {
            howLong = timePerShot;
        }
        return (howLong / timePerShot) * 100;
    }

    public void Shoot()
    {
        if(muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        //We need to find out what the player is aiming at
        Camera camera = PlayerManager.Instance.GetPlayer().GetPlayerCamera();      
        Ray crosshair = new Ray(camera.transform.position, camera.transform.forward);
        Vector3 aimPoint;
        RaycastHit hit;

        if (Physics.Raycast(crosshair, out hit, range))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = crosshair.origin + crosshair.direction * range;
        }


        // Now we know what to aim at, fire from the muzzle
        Vector3 muzzlePosition = gameObject.transform.Find("Gun/Muzzle").transform.position;
        Ray beam = new Ray(muzzlePosition, aimPoint - muzzlePosition);
        if (Physics.Raycast(beam, out hit, range))
        {
            if (hit.transform)
            {
                EnemyController target = hit.transform.GetComponent<EnemyController>();
                if (target != null)
                {
                    target.Process(hit);
                    lastFire = Time.time;
                    nextTimeToFire = Time.time + timePerShot;
                }
            }
        }
    }
}
