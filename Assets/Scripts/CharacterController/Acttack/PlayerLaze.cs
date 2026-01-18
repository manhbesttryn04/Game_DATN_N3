using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaze : MonoBehaviour {
    //Am thanh
    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    /// </summary>
    // ID = 2
    private SkillController skill = new SkillController(); // Thong tin ky nang

	public GameObject ObjectGraphics;
    public CoolDownController AnimationCoolDown;
	public KeyCode KeyActive;
	public bool Move = false;
	public string NameParameter;

	public static bool Active = false;
	protected bool press = false;

    public static bool ActiveLaze = false;

    private void Awake()
    {
        AnimationCoolDown.SetDisable();
    }

	public bool GetActive(){
		return Active;
	}

	public void SetActive(bool value){
		Active = value;
	}

	protected void CheckPressKeyBoard ()
	{
        if (skill.Status == 1)
        {
            AnimationCoolDown.ButtonClick.interactable = true;
            if (PlayerHealth.Skill && !ChangePlayer.IsKnight)
            {
                if ((ActiveLaze || Input.GetKeyDown(KeyActive)) && !Active && !press && this.gameObject.activeSelf && !KyNangHuc.Active)
                {
                    sound.Playsound("ChuongLaze");
                    Active = true;
                    press = true;
                }
            }
        }
        else
        {
            AnimationCoolDown.ButtonClick.interactable = false;
        }
        AnimationCoolDown.SetTimeDownForButton(ref press, skill.TimeCountDown.Current + 1);
	}

	protected void HandlingAttack ()
	{
		if (Active && !PlayerHealth.control.GetHurt ()) {
            ThayDoiThongSo(true);
            PlayerMoving.anim.SetBool("Walk", false);
            Invoke("InvokeCancelAnimation", skill.TimeLive.Current);
		}
	}

    private void ThayDoiThongSo(bool value)
    {
        KyNangChuong.LockAttack = value;
        ObjectGraphics.SetActive(value);
        //
        if (!ChangePlayer.IsKnight)
        {
            SetAnimation(value);
        }
        
    }
	protected void HandlingUpdate ()
	{
		CheckPressKeyBoard ();
		HandlingAttack ();
	}
	
	// Update is called once per frame
	private void Update () {
        skill = SkillManager.GetSkillByID(1, 1); // Lay thong tin
         HandlingUpdate();
        //
         if ((ChangePlayer.IsKnight || PlayerMoving.h != 0 || PlayerMoving.Jump) && Active)
         {
            InvokeCancelAnimation();
         }
	}

	protected void InvokeCancelAnimation(){
        ThayDoiThongSo(false);
        Active = false;
        ActiveLaze = false; // Huy active laze
		CancelInvoke ("InvokeCancelAnimation");
	}

    private void SetAnimation(bool value)
    {
        PlayerMoving.anim.SetBool(NameParameter, value);
    }
}
