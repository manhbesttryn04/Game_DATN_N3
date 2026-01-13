using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour {

    public GameObject PanelKnight;
    public GameObject PanelDragon; 

	// Update is called once per frame
	void Update () {
        if (ChangePlayer.IsKnight)
        {
            SetChangePanel(true);
        }
        else
        {
            SetChangePanel(false);
        }
	}

    private void SetChangePanel(bool value)
    {
        PanelKnight.SetActive(value);
        PanelDragon.SetActive(!value);
    }
}
