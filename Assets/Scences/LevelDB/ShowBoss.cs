using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBoss : MonoBehaviour {

    public GameObject EnmBoss;
    public GameObject Enm;
    public bool isFunc = true;
    private void Update()
    {
        if (isFunc)
        {
            ShowEnmBoss();
        }
    }
    private void ShowEnmBoss()
    {
        if (!Enm.activeSelf && isFunc)
        {
            EnmBoss.SetActive(true);
            isFunc = false;
        }
    }
}
