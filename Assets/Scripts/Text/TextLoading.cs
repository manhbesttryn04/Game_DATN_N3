using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoading : MonoBehaviour {
    private Text textLoading;
    private float nextTime = 0f;

    private void Awake()
    {
        textLoading = GetComponent<Text>();
    }
	// Use this for initialization
	void Start () {
        textLoading.text = "Loading";
	}
	
	// Update is called once per frame
	void Update () {
        if (nextTime < Time.time)
        {
            textLoading.text += ". ";
            if (textLoading.text.Length >= 15)
            {
                textLoading.text = "Loading";
            }
            nextTime = 0.2f + Time.time;
        }
	}
}
