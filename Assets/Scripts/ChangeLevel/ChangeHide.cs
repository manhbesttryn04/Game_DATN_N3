using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHide : MonoBehaviour {
    // Am thanh
    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    // ID = 2; // Ma ky nang tan hinh
    private SkillController skill = new SkillController();

    public CoolDownController HienHoiChieu;
    public CoolDownController HienThoiGianTanHinh;
    public GameObject AnimationChange;
    public float timeCancelAnimation = 0.4f; // Thoi gian huy hieu ung boc hoi
    public KeyCode KeyAn;

    private bool PresButton = false; // Xac nhan nut, thuc hien nhan tac dong hoi chieu
    private bool showAnimation = true;
    public static SpriteRenderer hideCharacter; //
    public static bool TanHinh = false; // Xac nhan tan hinh
    public static bool Active = false; // Nhan tac dong kich hoat khong can nhat nut, moi khi no bang true
    public void Awake()
    {
        HienThoiGianTanHinh.SetDisable();
        HienHoiChieu.SetDisable();
    }

    private void HienTanHinh()
    {
        // Neu da nhan nut
        if (PresButton && !PlayerHealth.control.GetDie())
        {
            // Hien thoi gian hoi chieu
            HienHoiChieu.SetTimeDownForButton(ref PresButton, skill.TimeCountDown.Current+1);
        }
        // Thuc hien tan hinh
        if (TanHinh)
        {
            // Lam mo nhan vat di 50%
            hideCharacter.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            // Xu ly thoi gian hien, an
            HienThoiGianTanHinh.SetTimeDownForImage(ref TanHinh, skill.TimeLive.Current+1); // Ap dung, cho nut
        }
        // Thuc hien huy tan hinh voi cac dieu kien ben duoi
        if ((!TanHinh || PlayerHealth.control.GetDie()) && PresButton) // Sau thoi gian hien, thi no se an
        {
            HuyTanHinh(); // Thuc hien huy tan hinh
            if(showAnimation){
                AnimationChange.SetActive(true);
                showAnimation = false;
            }
        }
    }

    /// <summary>
    /// Nhan su kien nhan nut
    /// </summary>
    private void XuLyNhanNutTanHinh()
    {
        if (ChangePlayer.IsKnight)
        {
            if (skill.Status != 0)
            {
                HienHoiChieu.ButtonClick.interactable = true;
                if (PlayerHealth.Skill)
                {
                    // Nhan tac dong
                    if ((Active || Input.GetKey(KeyAn)) && !TanHinh && !PresButton) // bien thanh tan hinh. 
                    {
                        sound.Playsound("TanHinh");
                        TanHinh = true; // Nhan su kien thuc hien tan hinh tan hinh
                        PresButton = true; // Nhan tac dong nhan nut
                        showAnimation = true; // Hien hieu ung
                        // Xu ly hien hieu ung tan hinh
                        AnimationChange.SetActive(true);

                    }
                }
            }
            else
            {
                HienHoiChieu.ButtonClick.interactable = false;
            }
        }
    }

    private void HuyTanHinh()
    {
        TanHinh = false; // Tat tan hinh
        // Lam mo nhan vat di 100%
        hideCharacter.color = new Color(1.0f, 1.0f, 1.0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        skill = SkillManager.GetSkillByID(0, 2);
        XuLyNhanNutTanHinh(); // Xac nhan da nhan nut
        HienTanHinh(); // Thuc hien xu ly
        if (!TanHinh)
        {
            // Lam mo nhan vat di 50%
            hideCharacter.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        }
    }
}
