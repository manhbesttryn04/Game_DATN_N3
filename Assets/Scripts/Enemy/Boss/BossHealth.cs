using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    public static BossHealth control;

    public GameObject BackGround;
    public Slider enemySlider;
    public Image Fill;

    private BossCollection bossColl;
    private GameObject EnemyBoss;

    void Awake()
    {
        if (control == null)
        {
            control = this;
        }
    }

	// Use this for initialization
	void Start () {
        if (control == null)
        {
            control = this;
        }
        EnemyBoss = null;
	}

    void Update()
    {
        if (control == null)
        {
            control = this;
        }
        if (EnemyBoss == null) // Khi chua co
        {
            if (GameObject.FindGameObjectWithTag("BossCon") != null)
            {
                // Tim doi tuong
                EnemyBoss = GameObject.FindGameObjectWithTag("BossCon");
            }
            else if (GameObject.FindGameObjectWithTag("Boss") != null)
            {
                // Tim doi tuong
                EnemyBoss = GameObject.FindGameObjectWithTag("Boss");
            }

            if (EnemyBoss != null)
            {
                if (EnemyBoss.activeSelf && hasComponent.HasComponent<BossCollection>(EnemyBoss))
                {
                    bossColl = EnemyBoss.GetComponent<BossCollection>(); // Lay thong tin, de kiem tra boss da chet chua
                }
            }
        }
        //
        if (bossColl != null && EnemyBoss.activeSelf && bossColl.EnemyGraphics.activeSelf) // Khi da co, kiem tra da chet hay chua
        {
            SetActiveHealth(true); // Hien thanh mau
        }
        else
        {
            SetActiveHealth(false); // Hien thanh mau
        }
    }

    private void SetActiveHealth(bool value)
    {
        BackGround.SetActive(value);
        enemySlider.gameObject.SetActive(value);
    }
}
