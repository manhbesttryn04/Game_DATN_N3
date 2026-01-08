using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CoolDownController{

    public Button ButtonClick;
    public Image ImageBlock;
    public Text TextTimeDown;
    private bool Lock = false;

    public void SetButtonInteractable(bool value)
    {
        if (ButtonClick != null)
        {
            ButtonClick.interactable = value;
        }
    }

    private void SetFillAmount()
    {
        if (!Lock)
        {
            ImageBlock.gameObject.SetActive(true);
            ImageBlock.fillAmount = 1;
            Lock = true;
        }
    }

    /// <summary>
    /// Chuyen dinh dang thoi gian de the hien len giao dien
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private string GetFormatTimeDown(float time)
    {
        string stringTime = ((int)time).ToString();
        if (time < 1)
        {
            if (Math.Round(time, 1) == 0)
            {
                return "";
            }
            return Math.Round(time, 1).ToString();
        }
        
        return stringTime;
    }

    public void SetTimeDownForButton(ref bool Active,float timeDown)
    {
        if (ButtonClick != null && ImageBlock != null && TextTimeDown != null)
        {
            if (Active)
            {
                SetFillAmount(); // Dua fillAmount ve trang thai binh thuong
                ButtonClick.enabled = false; // Khoa nut lai
                float TimeCurrunt = (1 / timeDown) * Time.deltaTime; // Lay thoi gian hoi
                ImageBlock.fillAmount -= TimeCurrunt; // Thuc hien gian thoi gan
                TextTimeDown.text = GetFormatTimeDown(ImageBlock.fillAmount * timeDown); // Dinh dang lai thoi gian hien thi ra ben ngoai
                if (ImageBlock.fillAmount <= 0)
                {
                    ImageBlock.fillAmount = 0; // Mat hieu ung thoi gian hoi
                    ButtonClick.enabled = true; // Khoa nut lai
                    Active = false;// Thuc hien cho phep nhan nut
                    Lock = false; // Mo khoa cho thoi gian hoi chieu
                }
            }
        }
        else
        {
            Debug.Log("Button, Image and Text show time down not finded.");
        }
    }

    public void SetTimeDownForImage(ref bool Active, float timeDown)
    {
        if (ImageBlock != null && TextTimeDown != null)
        {
            if (Active)
            {
                SetFillAmount(); // Dua fillAmount ve trang thai binh thuong
                float TimeCurrunt = (1 / timeDown) * Time.deltaTime; // Lay thoi gian hoi
                ImageBlock.fillAmount -= TimeCurrunt; // Thuc hien gian thoi gan
                TextTimeDown.text = GetFormatTimeDown(ImageBlock.fillAmount * timeDown); // Dinh dang lai thoi gian hien thi ra ben ngoai
                if (ImageBlock.fillAmount <= 0)
                {
                    ImageBlock.fillAmount = 0; // Mat hieu ung thoi gian hoi
                    Active = false;// Thuc hien cho phep nhan nut
                    Lock = false; // Mo khoa cho thoi gian hoi chieu
                    ImageBlock.gameObject.SetActive(false);
                }
            }
            else
            {
                SetDisable(); // Cai dat tro ve mac dinh
            }
        }
        else
        {
            Debug.Log("Image and Text show time down not finded.");
        }
    }

    public void SetDisable()
    {

        ImageBlock.fillAmount = 0;
        ImageBlock.gameObject.SetActive(false);
        TextTimeDown.text = "";
    }
}
