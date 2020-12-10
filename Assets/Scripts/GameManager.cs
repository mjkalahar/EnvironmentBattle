using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int round;
    float currentTime;

    float pauseTimer;

    bool running = false;
    bool roundPause = false;

    public int timeBetweenWaves = 30;
    public Resource resourcePrefab1;
    public Resource resourcePrefab2;
    public Resource resourcePrefab3;
    public Resource resourcePrefab4;
    public EnemyController enemyOnePrefab;

    public HUD hud;

    private int resourceCount = 10;

    public Vector3 PLAYER_SPAWN_POS = new Vector3(32, 0, 32);
    public Vector3 RESOURCE_CENTER_SPAWN_POS = new Vector3(32, 0, 32);
    public Vector3 ENEMY_ONE_SPAWN_POS = new Vector3(-27, 1, -26);

    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        //Pre game - round 0
        //Will increment to be round 1 as soon as game starts first wave
        round = 0;
        running = true;
        PlayerManager.Instance.GetPlayer().transform.position = PLAYER_SPAWN_POS;
        PlayerManager.Instance.GetPlayer().Respawn();
        SpawnResources();
        StartWave();
    }

    public static HUD GetHUD()
    {
        return Instance.hud;
    }

    void SpawnResources()
    {
        Vector3 center = RESOURCE_CENTER_SPAWN_POS;
        int numResources = resourceCount;

        for (int i = 0; i < numResources; i++)
        {
            Vector3 pos = RandomCircle(center, 10.0f, (float)360.0 / numResources * i);
            //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Quaternion rot = new Quaternion();
            rot.SetLookRotation(center - pos, Vector3.up);

            if (i % 4 == 0)
            {
                Instantiate(resourcePrefab4, pos, rot);
            }
            else if(i % 3 == 0)
            {
                Instantiate(resourcePrefab3, pos, rot);
            }
            else if(i % 2 == 0)
            {
                Instantiate(resourcePrefab2, pos, rot);
            }
            else
            {
                Instantiate(resourcePrefab1, pos, rot);
            }
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, float angle)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return pos;
    }

    void EndGame()
    {
        running = false;
        StunAllCharacters();
    }

    public void StartWave()
    {
        roundPause = false;
        UnstunPlayerAndResources();
        round++;

        for (int i = 0; i < round; i++)
        {
            Instantiate(enemyOnePrefab, ENEMY_ONE_SPAWN_POS, Quaternion.identity);
        }
    }

    public void EndWave()
    {
        roundPause = true;
        pauseTimer = 0;
        WipeDeadEnemies();
        StunAllCharacters();
    }

    public List<Character> GetAllCharacters()
    {
        Character[] gameObjects = FindObjectsOfType(typeof(Character)) as Character[];
        List<Character> characters = new List<Character>();
        foreach (Character character in gameObjects)
        {
            characters.Add(character);
        }

        return characters;
    }

    public void StunAllCharacters()
    {
        List<Character> characters = GetAllCharacters();
        foreach(Character character in characters)
        {
            character.Stun();
        }
    }

    public void UnstunPlayerAndResources()
    {
        List<Character> characters = GetAllCharacters();
        foreach (Character character in characters)
        {
            if(character.GetType() == typeof(Player))
            {
                character.Unstun();
            }
            else if(character.GetType() == typeof(Resource))
            {
                character.Unstun();
            }
        }
    }

    public Resource GetRandomAliveResource()
    {
        Resource[] gameObjects = FindObjectsOfType(typeof(Resource)) as Resource[];
        List<Resource> livingResources = new List<Resource>();

        foreach (Resource resource in gameObjects)
        {
            if (!resource.isDead())
            {
                livingResources.Add(resource);
            }
        }

        var ran = Random.Range(0, livingResources.Count);
        return livingResources.ToArray()[ran];
    }

    public List<Resource> GetAliveResources()
    {
        Resource[] gameObjects = FindObjectsOfType(typeof(Resource)) as Resource[];
        List<Resource> livingResources = new List<Resource>();

        foreach (Resource resource in gameObjects)
        {
            if(!resource.isDead())
            {
                livingResources.Add(resource);
            }
        }

        return livingResources;
    }

    public List<Resource> GetResources()
    {
        Resource[] gameObjects = FindObjectsOfType(typeof(Resource)) as Resource[];
        List<Resource> resources = new List<Resource>();
        foreach (Resource resource in gameObjects)
        {
            resources.Add(resource);
        }

        return resources;
    }

    public List<EnemyController> GetAliveEnemies()
    {
        EnemyController[] gameObjects = FindObjectsOfType(typeof(EnemyController)) as EnemyController[];
        List<EnemyController> enemies = new List<EnemyController>();

        foreach(EnemyController enemy in gameObjects)
        {
            if(!enemy.isDead())
            {
                enemies.Add(enemy);
            }
        }

        return enemies;
    }

    public void WipeDeadEnemies()
    {
        EnemyController[] gameObjects = FindObjectsOfType(typeof(EnemyController)) as EnemyController[];
        foreach(EnemyController enemy in gameObjects)
        {
            if(enemy.isDead())
            {
                Destroy(enemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            currentTime += Time.deltaTime;

            if (roundPause)
            {
                Debug.Log("Time left: " + (int)(timeBetweenWaves - pauseTimer));
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= timeBetweenWaves)
                {
                    StartWave();
                }
            }
            else
            {
                if (GetAliveResources().Count == 0)
                {
                    Debug.Log("All resources dead");
                    EndGame();
                }

                if (GetAliveEnemies().Count == 0)
                {
                    Debug.Log("All enemies dead");
                    EndWave();
                }

                if (PlayerManager.Instance.GetPlayer().isDead())
                {
                    Debug.Log("Player Died");
                    EndGame();
                }

                //ConvertDeadTreesToStumps();
            }
        }
    }
}
