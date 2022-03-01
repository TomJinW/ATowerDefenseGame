using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    //Still need to implement these
    public int money = 0;
    public int premiumCurrency = 0;

    //What wave the player is on
    public int waveNumber = 1;

    //How many enemies spawn per round
    public int numEnemies = 10;

    //Should enemies be spawned - happens at the beginning of the wave
    public bool spawnEnemies = true;

    //What kind of enemies can be spawned
    public List<GameObject> enemyTypes;

    //Spawn all of the enemies under the Enemies gameObject
    private GameObject enemyParent;

    //Holds the current wave's enemies
    private List<GameObject> enemies;

    //Iterator number
    private int numEnemySpawned = 0;

    //Time to spawn enemies
    public float timeToSpawn = 1f;

    //Base
    public GameObject baseObject;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = GameObject.Find("Enemies");
        enemies = new List<GameObject>();

        Instantiate(baseObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnEnemies)
        {
            StartCoroutine(SpawnEnemies(timeToSpawn));
        }
        else if(!spawnEnemies && enemies != null && enemies.Count == 0)
        {
            spawnEnemies = true;

            NextWave();
        }
    }

    public void NextWave()
    {
        waveNumber++;

        numEnemies += 5;

        numEnemySpawned = 0;
    }

    private IEnumerator SpawnEnemies(float timeToSpawnNext)
    {
        while(numEnemySpawned < numEnemies)
        {

            enemies.Add(Instantiate(
                enemyTypes[0],
                new Vector3(-21, 0, Random.Range(-23, 3)),
                Quaternion.identity,
                enemyParent.transform
                ));

            numEnemySpawned++;
            spawnEnemies = false;

            yield return new WaitForSeconds(timeToSpawnNext);
        }
    }
}
