using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChuongController : MonoBehaviour {

    [Header("Thong tin")]
    public Button ButtonChuong;
    public Image ImageBoss;
    public Image Block;
    //
    [Header("Thong tin dieu kien mo khoa")]
    public IndexBoss boss;
    //public int Level;
    //
    //[Header("Thong tin bang thong bao")]
    //public GameObject panelMessage;
    //public Text textMessage;
    //private bool Lock = true;
	// Use this for initialization
	void Start () {
        // Lay lock
        //
	}
	
	// Update is called once per frame
	void Update () {
        if (EnemyManager.CheckIndexBoss(EnemyManager.LoadBoss(), boss))
        {
            ButtonChuong.interactable = true;
            ImageBoss.color = new Color(1,1,1,1);
            Block.enabled = false;
        }
        else
        {
            ButtonChuong.interactable = false;
            ImageBoss.color = new Color(0,0,0,1);
            Block.enabled = true;
        }
	}

    //public void UnLock()
    //{
    //    if (!EnemyManager.CheckIndexBoss(EnemyManager.LoadBoss(), boss))
    //    {
    //        if (PlayerManager.HasPlayGame())
    //        {
    //            if (PlayerManager.LoadInformation().Level < Level)
    //            {
    //                ShowMessage(string.Format("Bạn cần đạt cấp độ {} và phải tiêu diệt được tên cai trị ", Level,GetNameBoss(boss)));
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Lock = false;
    //        // Luu lock

    //    }
    //}

    //public string GetNameBoss(IndexBoss boss){
    //    if (boss == IndexBoss.Chapter1)
    //    {
    //        return "SA MẠC CÁT";
    //    }
    //    else if (boss == IndexBoss.Chapter2)
    //    {
    //        return "RỪNG SÂU THẲM";
    //    }
    //    else if (boss == IndexBoss.Chapter3)
    //    {
    //        return "THUNG LŨNG CHẾT";
    //    }
    //    return "";
    //}

    //private void ShowMessage(string message)
    //{
    //    textMessage.text = message;
    //    panelMessage.SetActive(true);
    //}

    //public void CloseMessage()
    //{
    //    panelMessage.SetActive(false);
    //}
}
