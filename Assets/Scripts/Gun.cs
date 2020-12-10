using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;

    public ParticleSystem muzzleFlash;
    private float nextTimeToFire;

    public float damage = 5.3f;
    public float timePerShot = .2f;

    void Start()
    {
        nextTimeToFire = 0.0f;
    }

    void Update()
    {
        bool ready = Time.time >= nextTimeToFire;
        if(ready && Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    void Shoot()
    {
        if(muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        Camera camera = PlayerManager.Instance.GetPlayer().GetPlayerCamera();
        //We need to find out what the player is aiming at
        Ray crosshair = new Ray(camera.transform.position, camera.transform.forward);
        Vector3 aimPoint;
        RaycastHit hit;
        Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.green, 2, false);
        Debug.DrawLine(camera.transform.position, camera.transform.forward * range, Color.blue, 2, false);
        if (Physics.Raycast(crosshair, out hit, range))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = crosshair.origin + crosshair.direction * range;
        }

        Debug.DrawLine(camera.transform.position, aimPoint, Color.yellow, 2, false);



        Vector3 muzzlePosition = gameObject.transform.Find("Muzzle").transform.position;

        // Now we know what to aim at, fire from the player
        Ray beam = new Ray(muzzlePosition, aimPoint - muzzlePosition);
        Debug.DrawRay(muzzlePosition, aimPoint - muzzlePosition, Color.red, 2, false);
        // If we don't hit anything, just go straight to the aim point.
        if (!Physics.Raycast(beam, out hit, range))
            Debug.Log("Aim point: " + aimPoint);
        else
            // Otherwise, stop at whatever we hit on the way.
            Debug.Log("Hit this: " + hit.transform + " before aim point") ;

        if (hit.transform)
        {
            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if (target != null)
            {
                target.Process(hit);
                nextTimeToFire = Time.time + timePerShot;
            }

            
        }
    }
}
