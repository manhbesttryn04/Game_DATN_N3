using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageBoxx : MonoBehaviour {
    public static MessageBoxx control; // Dung de truy xuat ra ben ngoai

    // Bang thong bao lon dung de truy xuat ra ben ngoai 
    // Bang thong bao nho dung khi da hien bang thong bao lon
    [Header("Thong tin bang thong bao lon")]
    public GameObject MessageBig; // Bang thong bao
    public Text TextMessageBig; // Chu hien thi
    private bool isBig = false;
    [Header("Thong tin bang thong bao nho")]
    public GameObject MessageSmall; // Bang thong bao
    public Text TextMessageSmall; // Chu hien thi
    public Text TextButtonSmall; // Chu cua button
    [Header("Thong bao tang cap do")]
    public GameObject MessageLevel; // Bang thong bao
                                    // Use this for initialization
    public SoundManager sound;
    // Use this for initialization
  
    void Start () {
        if (control == null)
        {
            control = this;
        }
        MessageBig.SetActive(false); // Mac dinh la khong hien len
        MessageSmall.SetActive(false); // Mac dinh la khong hien len
        MessageLevel.SetActive(false);

        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMessageLevel();
        }
	}

    public void CloseMessageLevel()
    {
        MessageLevel.SetActive(false);
        sound.Playsound("Click");

    }

    public void ShowMessageLevel()
    {
        MessageLevel.SetActive(true);
    }

    // Thuc hien hien thi bang thong bao
    public void ShowBig(string text)
    {
        isBig = true;
        // Chan di chuyen
        BlockMovingAndUseSkill(false);
        // Hien thi bang thong bao
        MessageBig.SetActive(true);
        // Hien thi chu thong bao
        TextMessageBig.text = text;
    }

    // Thuc hien hien thi bang thong bao
    public void ShowSmall(string textMessage,string textButton)
    {
        // Chan di chuyen
        BlockMovingAndUseSkill(false);
        // Hien thi bang thong bao
        MessageSmall.SetActive(true);
        // Hien thi chu thong bao
        TextMessageSmall.text = textMessage;
        // Hien text button
        TextButtonSmall.text = textButton;
        //
    }

    private int CheckSceneIndex(int LevelLoad){

        if (LevelLoad >= 1 && LevelLoad <= 5)
        {
            return 1;
        }
        else if (LevelLoad >= 6 && LevelLoad <= 9)
        {
            return 6;
        }
        else if (LevelLoad >= 10 && LevelLoad <= 13)
        {
            return 10;
        }
        return 14;
    }

    private void BlockMovingAndUseSkill(bool value)
    {
        PlayerMoving.Move = value; // Chan di chuyen
        PlayerMoving.LockJump = !value; // Chan nhay
        // Tat hieu ung di chuyen
        if (PlayerMoving.anim.GetBool("Walk"))
        {
            PlayerMoving.anim.SetBool("Walk", value);
        }
        // Chan su dung skill
        PlayerHealth.Skill = value;
    }

    public void ThuHienTroVeNoiBatDau()
    {
        sound.Playsound("Click");
        // Tro ve noi dau tien cua moi chuong
        // Tat hop thoai
        Destroy(GameObject.FindGameObjectWithTag("SystemPlayer")); // Xoa doi tuong hien tai
        MessageBig.SetActive(false);
        // Thuc hien kiem tra canh hien tai cua chuong nao
        // Kiem tra thong qua so levelload trong build setting, sau do kiem tra canh hien tai nam o dau
        ChangeDoor.SetLoadScene(Direction.Left, CheckSceneIndex(SceneManager.GetActiveScene().buildIndex)); // Thuc hien load toi canh dau tien cua chuong
    }

    public void ThucHienHoiSinh(){
      
        // Kiem tra tien co du de hoi sinh
        if (PlayerHealth.control._Player.Gold >= 500000)
        {
            sound.Playsound("Click");
            // Neu so tien du
            // Tru so tien voi tien
            PlayerHealth.control._Player.Gold -= 500000;
            PlayerHealth.control._Player.SaveInformation(); // Luu lai thong tin xuong he thong
            // Load lai scen hien tai
            Destroy(GameObject.FindGameObjectWithTag("SystemPlayer")); // Xoa doi tuong hien tai
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // câu lệnh này sẽ đưa người chơi, chơi lại màn hiện tại
        }
        else
        {
            sound.Playsound("Click");
            // Nguoc lai neu so tien trong tui nho hon so tien hoi sinh
            // Hien bang thong thong bao nho
            this.ShowSmall("Vàng của bạn không đủ để hồi sinh, hãy quay lại sau nhé?", "ĐỒNG Ý!"); 
        }
    }

    public void HideSmail()
    {
        MessageSmall.SetActive(false); // An bang thong bao nho
        if (!isBig)
        {
            //// Chan di chuyen
            BlockMovingAndUseSkill(true);
            sound.Playsound("Click");
        }
     
    }

    public void HideBig()
    {
        // Chan di chuyen
        BlockMovingAndUseSkill(true);
        isBig = false;
        MessageBig.SetActive(false); // An bang thong bao nho
        sound.Playsound("Click");
    }

    public void BlockMovingAndSkill(bool value)
    {
        PlayerHealth.Skill = value; // Khoa thuc hien skill
        PlayerMoving.Move = value; // Khoa di chuyen qua lai
        PlayerMoving.LockJump = !value; // Kho nhay
    }
}
