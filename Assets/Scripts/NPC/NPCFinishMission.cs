using UnityEngine;
using System.Collections;

public class NPCFinishMission : MonoBehaviour
{
    public GameObject goldPrefab;
    public int goldAmount = 10;
    private bool isPlayerNearby = false;
    private bool canComplete = false; 
    private bool isDone = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && canComplete && !isDone)
        {
            // Khóa ngay lập tức để không bấm F liên tục được
            isDone = true; 
            StartCoroutine(SpawnGoldRoutine());
        }
    }

    // Dùng Coroutine để vàng rơi từ từ, tránh giật lag hoặc crash
    IEnumerator SpawnGoldRoutine()
    {
        Debug.Log("Đang trả thưởng nhiệm vụ Qua sông...");

        for (int i = 0; i < goldAmount; i++)
        {
            if (goldPrefab != null)
            {
                GameObject gold = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = gold.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 force = new Vector2(Random.Range(-2f, 2f), Random.Range(3f, 6f));
                    rb.AddForce(force, ForceMode2D.Impulse);
                }
            }
            // Đợi một khoảng thời gian cực ngắn giữa mỗi đồng vàng để máy không bị sốc
            yield return new WaitForSeconds(0.05f); 
        }
        
        Debug.Log("Hoàn thành nhiệm vụ an toàn!");
    }

    public void SetReadyToComplete() { canComplete = true; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = false;
    }
}
