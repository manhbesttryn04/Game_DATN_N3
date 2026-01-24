using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public NPCFinishMission targetNPC; // Kéo NPC trả nhiệm vụ vào đây

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Báo cho NPC biết là người chơi đã hoàn thành thử thách vượt sông
            if (targetNPC != null)
            {
                targetNPC.SetReadyToComplete();
                Debug.Log("Bạn đã sang bờ! Hãy quay về gặp NPC để nhận vàng.");
            }
        }
    }
}
