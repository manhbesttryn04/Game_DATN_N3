using UnityEngine;

public class KyNangChuong : MonoBehaviour {
    //Am thanh
    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    // Khai bao cho chuc nang chuong 
    public string NameParameter = "Attacking";
	public InformationChuong ThongSoKyNang; 

	public float timeAnimation = 0.5f; // Thoi gian huy animatiton
	public float timeShowDan = 0.5f;
	public KeyCode KeyChuong;
	// end khai bao
	private DanCharacter vienDan;
	private float timeChuong; // Thoi gian thuc hien chuong, dung de thay doi thuong xuyen

    public static bool Active = false; // Nhan tac dong nhan nut
    public static bool LockAttack = false;
	void Awake(){
		vienDan = ThongSoKyNang.DanGraphics.GetComponent <DanCharacter> ();
		vienDan.TimeLive = ThongSoKyNang.timeLive;
		vienDan.Speed = ThongSoKyNang.speedDan;
	}

	// Update is called once per frame
	void Update () {
        // Chuong lua
        ExecuteSkillChuong(true);
	}

	private void ExecuteSkillChuong(bool value){
		// ThongSoKyNang.allow : Mo khoa skill
		if (value) {
            if (!ChangePlayer.IsKnight)
            {
                if (PlayerHealth.Skill && !LockAttack)
                {
                    // Kiem tra khi nha J
                    if ((Active || Input.GetKeyDown(KeyChuong)) && !ThongSoKyNang.GetActived() && !PlayerHealth.control.GetHurt())
                    {
                      
                        PlayerMoving.anim.SetBool(NameParameter, true);
                        // Thuc hien ban khi dang chay
                        PlayerMoving.Move = false; // Dung chay, khi skill dc thuc hien
                        //PlayerMoving.Jump = false; 
                        // Xac nhan chuong
                        sound.Playsound("PhunLua");
                        ThongSoKyNang.SetActived(true);
                        timeChuong = ThongSoKyNang.timeNextAttack; // Cai dat thoi gian	
                        // Khoi tao dan
                        Invoke("ConstructionDan", timeShowDan);
                        //
                    }
                }
                Active = false; // Nham tac dung chi nhan mot lan
            }
			SetKhoangThoiGianDanXuatHien ();

            if (PlayerMoving.anim.GetBool(NameParameter))
            {
                if (timeAnimation > 0)
                {
                    timeAnimation -= Time.deltaTime;
                }
                else
                {
                    timeAnimation = 0.5f;
                    PlayerMoving.anim.SetBool(NameParameter, false);
                    PlayerMoving.Move = true;
                }
            }
		}
	}

	// Khoi tao dan
	private void ConstructionDan(){
        if (!LockAttack)
        {
            if (PlayerMoving.myBody.gameObject.transform.localScale.x > 0)
            {
                Vector3 scale = ThongSoKyNang.DanGraphics.transform.localScale;
                if (scale.x < 0)
                {
                    scale.x *= -1;
                }
                ThongSoKyNang.DanGraphics.transform.localScale = scale;
                // Khoi tao dan cho nhan vat
                ThongSoKyNang.DanGraphics.SetActive(true);
                Instantiate(ThongSoKyNang.DanGraphics, ThongSoKyNang.GunTip.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Vector3 scale = ThongSoKyNang.DanGraphics.transform.localScale;
                if (scale.x > 0)
                {
                    scale.x *= -1;
                }
                ThongSoKyNang.DanGraphics.transform.localScale = scale;
                // Khoi tao dan cho nhan vat
                Instantiate(ThongSoKyNang.DanGraphics, ThongSoKyNang.GunTip.position, Quaternion.Euler(0, 0, 0));
            }
        }
	}

	// Quy dinh thoi gian cho dan xuat hien
	private void SetKhoangThoiGianDanXuatHien(){
		if (ThongSoKyNang.GetActived ()) {
			if (timeChuong > 0) {
				timeChuong -= Time.deltaTime;
			} else {
				ThongSoKyNang.SetActived (false);
			}
		}
	}
}
