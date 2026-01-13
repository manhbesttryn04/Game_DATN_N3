using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BChapter2Controller : MonoBehaviour {

    [Header("Thong tin ky nang quai:")]
    public GameObject EnemyDragon;
    [Space(10)]
    [Header("Thong tin ky nang 1:")]
    public GameObject DanLuaGraphics;
    public float timeAttack = 1f;
    public Transform GunTip;
    public float jumpAttack = 10f;
  
    private bool AttackPhuaLua = false;
    [Space(10)]
    [Header("Thong tin ky nang 2:")]
    public GameObject LazeGraphics;
    public float timeLive = 3f;
    public float minX, maxX; // Khoang tan cong
    public float minY,maxY; // Khoang tan cong

    private Transform playerGraphics;
    private bool IsHealthLess = false; // Dieu kien thuc hien ky nang thu 2, rong < 50% mau
    private Animator anim;
    private EnemyHealth enemyHealth;
  
    private void Awake()
    {
        enemyHealth = EnemyDragon.GetComponent<EnemyHealth>(); // Lay thong tin chi so mau
        anim = EnemyDragon.GetComponent<Animator>(); // Lay thong tin animation
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyHealth._Enemy.Health.Current < (enemyHealth._Enemy.Health.Max / 2))
        {
            IsHealthLess = true; // Kich hoat ky nang thu 2, laze
            // Doi mau thanh mau
            enemyHealth.Fill.color = Color.red; // Mau do
        }
        // Thuc hien tan cong
        ThucHienTanCong();
	}

    private void ThucHienTanCong()
    {
        //Thuc hien ky nang 1 phun lua
        if (AttackPhuaLua && !IsHealthLess)
        {
            ThucHienTanCongVoiKyNangPhunLua();
        }
        //Thuc hien ky nang 2 phun laze
        else if (IsHealthLess)
        {
            // Thuc hien ky nang thu 2, laze
            ThucHienTanCongVoiKyNangLaze();
        }
    }

    private bool NamTrongKhoangXY()
    {
        // Kiem tra pham vi hoat dong
        if ((EnemyDragon.transform.position.x >= minX && EnemyDragon.transform.position.x <= maxX)
            && (EnemyDragon.transform.position.y >= minY && EnemyDragon.transform.position.y <= maxY))
        {
            return true;
        } 
        return false;
    }
    private void ThucHienTanCongVoiKyNangLaze()
    {
        // Kiem tra pham vi tan cong cua rong
        if (NamTrongKhoangXY())
        {
           LazeGraphics.SetActive(true); // Tan cong
        }
        else
        {
            LazeGraphics.SetActive(false); // Huy tan cong
        }
    }

    private void InvokeLaze()
    {
        LazeGraphics.SetActive(false);
    }

    private void ThucHienTanCongVoiKyNangPhunLua()
    {
        // Khi rong dat tam cao du lon, se thuc hien tan cong bang vien dan lua
        if(EnemyDragon.transform.position.y > playerGraphics.position.y + jumpAttack){
          
            // Neu rong dang huong ve nhan vat thi moi tan cong
            if (EnemyDragon.transform.localScale.x > 0 && EnemyDragon.transform.position.x < playerGraphics.position.x )
            {
                // Khi rong huong ve ben phai, co nghia la ben trai cua nhan vat
                anim.SetBool("Attack",true);
                Invoke("InvokeRight", timeAttack);
            }
            else
            if (EnemyDragon.transform.localScale.x < 0 && EnemyDragon.transform.position.x > playerGraphics.position.x)
            {
                // Khi rong huong ve ben trai, co nghia la ben phai cua nhan vat
                anim.SetBool("Attack", true);
                Invoke("InvokeLeft", timeAttack);
            }
        }
    }

    // Thuc hien doi huong phun lua
    private void InvokeRight()
    {
        // Vien dan bay nghien ve phia phai
        Instantiate(DanLuaGraphics, GunTip.position, Quaternion.Euler(0, 0, -30));
        AttackPhuaLua = false; // Tat tan cong
        anim.SetBool("Attack", false); // Tat hieu ung
        CancelInvoke("InvokeRight"); // Tat hàng chờ, không cho InvokeRight duoc thuc hien lan nua
        // Khi su dung [Invoke] no se tiep tuc thuc hien trong mot khoang thoi gian, sau do moi dung lai
    }

    // Thuc hien doi huong phun lua
    private void InvokeLeft()
    {
        // Vien dan bay nghien ve phia ben trai
        Instantiate(DanLuaGraphics, GunTip.position, Quaternion.Euler(0, 180, -30));
        AttackPhuaLua = false;
        anim.SetBool("Attack", false);
        CancelInvoke("InvokeLeft");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        // Kien tra va cham
        if (col.CompareTag("Player"))
        {
            // Neu rong >= 50% mau thuc hien tan cong kn1
            if (!IsHealthLess)
            {
                AttackPhuaLua = true;
            }
            // Lay vi tri nhan vat
            playerGraphics = col.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Kien tra va cham
        if (col.CompareTag("Player"))
        {
            if (AttackPhuaLua)
            {
                AttackPhuaLua = false; // Tat tan cong
            }
            // Xoa doi tuong nhan vat
            playerGraphics = null;
        }
    }
}
