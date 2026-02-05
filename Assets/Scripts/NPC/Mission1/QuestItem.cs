using UnityEngine;

public class QuestItem : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // Cách 1: Sử dụng Instance (Tối ưu nhất về tốc độ)
            if (QuestNPC.Instance != null) {
                
                // (Tùy chọn) Chỉ cho phép nhặt nếu đang ở đúng giai đoạn nhiệm vụ 1
                if (QuestNPC.Instance.currentState == QuestNPC.QuestState.DoingQuest1) {
                    QuestNPC.Instance.DaNhatDuocDo(); 
                    Destroy(gameObject); 
                } else {
                    Debug.Log("Bạn chưa nhận nhiệm vụ hoặc đã qua giai đoạn này!");
                }
                
                return; // Kết thúc hàm nếu đã tìm thấy Instance
            }

            // Cách 2: Dự phòng nếu Instance vì lý do nào đó bị null (Chuẩn mới FindFirstObjectByType)
            QuestNPC npc = Object.FindFirstObjectByType<QuestNPC>(); 
            if (npc != null) {
                npc.DaNhatDuocDo(); 
                Destroy(gameObject); 
            }
        }
    }
}