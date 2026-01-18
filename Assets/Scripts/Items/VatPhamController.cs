using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatPhamController : MonoBehaviour {
    public static VatPhamController control;
    private static List<VatPham> listVatPham = new List<VatPham>();
    public GameObject[] Items;
    public bool Load = true;
    void Awake()
    {
        if (control == null)
        {
            control = this;
        }
        //
        if (Load)
        {
            if (PlayerManager.LoadItem() == null)
            {
                KhoiTaoDanhSachVatPham();
            }
            else
            {
                listVatPham = PlayerManager.LoadItem();
            }
        }
        else
        {
            KhoiTaoDanhSachVatPham();
        }
    }

    private void KhoiTaoDanhSachVatPham()
    {
        listVatPham.Add(new VatPham(1,"Ngọc hỏa",0,false));
        listVatPham.Add(new VatPham(2, "Ngọc thổ", 0, false));
        listVatPham.Add(new VatPham(3, "Ngọc phong", 0, false));
        listVatPham.Add(new VatPham(4, "Ngọc băng", 0, false));
        listVatPham.Add(new VatPham(5, "Tăng máu", 200f));
        listVatPham.Add(new VatPham(6, "Tăng sát thương", 10, 5f));
        listVatPham.Add(new VatPham(7, "Kim cương vàng", 200f));
        listVatPham.Add(new VatPham(8, "Kim cương xanh", 400f));
        listVatPham.Add(new VatPham(9, "Kim cương xanh lá", 600f));
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerManager.SaveItem(listVatPham);
	}

    public static VatPham GetItemByID(int id)
    {
        if (listVatPham.Count > 0)
        {
            // Tim trong danh sach
            foreach (VatPham VP in listVatPham)
            {
                // kiem tra loai ky nang
                if (id == VP.ID)
                {
                    return VP;
                }
            }
        }
        return null;
    }

    public static bool UpdateItem(VatPham Item)
    {
        if (listVatPham.Count > 0)
        {
            // Tim trong danh sach
            for (int i = 0; i < listVatPham.Count; ++i)
            {
                // Kiem tra  cua nhan vat nao
                if (Item.ID == listVatPham[i].ID)
                {
                    listVatPham[i] = Item;
                    return true;
                }
            }
        }
        return false;
    }

    public GameObject GetItem(int index)
    {
        if (index < Items.Length)
        {
            return Items[index];
        }
        return null;
    }
}
