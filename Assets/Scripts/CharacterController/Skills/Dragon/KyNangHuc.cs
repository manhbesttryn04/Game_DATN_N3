using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyNangHuc : MonoBehaviour {

    //Am thanh
    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    // ID = 1
    private SkillController skill = new SkillController(); // Thong tin ky nang

    public GameObject ObjectGraphics;
    public CoolDownController AnimationCoolDown;
    public KeyCode KeyActive;
    public bool Move = false;
    public string NameParameter;

    public static bool Active = false;
    protected bool press = false;

    public static bool ActiveHuc = false;

	public float forceAttack = 20f;
	private bool Cancel = false;
    private bool boolCancel = false;

    private void Awake()
    {
        AnimationCoolDown.SetDisable();
    }

	private void HandlingAttackHuc ()
	{
        if (Active)
        {
			// Code thuc hien
			PlayerHealth.control.SetMienKhang (true); // Tat hieu ung vi tat cong
			// Thuc hien bi day toi
            ThucHienThemLucDay();
			// Khoa truc z,y
			PlayerMoving.myBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			// Hien cac hieu ung khac...
			Cancel = true;
            ThayDoiCacThaoTac(true);
            Invoke("InvokeCancelAnimationChildren", skill.TimeLive.Current);
		}
	}

    private void ThucHienThemLucDay()
    {
        if (PlayerMoving.myBody.gameObject.transform.localScale.x < 0)
        {
            PlayerMoving.myBody.AddForce(new Vector2(-forceAttack, 0f), ForceMode2D.Force);
        }
        else
        {
            PlayerMoving.myBody.AddForce(new Vector2(forceAttack, 0f), ForceMode2D.Force);
        }
    }

    protected void CheckPressKeyBoard()
    {
        if (skill.Status == 1)
        {
            AnimationCoolDown.ButtonClick.interactable = true;
            if (PlayerHealth.Skill && !ChangePlayer.IsKnight)
            {
                if ((ActiveHuc || Input.GetKeyDown(KeyActive)) && !Active && !press && this.gameObject.activeSelf && !PlayerLaze.Active)
                {
                    sound.Playsound("Huc");
                    Active = true;
                    press = true;
                }
            }
        }
        else
        {
            AnimationCoolDown.ButtonClick.interactable = false;
        }
        //
        if (press)
        {
            AnimationCoolDown.SetTimeDownForButton(ref press, skill.TimeCountDown.Current + 1);
            if (!press)
            {
                PlayerMoving.Move = true;
                PlayerMoving.LockJump = false;
            }
        }
    }

    private void ThayDoiCacThaoTac(bool value)
    {
        KyNangChuong.LockAttack = value;
        ObjectGraphics.SetActive(value);
        PlayerMoving.LockJump = value;
        SetAnimation(value);
    }
    
	// Update is called once per frame
	public void Update () {
        skill = SkillManager.GetSkillByID(1, 1); // Lay thong tin
        CheckPressKeyBoard();

        HandlingAttackHuc();
        // Huy ky nang
        if (((ChangePlayer.IsKnight && !boolCancel) || PlayerMoving.h != 0 || PlayerMoving.Jump) && Active)
        {
            boolCancel = true;
            InvokeCancelAnimationChildren();
        }
        else
        {
            boolCancel = false;
        }
	}

	private void InvokeCancelAnimationChildren(){
		// Khoa z
		PlayerMoving.myBody.constraints = RigidbodyConstraints2D.FreezeRotation;

		PlayerHealth.control.SetMienKhang (false); // Tat hieu ung vi tat cong
		if (Cancel) {
			Cancel = false;
			PlayerMoving.myBody.linearVelocity = Vector2.zero;
            Active = false;
		}
        InvokeCancelAnimation(); // Huy
		CancelInvoke ("InvokeCancelAnimationChildren");
	}

    protected void InvokeCancelAnimation()
    {
        ObjectGraphics.SetActive(false);
        if (press && Active)
        {
            if (PlayerMoving.anim != null)
            {
                SetAnimation(false);
            }
        }
        if (press)
        {
            Active = false;
            ActiveHuc = false; // Huy active laze
            KyNangChuong.LockAttack = false;
        }
        ThayDoiCacThaoTac(false);
    }

    private void SetAnimation(bool value)
    {
        if(!ChangePlayer.IsKnight){
            PlayerMoving.anim.SetBool(NameParameter, value);
        }
    }
}
