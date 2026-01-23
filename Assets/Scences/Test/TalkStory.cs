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
    //Thoi gian chay chu
    public float timeDelay = 1f;
    //Thoi gian dem hien thi chu
    public float timeDelayCurrent = 0f;
    //Toc do chay chu
    public float SpeedDelay = 0.01f;
    //Trang thai bat dau dem thoi gian
    public bool StartDelay = false;


    public void Update()
    {//Xu ly ket thuc thoai
        EndTalk();
       StartTalk();
         BoDemTuDongChayTalk();

    }
    //Bat dau thoai
    public void StartTalk()
    {
        if (isActivee)
        {
            talkOject.SetActive(true);
            LoadTalk();
            StartDelay = true;

        }
    }
    //Tu dong chay thoai
    public void BoDemTuDongChayTalk(){
        if (StartDelay)
        {
            timeDelayCurrent += SpeedDelay * Time.unscaledDeltaTime;
        }

        if (timeDelayCurrent >= timeDelay)
        {
            index++;
            isNext = true;
            LoadTalk();
            timeDelayCurrent = 0f;
        }
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
            StartDelay = false;
            timeDelayCurrent = 0f;
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
