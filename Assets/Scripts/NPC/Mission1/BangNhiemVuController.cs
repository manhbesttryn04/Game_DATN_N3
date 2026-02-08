using UnityEngine;
using TMPro;
using System.Collections;

public class BangNhiemVuController : MonoBehaviour {
    [Header("Cấu hình UI")]
    public TextMeshProUGUI textNoiDung;
    public bool tuDongAn = false; // Mặc định tắt tự động ẩn để người chơi dễ theo dõi NV
    public float thoiGianHien = 4f; 

    private Coroutine currentCoroutine;

    void Start() {
        // Nếu lúc vào game đã có dữ liệu nhiệm vụ cũ, nó sẽ hiện lên
        if (textNoiDung != null && string.IsNullOrEmpty(textNoiDung.text)) {
            gameObject.SetActive(false);
        }
    }

    public void CapNhatNhiemVu(string thongTin) {
        if (textNoiDung == null) return;

        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }

        textNoiDung.text = thongTin; 
        
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }

        // Chỉ chạy Coroutine ẩn nếu bạn bật tính năng tự động ẩn
        if (tuDongAn) {
            currentCoroutine = StartCoroutine(TuDongAn());
        }
    }

    // Hàm này dùng để xóa nội dung và ẩn bảng khi hoàn thành tuyệt đối
    public void AnBangNhiemVu() {
        textNoiDung.text = "";
        gameObject.SetActive(false);
    }

    IEnumerator TuDongAn() {
        yield return new WaitForSeconds(thoiGianHien);
        if (this != null && gameObject != null) {
            gameObject.SetActive(false);
        }
        currentCoroutine = null;
    }
}