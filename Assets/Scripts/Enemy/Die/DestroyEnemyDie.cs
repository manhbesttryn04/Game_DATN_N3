using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyDie : MonoBehaviour {

    public GameObject EnemyGraphics;

    public GameObject BossGraphics;
    private bool KhoiTao = false;
    void Awake()
    {
        if (!EnemyGraphics.activeSelf)
        {
            BossGraphics.SetActive(true); // Hien con rong
            Destroy(this.gameObject);            
        }
    }
	// Update is called once per frame
	void Update () {
        if (!EnemyGraphics.activeSelf && !KhoiTao)
        {
            KhoiTao = true;
            BossGraphics.SetActive(true); // Hien con rong
            Destroy(this.gameObject);
        }
	}
}
