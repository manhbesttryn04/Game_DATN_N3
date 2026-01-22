using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PauseBoss : MonoBehaviour
{//scipt dung de pause boss va player khi dang thoai chuyen
    public EnemyLaoVao bossScript;
    public BChapter2Controller bChapter2Controller;
    public TalkStroy talkStroy;
    public EnemyHealth bossHealth;
    public PlayerMoving playerMoving;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(talkStroy.GetIsActive() == true)
       {
           bossScript.enabled = false;
            bChapter2Controller.enabled = false;
            playerMoving.enabled = false;
           // Debug.Log("Boss pause");
            //Debug.Log(talkStroy.GetIsActive());
        }
       else if( talkStroy.GetIsActive() == false && bossHealth._Enemy.Health.Current >1600) {
       {
           bossScript.enabled = true;
            bChapter2Controller.enabled = true;
            playerMoving.enabled = true;
           
        }

        }else if(talkStroy.GetIsActive() == false && bossHealth._Enemy.Health.Current <= 1600)
        {
            bossScript.enabled = false;
            bChapter2Controller.enabled = false;
            
        }
    }
    public void PauseBossandPlayer()
    {
        bossScript.enabled = false;
        bChapter2Controller.enabled = false;
    }
}
