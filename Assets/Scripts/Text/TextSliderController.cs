using System;
using UnityEngine;
using UnityEngine.UI;

public class TextSliderController : MonoBehaviour {

	public Slider playerSlider;
	private Text txtText;
    public bool Pesent = false; 
	//
	private void Awake(){
		txtText = GetComponent <Text>();	
	}

	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
        // Khong phai la phan tram
        if (!Pesent)
        {
            txtText.text = string.Format("{0}/{1}", playerSlider.value.ToString(), playerSlider.maxValue.ToString());
        }
        else // La phan tram
        {
            float phanTram = ((playerSlider.value * 100)/ playerSlider.maxValue);
            txtText.text = string.Format("{0}%", Math.Round(phanTram, 2).ToString()); 
        }
	}
}
