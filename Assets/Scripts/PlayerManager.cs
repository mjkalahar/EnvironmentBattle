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

    public void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
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
        if (currentGun != null)
        {
            if (currentGun == newGun) return;
            Destroy(currentGun.gameObject);
        }
        currentGun = SpawnWeapon(newGun);
    }

    public Gun SpawnWeapon(Gun gun)
    {
        return Instantiate(gun, OFFSCREEN_SPAWN_POINT.transform.position, Quaternion.identity);
    }

}
