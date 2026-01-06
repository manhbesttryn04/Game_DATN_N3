using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// File chua thong tin luc sat thuong khi thuc hien tan cong quai
/// File nay duoc di doi voi mot loai Collider2D dung de va cham voi Enemy.
/// Muc dich: Truyen sat thuong cua nhan vat toi cho Enemy
/// </summary>
public class PlayerDamage : MonoBehaviour {
	// Sat thuong co ban cua layer nam trong Player.cs
	// Luc sat thuong cua mot ky nang cua player
	public int ID = 0;
    private int NV = 0;

    private void Update()
    {
        if (ChangePlayer.IsKnight)
        {
            NV = 0;
        }
        else
        {
            NV = 1;
        }
    }
	// Xac nhan va cham tan cong
	// 
	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerEnter2D(Collider2D other){
		// Khi bi enemy tan cong
		if (other.CompareTag ("Enemy")) {
			// Lay thanh phan script EnemyHealth
			EnemyHealth enemy = other.gameObject.GetComponent <EnemyHealth> ();
			if (enemy != null) {
				// Truyen cho enemy mot luc sat thuong
                enemy.addDamage(PlayerHealth.control._Player.Damage + SkillManager.GetSkillByID(NV, ID).Receive.Current + PlayerHealth.AddDamage);

                // Thuc hien khoi tao doi tuong text de hien thi mau tang
                ThucHienThemChiSoSatThuongKhiTanCong(other);
                // Thuc hien khoi tao vat pham ngau nhien
                ThucHienThemCacVatPhamNgauNhien(other,enemy);
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
        textDamage.text = "-" + (PlayerHealth.control._Player.Damage + SkillManager.GetSkillByID(NV, ID).Receive.Current + PlayerHealth.AddDamage).ToString(); // Set damage 
        Instantiate(PlayerHealth.control.CanvasDamage, other.gameObject.transform.position, Quaternion.Euler(0, 0, 0), GameObject.FindGameObjectWithTag("SystemPlayer").transform); // Clone
    }
}
