using UnityEngine;

public class NPCMission2D : MonoBehaviour
{
    [Header("Đối tượng UI")]
    public GameObject pressFHint;     // Chữ "Nhấn F"
    public GameObject missionPanel;   // Bảng nội dung nhiệm vụ

    [Header("Trạng thái")]
    private bool isPlayerNearby = false;
    private bool isPanelActive = false; // Kiểm tra bảng đang bật hay tắt

    private NPCFinishMission finishScript;
    public GameObject player;
    
    

    void Start()
    {
        isPanelActive = true;
        finishScript = GetComponent<NPCFinishMission>();
        // Đảm bảo mọi thứ ẩn khi bắt đầu
        if (pressFHint != null) pressFHint.SetActive(false);
        if (missionPanel != null) missionPanel.SetActive(false);
      
        
    }

    void Update()
    {
        // Nếu người chơi đang ở trong vùng Trigger
        if (isPlayerNearby)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleMissionPanel();
            }
        }
        PlayerMeetMission();
    }
    public void PlayerMeetMission()
    {
        if(Vector3.Distance(player.transform.position, this.transform.position) < 10f && isPanelActive)
        {
            missionPanel.SetActive(true);
            Debug.Log("Ban dang co 1 nhiem vu");
        }
    }

    void ToggleMissionPanel()
    {
        // Đảo ngược trạng thái bật/tắt (Nếu đang true thì thành false và ngược lại)
        isPanelActive = !isPanelActive;

        if (missionPanel != null)
        {
            missionPanel.SetActive(isPanelActive);
        }

        // Khi bảng nhiệm vụ hiện thì ẩn chữ "Nhấn F" đi cho đỡ vướng, và ngược lại
        if (pressFHint != null)
        {
            pressFHint.SetActive(!isPanelActive);
        }

        if (isPanelActive) {
            Debug.Log("Đã mở bảng nhiệm vụ");
            // Bạn có thể gọi thêm logic Spawn quái hoặc thùng ở đây nếu muốn
        } else {
            Debug.Log("Đã đóng bảng nhiệm vụ");
        }
    }

    // --- Xử lý vùng phạm vi ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            // Chỉ hiện chữ F nếu bảng nhiệm vụ đang đóng
            if (!isPanelActive && pressFHint != null) 
                pressFHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            isPanelActive = false; // Reset trạng thái khi đi xa

            if (pressFHint != null) pressFHint.SetActive(false);
            if (missionPanel != null) missionPanel.SetActive(false);
        }
    }
}
