using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNPC : MonoBehaviour {
    public static QuestNPC Instance;

    public enum QuestState { None, DoingQuest1, DoingQuest2, AllDone }
    
    [Header("Trạng thái")]
    public QuestState currentState = QuestState.None;
    private bool daNhatDo = false;
    public int quaiCanDiet = 20; // Số lượng quái cần diệt
    public int quaiDaDiet = 0;

    [Header("Kết nối UI")]
    public BangNhiemVuController scriptBangNV; 
    public GameObject thongBaoPanel;  
    public TextMeshProUGUI loiThoaiText;
    public GameObject goiYVungInteraction; 

    private bool playerInRange = false;

    void Awake() {
        // Singleton pattern đơn giản
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject); // Đảm bảo không có 2 NPC trùng nhau nếu load lại scene
        }
    }

    void Start() {
        LoadProgress();
        // Kiểm tra null an toàn khi bắt đầu game
        if (goiYVungInteraction != null) goiYVungInteraction.SetActive(false);
        if (thongBaoPanel != null) thongBaoPanel.SetActive(false);
    }

    void Update() {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) {
            Interaction();
        }
    }

    public void Interaction() {
        // [FIX] Kiểm tra null trước khi bật/tắt UI để tránh lỗi Reference
        if (thongBaoPanel != null) thongBaoPanel.SetActive(true);
        if (goiYVungInteraction != null) goiYVungInteraction.SetActive(false);

        switch (currentState) {
            case QuestState.None:
                if (loiThoaiText != null) loiThoaiText.text = "Làm ơn tìm giúp tôi chiêc hộp bị mất ở bìa sa mạc!";
                currentState = QuestState.DoingQuest1;
                
                if (scriptBangNV != null) scriptBangNV.CapNhatNhiemVu("NV: Tìm chiếc hộp cũ bị mất.");
                SaveProgress();
                break;

            case QuestState.DoingQuest1:
                if (daNhatDo) {
                    if (loiThoaiText != null) loiThoaiText.text = "Cảm ơn! Giờ hãy diệt " + quaiCanDiet + " con quái giúp tôi nhé.";
                    
                    // [FIX] Kiểm tra PlayerHealth an toàn
                    if (PlayerHealth.control != null && PlayerHealth.control._Player != null) {
                        PlayerHealth.control._Player.Gold += 100; 
                        PlayerHealth.control._Player.Experience.Current += 100f;
                    }

                    currentState = QuestState.DoingQuest2;
                    // Cập nhật text động theo biến quaiCanDiet
                    if (scriptBangNV != null) scriptBangNV.CapNhatNhiemVu("NV: Tiêu diệt quái vật (0/" + quaiCanDiet + ")");
                    SaveProgress();
                } else {
                    if (loiThoaiText != null) loiThoaiText.text = "Bạn vẫn chưa tìm thấy hộp của tôi...";
                }
                break;

            case QuestState.DoingQuest2:
                if (quaiDaDiet >= quaiCanDiet) {
                    if (loiThoaiText != null) loiThoaiText.text = "Tuyệt vời! Bạn là vị cứu tinh của tôi.";
                    
                    if (PlayerHealth.control != null && PlayerHealth.control._Player != null) {
                        PlayerHealth.control._Player.Gold += 500; 
                        PlayerHealth.control._Player.Experience.Current += 200f;
                    }

                    currentState = QuestState.AllDone;
                    if (scriptBangNV != null) scriptBangNV.CapNhatNhiemVu("HOÀN THÀNH NHIỆM VỤ!");
                    SaveProgress();
                } else {
                    if (loiThoaiText != null) loiThoaiText.text = "Cố lên, còn " + (quaiCanDiet - quaiDaDiet) + " con quái nữa.";
                }
                break;

            case QuestState.AllDone:
                if (loiThoaiText != null) loiThoaiText.text = "Chào người anh hùng!";
                break;
        }
    }

    public void DaNhatDuocDo() {
        daNhatDo = true;
        if (scriptBangNV != null) scriptBangNV.CapNhatNhiemVu("Đã có hộp! Quay lại gặp NPC.");
        SaveProgress();
    }

    public void OnEnemyKilled() {
        if (currentState == QuestState.DoingQuest2 && quaiDaDiet < quaiCanDiet) {
            quaiDaDiet++;
            if (scriptBangNV != null) scriptBangNV.CapNhatNhiemVu("NV: Tiêu diệt quái vật (" + quaiDaDiet + "/" + quaiCanDiet + ")");
            SaveProgress();
        }
    }

    // --- Lưu & Tải (PlayerPrefs) ---
    void SaveProgress() {
        PlayerPrefs.SetInt("QS", (int)currentState);
        PlayerPrefs.SetInt("QK", quaiDaDiet);
        PlayerPrefs.SetInt("QI", daNhatDo ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadProgress() {
        // Mặc định là QuestState.None (0) nếu chưa có dữ liệu
        currentState = (QuestState)PlayerPrefs.GetInt("QS", 0);
        quaiDaDiet = PlayerPrefs.GetInt("QK", 0);
        daNhatDo = PlayerPrefs.GetInt("QI", 0) == 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            // [FIX] Kiểm tra null trước khi bật gợi ý
            if (currentState != QuestState.AllDone && goiYVungInteraction != null) {
                goiYVungInteraction.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            
            // [FIX QUAN TRỌNG] Kiểm tra null trước khi tắt UI
            // Đây là chỗ sửa lỗi MissingReferenceException
            if (goiYVungInteraction != null) {
                goiYVungInteraction.SetActive(false);
            }
            
            if (thongBaoPanel != null) {
                thongBaoPanel.SetActive(false);
            }
        }
    }
}