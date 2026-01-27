using System.Collections;                 // Dùng cho Coroutine (IEnumerator, WaitForSeconds)
using System.Linq;
using TMPro;                              // TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement;        // Load scene
using UnityEngine.UI;                     // Button, UI

public class StartCutsScene : MonoBehaviour
{
    [Header("Story Cut Scene")]

    public GameObject[] ojImageStory;      // Danh sách hình ảnh tương ứng từng đoạn story
    public string[] storyText;             // Nội dung chữ của story
    public TextMeshProUGUI storyTextBox;   // Ô hiển thị text
    public Button buttonSkip;              // Nút Skip
    public GameObject buttonPlay;          // Nút Play (hiện khi hết story)

    public int index = 0;                  // Vị trí story hiện tại
    public bool isTyping = false;           // Đang đánh chữ hay không
    public bool isPlaying = false;          // Đã bấm Play chưa

    public AudioSource musicBG;             // Nhạc nền
    public float typingSpeed = 0.05f;       // Tốc độ gõ chữ
    public float delay = 1f;                // Thời gian chờ giữa các đoạn story
    public float currentDelay = 0;          // Thời gian delay hiện tại
    public float speedDelay = 1f;           // Tốc độ cộng delay
    public bool isDelay = true;             // Có đang delay không

    public TextCutScene textCut;

    void Start()
    { 
        UpdateTextCutScene();          // Cập nhật text từ ScriptableObject
        StartStory();                       // Bắt đầu story ngay khi vào scene
    }

    void Update()
    {
        EndStory();                         // Kiểm tra đã hết story chưa
        RunStory();                         // Chạy logic story
    }

    // ===================== CHẠY STORY =====================
    public void RunStory()
    {
        // Nếu CHƯA bấm Play
        if (!isPlaying)
        {
            // Nếu đang trong trạng thái delay
            if (isDelay)
            {
                currentDelay += speedDelay * Time.deltaTime;

                // Khi delay đủ thời gian
                if (currentDelay >= delay)
                {
                    index++;               // Sang đoạn story tiếp theo
                    isTyping = true;       // Cho phép gõ chữ
                    NextStory();           // Chạy story mới
                    currentDelay = 0;
                    isDelay = false;
                }
            }

            // Khi gõ chữ xong (text hiện đầy đủ)
            if (storyTextBox.text == storyText[index] && index <= storyText.Length - 1)
            {
                isDelay = true;            // Bắt đầu delay để sang đoạn mới
            }

            // Nếu là đoạn cuối thì không delay nữa
            if (index >= storyText.Length - 1)
                isDelay = false;
        }
        else
        {
            // Nếu đã bấm Play thì tắt textbox
            storyTextBox.gameObject.SetActive(false);
        }
    }

    // ===================== STORY TIẾP THEO =====================
    public void NextStory()
    {
        if (isTyping)
        {
            // Tắt tất cả hình
            for (int i = 0; i < ojImageStory.Length; i++)
            {
                ojImageStory[i].SetActive(false);
            }

            // Bật hình tương ứng với index
            ojImageStory[index].SetActive(true);

            // Gõ chữ
            StartCoroutine(TypeText(storyText[index]));
            isTyping = false;
        }
    }

    // ===================== STORY BAN ĐẦU =====================
    public void StartStory()
    {
        // Tắt hết hình
        for (int i = 0; i < ojImageStory.Length; i++)
        {
            ojImageStory[i].SetActive(false);
        }

        // Bật hình đầu tiên
        ojImageStory[index].SetActive(true);

        // Gõ đoạn đầu
        StartCoroutine(TypeText(storyText[index]));
    }

    // ===================== BẤM PLAY =====================
    public void PlayGame(string namescene)
    {
        isPlaying = true;
        isTyping = false;

        musicBG.enabled = false;            // Tắt nhạc nền
        storyTextBox.text = "";             // Xóa chữ

        buttonSkip.gameObject.SetActive(false);
        buttonPlay.gameObject.SetActive(false);

        // Load scene Loading trước
        SceneManager.LoadScene("Load", LoadSceneMode.Additive);
        StartCoroutine(Loading(namescene));
    }

    // ===================== LOAD SCENE =====================
    public IEnumerator Loading(string namescene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(namescene);
    }

    // ===================== KẾT THÚC STORY =====================
    public void EndStory()
    {
        // Nếu đang ở đoạn cuối
        if (index >= storyText.Length - 1)
        {
            if (!isPlaying)
            {
                buttonPlay.SetActive(true);     // Hiện nút Play
                buttonSkip.gameObject.SetActive(false);
            }
        }
    }
    public void UpdateTextCutScene()
    {
        for(int i = 0; i < textCut.texts.Length; i++)
        {
            storyText[i] = textCut.texts[i];
        }
    }

    // ===================== HIỆU ỨNG GÕ CHỮ =====================
    public IEnumerator TypeText(string text)
    {
        storyTextBox.text = "";

        foreach (char c in text)
        {
            storyTextBox.text += c;         // Gõ từng chữ
            yield return new WaitForSeconds(typingSpeed);
            buttonSkip.interactable = true;
        }
    }

    // code của Mạnh 😎
}
