using UnityEngine;

public class EnemyLaze : MonoBehaviour {

    //public SoundManager sound;
    //private void Start()
    //{
    //    sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    //}
    public  float Damage = 0f;
	// Use this for initialization
	private float nextAttack = 0f;
	public float timeAttack = 0.2f;
	//Xu ly va cham khi bi tan cong
	private void OnTriggerStay2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            // Xac nhan enemy tan cong player
            if (other.CompareTag("Player") && nextAttack < Time.time )
            {
              //  sound.Playsound("BombVaCham");
                // Lay thong tin player
                PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
                // Gan luc sat thuong cua enemy cho player
                player.addDamage(Damage);
                nextAttack = timeAttack + Time.time;
               
            }
        }
	}
}
