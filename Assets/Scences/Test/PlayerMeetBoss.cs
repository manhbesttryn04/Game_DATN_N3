using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMeetBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    //Thoai boss va player
    public TalkStroy TalkStroy;
    public TalkStroy TalkStroy1;
    //Tag cua boss
    public string nameTagBoss;
    //Vitri khoang cach de bat dau thoai
    public float tagetDistance;
    //Mau cua boss
    public EnemyHealth bossHealth;
    public GameObject bossHealthBar;
    //GameObject bossRun
    public GameObject bossRun;
    //Vi tri boss chay den khi sap chet
    public  GameObject TransformBossChat2;
    public GameObject TransformBossTron;
    //SpriteRenderer boss
    public SpriteRenderer spriteRenderer;
    //Pause boss va player
    public PauseBoss pauseBoss;
    //Roi vat thuong
    public GameObject ngocLua;
    public GameObject viTriRoi;
    //boxcolider boss
    public PolygonCollider2D bossCollider;
    public GameObject[] boxChan;
  

    public bool isDrop = true;
   
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag(nameTagBoss);
        player = GameObject.FindGameObjectWithTag("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
       MeetBoss();
        BossChayKhiSapChet();
    }
    //Khi gap boss thi bat dau thoai
    public void MeetBoss()
    {
        if(boss != null) {
            if(Vector3.Distance(player.transform.position, boss.transform.position) < tagetDistance && TalkStroy.GetIndex() < TalkStroy.GetLength()) {
             
                TalkStroy.isActivee = true;
               // Debug.Log(TalkStroy.GetIndex());
            }
            }else Debug.Log("Boss is null");
    }
    //Boss chay khi sap chet
    public void BossChayKhiSapChet()
    {//Boss chay den vi tri thoai khi sap chet
        if (bossHealth._Enemy.Health.Current <= 1600&& TalkStroy1.GetIndex() < TalkStroy1.GetLength())
        { //pause dung tan cong va lao vao
            pauseBoss.PauseBossandPlayer();
            //tat box collider de khong bi anh huong boi va cham
            bossCollider.enabled = false;
            //Boss chay den vi tri thoai
            spriteRenderer.gameObject.GetComponent<Transform>().localScale = new Vector3(2.5f, 2.5f, 1f);
            bossRun.gameObject.transform.position = Vector2.MoveTowards(bossRun.gameObject.transform.position, TransformBossChat2.gameObject.transform.position, 20f * Time.deltaTime);
            if(Vector3.Distance(bossRun.transform.position, TransformBossChat2.gameObject.transform.position) < 0.2f)
            {//Boss dung lai va bat dau thoai
                spriteRenderer.gameObject.GetComponent<Transform>().localScale = new Vector3(-2.5f, 2.5f, 1f);
                pauseBoss.playerMoving.enabled = false;
                TalkStroy1.isActivee = true;
            }
            
        }else if(TalkStroy1.GetIsActive() == false && bossHealth._Enemy.Health.Current <= 1600)
        { Destroy(boxChan[0]);
            Destroy(boxChan[1]);
          
            //Sau khi thoai xong thi cho phep di chuyen
            pauseBoss.playerMoving.enabled = true;
            spriteRenderer.gameObject.GetComponent<Transform>().localScale = new Vector3(2.5f, 2.5f, 1f);
            bossRun.gameObject.transform.position = Vector2.MoveTowards(bossRun.gameObject.transform.position, TransformBossTron.gameObject.transform.position, 10f * Time.deltaTime);
            //Roi vat thuong
            if(isDrop)
            {
                GameObject ngoc = Instantiate(ngocLua, viTriRoi.transform.position,Quaternion.identity);
                if(ngoc != null)
                {
                    Debug.Log("Roi vat thuong thanh cong");
                }else
                {
                    Debug.Log("Roi vat thuong that bai"); }
                 isDrop = false;

            }
        }


    }
   
}
