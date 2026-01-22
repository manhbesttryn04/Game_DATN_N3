using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkStroy : MonoBehaviour
{//Chuoi thoai va hinh anh nhan vat
    public string[] talkStory;
    public Sprite[] imageChatacters;
    //textMeshPro va hinh anh nhan vat
    public TextMeshProUGUI textTalk;
    public GameObject imageCharacter;
    public GameObject talkOject;
    //Chi so chuoi thoai hien tai
    public int index = 0;
    //Trang thai hien thi thoai
    public bool isActivee= true;
    public bool isNext = false;
   
   
    public void Update()
    {//Xu ly ket thuc thoai
        EndTalk();
        //Xu ly hien thi thoai
        if (isActivee)
        {
            talkOject.SetActive(true);
            LoadTalk();
           
        }
       // Debug.Log(isActivee+ "Story");
    }

    //Hien thi thoai va hinh anh nhan vat

    public void LoadTalk()
    {
        if (isNext)
        {
            imageCharacter.gameObject.GetComponent<Image>().sprite = imageChatacters[index];
            textTalk.text = talkStory[index];
            isNext = false;
        }
    }
    //Chuyen den thoai tiep theo
    public void NextTalk()
    {
        index++;
        isNext = true;

        LoadTalk();
     
    }
    //Ket thuc thoai
    public void EndTalk()
    {
        if(index >= imageChatacters.Length || index >= talkStory.Length)
        {
            isActivee = false;
        }

        if(!isActivee)
        {
            talkOject.SetActive(false);
        }
    }
    public bool GetIsActive() { 
        return isActivee;
    }
    public int GetIndex() {
        return index;
    }
    public int GetLength() {
        return talkStory.Length;
    }



}
