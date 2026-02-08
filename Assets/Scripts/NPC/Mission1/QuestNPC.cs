using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNPC : MonoBehaviour {
    public static QuestNPC Instance;

    public enum QuestState { None, DoingQuest1, DoingQuest2, AllDone }

    [Tooltip("ID duy nhất để lưu dữ liệu riêng cho từng Map")]
    public string questID = "Level_1_4"; 

    public bool canTimDo = true;
    public bool canVuotDiaHinh = true;
    public bool canDietQuai = true;
    public int soQuaiCanDiet = 5;

    public string tieuDeGiaiDoan1 = "NV: Khám phá khu vực";
    public string tieuDeGiaiDoan2 = "NV: Tiêu diệt mối nguy hại";
    [TextArea] public string thoai_LucDau = "Vùng này nguy hiểm, hãy giúp tôi tìm đồ và khảo sát địa hình!";
    [TextArea] public string thoai_ChuyenGiaiDoan = "Cảm ơn! Giờ hãy hạ gục lũ quái vật đang bao vây.";
    [TextArea] public string thoai_HoanThanhAll = "Bạn thực sự là một anh hùng!";

    public QuestState currentState = QuestState.None;
    public bool daNhatDo = false;
    public bool daVuotDiaHinh = false; 
    public int quaiDaDiet = 0;
    public BangNhiemVuController scriptBangNV; 
    public GameObject thongBaoPanel;  
    public TextMeshProUGUI loiThoaiText;
    public GameObject goiYVungInteraction; 

    private bool playerInRange = false;

    void Awake() {
        Instance = this;
    }

    void Start() {
        LoadProgress();
        if (goiYVungInteraction != null) goiYVungInteraction.SetActive(false);
        if (thongBaoPanel != null) thongBaoPanel.SetActive(false);
        UpdateMissionBoard();
    }

    void Update() {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) {
            Interaction();
        }
    }

    public void Interaction() {
        if (thongBaoPanel != null) thongBaoPanel.SetActive(true);
        if (goiYVungInteraction != null) goiYVungInteraction.SetActive(false);

        switch (currentState) {
            case QuestState.None:
                Noi(thoai_LucDau);
                currentState = QuestState.DoingQuest1;
                KiemTraChuyenGiaiDoan(); 
                break;

            case QuestState.DoingQuest1:
                if (KiemTraDieuKienQ1()) {
                    Noi(thoai_ChuyenGiaiDoan);
                    if (canDietQuai) currentState = QuestState.DoingQuest2;
                    else currentState = QuestState.AllDone;
                } else {
                    Noi("Bạn chưa hoàn thành các yêu cầu tìm đồ hoặc địa hình.");
                }
                break;

            case QuestState.DoingQuest2:
                if (quaiDaDiet >= soQuaiCanDiet) {
                    Noi(thoai_HoanThanhAll);
                    currentState = QuestState.AllDone;
                } else {
                    Noi($"Hãy hạ thêm {soQuaiCanDiet - quaiDaDiet} con quái nữa!");
                }
                break;

            case QuestState.AllDone:
                Noi("Cảm ơn vì sự giúp đỡ của bạn!");
                break;
        }
        SaveProgress();
        UpdateMissionBoard();
    }

    bool KiemTraDieuKienQ1() {
        bool checkItem = !canTimDo || daNhatDo;
        bool checkTerrain = !canVuotDiaHinh || daVuotDiaHinh;
        return checkItem && checkTerrain;
    }

    void KiemTraChuyenGiaiDoan() {
        if (KiemTraDieuKienQ1() && currentState == QuestState.DoingQuest1) {
            if (canDietQuai) currentState = QuestState.DoingQuest2;
            else currentState = QuestState.AllDone;
        }
    }

    public void DaNhatDuocDo() {
        daNhatDo = true;
        UpdateMissionBoard();
        SaveProgress();
    }

    public void XacNhanVuotDiaHinh() {
        daVuotDiaHinh = true;
        UpdateMissionBoard();
        SaveProgress();
    }

    public void OnEnemyKilled() {
        if (currentState == QuestState.DoingQuest2 && quaiDaDiet < soQuaiCanDiet) {
            quaiDaDiet++;
            UpdateMissionBoard();
            SaveProgress();
        }
    }

    void UpdateMissionBoard() {
        if (scriptBangNV == null || currentState == QuestState.None) return;

        if (currentState == QuestState.AllDone) {
            scriptBangNV.CapNhatNhiemVu("<color=yellow>HOÀN THÀNH!</color>");
            return;
        }

        string noiDung = "";
        if (currentState == QuestState.DoingQuest1) {
            noiDung = $"<b>{tieuDeGiaiDoan1}</b>";
            if (canTimDo) noiDung += daNhatDo ? "\n- Vật phẩm: <color=green>Đã xong</color>" : "\n- Vật phẩm: Đang tìm...";
            if (canVuotDiaHinh) noiDung += daVuotDiaHinh ? "\n- Địa hình: <color=green>Đã xong</color>" : "\n- Địa hình: Khám phá...";
        } 
        else if (currentState == QuestState.DoingQuest2) {
            noiDung = $"<b>{tieuDeGiaiDoan2}</b>\nTiến độ: {quaiDaDiet}/{soQuaiCanDiet}";
        }

        scriptBangNV.CapNhatNhiemVu(noiDung);
    }

    void Noi(string t) { if (loiThoaiText != null) loiThoaiText.text = t; }

    void SaveProgress() {
        PlayerPrefs.SetInt(questID + "_State", (int)currentState);
        PlayerPrefs.SetInt(questID + "_Kills", quaiDaDiet);
        PlayerPrefs.SetInt(questID + "_Item", daNhatDo ? 1 : 0);
        PlayerPrefs.SetInt(questID + "_Terrain", daVuotDiaHinh ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadProgress() {
        currentState = (QuestState)PlayerPrefs.GetInt(questID + "_State", 0);
        quaiDaDiet = PlayerPrefs.GetInt(questID + "_Kills", 0);
        daNhatDo = PlayerPrefs.GetInt(questID + "_Item", 0) == 1;
        daVuotDiaHinh = PlayerPrefs.GetInt(questID + "_Terrain", 0) == 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            if (goiYVungInteraction != null) goiYVungInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            if (goiYVungInteraction != null) goiYVungInteraction.SetActive(false);
            if (thongBaoPanel != null) thongBaoPanel.SetActive(false);
        }
    }
}