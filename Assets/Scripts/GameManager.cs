using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variables
    //Still need to implement these
    public int money = 600;
    public int premiumCurrency = 0;

    //What wave the player is on
    public int waveNumber = 1;

    //How many enemies spawn per round
    public int numEnemies = 10;
    public float numEnemiesFloat = 10;

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

    public GameObject winUI;
    public GameObject loseUI;
    private bool lost = false;
    private bool currentlyWinning = false;

    public List<int> weights;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = GameObject.Find("Enemies");
        enemies = new List<GameObject>();
        premiumCurrency = 0;
        //Instantiate(baseObject);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] teki = GameObject.FindGameObjectsWithTag("Enemy");

        //Debug.Log("DEBUG");
        //Debug.Log(spawnEnemies);
        //Debug.Log(enemies);
        //Debug.Log(teki.Length);
        //Debug.Log(lost);
        if (spawnEnemies)
        {
            StartCoroutine(SpawnEnemies(timeToSpawn));
        }
       
        else if(!spawnEnemies && enemies != null && teki.Length == 0 && numEnemySpawned == numEnemies && !lost && !currentlyWinning)
        {
            Internals.gems++;
            WinWave();
        }
    }

    public void NextWave()
    {
        waveNumber++;

        numEnemiesFloat += 2;
        numEnemiesFloat = numEnemiesFloat * 1.15f;
        numEnemies = (int)numEnemiesFloat;
        numEnemySpawned = 0;

        Internals.zombieHPScaler *= 1.05f;

        if (waveNumber % 2 == 0 && timeToSpawn > .2f)
            timeToSpawn -= .2f;

        if(waveNumber == 5)
        {
            weights = new List<int>() { 13, 17, 20 };
        }
        else if (waveNumber == 10)
        {
            weights = new List<int>() { 6, 12, 20 };
        }

        spawnEnemies = true;
        currentlyWinning = false;
    }

    private IEnumerator SpawnEnemies(float timeToSpawnNext)
    {
        while(numEnemySpawned < numEnemies)
        {

            enemies.Add(Instantiate(
                enemyTypes[GetEnemyIndex()],
                new Vector3(0, 0.89f, 0),
                Quaternion.identity,
                enemyParent.transform
                ));

            numEnemySpawned++;
            spawnEnemies = false;

            yield return new WaitForSeconds(timeToSpawnNext);
        }
    }

    private int GetEnemyIndex()
    {
        int roll = Random.Range(0, 20);

        for (int i = 0; i < weights.Count; i++)
        {
            if (roll < weights[i])
                return i;
        }

        return 0;
    }

    public void WinWave()
    {
        //Enable next wave start UI
        //Maybe just a button to advance to the next wave
        //Maybe shop icon appears so the player can buy more stuff
        currentlyWinning = true;
        StartCoroutine(StartStop(winUI, 2f));
    }

    public void winBtnClicked() { }
    public void LoseWave()
    {
        //Pop up lose UI with back to main menu button
        //Display wave number they made it to
        lost = true;
        enemies.Clear();
        loseUI.SetActive(true);
        Debug.Log("Child 2 name: " + loseUI.transform.GetChild(2).name);
        string plural = waveNumber == 1 ? " Wave" : " Waves";
        loseUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "You Survived " + waveNumber + plural;
    }

    private IEnumerator StartStop(GameObject obj, float duration)
    {
        obj.SetActive(true);

        yield return new WaitForSeconds(duration);

        obj.SetActive(false);
        NextWave();
    }
}
