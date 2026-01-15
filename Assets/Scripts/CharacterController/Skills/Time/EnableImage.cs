using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableImage : MonoBehaviour {

    private Button btn;
    private Image img;
    private void Awake()
    {
        btn = GetComponentInParent<Button>();
        img = GetComponent<Image>();
    }

	// Update is called once per frame
	void Update () {
        if (btn.interactable)
        {
            img.enabled = true;
        }
        else
        {
            img.enabled = false;
        }
	}
}
