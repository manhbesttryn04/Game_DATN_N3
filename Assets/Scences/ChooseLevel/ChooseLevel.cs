using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour {

    public GameObject ButtonHien;
    public GameObject ButtonAn;
    public GameObject ImgBlock;
    public static bool isLevel = true;
    public void Update()
    {
        if(!isLevel)
        {
            ButtonHien.SetActive(true);
            ButtonAn.SetActive(false);
            ImgBlock.SetActive(false);
        }
    }

}
