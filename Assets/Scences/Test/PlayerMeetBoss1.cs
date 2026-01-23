using UnityEngine;

public class PlayerMeetBoss1 : MonoBehaviour
{ //GameObject player, bossSoi
    public GameObject player;
    public GameObject bossSoi;
    //Thoai boss soi va player
    public TalkStroy talkStroy;
    public TalkStroy talkStroy1;
    //Khoang cach de bat dau thoai
    public float distance;
    //Di chuyen cua boss soi
    public enemyMovingWalk enemyMovingWalk;
    //Sinh mau cua boss soi
    public EnemyHealth enemyHealth;
    //Hinh anh boss soi chet
    public GameObject bossDieImage;
    //Vi tri sinh hinh anh boss soi chet
    public GameObject tagerInstaneImageSoiDie;
    //Hinh anh boss soi chet
    public GameObject soiDie;
    public bool isBossSoiDie = true;
    //Hieu ung boc hoi khi boss chet
    public GameObject HieuUngBocHoiBossDie;
    public bool isHieuUngBossDie = true;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        MeetBossSoi();
        BossSoiChet();
    }
    public void MeetBossSoi()
    {
        if(bossSoi != null)
        {
            if(Vector3.Distance(player.gameObject.transform.position,bossSoi.gameObject.transform.position)<= distance&& talkStroy.GetIndex() < talkStroy.GetLength())
            {
                //bossSoi.gameObject.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
                enemyMovingWalk.enabled = false;
                talkStroy.isActivee = true;
                Time.timeScale = 0f;
            }
            else if( talkStroy.GetIndex() >= talkStroy.GetLength()){
               enemyMovingWalk.enabled = true;
                Time.timeScale = 1f;
            }
        }else Debug.Log("Khong co boss soi");
    }
    public void BossSoiChet()
    {
        if(enemyHealth._Enemy.Health.Current <=0 && talkStroy1.GetIndex() < talkStroy1.GetLength())
        {   if(isBossSoiDie){
                GameObject SoiDie = Instantiate(bossDieImage, new Vector3(tagerInstaneImageSoiDie.transform.position.x,-5.4f,tagerInstaneImageSoiDie.transform.position.z), Quaternion.identity);
                soiDie = SoiDie;
                
                isBossSoiDie = false;
             
            }
            talkStroy1.isActivee = true;

        }
        else if(talkStroy1.GetIndex() >= talkStroy1.GetLength())
        {
         
            Destroy(soiDie);
            if(isHieuUngBossDie){ 
                GameObject hieuUngBocHoi = Instantiate(HieuUngBocHoiBossDie, new Vector3(soiDie.transform.position.x,soiDie.transform.position.y,soiDie.transform.position.z), Quaternion.identity);
                hieuUngBocHoi.SetActive(true);
                Destroy(hieuUngBocHoi, 0.5f);
                isHieuUngBossDie = false;
            }
        }
} }
