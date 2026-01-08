using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BangNgoc : MonoBehaviour {

    public Text SLNgocHoa;
    public Text SLNgocTho;
    public Text SLNgocPhong;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (VatPhamController.GetItemByID(1).ID != -1
            && VatPhamController.GetItemByID(2).ID != -1
            && VatPhamController.GetItemByID(3).ID != -1)
        {
            SLNgocHoa.text = "x" + VatPhamController.GetItemByID(1).Quality.ToString();
            SLNgocTho.text = "x" + VatPhamController.GetItemByID(2).Quality.ToString();
            SLNgocPhong.text = "x" + VatPhamController.GetItemByID(3).Quality.ToString();
        }
	}
}
