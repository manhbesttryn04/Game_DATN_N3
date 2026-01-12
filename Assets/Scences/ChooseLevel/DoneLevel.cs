using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoneLevel : MonoBehaviour {

    public GameObject enmBoss;
    public GameObject Canvas;
	// Update is called once per frame
	void Update () {
        if (!enmBoss.activeSelf)
        {
            ChooseLevel.isLevel = false;
            SceneManager.LoadScene("MenuChoose");

        }
      
    }
}
