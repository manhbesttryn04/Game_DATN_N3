using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportController : MonoBehaviour {

    public InputField Mau;
    public InputField No;
    public InputField CapDo;
    public InputField SatThuong;
    public InputField Vang;

    public Text Message;

    private bool Show = false;
    private void UpLoadInformation()
    {
        Mau.text = PlayerHealth.control._Player.Health.Max.ToString();
        No.text = PlayerHealth.control._Player.Infuriate.Max.ToString();

        CapDo.text = PlayerHealth.control._Player.Level.ToString();
        SatThuong.text = PlayerHealth.control._Player.Damage.ToString();
        Vang.text = PlayerHealth.control._Player.Gold.ToString();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!Show)
        {
            UpLoadInformation();
            Show = true;
        }
	}

    public void Hack()
    {
        // Mau
        if (Mau.text != "")
        {
            PlayerHealth.control._Player.Health.Max = Convert.ToInt32(Mau.text);
            PlayerHealth.control._Player.Health.Current = PlayerHealth.control._Player.Health.Max;
        }
        // No
        if (No.text != "")
        {
            PlayerHealth.control._Player.Infuriate.Max = Convert.ToInt32(No.text);
            PlayerHealth.control._Player.Infuriate.Current = PlayerHealth.control._Player.Infuriate.Max - 10;
        }
        // Cap do
        if (CapDo.text != "")
        {
            PlayerHealth.control._Player.Level = Convert.ToInt32(CapDo.text);
        }
        // Cap do
        if (SatThuong.text != "")
        {
            PlayerHealth.control._Player.Damage = Convert.ToInt32(SatThuong.text);
        }
        // Cap do
        if (Vang.text != "")
        {
            PlayerHealth.control._Player.Gold = Convert.ToInt32(Vang.text);
        }
        //
        Show = false;
    }
}
