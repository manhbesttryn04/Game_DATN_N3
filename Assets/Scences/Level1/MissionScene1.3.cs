using Unity.VisualScripting;
using UnityEngine;

public class MissionScene1c3 : MonoBehaviour
{
    public GameObject canvasMission;
    public GameObject player;
    public GameObject boss;
    public GameObject boss1;
    public bool isBoss = true;
    public GameObject box;
    public GameObject targetStarMission;
    public float meetDistance=3.0f;
    public bool isMissonActive= true;
    public EnemyHealth bossHealth;
    public EnemyHealth boss1Health;
    void Start()
    {
        canvasMission.SetActive(false);
        boss.SetActive(false);
        boss1.SetActive(false);
        box.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMeetMisson();
        DestroyBoss();
        WinBossMission();
        if (bossHealth._Enemy.Health.Current <= 0)
        {
            Destroy(boss);
        }
    }
    public void PlayerMeetMisson()
    {
        if(Vector3.Distance(player.transform.position,targetStarMission.transform.position) <= meetDistance && isMissonActive)
        {if(isBoss){
            boss.SetActive(true);
            boss1.SetActive(true); 
                isBoss=false;
            }
            canvasMission.SetActive(true);
         
        }
    }
    public void WinBossMission()
    {
        if(boss == null && boss1 == null)
        { isMissonActive=false;
            canvasMission.SetActive(false);
              box.SetActive(true);
        }
    }
    public void DestroyBoss()
    {
        if(boss1Health._Enemy.Health.Current <= 0)
        {
            Destroy(boss1);
        }
        
        }
}
