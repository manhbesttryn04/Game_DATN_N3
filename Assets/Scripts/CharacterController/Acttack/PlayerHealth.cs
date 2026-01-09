using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Flie nay dung de chua thong tin co ban cua nhan vat(mau, mana, sat thuong co ban,...)
/// Thuc hien cac hieu ung khi bi tan cong...
/// </summary>
public class PlayerHealth : MonoBehaviour {
    // Am thanh 
    public SoundManager sound;
    public static int nextLevel = 3;
    //
    public static PlayerHealth control;  // Lay thong tin ben trong cua nhung bien public cua lop
    // vd: _Player,IsLoad....
    public static float AddDamage = 0f;
	// Thong tin co ban cua nguoi choi
	public Player _Player;
	public bool IsLoad;
	// Thoi gian ton tai sau chet
	public float timeAnimationDie = 2.2f;
	public float timeCancelHurt = 0.5f;
    public GameObject CanvasDamage;
	// Xac nhan dung lai khi bi danh
	private bool Hurt = false;
	// Xac nhan nguoi choi con song hay chet
	private bool Die = false;
	private bool MienKhang = false;
    public static bool Skill = true;
	/// <summary>
	/// Gets the die.
	/// </summary>
	/// <returns><c>true</c>, if die was gotten, <c>false</c> otherwise.</returns>
	public bool GetDie(){
		return this.Die;
	}

	public bool GetMienKhang(){
		return this.MienKhang;
	}

	public void SetMienKhang(bool value){
		this.MienKhang = value;
	}
	/// <summary>
	/// Gets the hurt.
	/// </summary>
	/// <returns><c>true</c>, if hurt was gotten, <c>false</c> otherwise.</returns>
	public bool GetHurt(){
		return this.Hurt;
	}
	/// <summary>
	/// Awake this instance.
	/// </summary>
	private void Awake(){
		// Khoi tao thong tin ve animaition
		//changePlayer = GetComponent <ChangePlayer>();
		// Load lai thong tin khi bat dau
	}

	// Use this for initialization
	void Start () {
        // lay thong tin am thanh
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
        //
        if (!Die) {
			if (IsLoad) {
				if (_Player.hasExist ()) {
					_Player = _Player.LoadInformation (); // Lay du lieu da luu trong thu muc Data/DataPlayer.dat
					if (_Player.Health.Current == 0.0f) {
						_Player.SetDefault ();
					}
				}
			} else {
				//_Player.SetDefault ();
			}
		}
		// 
		if (control == null) {
			control = this;
		}
        // Cai dat lai
        Skill = true;
  	}

	private void ExecuteSaveInformation ()
	{
        _Player.SaveInformation();
	}

	private void Update(){
        if (control == null || control != this)
        {
            control = this;
        }
		// Luu thong tin 
		ExecuteSaveInformation ();
	}

	/// <summary>
	/// Adds the damage.
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void addDamage(float damage){
		if (!MienKhang) {
			// TH: Khong bi tan cong
			if (damage <= 0) {
				return;
			}

            ShowAnimationDamage(damage);

			// Giam mau cho nhan vat
			_Player.SetHealth (0, _Player.Health.Current - damage);
            // Them nộ
            _Player.SetInfuriate(0, _Player.Infuriate.Current + 5);
			// Hien animation bi tan cong
			SetAnimationHurt ();
			// TH: Mau bi giam het
			if (_Player.Health.Current <= 0) {
				// Nhan vat se tu vong
				Dead ();
			}
		}
	}

    private void ShowAnimationDamage(float damage)
    {
        // Thuc hien khoi tao doi tuong text de hien thi mau tang
        Text textDamage = CanvasDamage.GetComponentInChildren<Text>(); // GetComponent
        textDamage.color = new Color(0.157163f, 0.7169812f, 0.03720186f,1);
        textDamage.text = "-" + (damage).ToString(); // Set damage 
        Instantiate(CanvasDamage, this.gameObject.transform.position, Quaternion.Euler(0, 0, 0), GameObject.FindGameObjectWithTag("SystemPlayer").transform); // Clone
    }

	/// <summary>
	/// Sets the animation hurt.
	/// </summary>
	private void SetAnimationHurt(){
		// hien aniamtion
		PlayerMoving.anim.SetBool ("Hurt",true);
		// Xac nhan hurt de chan di chuyen
		Hurt = true;
        PlayerMoving.Jump = false;
        Invoke ("SetBoolHurtTime", timeCancelHurt);
		// Vang ra 1 khoang
        pushBack(PlayerMoving.myBody.gameObject.transform,1f,0f);
	}

	/// <summary>
	/// Sets the bool hurt time.
	/// </summary>
	private void SetBoolHurtTime(){
		PlayerMoving.anim.SetBool ("Hurt",false);
		Hurt = false;
	}

	/// <summary>
	/// Sets the information default.
	/// </summary>
	private void SetInformationDefault(){
		// Thay doi thanh mau tro ve 0
		// Truong hop dung phai gai chet
		_Player.Health.Current = 0;
        _Player.Infuriate.Current = 0;
	}

	/// <summary>
	/// Dead this instance.
	/// </summary>
	public void Dead(){
        // Hien thi thong bao
        MessageBoxx.control.ShowBig("Trình còn kém lắm, rèn luyện thêm nhé.\nBạn có thể dùng 500.000 vàng để hồi sinh trở lại.");
		// Hien animation chet
		PlayerMoving.anim.SetBool ("Die",true);
		// Set thong tin cua player
		SetInformationDefault();
		// Xac nhan nhan vat da chet
		this.Die = true;
		// Huy nhan vat
        Invoke("DeadPlayer", timeAnimationDie);
        // Goi am thanh khi nhan vat chet
      
	}

    private void DeadPlayer()
    {
        this.gameObject.SetActive(false);
    }

	/// <summary>
	/// Pushs the back.
	/// </summary>
	/// <param name="pushObject">Push object.</param>
    public void pushBack(Transform pushObject, float forcex, float forcey)
    {
		// Luc day
        Vector2 pushDerection = new Vector2(forcex, forcey);
		if (pushObject.transform.localScale.x > 0) {
            pushDerection = new Vector2(-forcex, forcey);
		}
        //
        PlayerMoving.myBody.linearVelocity = Vector2.zero;
		// Gan luc day
        PlayerMoving.myBody.AddForce(pushDerection, ForceMode2D.Impulse);
	}
}