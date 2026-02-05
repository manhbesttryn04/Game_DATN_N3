using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission13 : MonoBehaviour
{
    [Header("Giao diện UI")]
    public GameObject missionPanel;   

    [Header("Boss Prefabs")]
    public GameObject bossWave1; 
    public GameObject bossWave2; 
    public Transform[] spawnPoints;   

    [Header("Vật cản (Tường chặn đường)")]
    public List<GameObject> walls = new List<GameObject>(); 

    [Header("Phần thưởng tự động")]
    public int goldReward = 2000;
    public float expReward = 1000f;

    [Header("Trạng thái")]
    public int currentWave = 0;      
    public int enemiesRemainingInWave = 0; 
    public bool onMission = false;
    private bool rewardGiven = false;

    void Start()
    {
        // Lúc đầu game phải ẩn tường đi để người chơi đi vào được
        SetWallsActive(false);
        if (missionPanel != null) missionPanel.SetActive(false);
    }

    // Hàm phụ để bật/tắt toàn bộ tường trong List
    private void SetWallsActive(bool isActive)
    {
        foreach (GameObject wall in walls)
        {
            if (wall != null) wall.SetActive(isActive);
        }
    }

    public void NotifyEnemyKilled()
    {
        if (!onMission || rewardGiven) return;

        enemiesRemainingInWave--;
        Debug.Log("<color=green>Boss đã chết!</color> Đang kiểm tra để ra đợt tiếp theo...");

        if (enemiesRemainingInWave <= 0)
        {
            StartCoroutine(CheckNextStep());
        }
    }

    IEnumerator CheckNextStep()
    {
        yield return new WaitForSeconds(1.5f); 

        if (currentWave == 1) 
        {
            Debug.Log("<color=yellow>Triệu hồi MiniBoss đợt 2!</color>");
            SpawnBossWave(2);
        }
        else if (currentWave == 2) 
        {
            Debug.Log("<color=cyan>Nhiệm vụ hoàn tất, đang mở đường...</color>");
            FinishMission();
        }
    }

    void SpawnBossWave(int waveNumber)
    { 
        currentWave = waveNumber;
        enemiesRemainingInWave = 1;

        GameObject bossToSpawn = (waveNumber == 1) ? bossWave1 : bossWave2;

        if (bossToSpawn != null && spawnPoints.Length > 0)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(bossToSpawn, sp.position, Quaternion.identity);
        }
    }

    void FinishMission()
    {
        onMission = false;
        rewardGiven = true;

        // Cộng thưởng tự động trực tiếp vào Player
        if (PlayerHealth.control != null && PlayerHealth.control._Player != null)
        {
            PlayerHealth.control._Player.Gold += goldReward;
            PlayerHealth.control._Player.Experience.Current += expReward;
        }

        if (missionPanel != null) missionPanel.SetActive(false);
        
        // NHIỆM VỤ XONG -> MỞ TƯỜNG
        SetWallsActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !onMission && !rewardGiven)
        {
            onMission = true;
            
            // BẮT ĐẦU NHIỆM VỤ -> ĐÓNG TƯỜNG CHẶN ĐƯỜNG
            SetWallsActive(true);

            if (missionPanel != null) missionPanel.SetActive(true);
            SpawnBossWave(1); 
        }
    }
}