using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int round;
    float currentTime;

    float pauseTimer;
    float warmupTimer;

    bool running = false;
    bool roundPause = false;

    public int timeBetweenWaves = 30;
    public int warmupTime = 10;

    public Resource resourcePrefab1;
    public Resource resourcePrefab2;
    public Resource resourcePrefab3;
    public Resource resourcePrefab4;
    public EnemyController enemyOnePrefab;
    public EnemyController enemyTwoPrefab;
    public EnemyController enemyThreePrefab;
    public EnemyController enemyOneBossPrefab;
    public EnemyController enemyTwoBossPrefab;
    public EnemyController enemyThreeBossPrefab;

    public HUD hud;

    private int resourceCount = 10;

    public GameObject PLAYER_SPAWN_POS;
    public GameObject RESOURCE_CENTER_SPAWN_POS;
    public GameObject SPAWN_POINT1;
    public GameObject SPAWN_POINT2;

    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StunAllCharacters();
        roundPause = true;
        round = 0;
        running = true;
        PlayerManager.Instance.GetPlayer().transform.position = PLAYER_SPAWN_POS.transform.position;
        PlayerManager.Instance.GetPlayer().Respawn();
        GetHUD().ShowStarting();
        SpawnResources();
    }

    void StartGame()
    {
        //Pre game - round 0
        //Will increment to be round 1 as soon as game starts first wave
        round = 0;
        running = true;
        PlayerManager.Instance.GetPlayer().transform.position = PLAYER_SPAWN_POS.transform.position;
        PlayerManager.Instance.GetPlayer().Respawn();
        GetHUD().HideRoundCompleted();
        GetHUD().HideGameOver();
        GetHUD().HideNextWave();
        StartWave();
    }

    public static HUD GetHUD()
    {
        return Instance.hud;
    }

    void SpawnResources()
    {
        Vector3 center = RESOURCE_CENTER_SPAWN_POS.transform.position;
        int numResources = resourceCount;

        for (int i = 0; i < numResources; i++)
        {
            Vector3 pos = RandomCircle(center, 15.0f, (float)360.0 / numResources * i);
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
        GetHUD().ShowGameOver();
        GetHUD().UpdateRoundsComplete(round-1);
        GetHUD().ShowRoundsComplete();
        StunAllCharacters();
    }

    public void StartWave()
    {
        roundPause = false;
        UnstunPlayerAndResources();
        round++;
        GetHUD().HideNextWave();
        GetHUD().HideRoundCompleted();
        GetHUD().HideStarting();
        SpawnCurrentWave();
    }

    public void SpawnCurrentWave()
    {
        float score = round;

        while (score > 0)
        {
            if (score >= enemyThreeBossPrefab.GetDifficultyScore())
            {
                SpawnEnemy(enemyThreeBossPrefab);
                score -= enemyThreeBossPrefab.GetDifficultyScore();
            }
            else if (score >= enemyTwoBossPrefab.GetDifficultyScore())
            {
                SpawnEnemy(enemyTwoBossPrefab);
                score -= enemyTwoBossPrefab.GetDifficultyScore();
            }
            else if (score >= enemyOneBossPrefab.GetDifficultyScore())
            {
                SpawnEnemy(enemyOneBossPrefab);
                score -= enemyOneBossPrefab.GetDifficultyScore();
            }
            else if (score >= enemyThreePrefab.GetDifficultyScore())
            {
                SpawnEnemy(enemyThreePrefab);
                score -= enemyThreePrefab.GetDifficultyScore();
            }
            else if (score >= enemyTwoPrefab.GetDifficultyScore())
            {
                SpawnEnemy(enemyTwoPrefab);
                score -= enemyTwoPrefab.GetDifficultyScore();
            }
            else if (score >= enemyOnePrefab.GetDifficultyScore())
            {
                SpawnEnemy(enemyOnePrefab);
                score -= enemyOnePrefab.GetDifficultyScore();
            }
        }
    }

    public void SpawnEnemy(EnemyController enemy)
    {
        Instantiate(enemy, RandomEnemyAreaSpawn(), Quaternion.identity);
    }

    public Vector3 RandomEnemyAreaSpawn()
    {
        Vector3 spawn1 = SPAWN_POINT1.transform.position;
        Vector3 spawn2 = SPAWN_POINT2.transform.position;


        Vector3 spawn = new Vector3(Random.Range(spawn1.x, spawn2.x), 0, Random.Range(spawn1.z, spawn2.z));
        return spawn;
    }

    public void EndWave()
    {
        GameManager.GetHUD().ShowRoundCompleted();
        GameManager.GetHUD().ShowNextWave();
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

        if (livingResources.Count > 0)
        {
            var ran = Random.Range(0, livingResources.Count);
            return livingResources.ToArray()[ran];
        }
        else
        {
            return null;
        }
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
            float currentWait = 0;
            currentTime += Time.deltaTime;
            
            if (roundPause)
            {
                if (round > 0)
                {
                    pauseTimer += Time.deltaTime;
                    currentWait = timeBetweenWaves - pauseTimer;
                    GetHUD().UpdateNextWave(timeBetweenWaves - pauseTimer);
                    if (pauseTimer >= timeBetweenWaves)
                    {
                        StartWave();
                    }
                }
                else
                {
                    warmupTimer += Time.deltaTime;
                    currentWait = warmupTime - warmupTimer;
                    GetHUD().UpdateStarting(warmupTime - warmupTimer);
                    if (warmupTimer >= warmupTime)
                    {
                        StartGame();
                    }
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
            }

            GetHUD().UpdateCountdown(currentWait);
        }
    }
}
