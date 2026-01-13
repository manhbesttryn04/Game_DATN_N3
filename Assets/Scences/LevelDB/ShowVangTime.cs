using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVangTime : MonoBehaviour {

    public GameObject Vang;
    public float time = 5;
    public void Update()
    {
        Invoke("SettingHidden", time);
    }
    private void SettingHidden()
    {
        Vang.SetActive(false);
        CancelInvoke(); 
    }
}
