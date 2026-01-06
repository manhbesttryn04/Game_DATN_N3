using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Thuc hien dieu khien cac hieu ung, cac ky nang cua boss
/// </summary>
public class BChapter3Controller : MonoBehaviour {

    [Header("Thong tin boss")]
    public GameObject BossGraphics;

    private bool DanhThuong = true; // Xac nhan danh thuong
    private enemyMovingWalk movingWalk;

    // Khai bao cho ky nang tan hinh
    [Header("Hieu ung tan hinh")]
    public GameObject KhoiGraphics;

    // Khai bao cho ky nang luot gio
    [Header("Hieu ung luot gio")]
    public GameObject LuotGraphics;// hieu ung luot
    public float Speed = 50f; // Luc tac dong cho luot
    private bool facing = false; // Thoi gian luot tu A -> B
    private int lenLuotGio = 0; // So lan luot gio
    private bool LuotGio = false;  // Xac nhan thuc hien luot gio

    private EnemyHealth enemyHealth; // Lay sat thuong
    private SpriteRenderer spriteRenderer; // An hien boss
    private float timeHide = 5f; // Thoi gian an trong bao lau
    private bool Hide = false; // Xac nhan dang an
    private bool ShowAnimation = false; // Xac nhan hien hieu ung boc hoi
    
    public bool GetDanhThuong()
    {
        return this.DanhThuong; // Get
    }

    private void Awake(){
        // Tan hinh
        enemyHealth = BossGraphics.GetComponent<EnemyHealth>();
        spriteRenderer = BossGraphics.GetComponent<SpriteRenderer>();
        // Di chuyen
        movingWalk = GetComponent<enemyMovingWalk>();
    }
	
	// Update is called once per frame
	void Update () {
        // Khi boss chua chet se thuc hien cac ky nang
        if (BossGraphics.activeSelf)
        {
            // Thuc hien ky nang tan hinh
            ThucHienTanHinh();
        }
	}

    private void ThucHienTanHinh()
    {
        // Neu luong sat thuong nhan vao > 5% mau so voi hien tai, se thuc hien tan hinh
        // Va luong mau quai hien tai phai lon hon 500 mau.
        if (enemyHealth.GetSatThuong() >= 500 && enemyHealth._Enemy.Health.Current > 200 && !Hide)
        {
            ChangeInformationToHide(true); // Thay doi cac thong tin, tan hinh
        }
        // Thuc hien huy tan hinh tron thoi gian quy dinh
        if (this.Hide)
        {
            Invoke("SetHide", timeHide);
        }
        // Khi boss hien tro lai se thuc hien ky nang luot gio cua minh
        // Thuc hien gio duoc bat, sau khi ket thuc tan hinh
        if (LuotGio && !this.Hide)
        {
            ThucHienLuotGio();
        }
    }

    private void ThucHienLuotGio()
    {
        // Thay doi cac thong tin khi thuc hien luot gio
        ChangeToLuotGio(false); // Tat cac thong so khi thuc hien luot gio
        // Them luc cho boss luot qua, luot lai
        XuLyLuotGio();
    }

    private void ChangeToLuotGio(bool value)
    {
        this.DanhThuong = value; // Tat che do danh thuong
        enemyHealth.SetTanHinh(!value); // Bat tan hinh, de boss khong phai nhan sat thuong tu nhan vat
        movingWalk.Walk = value; // Thay doi di chuyen
    }

    /// <summary>
    /// Thuc hien xu ly them luc day
    /// </summary>
    private void XuLyLuotGio()
    {
        // Hien hieu ung luot gio cho boss
        LuotGraphics.SetActive(true); 
        // Day mot luc, ve huong ma boss dang huong ve
        movingWalk.AddForceWalk(Speed);
        // Sau khi them luc, thuc hien doi huong di chuyen
        SetTimeWind();
        // Sau khi thuc hien doi huong 4 lan thi dung lai, dua moi thu tro ve binh thuong
        if (lenLuotGio >= 4)
        {
            DefaultSkillWind(); // Thuc hien thay doi ve trang thai ban dau
        }
    }

    private void DefaultSkillWind()
    {
        // An hieu ung luot gio cho boss
        LuotGraphics.SetActive(false);
        // Tat hieu ung
        // Thay doi cac thong tin khi thuc hien luot gio
        ChangeToLuotGio(true); // Tat cac thong so khi thuc hien luot gio, di chuyen binh thuong
        LuotGio = false; // Da thuc hien luot gio xong
        lenLuotGio = 0; // Set so luot gio ve 0, de thuc hien cho lan tiep theo
    }

    private void SetTimeWind()
    {
        // Luot tu min - max, nam trong pham vi di chuyen cua boss
        if (((this.gameObject.transform.position.x <= movingWalk.minX) || (this.gameObject.transform.position.x >= movingWalk.maxX)) && !facing)
        {
            facing = true; // Xac nhan da doi huong
            // Bat buoc boss dung yen ngay tuc thi
            movingWalk.SetVelocity(Vector2.zero);
            // Doi huong luot
            movingWalk.flip();
            lenLuotGio++; // Tang lan luot gio len 1
        }
        // Neu boss dang nam trong pham vi di chuyen cua no thi facing luon luon mang gia tri sai(false)
        if ((this.gameObject.transform.position.x > movingWalk.minX)
            && (this.gameObject.transform.position.x < movingWalk.maxX) && facing)
        {
            facing = false; // luon luon = false khi nam trong pham vi di chuyen
        }
    }

    private void ChangeInformationToHide(bool value)
    {
        SetShowAnimationHide(value);
        this.Hide = value; // Xac nhan da tan hinh
        // Thuc hien tan hinh
        spriteRenderer.enabled = !value;
        // Kich hoat tinh nang tan hinh
        enemyHealth.SetTanHinh(value);
    }

    /// <summary>
    /// Thuc hien hien hieu ung khi, boss an/hien
    /// </summary>
    /// <param name="value"></param>
    private void SetShowAnimationHide(bool value)
    {
        // TH: Boss dang hien dang bat dau an
        // Hien hieu ung boc hoi
        if (!ShowAnimation && value)
        {
            KhoiGraphics.SetActive(value);
            ShowAnimation = true; // Cho phep hien khi no hien
        }
        // TH: Boss dang an duoc hien lai binh thuong
        if (ShowAnimation && !value)
        {
            KhoiGraphics.SetActive(true); // Hien lai lan nua
            ShowAnimation = false;
        }
    }

    private void SetHide()
    {
        LuotGio = true; // Thuc hien luot gio duoc thuc hien
        enemyHealth.SetSatThuong(0); // Set luong sat thuong nhan vao ve bang 0
        ChangeInformationToHide(false); // Thay doi cac thong tin, huy tan hinh
        CancelInvoke("SetHide");
    }
}