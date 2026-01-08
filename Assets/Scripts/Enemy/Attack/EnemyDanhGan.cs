using UnityEngine;

public class EnemyDanhGan : MonoBehaviour {
    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    // Xac nhan va cham tan cong
    public Collider2D trigger;
	//
	public float timeAnimationAttack = 0.5f;
	// Lay thong tin tu class EnemyHealth
	public GameObject EnemyGraphics;
	protected EnemyHealth enemyHealth;
	// Thoi gian gay ra lan sat thuong tiep theo
	public float damageRateTime = 3f;
	// Animation exist
	public bool hasAnimationAttack = true;
	// Luot gay sat thuong tiep theo
	protected float nextDamage;
	//
	protected Animator anim;
	public virtual void Awake(){
		nextDamage = 0f;
		// Lay thong tin cua enemy
		enemyHealth = EnemyGraphics.GetComponent <EnemyHealth> ();
		anim = EnemyGraphics.GetComponent <Animator> ();
	}

	private void Update(){
	}

	protected void CheckAttack (Collider2D other)
	{
        if (!ChangeHide.TanHinh)
        {
		    // Xac nhan enemy tan cong player
		    if (trigger.isTrigger && other.CompareTag ("Player") && nextDamage < Time.time) {
			    if (hasAnimationAttack && this.gameObject.activeSelf) {
				    anim.SetBool ("Acttack", true);
                    sound.Playsound("AttackEnemy");
			    }
			    // Lay thong tin player
			    PlayerHealth player = other.gameObject.GetComponent<PlayerHealth> ();
			    // Gan luc sat thuong cua enemy cho player
			    player.addDamage (enemyHealth._Enemy.GetDamage ());
			    nextDamage = damageRateTime + Time.time;
		    }
        }
	}

	//Xu ly va cham khi bi tan cong
	private void OnTriggerStay2D(Collider2D other){	
		CheckAttack (other);
	}

	private void OnTriggerExit2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            // Xac nhan enemy tan cong player
            if (other.CompareTag("Player"))
            {
                Invoke("SetAniamtionActtack", timeAnimationAttack);
            }
        }
	}

	private void SetAniamtionActtack(){
		if (hasAnimationAttack && this.gameObject.activeSelf) {
			anim.SetBool ("Acttack", false);	
		}
	}
			
}