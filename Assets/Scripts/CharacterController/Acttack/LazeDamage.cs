using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazeDamage : MonoBehaviour {

	public float timeAttack = 0.5f;

	private float nextTimeAttack = 0f;

    private void OnTriggerStay2D(Collider2D other){
		if (!PlayerHealth.control.GetHurt ()) {
			// Khi bi enemy tan cong
			if (other.CompareTag ("Enemy") && nextTimeAttack < Time.time) {
				// Lay thanh phan script EnemyHealth
				EnemyHealth enemy = other.gameObject.GetComponent <EnemyHealth> ();
				if (enemy != null) {
					// Truyen cho enemy mot luc sat thuong
                    enemy.addDamage(PlayerHealth.control._Player.Damage + SkillManager.GetSkillByID(1, 2).Receive.Current + PlayerHealth.AddDamage);
				}
				nextTimeAttack = timeAttack + Time.time;

                // Thuc hien khoi tao doi tuong text de hien thi mau tang
                ThucHienThemChiSoSatThuongKhiTanCong(other);

                // Thuc hien khoi tao vat pham ngau nhien
                // Thuc hien khoi tao vat pham ngau nhien
                ThucHienThemCacVatPhamNgauNhien(other, enemy);
			}
		}
	}

    private void ThucHienThemCacVatPhamNgauNhien(Collider2D other, EnemyHealth enemy)
    {
        if (enemy._Enemy.Health.Current <= 0)
        {
            int index = Random.Range(0, 10);
            // 
            if (VatPhamController.control.GetItem(index) != null)
            {
                Instantiate(VatPhamController.control.GetItem(index), other.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    private void ThucHienThemChiSoSatThuongKhiTanCong(Collider2D other)
    {
        Text textDamage = PlayerHealth.control.CanvasDamage.GetComponentInChildren<Text>(); // GetComponent
        textDamage.color = Color.red;
        textDamage.text = "-" + (PlayerHealth.control._Player.Damage + SkillManager.GetSkillByID(1, 2).Receive.Current + PlayerHealth.AddDamage).ToString(); // Set damage 
        Instantiate(PlayerHealth.control.CanvasDamage, other.gameObject.transform.position, Quaternion.Euler(0, 0, 0), GameObject.FindGameObjectWithTag("SystemPlayer").transform); // Clone
    }
}
