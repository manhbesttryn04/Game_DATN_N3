using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public SoundManager sound;
    [Header("Doi tuong tuy chinh")]
    public GameObject TableOptions; // Bang thong bao
    public GameObject Top;
    public GameObject Bottom;
    public GameObject MovingAndSkill; // Doi tuong gameobject
    // Quan ly xuat hien
    private bool ShowUp = false; // Xac nhan len xuong

    [Header("Bang hien thong tin ca nhan")]
    public GameObject PersonalGraphics;
    public GameObject TopPersonal;
    public GameObject BottomPersonal;
    private bool ShowSeflt = false; // Xac nhan len xuong

    [Header("Bang hien thong tin ky nang")]
    public GameObject SkillGraphics;
    public GameObject TopSkill;
    public GameObject BottomSkill;
    private bool ShowSkill = false;

    [Header("Bang hien thong tin ngoc")]
    public GameObject NgocGraphics;
    public GameObject TopNgoc;
    public GameObject BottomNgoc;
    private bool ShowNgoc = false;
    public void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    public void ShowSkillGraphics()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            // 
            MessageBoxx.control.BlockMovingAndSkill(false);
            // An UI di chuyen va ky nang
            MovingAndSkill.SetActive(false);
            // hien doi tuong
            // Xac nhan di chuyen len
            ShowUp = false;
            ShowSkill = true;
            sound.Playsound("Click");
        }

    }

    public void CloseSkillGraphics()
    {
        // 
        MessageBoxx.control.BlockMovingAndSkill(true);
        // An UI di chuyen va ky nang
        MovingAndSkill.SetActive(true);
        // hien doi tuong
        ShowSkill = false;
        sound.Playsound("Click");
    }


    public void ShowPersonal()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            // 
            MessageBoxx.control.BlockMovingAndSkill(false);
            // An UI di chuyen va ky nang
            MovingAndSkill.SetActive(false);
            // hien doi tuong
            // Xac nhan di chuyen len
            ShowUp = false;
            ShowSeflt = true;
            sound.Playsound("Click");
        }
    }

    public void ClosePersonal()
    {
        // 
        MessageBoxx.control.BlockMovingAndSkill(true);
        // An UI di chuyen va ky nang
        MovingAndSkill.SetActive(true);
        // hien doi tuong
        //PersonalGraphics.SetActive(false);
        ShowSeflt = false;
        sound.Playsound("Click");
    }

    public void ShowOption()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            // 
            MessageBoxx.control.BlockMovingAndSkill(false);
            // An UI di chuyen va ky nang
            MovingAndSkill.SetActive(false);
            // Xac nhan di chuyen len
            ShowUp = true;
            sound.Playsound("Click");
        }
    }

    public void ShowBangNgoc()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            // 
            MessageBoxx.control.BlockMovingAndSkill(false);
            // An UI di chuyen va ky nang
            MovingAndSkill.SetActive(false);
            // Xac nhan di chuyen len
            ShowNgoc = true;
            ShowUp = false;
            sound.Playsound("Click");
        }
    }

    public void CloseBangNgoc()
    {
        // Mo 
        MessageBoxx.control.BlockMovingAndSkill(true);
        // An
        MovingAndSkill.SetActive(true);
        // Xac nhan di chuyen len
        ShowNgoc = false;
        sound.Playsound("Click");
    }

    public void CloseOption()
    {
        // Mo 
        MessageBoxx.control.BlockMovingAndSkill(true);
        // An
        MovingAndSkill.SetActive(true);
        // Xac nhan di chuyen len
        ShowUp = false;
        sound.Playsound("Click");
    }

	// Update is called once per frame
	void Update () {
        // Hien thong bao
        XuLyHienThongBao();
        //
        XuLyDongThongBaoBangNut();
        // Xu ly hien tu chon
        ThucHienDiChuyenBangThongBaoLen();
        // Xu ly hien thong tin bang than
        ThucHienDiChuyenBangThongBaoBanThan();
        // Xu ly hien thong tin ky nang
        ThucHienDiChuyenBangThongBaoKyNang();
        // Xu ly bang ngoc
        ThucHienDiChuyenBangNgoc();
	}

    private void XuLyHienThongBao()
    {
        // Nhan phim F1 de hien option
        if (Input.GetKey(KeyCode.F1))
        {
            ShowOption();
        }
    }

    private void XuLyDongThongBaoBangNut()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //
            if (ShowUp)
            {
                CloseOption();
            }
            //
            if (ShowSeflt)
            {
                ClosePersonal();
            }
            //
            if (ShowSkill)
            {
                CloseSkillGraphics();
            }

            if (ShowNgoc)
            {
                CloseBangNgoc();
            }
        }
    }

    private void ThucHienDiChuyenBangThongBaoKyNang()
    {
        if (ShowSkill)
        {
            // Di chuyen vi tri cua bang thong bao xuat hien len tren
            // Vector3.Lerp: Lam di chuyen doi tuong mot cach muot ma
            SkillGraphics.transform.position = Vector3.Lerp(SkillGraphics.transform.position, BottomSkill.transform.position, 3f * Time.deltaTime);
        }
        else
        {
            // Di chuyen vi tri cua bang thong bao ve vi tri ban dau
            SkillGraphics.transform.position = Vector3.Lerp(SkillGraphics.transform.position, TopSkill.transform.position, 3f * Time.deltaTime);
        }
    }

    private void ThucHienDiChuyenBangThongBaoBanThan()
    {
        if (ShowSeflt)
        {
            // Di chuyen vi tri cua bang thong bao xuat hien len tren
            // Vector3.Lerp: Lam di chuyen doi tuong mot cach muot ma
            PersonalGraphics.transform.position = Vector3.Lerp(PersonalGraphics.transform.position, BottomPersonal.transform.position, 3f * Time.deltaTime);
        }
        else
        {
            // Di chuyen vi tri cua bang thong bao ve vi tri ban dau
            PersonalGraphics.transform.position = Vector3.Lerp(PersonalGraphics.transform.position, TopPersonal.transform.position, 3f * Time.deltaTime);
        }
    }

    private void ThucHienDiChuyenBangThongBaoLen()
    {
        if (ShowUp)
        {
            // Di chuyen vi tri cua bang thong bao xuat hien len tren
            // Vector3.Lerp: Lam di chuyen doi tuong mot cach muot ma
            TableOptions.transform.position = Vector3.Lerp(TableOptions.transform.position, Top.transform.position,3f*Time.deltaTime);
        }
        else
        {
            // Di chuyen vi tri cua bang thong bao ve vi tri ban dau
            TableOptions.transform.position = Vector3.Lerp(TableOptions.transform.position, Bottom.transform.position, 3f * Time.deltaTime);   
        }
    }

    private void ThucHienDiChuyenBangNgoc()
    {
        if (ShowNgoc)
        {
            // Di chuyen vi tri cua bang thong bao xuat hien len tren
            // Vector3.Lerp: Lam di chuyen doi tuong mot cach muot ma
            NgocGraphics.transform.position = Vector3.Lerp(NgocGraphics.transform.position, TopNgoc.transform.position, 3f * Time.deltaTime);
        }
        else
        {
            // Di chuyen vi tri cua bang thong bao ve vi tri ban dau
            NgocGraphics.transform.position = Vector3.Lerp(NgocGraphics.transform.position, BottomNgoc.transform.position, 3f * Time.deltaTime);
        }
    }
}
