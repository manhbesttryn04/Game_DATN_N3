using UnityEngine;
using TMPro;
using System.Collections;

public class BangNhiemVuController : MonoBehaviour {
    [Header("Cấu hình UI")]
    public TextMeshProUGUI textNoiDung;
    public float thoiGianHien = 4f; 

    private Coroutine currentCoroutine;

    /// <summary>
    /// Gọi hàm này để thay đổi nội dung nhiệm vụ trên màn hình
    /// </summary>
    public void CapNhatNhiemVu(string thongTin) {
        if (textNoiDung == null) {
            Debug.LogWarning("Chưa gán TextMeshProUGUI vào BangNhiemVuController!");
            return;
        }

        // Dừng Coroutine đang chạy để reset lại thời gian đếm ngược
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }

        textNoiDung.text = thongTin; 
        
        // Đảm bảo object đang bật để Coroutine có thể chạy
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }

        currentCoroutine = StartCoroutine(TuDongAn());
    }

    IEnumerator TuDongAn() {
        yield return new WaitForSeconds(thoiGianHien);
        
        // Kiểm tra null một lần nữa trước khi ẩn để tránh lỗi nếu object bị hủy bất ngờ
        if (this != null && gameObject != null) {
            gameObject.SetActive(false);
        }
        
        currentCoroutine = null;
    }
}