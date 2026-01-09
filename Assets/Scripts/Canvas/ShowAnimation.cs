using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAnimation : MonoBehaviour {

    // Doi tuong hien thi
    public GameObject Knight;
    public GameObject Dragon;
    // Vi tri xuat hien
    public GameObject Top;
    public GameObject Center;
    public GameObject Bottom;
    // Cho rong
    public GameObject CenterRong;
    // Nut dieu khien
    public Button ButtonTop;
    public Button ButtonBottom;

    public Text NamePlayer;

    public static bool Count = false;
    public SoundManager sound;
	// Use this for initialization
	void Start () {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
        ThucHienThayDoiDoiTuong();
        //
        ThucHienDiChuyen();
	}

    private void ThucHienThayDoiDoiTuong()
    {
        if (!Count)
        {
          
            ButtonBottom.interactable = false;
            ButtonTop.interactable = true;
            NamePlayer.text = "HIẾP SĨ ALADUT";
        }
        else if (Count)
        {
          
            ButtonBottom.interactable = true;
            ButtonTop.interactable = false;
            NamePlayer.text = "RỒNG DOMATO";
        }
    }

    private void ThucHienDiChuyen()
    {
        if (!Count)
        {
         
            // Di chuyen vi tri cua bang thong bao xuat hien len tren
            // Vector3.Lerp: Lam di chuyen doi tuong mot cach muot ma
            Knight.transform.position = Vector3.Lerp(Knight.transform.position, Center.transform.position, 3f * Time.deltaTime);
            Dragon.transform.position = Vector3.Lerp(Dragon.transform.position, Bottom.transform.position, 3f * Time.deltaTime);
        }
        else if (Count)
        {
           
            // Di chuyen vi tri cua bang thong bao ve vi tri ban dau
            Knight.transform.position = Vector3.Lerp(Knight.transform.position, Top.transform.position, 3f * Time.deltaTime);
            Dragon.transform.position = Vector3.Lerp(Dragon.transform.position, CenterRong.transform.position, 3f * Time.deltaTime);
        }
    }

    public void MovingUp()
    {
        if (!Count)
        {
            // Tang len 1
            Count = true;
            SkillMessage.CountSkill = 0; // Hien lai ky nang dau tien
            sound.Playsound("Click");
        }
    }

    public void MovingDown()
    {
        if (Count)
        {
            // Tang len 1
            Count = false; // Hien 
            sound.Playsound("Click");
            SkillMessage.CountSkill = 0; // Hien lai ky nang dau tien
        }
    }
}
