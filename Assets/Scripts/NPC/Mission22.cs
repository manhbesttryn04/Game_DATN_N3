using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission22 : MonoBehaviour
{
    [Header("Giao dien UI")]
    public GameObject missionPanel;   

    [Header("Vat pham thuong")]
    public GameObject rewardChest;    
    public List<GameObject> silverCoins = new List<GameObject>(); 

    [Header("Vat can duong")]
    public GameObject wall1;          
    public GameObject wall2;          

    [Header("Cai dat Quai")]
    public GameObject enemyPrefab;    
    public Transform[] spawnPoints;   

    [Header("Thong so")]
    public float autoCloseTime = 20f;

    public int currentWave = 1;      
    public int enemiesInActiveWave = 0; 
 public int totalKilled = 0;
    public bool missionTriggered = false;
    public bool rewardSpawned = false;
    public bool onSpawnWave = false;
    public bool onMission = false;

    public List<GameObject> activeEnemies = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // An cac doi tuong khi moi vao game
        if (missionPanel != null) missionPanel.SetActive(false);
        if (rewardChest != null) rewardChest.SetActive(false);
        foreach (GameObject coin in silverCoins) if (coin != null) coin.SetActive(false);
        if (wall1 != null) wall1.SetActive(false);
        if (wall2 != null) wall2.SetActive(false);
    }
    public void Update()
    {if(onMission){ EnemyKilled(); }
        
    }
    public void EnemyKilled()
    { if(rewardSpawned) return;
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].gameObject.transform.GetChild(0).gameObject.GetComponent<EnemyHealth>()._Enemy.Health.Current <= 0)
            { Destroy(activeEnemies[i]);
                activeEnemies.RemoveAt(i);
                
                enemiesInActiveWave--;
                totalKilled++;
                
            }
        }
       if(activeEnemies.Count <= 0)
        {
            onSpawnWave = true;
            OnEnemyKilled();
        }
       
    }

    public void OnEnemyKilled()
    {
        if (rewardSpawned) return;


        if (enemiesInActiveWave <= 0 && onSpawnWave)
        {
            if (currentWave == 1) { currentWave = 2; SpawnWave(3); }
            else if (currentWave == 2) { currentWave = 3; SpawnWave(4); }
            else if (currentWave == 3 && totalKilled >= 10) FinishMission();
            onSpawnWave = false;
        }
    }
    // Update is called once per frame
    void SpawnWave(int count)

    { //Riset List quai
      
        if (enemyPrefab == null || spawnPoints.Length == 0) return;
        activeEnemies.Clear();
        enemiesInActiveWave = count;
        for (int i = 0; i < count; i++)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
         GameObject enemy = Instantiate(enemyPrefab, sp.position, Quaternion.identity);
            activeEnemies.Add(enemy);

        }
    }

    void FinishMission()
    {
        rewardSpawned = true;
        if (missionPanel != null) missionPanel.SetActive(false);
        if (rewardChest != null) rewardChest.SetActive(true); 
        foreach (GameObject coin in silverCoins) if (coin != null) coin.SetActive(true); 
        if (wall1 != null) wall1.SetActive(false); 
        if (wall2 != null) wall2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !missionTriggered)
        {
            missionTriggered = true;
            if (wall1 != null) wall1.SetActive(true);
            if (wall2 != null) wall2.SetActive(true);
            if (missionPanel != null) missionPanel.SetActive(true);
            onMission = true;

            StartCoroutine(AutoClosePanel(autoCloseTime));
            SpawnWave(3); // Dot 1
        }
    }

    IEnumerator AutoClosePanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (missionPanel != null && !rewardSpawned) missionPanel.SetActive(false);
    }
}
