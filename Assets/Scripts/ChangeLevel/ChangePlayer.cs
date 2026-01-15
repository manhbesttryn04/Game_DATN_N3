using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour {
    //Am thanh
    public SoundManager sound;
 

    // Id = 3
    private SkillController skill;

	public GameObject KnightCharater;
	public GameObject DragonCharater;
    public CoolDownController AnimationPressChange;
    public CoolDownController AnimationAutoChange;
	public KeyCode KeyChange;

	public GameObject AnimationChange;
	public float timeCancel = 0.8f;

	public static bool IsKnight = true; // Kiem tra nhan vat hiep si/rong
	public static bool ActiveChange = false; // Thuc hien nhan su kien nhan nut
    private bool Active = false;
    private bool Lock = false;
    private bool AutoChange = false; //

    private bool Press = false; // Xac

    private void Awake()
    {
        AnimationPressChange.SetDisable();
        AnimationAutoChange.SetDisable();
    }

	private void CheckActiveknightCharacter(){
        if (!Lock)
        {
            IsKnight = PlayerManager.LoadChangePlayer();
        }
        else
        {
            IsKnight = true;
        }
		SetChange (IsKnight);
	}

	// Use this for initialization
	private void Start () {
		CheckActiveknightCharacter ();
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }

	// Thuc hien thay doi animation theo gameobj    ect
	public Animator SetAnimationByGameObject(){
		if (IsKnight) {
            ChangeHide.hideCharacter = KnightCharater.GetComponent<SpriteRenderer>();
			return KnightCharater.GetComponent <Animator> ();
		} else {
            ChangeHide.hideCharacter = DragonCharater.GetComponent<SpriteRenderer>();
			return DragonCharater.GetComponent <Animator>();
		}
	}

    // Thuc hien thay doi animation theo gameobj    ect
    public SpriteRenderer SetSpriteRendererByGameObject()
    {
        if (IsKnight)
        {
            return KnightCharater.GetComponent<SpriteRenderer>();
        }
        else
        {
            return DragonCharater.GetComponent<SpriteRenderer>();
        }
    }

	/// <summary>
	/// Sets the change.
	/// </summary>
	/// <param name="value">If set to <c>true</c> value.</param>
	private void SetChange(bool value){
        KnightCharater.GetComponent<SpriteRenderer>().enabled = value;
        DragonCharater.GetComponent<SpriteRenderer>().enabled = !value;
		IsKnight = value; // Gan lai doi tuong da thay doi
        //SetEnabledPlayerLaze(); // Kich hoat ky nang laze
		// Luu lai thong tin chuyen doi
        if (!Lock)
        {
            // Thuc hien luu lai trang thai chuyen doi khi trong truong hop nguoi choi da nhan duoc ky nang
            // Nhan duoc ky nang khi Lock = false
            PlayerManager.SaveChangePlayer(IsKnight);
        }
        // Lay thong tin hieu ung
		PlayerMoving.anim = SetAnimationByGameObject ();
        // Lay thong tin anh
        ChangeHide.hideCharacter = SetSpriteRendererByGameObject();
	}

    private void SetEnabledPlayerLaze()
    {
        if (!IsKnight)
        {
            KnightCharater.GetComponent<PlayerLaze>().enabled = false;
        }
        else
        {
            KnightCharater.GetComponent<PlayerLaze>().enabled = true;
        }
    }

    private void SetAutoChang()
    {
        Active = true; // Bat tro ve trang thai ban dau
        //AutoChange = false; // Tat auto change
        PlayerHealth.control._Player.SetInfuriate(0, 0); // Set ve mac dinh
        CancelInvoke("SetAutoChang"); // Huy thuc hien cho
    }

	private void HandlingChangePlayer ()
	{
        // Chuyen duoc khi lock true;
        if (!Lock)
        {
            if (PlayerHealth.Skill)
            {
                // Kiem tra nhan nut
                if ((Input.GetKey(KeyChange) || ActiveChange) && !Active && !Press)
                {
                    sound.Playsound("BienHinh");
                    Active = true;
                    Press = true;

                }
            }
                // Tuong tac voi button
                AnimationPressChange.SetButtonInteractable(true);
                // Thuc hien hieu ung thoi gian hoi
                AnimationPressChange.SetTimeDownForButton(ref Press, skill.TimeCountDown.Current + 1);
                // An hieu ung thoi gian hoi tu dong
                AnimationAutoChange.SetDisable();
                // Huy nhan
                ActiveChange = false;
                PlayerHealth.control._Player.SetInfuriate(0, PlayerHealth.control._Player.Infuriate.Max); // Set ve mac dinh
        }
        else
        {
            // Huy tuong tac voi button
            AnimationPressChange.SetButtonInteractable(false);
            // Auto change between players
            HandlingAutoChangePlayer();
        }

		// Thuc hien chuyen doi 
        if (Active && !PlayerHealth.control.GetDie())
        {
			AnimationChange.SetActive (true); // Hien hieu ung chuyen

			Invoke ("InvokeCancelAnimation",timeCancel);
		}
	}

    private void HandlingAutoChangePlayer()
    {
        // Chuyen bi khoa khi nguoi choi chua nhan dc ky nang nay
        // Trong truong hop nay ky nang dc ap dung khi no dat max
        // Khi nguoi choi nhan duoc ky nang nay Lock se duoc load len tu file da luu lai, kiem tra ky nang nay
        // Co con bi khoa hay khong, neu khong con, thi no luon dat max va khong tu dong chuyen doi nua
        if (Lock)
        {
            if ((PlayerHealth.control._Player.Infuriate.Current >= PlayerHealth.control._Player.Infuriate.Max) && !AutoChange && !ChangeHide.TanHinh)
            {
                // Thuc hien tu dong chuyen hoa
                Active = true;
                AutoChange = true;
            }
            // Truong hop sau khi tu dong chuyen doi thanh cong, se chuyen lai thanh nguoi trong thoi gian nhat dinh 
            if (AutoChange)
            {
                AnimationAutoChange.SetTimeDownForImage(ref AutoChange, skill.TimeLive.Current + 1); // Hien hieu ung hoi chieu
                // Tro ve mac dinh
                if (!AutoChange)
                {
                    SetAutoChang();
                }
            }
            else
            {
                // Huy hien thi thoi gian hoi chiu
                AnimationAutoChange.SetDisable();
            }
        }
    }

	private void InvokeCancelAnimation(){
        Active = false;
        ActiveChange = false;
        // Mo khoa di chuyen khi chuyen doi giua cac nhan vat
        PlayerMoving.LockJump = false;
        PlayerMoving.Move = true;
		// Huy hieu ung bien hinh
		AnimationChange.SetActive (false);
		// Thuc hien hien cac doi tuong can thiet
		if (!IsKnight) {
			SetChange (true);
		}
		else {
			SetChange (false);
			// false: hien rong, an hiep si
		}
		CancelInvoke ("InvokeCancelAnimation");
	}

	// Update is called once per frame
	private void Update () {
        skill = SkillManager.GetSkillByID(-1, 3);
        // Khoa ky nang
        if (skill.Status == 0)
        {
            Lock = true;
        }
        else
        {
            Lock = false;
        }
		HandlingChangePlayer ();
	}
}
