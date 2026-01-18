using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BangNgoc : MonoBehaviour {

    public Text SLNgocHoa;
    public Text SLNgocTho;
    public Text SLNgocPhong;

	private int soLuongMacDinh = 99;

	// Use this for initialization
	void Start () {
		CapNgocMienPhi();
	}

    void CapNgocMienPhi() {
        // Kiểm tra và cộng cho Ngọc Hoa (ID: 1)
        if (VatPhamController.GetItemByID(1).ID != -1) {
            VatPhamController.GetItemByID(1).Quality = soLuongMacDinh;
        }

        // Kiểm tra và cộng cho Ngọc Thổ (ID: 2)
        if (VatPhamController.GetItemByID(2).ID != -1) {
            VatPhamController.GetItemByID(2).Quality = soLuongMacDinh;
        }

        // Kiểm tra và cộng cho Ngọc Phong (ID: 3)
        if (VatPhamController.GetItemByID(3).ID != -1) {
            VatPhamController.GetItemByID(3).Quality = soLuongMacDinh;
        }
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
