using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    void Awake()
    {
        Instance = this; 
    }

    public Player player;

    private Gun currentGun;
    private Gun currentGunPrefab;

    public GameObject OFFSCREEN_SPAWN_POINT;

    public Gun gun1;
    public Gun gun2;
    public Gun gun3;

    public void Start()
    {
        SwitchWeapons(gun1);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Gun GetCurrentGun()
    {
        return currentGun;
    }

    public Gun GetCurrentGunPrefab()
    {
        return currentGunPrefab;
    }

    public void Update()
    {
        if (GetCurrentGun().CanFire() && Input.GetButton("Fire1") && !GetPlayer().isStunned() && !GetPlayer().isDead())
        {
            GetCurrentGun().Shoot();
        }

        UpdateCurrentGun();

        if (Input.GetKey(KeyCode.Alpha1))
        {
            SwitchWeapons(gun1);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SwitchWeapons(gun2);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SwitchWeapons(gun3);
        }
    }

    public void SwitchWeapons(Gun newGun)
    {
        if (GetCurrentGunPrefab() != newGun)
        {
            if (GetCurrentGun() != null)
            {
                Destroy(GetCurrentGun().gameObject);
            }
            currentGun = SpawnWeapon(newGun);
            currentGunPrefab = newGun;
        }
    }

    public Gun SpawnWeapon(Gun gun)
    {
        return Instantiate(gun, OFFSCREEN_SPAWN_POINT.transform.position, Quaternion.identity);
    }

    public void UpdateCurrentGun()
    {
        Vector3 gripPos = GetPlayer().GetRightHandGripPostion();
        Vector3 gunPos = new Vector3(gripPos.x, gripPos.y, gripPos.z);
        GetCurrentGun().transform.rotation = GetPlayer().transform.rotation;
        GetCurrentGun().transform.position = gunPos;
    }

}
