using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Thuc hien nhan tac dong danh gan
/// </summary>
public class BChapter3DanhGan : MonoBehaviour {

    [Header("Thong tin boss")]
    public GameObject BossGraphics;
    public float Damage = 0f;
    private EnemyHealth enemyHealth;
    private void Awake()
    {
        // Lam ro bien thong tin quai
        enemyHealth = BossGraphics.GetComponent<EnemyHealth>();
    }

    /// <summary>
    /// Chi thuc hien danh khi hieu ung duoc thuc hien
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiem tra nhan vat tan hinh
        if (!ChangeHide.TanHinh)
        {
            // Kiem tra nhan vat
            if (other.CompareTag("Player"))
            {
                // Them luc chien
                PlayerHealth.control.addDamage(enemyHealth._Enemy.GetDamage() + Damage);
            }
        }
    }
}
