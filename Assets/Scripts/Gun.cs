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

    public float xOffset = .1f;
    public float yOffset = .1f;
    public float zOffset = .5f;

    void Start()
    {
        nextTimeToFire = 0.0f;
    }

    void Update()
    {
        Player player = PlayerManager.Instance.GetPlayer();
        Vector3 gripPos = player.GetRightHandGripPostion();
        Vector3 gunPos = new Vector3(gripPos.x + xOffset, gripPos.y + yOffset, gripPos.z + zOffset);
        transform.position = gunPos;
        transform.rotation = player.transform.rotation;
        bool ready = Time.time >= nextTimeToFire;
        if(ready && Input.GetButton("Fire1") && !player.isStunned() && !player.isDead())
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
        Vector3 muzzlePosition = gameObject.transform.Find("Muzzle").transform.position;
        Ray beam = new Ray(muzzlePosition, aimPoint - muzzlePosition);
        if (Physics.Raycast(beam, out hit, range))
        {
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
}
