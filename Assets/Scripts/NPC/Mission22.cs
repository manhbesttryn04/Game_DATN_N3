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

    private int currentWave = 1;      
    private int enemiesInActiveWave = 0; 
    private int totalKilled = 0;
    private bool missionTriggered = false;
    private bool rewardSpawned = false;
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

    public void OnEnemyKilled()
    {
        if (rewardSpawned) return;

        enemiesInActiveWave--;
        totalKilled++;

        if (enemiesInActiveWave <= 0)
        {
            if (currentWave == 1) { currentWave = 2; SpawnWave(3); }
            else if (currentWave == 2) { currentWave = 3; SpawnWave(4); }
            else if (currentWave == 3 && totalKilled >= 10) FinishMission();
        }
    }
    // Update is called once per frame
    void SpawnWave(int count)
    {
        if (enemyPrefab == null || spawnPoints.Length == 0) return;

        enemiesInActiveWave = count;
        for (int i = 0; i < count; i++)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, sp.position, Quaternion.identity);
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
