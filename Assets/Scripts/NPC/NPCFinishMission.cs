using UnityEngine;
using System.Collections;

public class NPCFinishMission : MonoBehaviour
{
    [Header("Giao diện UI")]
    public GameObject missionPanel;   
    public GameObject pressFHint;     
    public GameObject rewardObject; 

    private bool isPlayerNearby = false;
    public bool canComplete = false;
    private bool isDone = false;

    void Update()
    {
        if (isPlayerNearby && canComplete && !isDone)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isDone = true;
                if (rewardObject != null) rewardObject.SetActive(true); // Hiện vàng
                if (missionPanel != null) missionPanel.SetActive(false);
                if (pressFHint != null) pressFHint.SetActive(false);
            }
        }
    }
    public void SetReadyToComplete() { canComplete = true; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canComplete && !isDone)
        {
            isPlayerNearby = true;
            if (missionPanel != null) missionPanel.SetActive(true);
            if (pressFHint != null) pressFHint.SetActive(true); // Hiện chữ F để báo hiệu có quà
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (missionPanel != null) missionPanel.SetActive(false);
            if (pressFHint != null) pressFHint.SetActive(false);
        }
    }
}
