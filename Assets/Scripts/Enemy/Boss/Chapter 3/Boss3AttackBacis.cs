using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Thuc hien thay doi hieu ung cung nhu la thuc hien danh thuong o boss
/// </summary>
public class Boss3AttackBacis : MonoBehaviour {

    private Animator myAnim; // Lay thong tin animation
    private enemyMovingWalk movingWalk;
    private BChapter3Controller managerBoss;
    private bool ArroundScope = false;
 
    private void Awake()
    {
        myAnim = GetComponent<Animator>(); // Lay animator
        movingWalk = GetComponentInParent<enemyMovingWalk>();
        managerBoss = GetComponentInParent<BChapter3Controller>();
    }

    private void Update()
    {
        // Neu cho phep danh thuong thi danh thuong
        if (managerBoss.GetDanhThuong())
        {
            // Thu chien hien hieu ung danh thuong
            ThucHienDanhGan();
           
        }
    }
 
    // Khi nguoi choi di vao pham vi tan cong se thuc hien danh gan
    private void ThucHienDanhGan()
    {
        // Kiem tra nguoi choi da di vao pham vi hoat dong
        if (CheckColliderAttack.IsScope)
        {
            // Thuc hien hien hieu ung danh
            if (!myAnim.GetBool("Attack2"))
            {
                // Hien animation
                myAnim.SetBool("Attack1", true);

            }
            // Xac nhan da thuc hien tan cong
            ArroundScope = true;
            // Thuc hien dung di chuyen khi tan cong
            movingWalk.Walk = false; // Chan di chuyen
            movingWalk.SetVelocity(Vector2.zero); // Dung di chuyen, ngay lap tuc cho boss
        }
        else
        {
            // Huy hieu ung
            if (ArroundScope)
            {
                // Thuc hien huy hieu ung khi no da thuc hien hieu ung
                Invoke("animationCancel", 1.5f);
            }
        }
    }

    /// <summary>
    /// Thay doi huy cac hieu ung dang duoc thuc hien
    /// </summary>
    private void animationCancel()
    {
        // Tat cac hieu tan cong
        myAnim.SetBool("Attack1", false);
        myAnim.SetBool("Attack2", false);
        movingWalk.Walk = true;
        ArroundScope = false;
        CancelInvoke("animationCancel"); // Huy hang cho
    }

    /// <summary>
    /// Thuc hien sau khi hieu ung danh thuong 1 duoc thuc hien xong
    /// </summary>
    public void animationAttack()
    {
        myAnim.SetBool("Attack1", false); // Thuc hien xong hieu ung se tat hieu ung danh thuong 1 di, de
        myAnim.SetBool("Attack2", true); // Kich hoat hieu ung danh thuong 2
    }

    /// <summary>
    /// Thay doi huy cac hieu ung dang duoc thuc hien
    /// </summary>
    public void animationEnd()
    {
        // Tat cac hieu tan cong
        myAnim.SetBool("Attack1", false);
        myAnim.SetBool("Attack2", false);
        movingWalk.Walk = true; // Kich hoat di chuyen
    }
}
