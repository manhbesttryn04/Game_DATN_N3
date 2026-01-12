 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    [Header("Thong tin button")]
    public Button buttonPrev; // Nut quay lai
    public Button buttonNext; // Nut tiep theo

    [Header("Thong tin anh")]
    public Image ImageLoad; // Noi de hien thi anh
    public Text textMoTa;
    public Text textContainer;
    public Scrollbar scrollText;
    public float speed = 0.0005f;
    private List<string> ListDescription = new List<string>();

    [Header("Thong tin doi tuong khac")]
    public GameObject panelIndex; // Doi tuong hien thi cac doi tuong ky hieu
    public GameObject panelGarenal;
    public GameObject panelEnd;
    public Button IndexSeleted; // Doi tuong duoc them vao panel Index
    public Button IndexDeleted;
    //
    private Image ImageSeleted; // Doi tuong duoc them vao panel Index
    private Image ImageDeleted;
    //
    private List<Sprite> listSpriteUp = new List<Sprite>(); // Danh sach anh load len tu he thong
    private int countIndex = 0; // Vi tri cua mang
    private Sprite[] image;
    private List<Image> ListImageButtonSelect = new List<Image>();
    public bool End = false;
    public bool IsEnd = true;
    private void Awake()
    {
        // Lay hinh
        ImageSeleted = IndexSeleted.GetComponent<Image>();
        ImageDeleted = IndexDeleted.GetComponent<Image>();
        // Lay anh tu he thong len
        image = Resources.LoadAll<Sprite>("Up");
        // Chuyen thanh list
        ConvertArrayToList(image);
        ContructorDescription();
    }

    private void ContructorDescription()
    {
        // Hien thi mo ta huong dan
        ListDescription.Add("1. Thông tin cơ bản của nhân vật.\nBao gồm:\n\t- Máu\n\t- Cấp độ\n\t- Kinh nghiệm\n\t- Thông tin kẻ địch\n\t- Thời gian thực hiện kỹ năng\n\t- và Vàng "); // 1
        ListDescription.Add("2. Thao tác di chuyển nhân vật.\n - Di chuyển qua trái: A/PageLeft\n - Di chuyển qua phải: D/PageRight\n - Nhảy lên: W/PageUp\n - Di chuyển xuống: S/PageDown."); // 2
        ListDescription.Add("3. Thao tác thực hiện kỹ năng của nhân vật."); // 8
        ListDescription.Add("4. Hiển thị bảng tùy chọn cho người chơi."); // 3
        ListDescription.Add("5. Hiển thị thông tin chi tiết của người chơi."); // 5
        ListDescription.Add("6. Hiển thị thông tin bảng ngọc, cần thiết để người chơi dễ dàng tìm thấy những viên ngọc."); // 6
        ListDescription.Add("7. Lưu trữ thông tin kỹ năng của người chơi và cho phép thực hiện nâng cấp các kỹ năng.\nMỗi khi đạt cấp mới bạn sẽ nhận được một điểm(+1) chuyên cần."); // 7
        ListDescription.Add("8. Bạn có thể di xuống những nơi có bản chỉ dẫn hướng xuống.\nBằng cách nhấn phím D/PageDown."); // 8
        ListDescription.Add("9. Bạn nên cẩn thật với những vũng nước, vì bạn không biết bơi."); // 8
    }

    private void IconSelect(Button button, int i)
    {
        // Lay doi tuong vua them
        Button objectBtn = Instantiate(button, button.transform);
        // Tao doi tuong con cho pannelIndex
        objectBtn.transform.SetParent(panelIndex.transform);
        // Gan su kien
        objectBtn.onClick.AddListener(delegate { SelectGUI(i); }); 
        // Hien doi tuong
        objectBtn.gameObject.SetActive(true);
        ListImageButtonSelect.Add(objectBtn.GetComponent<Image>());
    }

    private void ConvertArrayToList(Sprite[] image)
    {
        if (listSpriteUp.Count == 0)
        {
            // Doi mang
            for (int i = 0; i < image.Length; i++)
            {
                listSpriteUp.Add((Sprite)image[i]);
            }
        }
    }  

    // Use this for initialization
	void Start () {
        scrollText.value = 1;
        // Them nut
        ThucHienThemCacNutChon();
	}

    private void ThucHienThemCacNutChon()
    {
        for (int i = 0; i < listSpriteUp.Count; i++)
        {
            if (countIndex == i)
            {
                IconSelect(IndexSeleted, i);
            }
            else
            {
                IconSelect(IndexDeleted, i);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        // kiem tra nhan
        if (countIndex == 0)
        {
            buttonPrev.enabled = false;
        }
        else { buttonPrev.enabled = true; }
        //
        if(countIndex >= listSpriteUp.Count){
            End = true;
        }
        
            // Thuc hien hien thi huong dan ban dau
            if (!End)
            {
                panelEnd.SetActive(false);
                panelGarenal.SetActive(true);
                ImageLoad.sprite = listSpriteUp[countIndex]; // Anh hien tai
                SetSelection();
            }
            else if (IsEnd)
            {
                panelEnd.SetActive(true);
                panelGarenal.SetActive(false);
                // Hien thi cot chuyen
                ShowContainerGame();
            }
	}

    private void ShowContainerGame()
    {
        if(scrollText.value != 0){
            scrollText.value -= speed;
        }
    }

    private int dex = 0;

    private void SetSelection()
    {
        // Anh da chon
        ListImageButtonSelect[countIndex].sprite = ImageSeleted.sprite;
        // Hien mo ta
        textMoTa.text = ListDescription[countIndex];
        // Bo icon da chon
        if (dex != countIndex)
        {
            ListImageButtonSelect[dex].sprite = ImageDeleted.sprite;
            dex++;
            if (dex >= listSpriteUp.Count)
            {
                dex = 0;
            }
        }
    }

    // Thuc hien tang vi tri
    public void clickPrev()
    {
        if (countIndex != 0)
        {
            countIndex--;
        }
    }

    // Thuc hien giam vi tri
    public void clickNext()
    {
        if (countIndex < listSpriteUp.Count)
        {
            countIndex++;
        }
    }

    public void clickGuiNext()
    {
        if (countIndex < listSpriteUp.Count - 1)
        {
            countIndex++;
        }
    }

    public void SelectGUI(int index)
    {
        countIndex = index;
    }

    public void PlayGame(string name)
    {
        Scene.GoSceneName(name); // Chay toi level 1.1
    }
}
