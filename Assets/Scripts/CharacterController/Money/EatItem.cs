using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatItem : MonoBehaviour {

    public GameObject HieuUngNhanSatThuong;
    public CoolDownController AnimationCoolDown;
    private bool NhanST = false;
    private float timeLive = 0f;

    private SoundManager sound;
    private Rigidbody2D myRigid;
    private VatPham vpMau;
    private VatPham vpSatThuong;
	// Use this for initialization
	void Start () {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
        AnimationCoolDown.SetDisable();
	}

    private void Update()
    {
        ThucHienTangSatThuong();
    }

    private void ThucHienTangSatThuong()
    {
        // Thuc hien tang sat thuong
        if (NhanST)
        {
            HieuUngNhanSatThuong.SetActive(true); // Hien thi hieu ung
            AnimationCoolDown.SetTimeDownForImage(ref NhanST, timeLive); // Hien thi hieu ung hoi chieu

            // Sau thoi gian hoi chieu
            if (!NhanST)
            {
                PlayerHealth.AddDamage = 0f;
                // Huy hieu ung hoi chieu
                HieuUngNhanSatThuong.SetActive(false);
                // 
                vpSatThuong = null;
            }
        }
    }

    private void ThucHienChoPhepVatPhamBay(GameObject other)
    {
        // Cho trong luc = 0;
        myRigid.gravityScale = 0;
        myRigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        // + y
        myRigid.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        Destroy(other,1f);
    }

    private VatPham GetItem(GameObject other)
    {
        Item it = other.GetComponent<Item>();
        return VatPhamController.GetItemByID(it.ID);
    }

    private bool CheckExist(GameObject other)
    {
        if (hasComponent.HasComponent<Item>(other))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Item" && CheckExist(other.gameObject))
        {
            if (GetItem(other.gameObject).ID == 5)
            {
                vpMau = GetItem(other.gameObject); // Lay thong tin vat pham
            }
            else if (GetItem(other.gameObject).ID == 6)
            {
                if (vpSatThuong == null)
                {
                    vpSatThuong = GetItem(other.gameObject); // Lay thong tin vat pham
                    timeLive = vpSatThuong.TimeLive;
                    PlayerHealth.AddDamage = vpSatThuong.Eat;
                }
                else
                {
                    // Tang sat thuong
                    PlayerHealth.AddDamage += vpSatThuong.Eat/2;
                    // Tang thoi gian
                    timeLive += GetItem(other.gameObject).TimeLive; // Lay thong tin vat pham
                    // Lay thoi gian hien tai
                    float timeCurrent = AnimationCoolDown.ImageBlock.fillAmount * GetItem(other.gameObject).TimeLive; 
                    // Thoi gian hien tai sau khi cong them
                    timeCurrent += GetItem(other.gameObject).TimeLive;
                    AnimationCoolDown.ImageBlock.fillAmount = (timeCurrent/timeLive); // Cong them một luong thoi gian cho
                    // Lay thoi gian
                }
            }

            // Thuc hien an vang
            HandlingEatDiamond(other);
            // Thuc hien an tim
            HandlingEatHealth();
            // Thuc hien an sat thuong
            HandlingEatDamage();

            // Lay component collider2D
            Collider2D col = other.gameObject.GetComponent<Collider2D>();
            col.enabled = false;
            //
            myRigid = other.gameObject.GetComponent<Rigidbody2D>();
            // Xac nhan va cham
            ThucHienChoPhepVatPhamBay(other.gameObject);
        }
    }

    private void HandlingEatDiamond(Collision2D other)
    {
        if (GetItem(other.gameObject).ID == 7)
        {
            PlayerHealth.control._Player.Gold += GetItem(other.gameObject).Eat;
        }
        else if (GetItem(other.gameObject).ID == 8)
        {
            PlayerHealth.control._Player.Gold += GetItem(other.gameObject).Eat;
        }
        else if (GetItem(other.gameObject).ID == 9)
        {
            PlayerHealth.control._Player.Gold += GetItem(other.gameObject).Eat;
        }
    }

    private void HandlingEatHealth()
    {
        // Vat pham mau
        if (vpMau != null && vpMau.ID == 5)
        {
            PlayerHealth.control._Player.SetHealth(0, vpMau.Eat + PlayerHealth.control._Player.Health.Current);
            vpMau = null;
        }
    }

    private void HandlingEatDamage()
    {
        // Vat pham sat thuong
        if (vpSatThuong != null && vpSatThuong.ID == 6)
        {
            NhanST = true;
        }
    }
}
