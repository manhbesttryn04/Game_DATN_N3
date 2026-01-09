using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalController : MonoBehaviour {

    [Header("Thong tin cap hien tai")]
    public Text Mau;
    public Text SatThuong;
    public Text KinhNghiem;
    public Text No;
    public Text Vang;
    public Text Level;
    [Header("Thong tin cap tiep theo")]
    public Text MauNext;
    public Text SatThuongNext;
    public Text KinhNghiemNext;
    public Text NoNext;
    public Text VangNext;
    public Text LevelNext;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        InformationPlayer(); // Lay thong tin nhan vat
        // Thong tin cap tiep theo
        InformationNextPlayer();
	}

    private void InformationPlayer()
    {
        Player _player = PlayerHealth.control._Player; // Lay thong tin nguoi choi
        Mau.text = string.Format("{0}/{1}", _player.Health.Current, _player.Health.Max);
        SatThuong.text = string.Format("{0}", _player.Damage);
        KinhNghiem.text = string.Format("{0}/{1}", (int)_player.Experience.Current, _player.Experience.Max); ;
        No.text = string.Format("{0}/{1}", _player.Infuriate.Current, _player.Infuriate.Max); ;
        Vang.text = string.Format("{0:0,00}", _player.Gold);// 
        Level.text = "Level " + _player.Level;
    }

    private void InformationNextPlayer()
    {
        Player _player = ExperienceCharacter.ThongTinCapTiepTheo();
        MauNext.text = string.Format("{0}/{1}", _player.Health.Current, _player.Health.Max);
        SatThuongNext.text = string.Format("{0}", _player.Damage); ;
        KinhNghiemNext.text = string.Format("{0}/{1}", _player.Experience.Current, _player.Experience.Max); ;
        NoNext.text = string.Format("{0}/{1}", _player.Infuriate.Current, _player.Infuriate.Max); ;
        VangNext.text = string.Format("{0:0,00}", _player.Gold);// 
        LevelNext.text = "Level " + _player.Level;
    }
}
