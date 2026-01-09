using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasingHealth : MonoBehaviour {

    // hien thi am thanh
    public SoundManager sound;
    // ID = 1; // Ma ky nang hoi mau
    private SkillController skill;

    [Header("Hieu ung hoi mau")]
    // Hieu ung
    public GameObject HieuUngTangMau;
    public EnemyAnimation ChucNangMauBay;
    private float nextTimeFly = 0f;
    public float timeFly = 0.4f;
    [Header("Hieu ung hoi chieu")]
    public CoolDownController HoiButton;
    public CoolDownController HoiImage;
    [Header("Thong so thuc thi")]
    
    public float timeOnce = 0.5f;
    private float nextTimeOnce = 0f;
    // Thong so cho lan kich hoat ky nang lan tiep theo

	// Use this for initialization
    public KeyCode KeyActive;
    public static bool ActiveUp = false; // Kich hoat bang button
    private bool Active = false; // Thuc hien chieu thuc
    private bool press = false; // Xac dinh nhan nut

	void Awake () {
        HoiImage.SetDisable();
        HoiButton.SetDisable();
	}
    public void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    protected void CheckPressKeyBoard()
    {
        // Thuc hien kiem tra nut da nhan, xac nhan da nhan nut
        // Hien thi thoi gian hoi chieu cua button
        if (skill.Status != 0)
        {
            HoiButton.ButtonClick.interactable = true;
            if (PlayerHealth.Skill && ChangePlayer.IsKnight)
            {
                if ((ActiveUp || Input.GetKeyDown(KeyActive)) && !Active && !press)
                {
                    Active = true;
                    press = true;
                    sound.Playsound("HoiMau");
                }
            }
        }
        else
        {
            // Khoa button
            HoiButton.ButtonClick.interactable = false;
        }
        // Thuc hien hoi chieu
        HoiButton.SetTimeDownForButton(ref press, skill.TimeCountDown.Current + 1);
    }

    protected void HandlingAttack()
    {
        // Dien dung
        if (Active)
        {
            //Hien hieu ung hoi mau
            HieuUngTangMau.SetActive(true);
            // Thuc hien hoi mau trong thoi gian 
            ThucHienTangMau();
            // Thuc hien hoi chieu trong thoi gian cho hinh anh
            HoiImage.SetTimeDownForImage(ref Active, skill.TimeLive.Current + 1);
            // Ket thuc thoi gian tang mau
            // Tat hieu ung
            if (!Active)
            {
                HieuUngTangMau.SetActive(false);
            }
        }
    }

    private void ThucHienTangMau()
    {
        // Hien hieu ung mau bay
        KhoiTaoMauFly();
        // Tang mau
        if (nextTimeOnce < Time.time)
        {
            PlayerHealth.control._Player.SetHealth(0,PlayerHealth.control._Player.Health.Current + skill.Receive.Current);
            nextTimeOnce = timeOnce + Time.time;
        }
    }

    private void KhoiTaoMauFly()
    {
        if (nextTimeFly < Time.time)
        {
            Instantiate(ChucNangMauBay.Graphicss, ChucNangMauBay.GunTips.position, Quaternion.Euler(0, 0, 0));
            nextTimeFly = timeFly + Time.time;
        }
    }

	// Update is called once per frame
	void Update () {
        skill = SkillManager.GetSkillByID(0, 1);
        // Kiem tra bam nut
        CheckPressKeyBoard();
        // Thuc thi ky nang
        HandlingAttack();
        
	}
}
