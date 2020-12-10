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
    public Gun gun;

    public Player GetPlayer()
    {
        return player;
    }

    public Gun GetCurrentGun()
    {
        return gun;
    }
}
