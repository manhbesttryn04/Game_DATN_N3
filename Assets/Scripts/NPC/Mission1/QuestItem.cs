using UnityEngine;

public class QuestItem : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // Ưu tiên sử dụng Instance để báo cáo nhiệm vụ
            if (QuestNPC.Instance != null) {
                
                // KIỂM TRA: Chỉ cho nhặt nếu NPC đang ở trạng thái chờ Giai đoạn 1 (Tìm đồ/Vượt địa hình)
                if (QuestNPC.Instance.currentState == QuestNPC.QuestState.DoingQuest1) {
                    
                    // Gọi hàm xác nhận nhặt đồ trong QuestNPC
                    QuestNPC.Instance.DaNhatDuocDo(); 
                    
                    // Xóa vật phẩm khỏi bản đồ
                    Destroy(gameObject); 
                    Debug.Log("<color=green>Đã nhặt vật phẩm nhiệm vụ thành công!</color>");
                } 
                else if (QuestNPC.Instance.currentState == QuestNPC.QuestState.None) {
                    Debug.Log("Bạn cần nói chuyện với NPC để nhận nhiệm vụ trước khi nhặt món đồ này!");
                }
                else {
                    Debug.Log("Nhiệm vụ này đã qua giai đoạn tìm đồ.");
                }
                
                return; // Đã xử lý xong, thoát hàm
            }

            // DỰ PHÒNG: Nếu Instance bị null (hiếm khi xảy ra nếu đã đặt NPC trong Map)
            QuestNPC npc = Object.FindFirstObjectByType<QuestNPC>(); 
            if (npc != null && npc.currentState == QuestNPC.QuestState.DoingQuest1) {
                npc.DaNhatDuocDo(); 
                Destroy(gameObject); 
            }
        }
    }
}