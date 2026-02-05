using UnityEngine;

public class EatMoneyController : MonoBehaviour {

    [Header("Cấu hình")]
    public SoundManager sound;
    public int soTienCong = 100;

    void Start() {
        // Tự động tìm SoundManager nếu chưa kéo vào Inspector
        if (sound == null) {
            GameObject soundObj = GameObject.FindGameObjectWithTag("sound");
            if (soundObj != null) sound = soundObj.GetComponent<SoundManager>();
        }
    }

    // Quan trọng: Sử dụng OnTriggerEnter2D để "đi xuyên qua" vật phẩm
    private void OnTriggerEnter2D(Collider2D money) {
        if (money.CompareTag("Money")) {
            
            // 1. Vô hiệu hóa Collider của đồng tiền ngay lập tức
            money.enabled = false;

            // 2. Cộng tiền (Kiểm tra null để tránh lỗi)
            if (PlayerHealth.control != null && PlayerHealth.control._Player != null) {
                PlayerHealth.control._Player.Gold += soTienCong;
            }

            // 3. Phát âm thanh
            if (sound != null) sound.Playsound("Vang");

            // 4. Hiệu ứng bay và xóa
            ThucHienHieuUngBay(money.gameObject);
        }
    }

    private void ThucHienHieuUngBay(GameObject obj) {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        
        // Nếu vật phẩm chưa có Rigidbody thì thêm vào để xử lý lực
        if (rb == null) {
            rb = obj.AddComponent<Rigidbody2D>();
        }

        // Cập nhật chuẩn Unity mới nhất: sử dụng linearVelocity thay cho velocity
        rb.gravityScale = 0; 
        rb.linearVelocity = Vector2.zero; // Reset vận tốc về 0
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse); // Đẩy nhẹ lên trên

        // Xóa đồng tiền sau 0.5 giây
        Destroy(obj, 0.5f);
    }
}