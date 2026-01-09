using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip Vang, Chem, Bomb,Thua,HoiMau,QuaiBiDanh,TanHinh,BombVaCham,BienHinh,
        PhunLua,ChuongLaze, Huc, Nhay, LevelUp, AttackEnemy, BossRongPhunLua, UpSkill, Error, Click, ClickNut;
    public AudioSource adisrc;
	// Use this for initialization
	void Start () {
        Vang = Resources.Load<AudioClip>("Vang"); 
        Chem = Resources.Load<AudioClip>("Chem");
        Bomb = Resources.Load<AudioClip>("Bomb");
        Thua = Resources.Load<AudioClip>("Thua");
        HoiMau = Resources.Load<AudioClip>("HoiMau");
        QuaiBiDanh = Resources.Load<AudioClip>("QuaiBiDanh");
        TanHinh= Resources.Load<AudioClip>("TanHinh");
        BombVaCham = Resources.Load<AudioClip>("BombVaCham");
        BienHinh = Resources.Load<AudioClip>("BienHinh");
        PhunLua = Resources.Load<AudioClip>("PhunLua");
        ChuongLaze = Resources.Load<AudioClip>("ChuongLaze");
        Huc = Resources.Load<AudioClip>("Huc");
        Nhay = Resources.Load<AudioClip>("Jump");
        LevelUp = Resources.Load<AudioClip>("LevelUp");
        AttackEnemy = Resources.Load<AudioClip>("AttackEnemy");
        BossRongPhunLua = Resources.Load<AudioClip>("BossRongPhunLua");
        UpSkill = Resources.Load<AudioClip>("UpSkill");
        Error = Resources.Load<AudioClip>("Error");
        Click = Resources.Load<AudioClip>("Click");
        ClickNut = Resources.Load<AudioClip>("ClickNut");
        adisrc = GetComponent<AudioSource>();
    }
	
	public void Playsound(string clip)
    {
        switch(clip)
        {
            case "Vang":
                adisrc.clip = Vang;
                adisrc.PlayOneShot(Vang, 0.7f); break;
            case "Chem":
                adisrc.clip = Chem;
                adisrc.PlayOneShot(Chem, 0.7f); break;
            case "Bomb":
                adisrc.clip = Bomb;
                adisrc.PlayOneShot(Bomb, 0.7f); break;
            case "Thua":
                adisrc.clip = Thua;
                adisrc.PlayOneShot(Thua, 0.7f); break;
            case "HoiMau":
                adisrc.clip = HoiMau;
                adisrc.PlayOneShot(HoiMau, 1f); break;
            case "QuaiBiDanh":
                adisrc.clip = QuaiBiDanh;
                adisrc.PlayOneShot(QuaiBiDanh, 0.7f); break;
            case "TanHinh":
                adisrc.clip = TanHinh;
                adisrc.PlayOneShot(TanHinh, 0.7f); break;
            case "BombVaCham":
                adisrc.clip = BombVaCham;
                adisrc.PlayOneShot(BombVaCham, 0.7f); break;
            case "BienHinh":
                adisrc.clip = BienHinh;
                adisrc.PlayOneShot(BienHinh, 0.7f); break;
            case "PhunLua":
                adisrc.clip = PhunLua;
                adisrc.PlayOneShot(PhunLua, 0.7f); break;
            case "ChuongLaze":
                adisrc.clip = ChuongLaze;
                adisrc.PlayOneShot(ChuongLaze, 0.7f); break;
            case "Huc":
                adisrc.clip = Huc;
                adisrc.PlayOneShot(Huc, 0.7f); break;
            case "Nhay":
                adisrc.clip = Nhay;
                adisrc.PlayOneShot(Nhay, 0.7f); break;
            case "LevelUp":
                adisrc.clip = LevelUp;
                adisrc.PlayOneShot(LevelUp, 0.7f); break; 
            case "AttackEnemy":
                adisrc.clip = AttackEnemy; 
                adisrc.PlayOneShot(AttackEnemy, 0.7f); break;
            case "BossRongPhunLua":
                adisrc.clip = BossRongPhunLua; 
                adisrc.PlayOneShot(BossRongPhunLua, 0.7f); break;
            case "UpSkill":
                adisrc.clip = UpSkill; 
                adisrc.PlayOneShot(UpSkill, 0.7f); break;
            case "Error":
                adisrc.clip = Error;
                adisrc.PlayOneShot(Error, 0.7f); break;
            case "Click":
                adisrc.clip = Click;
                adisrc.PlayOneShot(Click, 0.7f); break;
            case "ClickNut":
                adisrc.clip = ClickNut;
                adisrc.PlayOneShot(ClickNut, 0.7f); break;



        }
    }
}
