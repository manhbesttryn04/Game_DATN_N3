using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour {

    private Image myImage;
    public Image youImage; 
	// Use this for initialization
    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

	// Update is called once per frame
	void Update () {
        myImage.color = youImage.color;
	}
}
