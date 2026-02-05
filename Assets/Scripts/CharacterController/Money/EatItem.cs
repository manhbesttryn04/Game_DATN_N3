using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatItem : MonoBehaviour {

    [Header("Hiệu ứng Buff Sát thương")]
    public GameObject HieuUngNhanSatThuong; // Kéo Aura quanh người vào đây
    public CoolDownController AnimationCoolDown; // Kéo UI Icon Buff vào đây
    
    private bool NhanST = false;
    private float timeLive = 0f;

    private SoundManager sound;
    private VatPham vpMau;
    private VatPham vpSatThuong;

    void Start () {
        // Tìm SoundManager an toàn
        GameObject soundObj = GameObject.FindGameObjectWithTag("sound");
        if (soundObj != null) sound = soundObj.GetComponent<SoundManager>();
        
        if (AnimationCoolDown != null) AnimationCoolDown.SetDisable();
    }

    private void Update()
    {
        LogicHieuUngBuff();
    }

    // --- XỬ LÝ VA CHẠM (DÙNG TRIGGER ĐỂ KHÔNG BỊ ĐẨY) ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Ăn Vàng (Tag Money - Cộng 100đ cố định)
        if (other.CompareTag("Money"))
        {
            other.enabled = false; // Tắt va chạm ngay
            if (PlayerHealth.control != null) PlayerHealth.control._Player.Gold += 100;
            if (sound != null) sound.Playsound("Vang");
            
            ThucHienHieuUngBay(other.gameObject);
        }

        // 2. Ăn Vật phẩm từ quái (Tag Item - Máu, Dame, Kim cương)
        else if (other.CompareTag("Item"))
        {
            Item it = other.GetComponent<Item>();
            if (it == null) return;

            VatPham duLieu = VatPhamController.GetItemByID(it.ID);
            if (duLieu == null) return;

            other.enabled = false; // Khóa va chạm

            switch (duLieu.ID)
            {
                case 5: // Bình máu
                    vpMau = duLieu;
                    HandlingEatHealth();
                    if (sound != null) sound.Playsound("EatHealth");
                    break;

                case 6: // Buff sát thương
                    XuLyBuffSatThuong(duLieu);
                    if (sound != null) sound.Playsound("EatDamage");
                    break;

                case 7: case 8: case 9: // Vàng/Kim cương cao cấp
                    PlayerHealth.control._Player.Gold += duLieu.Eat;
                    if (sound != null) sound.Playsound("Vang");
                    break;
            }

            ThucHienHieuUngBay(other.gameObject);
        }
    }

    // --- LOGIC BUFF SÁT THƯƠNG ---
    private void XuLyBuffSatThuong(VatPham duLieu)
    {
        if (vpSatThuong == null)
        {
            vpSatThuong = duLieu;
            timeLive = vpSatThuong.TimeLive;
            PlayerHealth.AddDamage = vpSatThuong.Eat;
        }
        else
        {
            // Cộng dồn nếu ăn thêm khi đang còn hiệu lực
            PlayerHealth.AddDamage += duLieu.Eat / 2;
            timeLive += duLieu.TimeLive;
            
            if (AnimationCoolDown != null && AnimationCoolDown.ImageBlock != null)
            {
                float timeCurrent = AnimationCoolDown.ImageBlock.fillAmount * timeLive;
                timeCurrent += duLieu.TimeLive;
                AnimationCoolDown.ImageBlock.fillAmount = Mathf.Clamp01(timeCurrent / timeLive);
            }
        }
        NhanST = true;
    }

    private void LogicHieuUngBuff()
    {
        if (NhanST && AnimationCoolDown != null)
        {
            if (HieuUngNhanSatThuong != null) HieuUngNhanSatThuong.SetActive(true);
            
            // Cập nhật thanh thời gian giảm dần
            AnimationCoolDown.SetTimeDownForImage(ref NhanST, timeLive);

            if (!NhanST) // Khi hết thời gian
            {
                PlayerHealth.AddDamage = 0f;
                if (HieuUngNhanSatThuong != null) HieuUngNhanSatThuong.SetActive(false);
                vpSatThuong = null;
            }
        }
    }

    // --- HIỆU ỨNG BAY VÀ BIẾN MẤT ---
    private void ThucHienHieuUngBay(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null) rb = obj.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero; // Chuẩn Unity mới (thay cho velocity)
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        
        Destroy(obj, 0.5f);
    }

    private void HandlingEatHealth()
    {
        if (vpMau != null && PlayerHealth.control != null)
        {
            float newHealth = PlayerHealth.control._Player.Health.Current + vpMau.Eat;
            PlayerHealth.control._Player.SetHealth(0, newHealth);
            vpMau = null;
        }
    }
}