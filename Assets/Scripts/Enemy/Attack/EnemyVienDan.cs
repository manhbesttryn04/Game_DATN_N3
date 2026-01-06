using UnityEngine;

public class EnemyVienDan : MonoBehaviour {
	
	public  float Damage = 0f;
	// Use this for initialization
	private bool Attack = false;
	//Xu ly va cham khi bi tan cong
	private void OnTriggerEnter2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            // Xac nhan enemy tan cong player
            if (other.CompareTag("Player") && !Attack)
            {
                Attack = true;
                // Lay thong tin player
                PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
                // Gan luc sat thuong cua enemy cho player
                player.addDamage(Damage);
            }
        }
	}
}
