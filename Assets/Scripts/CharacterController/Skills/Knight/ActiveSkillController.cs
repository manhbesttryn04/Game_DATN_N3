using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveSkillController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler 
{
    private Button buttonActive;

    private void Awake()
    {
        buttonActive = GetComponent<Button>();
    }

    private void Update()
    {
        if (!PlayerHealth.Skill || PlayerHealth.control.GetDie())
        {
            if (buttonActive.interactable)
            {
                buttonActive.enabled = false;
            }
        }
        else
        {
            buttonActive.enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        SetActiveSkill(true);
    }

    private void SetActiveSkill( bool value)
    {
        ////////////// Knight
        // Danh thuong
        if (gameObject.name == "danhThuongKnight")
        {
            PlayerDanhThuong.Active = value; // Thuc hien ky nang chem
        }
        // Laze
        if (gameObject.name == "lazeKnight")
        {
            PlayerLaze.ActiveLaze = value; // Thuc hien ky nang laze
        }
        // Tan hinh
        if (gameObject.name == "tanHinhKnight")
        {
            // Thuc hien ky nang tan hinh
            ChangeHide.Active = value;
        }
        // Hoi mau cho nhan vat
        if (gameObject.name == "hoiMauKnight")
        {
            // Thuc hien ky nang tan hinh
            IncreasingHealth.ActiveUp = value;
        }

        ////////////// Dragon
        // Danh thuong
        if (gameObject.name == "danhThuongDragon")
        {
            KyNangChuong.Active = value; // Thuc hien ky nang phun lua
        }
        // Huc
        if (gameObject.name == "hucDragon")
        {
            KyNangHuc.ActiveHuc = value; // Thuc hien ky nang laze
        }

        // Chuyen 
        if (gameObject.name == "chuyenPlayer")
        {
            ChangePlayer.ActiveChange = value; // Thuc hien ky nang chuyen doi nhan vat
        }
    }

    // Thuc hien xu ly nha nut
    public void OnPointerUp(PointerEventData data)
    {
        SetActiveSkill(false);
    }
}
