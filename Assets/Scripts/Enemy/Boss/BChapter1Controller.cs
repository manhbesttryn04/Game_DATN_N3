using UnityEngine;

public class BChapter1Controller : MonoBehaviour {

	public EnemyBanDan[] BanDan;
	public string[] arrayParas;
	public GameObject LazeControl;
	public Transform Guntip;
	private bool Laze = false;
	// Lay thong tin mau
	public GameObject EnemyGraPhics;
	public Collider2D coll;
	private EnemyHealth enemyHealh;
	private Animator animAT;
	private enemyMovingWalk enemyMoving;
	private bool scope = false;
	public float timeCancelAnimationAttack2 = 2.55f;
	public float timeAnimation = 0.2f;
	public float nextTimeAnimation = 2f;

	private void Awake(){
		enemyHealh = EnemyGraPhics.GetComponent <EnemyHealth>(); // Lay thong tin bi danh
		animAT = EnemyGraPhics.GetComponent <Animator>(); // Lay hieu ung
		enemyMoving = GetComponent <enemyMovingWalk>(); // Lay thong tin di chuyen
	}

	// Use this for initialization
	private void Start () {
		
	}

	/// <summary>
	/// Sets the enabled.
	/// </summary>
	/// <param name="value">If set to <c>true</c> value.</param>
	/// true: thuc hien cho script thu 0, nguoc lai kich hoat cai thu 2
	void SetEnabled (bool value)
	{
		animAT.SetBool (arrayParas [0], value);
		animAT.SetBool (arrayParas [1], !value);
		enemyMoving.enabled = false;
	}
	
	// Update is called once per frame
	private void Update () {
		
		if (enemyHealh.GetBiDanh () >= 5) {
			if(!Laze){
				enemyMoving.BlockFacing = true;
                //if(enemyMoving.facingRight){
                //    //Instantiate (LazeControl, Guntip.position, Quaternion.Euler (0, 0, 0));
                //}else{
                //    //Instantiate (LazeControl, Guntip.position, Quaternion.Euler (0, 0,180));
                //}
                LazeControl.SetActive(true); // Hien laze
				Laze = true;
			}
			SetEnabled (false); // Kich hoat animation tan cong 2
			BanDan [0].Active = false; // Huy kich hoat tan cong 1
			coll.enabled = false; // Huy xu ly va cham
			Invoke ("SetBiDanh",timeCancelAnimationAttack2);
		} else if (scope && !Laze) {
			SetEnabled (true);
			if(nextTimeAnimation < Time.time){
				BanDan [0].enabled = true;
				nextTimeAnimation = timeAnimation + Time.time;
			}
			enemyMoving.BlockFacing = false;
		} else {
			Invoke ("SetCancel",1f);
		}
	}

	private void SetCancel(){
		if(EnemyGraPhics.activeSelf){
			animAT.SetBool (arrayParas [0], false);
		}
		enemyMoving.enabled = true;
		enemyMoving.BlockFacing = false;
		CancelInvoke ();
	}

	private void SetBiDanh(){
		enemyHealh.SetBiDanh (0);
		Laze = false;
        LazeControl.SetActive(false); // Hien laze
        coll.enabled = true;
		enemyMoving.Walk = true;
		BanDan [0].Active = true;
		if(EnemyGraPhics.activeSelf){
			animAT.SetBool (arrayParas [1], false);
		}
		CancelInvoke ();
	}

	private void SestFlipEnemy (Collider2D other)
	{
		Transform transEnemy = EnemyGraPhics.transform;
		if (!enemyMoving.facingRight && other.transform.position.x > transEnemy.position.x) {
			enemyMoving.flip ();
		}
		else
			if (enemyMoving.facingRight && other.transform.position.x < transEnemy.position.x) {
				enemyMoving.flip ();
			}
	}

	private void OnTriggerEnter2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("DanPlayer") && !enemyMoving.BlockFacing)
            {
                SestFlipEnemy(other);
            }
        }
	}

	private void OnTriggerStay2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("Player"))
            {
                scope = true;
                enemyMoving.Walk = false;
                if (!enemyMoving.BlockFacing)
                {
                    SestFlipEnemy(other);
                }
            }
        }
	}

	private void OnTriggerExit2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("Player"))
            {
                scope = false;
                enemyMoving.Walk = true;
            }
        }
        else
        {
            scope = false;
            enemyMoving.Walk = true;
        }
	}
}
