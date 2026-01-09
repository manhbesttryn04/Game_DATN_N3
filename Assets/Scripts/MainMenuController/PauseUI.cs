using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {
    public GameObject message;
	public static bool PauseGame = false;
    public SoundManager sound;

    // 
    [Header("Bang hien thong tin cai dat")]
    public GameObject SettingGraphics;
    public GameObject TopSetting;
    public GameObject BottomSetting;
    private bool ShowST = false;
    public GameObject MovingAndSkill; // Doi tuong gameobject
    public void ShowSettingGraphics()
    {
        // 
        MessageBoxx.control.BlockMovingAndSkill(false);
        // An UI di chuyen va ky nang
        MovingAndSkill.SetActive(false);
        PauseGame = false;
        ShowST = true;
        sound.Playsound("Click");

    }

    public void CloseSettingGraphics()
    {
        // 
        MessageBoxx.control.BlockMovingAndSkill(true);
        // An UI di chuyen va ky nang
        MovingAndSkill.SetActive(true);
        // hien doi tuong
        ShowST = false;
        sound.Playsound("Click");
    }

	// Use this for initialization
	void Start () {
        PauseGame = false;
		message.SetActive(false); // ẩn hiện khung message 
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
        HandlingPauseGame();
        ThucHienDiChuyenBangThongCaiDat();
    }

    private void ThucHienDiChuyenBangThongCaiDat()
    {
        if (ShowST)
        {
            // Di chuyen vi tri cua bang thong bao xuat hien len tren
            // Vector3.Lerp: Lam di chuyen doi tuong mot cach muot ma
            SettingGraphics.transform.position = Vector3.Lerp(SettingGraphics.transform.position, BottomSetting.transform.position, 3f * Time.deltaTime);
        }
        else
        {
            // Di chuyen vi tri cua bang thong bao ve vi tri ban dau
            SettingGraphics.transform.position = Vector3.Lerp(SettingGraphics.transform.position, TopSetting.transform.position, 3f * Time.deltaTime);
        }
    }

    private void HandlingPauseGame()
    {
        if (!PlayerHealth.control.GetDie())
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame = false;

            }
            if (PauseGame)
            {

                message.SetActive(true); // Hiện lên 
                Time.timeScale = 0; // Cho màn hình và tất cả đều dừng lại
            }
            if (!PauseGame)
            {

                message.SetActive(false);
                Time.timeScale = 1; // hoạt động bình thường trở lại
            }
        }
        else
        {
            // 
            PauseGame = false;

        }
    }

    public void pauseGame() // bắt sự kiện khi mình nhấn nút Pause bên unity
    {
        PauseGame = true;
        sound.Playsound("Click");
    }

    public void resume()
    {
        PauseGame = false;
        sound.Playsound("Click");
    }

    public void restart()
    {
        // restart này chỉ để tạm dùng
        sound.Playsound("Click");
        PauseGame = false;
        Destroy(GameObject.FindGameObjectWithTag("SystemPlayer")); // Xoa doi tuong hien tai
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // câu lệnh này sẽ đưa người chơi, chơi lại màn hiện tại
	}

    public void quit()
    {
        sound.Playsound("Click");
        PauseGame = false;
        Destroy(GameObject.FindGameObjectWithTag("SystemPlayer")); // Xoa doi tuong hien tai
        //Application.Quit(); // khi nào buil game thì mới sử dụng được
        Scene.GoSceneName("MenuChoose");// 
    }

    public void TangCapDoMoi()
    {
        sound.Playsound("Click");
        // Tang thanh kinh nghiem
        PlayerHealth.control._Player.Experience.Current = PlayerHealth.control._Player.Experience.Max;
        // An thong bao
        PauseGame = false;
    }
}
