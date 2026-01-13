using UnityEngine;

public class EnemyBanDan : MonoBehaviour {

	public bool Active = true;

	public GameObject EnemyGraphics;
	public bool Walk = false;
	public InformationChuong ThongTinDan; 
	//
	public bool facingRight = true;
	public float timeFacing = 5f;
	private float nextFacing = 0f;
	public float nextStartAttack = 0.5f;
	private bool scope = false;
	//
	private EnemyVienDan enemyVienDan;
	private EnemyHealth enemyHealth;
	private DanEnemy danEnemy;
	private void Awake() {
		enemyHealth = EnemyGraphics.GetComponent <EnemyHealth>();
		enemyVienDan = ThongTinDan.DanGraphics.GetComponent <EnemyVienDan>();
		danEnemy = ThongTinDan.DanGraphics.GetComponent <DanEnemy>();
	}

	void ContructorStart ()
	{
		// Khoi tao thong tin mac dinh cho dan
		CreateThongTinDan ();
		// Trich xuat damage
		enemyVienDan.Damage = ThongTinDan.GetDamage () + enemyHealth._Enemy.GetDamage ();
		// Khoi tao thong tin vien dan
		danEnemy.Speed = ThongTinDan.speedDan;
		danEnemy.TimeLive = ThongTinDan.timeLive;
	}

	private void CreateThongTinDan(){
		//ThongTinDan.timeNextAttack = 3f;
		ThongTinDan.timeLive = 5f;
		ThongTinDan.speedDan = 20f;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Active) {
			ContructorStart ();
			if (EnemyGraphics.activeSelf) {
				if (!scope) {
					if (!Walk) {
						if (nextFacing < Time.time) {
							flip ();
							nextFacing = timeFacing + Time.time; 
						}	
					}
				}	 else { 
					if (nextStartAttack < Time.time) {
						ConstructionDan (); // Khoi tao nan
						nextStartAttack = ThongTinDan.timeNextAttack + Time.time; 
					}
				}
			}
		}
	}

	/// <summary>
	/// Flip this instance.
	/// </summary>
	private void flip(){
		facingRight = !facingRight; // Xac dinh doi huong
		Vector3 theScale = EnemyGraphics.transform.localScale;
		theScale.x *= -1; 
		EnemyGraphics.transform.localScale = theScale;
	}

	/// <summary>
	/// Constructions the dan.
	/// </summary>
	private void ConstructionDan(){
		if (EnemyGraphics.transform.localScale.x > 0) {
			Vector3 scale = ThongTinDan.DanGraphics.transform.localScale;
			if (scale.x < 0) {
				scale.x *= -1;
			}
			ThongTinDan.DanGraphics.transform.localScale = scale;
			// Khoi tao dan cho nhan vat
			Instantiate (ThongTinDan.DanGraphics, ThongTinDan.GunTip.position, Quaternion.Euler (0, 0, 0));
		} else {
			Vector3 scale = ThongTinDan.DanGraphics.transform.localScale;
			if (scale.x > 0) {
				scale.x *= -1;
			}
			ThongTinDan.DanGraphics.transform.localScale = scale;
			// Khoi tao dan cho nhan vat
			Instantiate (ThongTinDan.DanGraphics, ThongTinDan.GunTip.position, Quaternion.Euler (0, 0, 0));
		}
	}

	/// <summary>
	/// Raises the trigger stay2 D flip event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerStay2DFlip (Collider2D other)
	{
		if (!Walk) {
			if (facingRight && EnemyGraphics.transform.position.x > other.transform.position.x) {
				flip ();
			}
			else
				if (!facingRight && EnemyGraphics.transform.position.x < other.transform.position.x) {
					flip ();
				}
		}
	}

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerStay2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("Player") && !scope)
            {
                scope = true;
                OnTriggerStay2DFlip(other);
            }
        }
        else
        {
            scope = false;
        }
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			scope = false;
		}
	}
}
