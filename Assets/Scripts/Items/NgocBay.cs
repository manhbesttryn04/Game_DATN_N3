using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NgocBay : MonoBehaviour {

    public int ID;
    private bool Fly = false;
    private Rigidbody2D myRigid;
    private bool VaCham = false;
    void Awake() {
        myRigid = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Fly)
        {
            ThucHienChoPhepVatPhamBay();
            Fly = false;
        }
	}

    private void ThucHienChoPhepVatPhamBay()
    {
        // Cap nhat nguoi choi da nhan duoc ngoc
        VatPham vp = VatPhamController.GetItemByID(ID); // Lay thong tin vat pham
        // Thay doi da nha
        vp.Revice = true;
        vp.Quality = 1;
        VatPhamController.UpdateItem(vp);
        // Cho trong luc = 0;
        myRigid.gravityScale = 0;
        // + y
        myRigid.AddForce(new Vector2(0, 1) * 2, ForceMode2D.Impulse);
        Destroy(gameObject, 1);
        if(ID !=4)
        {
            MessageBoxx.control.ShowSmall("Chúc mừng bạn đã tìm thấy được " + vp.Name + ", hãy vào nâng cấp kỹ năng liền nhé.", "CHẤP NHẬN");
        }
        // Hien thong bao chua du diem
        else 
        {
            MessageBoxx.control.ShowSmall("Chúc mừng bạn vượt qua các thử thách.", "CHẤP NHẬN");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground") || other.collider.CompareTag("Falling") || other.collider.CompareTag("Player") && !VaCham)
        {
            VaCham = true;
            // Xac nhan bay
            Fly = true;
        }
    }
}
