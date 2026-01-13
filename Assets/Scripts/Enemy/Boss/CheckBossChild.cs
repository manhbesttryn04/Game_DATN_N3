using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossChild : MonoBehaviour {

    public GameObject EnemyGraphicsChildren;
    public GameObject BossShow;
    public GameObject BossGraphics;

	// Update is called once per frame
	void Update () {
        if (BossGraphics.activeSelf)
        {
            if (EnemyGraphicsChildren == null || !EnemyGraphicsChildren.activeSelf)
            {
                BossShow.SetActive(true);
            }
            else
            {
                BossShow.SetActive(false);
            }
        }
        else
        {
            BossShow.SetActive(false);
        }
	}
}
