using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnd : MonoBehaviour {


    public GameObject Enm;
    public bool isBool = true;
    private void Update()
    {
      
            ShowEnmBoss();
  
    }
    private void ShowEnmBoss()
    {
        if (!Enm.activeSelf && isBool )
        {
            MessageBoxx.control.ShowSmall("Chúc mừng bạn đã hoàn thành các thử thách.","CHẤP NHẬN");
            isBool = false;
        }
    }
}
