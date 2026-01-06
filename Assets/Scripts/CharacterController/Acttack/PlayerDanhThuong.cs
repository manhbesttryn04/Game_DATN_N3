using UnityEngine;

public class PlayerDanhThuong : MonoBehaviour {

    // ID = 0; // Ma ky nang danh thuong
    private SkillController SkillBasic = new SkillController(); // Thong tin ky nang

    private float attackdelay = 0.3f; // thời gian nhân vật đánh lại lần kế tiếp
    private bool attacking = false; // dùng để kiểm tra là tấn công hay không
    private Animator anim;
	private int qualityAttack = 0;

    public Collider2D trigger;
	public KeyCode KeyActive;
	public GameObject[] AnimationAttack;
	public Transform[] GunTip;

    public static bool LockAttack = false;
    public static bool Active = false;
    public SoundManager sound;
  
    private void Awake()
    {
		qualityAttack = 0;
        anim = GetComponent<Animator>();

		trigger.enabled = false;
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>(); 
    }

	public void displayAnimation(GameObject Graphics,Transform GunTip){
		if (Graphics != null && GunTip != null) {
            if (DoorCharacter._player.gameObject.transform.localScale.x > 0)
            {
				Vector3 scale = Graphics.transform.localScale;
				if (scale.x < 0) {
					scale.x *= -1;
				}
				Graphics.transform.localScale = scale;
                Graphics.SetActive(true);
				// Khoi tao dan cho nhan vat
				Instantiate (Graphics, GunTip.position, Quaternion.Euler (0, 0, 0));
			} else {
				Vector3 scale = Graphics.transform.localScale;
				if (scale.x > 0) {
					scale.x *= -1;
				}
				Graphics.transform.localScale = scale;
                Graphics.SetActive(true);
				// Khoi tao dan cho nhan vat
				Instantiate (Graphics, GunTip.position, Quaternion.Euler (0, 0, 0));
			}
		}
	}

	void SkillDanhThuongPlayer ()
	{
        if (ChangePlayer.IsKnight)
        {
            if (PlayerHealth.Skill)
            {
                // tinh chỉnh nút bấm và thời gian lập lại khi chém
                if ((Active || Input.GetKeyDown(KeyActive)) && !attacking && !LockAttack)
                {
                    sound.Playsound("Chem");
                    attacking = true;
                    trigger.enabled = true;
                    attackdelay = 0.3f;
                    // Thuc hien dua ra dieu kien tri hoan hien hieu ung
                    if (SkillBasic.Level.Current >= 5)
                    {
                        // Khoi tao doi tuong
                        if (qualityAttack < 3)
                        {
                            displayAnimation(AnimationAttack[0], GunTip[0]);
                            qualityAttack++;
                        }
                        else
                        {
                            displayAnimation(AnimationAttack[1], GunTip[1]);
                            qualityAttack = 0;
                        }
                    }
                }
            }
        }

		if (attacking) {
			if (attackdelay > 0) {
				attackdelay -= Time.deltaTime;
			}
			else {
				attacking = false;
                Active = false;
				trigger.enabled = false;
			}
		}
		// thao tac voi anh.
		anim.SetBool ("Attacking", attacking);
	}

	// Update is called once per frame
	void Update () {
        // Thong tin ky nang
        SkillBasic = SkillManager.GetSkillByID(0, 0); // Lay thong tin
        // Danh thuong
        SkillDanhThuongPlayer();
	}
}