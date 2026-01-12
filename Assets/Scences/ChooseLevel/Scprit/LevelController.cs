using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : ChuongController {

    public int indexLevel;
    public Image TableRequirment;
    private Text textYeuCau;

    private void Awake()
    {
        textYeuCau = TableRequirment.GetComponentInChildren<Text>();
    }

	// Use this for initialization
	void Start () {
		textYeuCau.text = "Yêu cầu cấp " + GetLevelByScene();
	}

    public int GetLevelByScene()
    {
        // Lay vi tri scene hien tai
        return (indexLevel - 1) * PlayerHealth.nextLevel;
    }

	// Update is called once per frame
	void Update () {
        
        if (PlayerManager.LoadInformation() != null && PlayerManager.LoadInformation().Level >= GetLevelByScene())
        {
            this.ButtonChuong.interactable = true;
            this.ImageBoss.color = new Color(1, 1, 1, 1);
            this.Block.enabled = false;
            // Doi mau
            TableRequirment.color = new Color(1,1,1,1);
        }
        else
        {
            this.ButtonChuong.interactable = false;
            this.ImageBoss.color = new Color(0, 0, 0, 1);
            this.Block.enabled = true;
        }
	}
}
