using System.Collections.Generic;
using UnityEngine;

public class EnemyLaoVao : enemyMovingFly
{
	#region
	// #region: dung de thuc hien gom nhom mo ta giup cho nguoi dung de doc
	// partial: La tu khoa, co the hieu la "phan chung". Duoc dung cho cac class,... 
	// dung cho cac truong hop muon mo rong 1 class ma han che thay doi noi dung ben trong class do
	// Cu phap: vs ta co class khai bao nhu sau: "public class EnemyLaoVao"
	// bang cach them tu khoa partial nguoi ta co the mo rong class.
	#endregion

    private bool scope = false;
    public bool Attack = false;
	public float timeAttack = 8f;
	private float nextTimeAttack = 0f;
	// Luu vi tri nhan vat khi va cham
	private Vector2 positionPlayer;
    public bool IsLaoVao = true; 

	/// <summary>
	/// Awake this instance.
	/// </summary>
	private void Awake(){
		nextTimeAttack = 0f;
		// Khoi tao mot danh sach
		base.DanhSachDiem = new List<Transform> ();
		if (base.DanhSachDiem.Count == 0) {
			// Tim nhung diem nam trong gameobject cha
			foreach (Transform trans in transform.parent) {
				// Tim nhung diem co tag = Point
				if (trans.tag == "Point") {
					// Them vao danh sach
					base.AddTransform (trans);

					// Hint base | this keywork:
					// base: calls method , properties,... of parent
					// this: calls method , properties,... of seft

					// Han che dung this de goi cac phuong thuc, properties cua class parent
					// Boi vi: de bi gay sung dot, gay kho hieu, de bi sai trong qua trinh phar trien
				}
			}
		}
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start(){
			
	}

	private void SetAttackAfterTime ()
	{
		if (this.Attack && nextTimeAttack < Time.time) {
			this.Attack = false;
			nextTimeAttack = timeAttack + Time.time;
		}
	}

	private void MovingFly ()
	{
		if (scope && !Attack) {
			base.FllowingPointOther (transform.position, positionPlayer, 0.5f + Speed);
		}
		else {
			// Kiem tra trang thai hoat dong cua enemy
			if (this.Enemy.activeSelf) {
				base.MovingFlyPonit ();
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        if(!PauseUI.PauseGame){
		    SetAttackAfterTime ();
		    MovingFly ();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!ChangeHide.TanHinh)
        {
            //base.Enemy.activeSelf: Dung de kiem tra trang thai cua enmXXX (Enemy)
            if (!(col.tag != "Player" || !base.Enemy.activeSelf) && IsLaoVao)
            {
                // Luu thong tin nhan vat
                positionPlayer = col.transform.position;
                scope = true;
            }
            if (!IsLaoVao)
            {
                scope = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!ChangeHide.TanHinh)
        {
            // Set pham vi tan cong cua enemy
            if (col.tag == "Player")
            {
                scope = false;
                positionPlayer = Vector2.zero;
            }
        }
        else
        {
            scope = false;
            //positionPlayer = Vector2.zero;
        }
    }
}
