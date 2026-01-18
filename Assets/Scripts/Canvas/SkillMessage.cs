using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMessage : MonoBehaviour {

    public SoundManager sound;


    [Header("Thong tin chung")]
    public Text TenKyNang;
    public Text DiemChuyenCan;
    public Button ButtonNangCap;
    [Header("Thong tin ky nang cap hien tai")]
    public Text YeuCauCapDo;
    public Text CapDo;
    public Text LabelNhanDuoc;
    public Text NhanDuoc;
    public Text LabelThoiGianTonTai;
    public Text LabelThoiGianHoiChieu;
    public Text ThoiGianTonTai;
    public Text ThoiGianHoiChieu;
    public Text TrangThai;
    public Text MoTa;
    [Header("Thong tin ky nang cap tiep theo")]
    public Text NYeuCauCapDo;
    public Text NCapDo;
    public Text NLabelNhanDuoc;
    public Text NNhanDuoc;
    public Text NLabelThoiGianTonTai;
    public Text NLabelThoiGianHoiChieu;
    public Text NThoiGianTonTai;
    public Text NThoiGianHoiChieu;
    public Text NTrangThai;

    // Thong tin chuyen ky nang hoac chuyen nhan vat
    public static int CountSkill = 0;
    [Header("Thong tin nut dieu huong")]
    public Button ButtonLeft;
    public Button ButtonRight;

    [Header("Thong tin bang thong bao")]
    public Image panelLeft;
    public GameObject ThuocTinh;

    [Header("Hien thi vi dụ sách")]
    public Image imageBook;
    public Sprite[] ListBook;

    private string lableSatThuong = "Sát thương cơ bản:";
    private int NhanVat = 0;

    // Xac nhan ky nang nao dang duoc chon
    private SkillController skill = new SkillController();

	// Use this for initialization
	void Start () {
        skill = SkillManager.GetSkillByID(0, 0);
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //
        DiemChuyenCan.text = PlayerHealth.control._Player.PointSkill.ToString();
        // Xac nhan dang hien nhan vat nao
        LayDoiTuongNhanVatHienThi();
        // Thuc hien khoa, mo khi nhan qua trai phai
        ThucHienKhoaButtonLeftRight();
        // Thuc hien lay thong tin ky nang
        ThucHienTimKyNangTheoMa();
	}
  //  sound.Playsound("Bomb");
                         
    private void ThuHienKhoaNutNangCap(SkillController skill)
    {
        if ((skill.Level.Current >= skill.Level.Max) && skill.Status == 1)
        {
            // Khoa nut
            ButtonNangCap.interactable = false;
            // Bo hien thi thong bao thuoc tinh tiep theo
            ThuocTinh.SetActive(false);
           
        }
        else
        {
            // Khoa nut
            ButtonNangCap.interactable = true;
            // Bo hien thi thong bao thuoc tinh tiep theo
            ThuocTinh.SetActive(true);
        }
    }

    private void LayDoiTuongNhanVatHienThi()
    {
        if (!ShowAnimation.Count)
        {
            NhanVat = 0;
        }
        else
        {
            NhanVat = 1;
        }
    }
    private void ThucHienKhoaButtonLeftRight()
    {
        if (CountSkill == 0)
        {
            ButtonLeft.interactable = false;
        }
        else
        {
            ButtonLeft.interactable = true;
        }

        if (CountSkill == 3)
        {
            ButtonRight.interactable = false;
        }else{
            ButtonRight.interactable = true;
        }
    }

    private void ThucHienTimKyNangTheoMa()
    {
        if (CountSkill >= 0 && CountSkill <= 2)
        {
            // Lay thong tin ky nang 
            skill = SkillManager.GetSkillByID(NhanVat, CountSkill);
        }
        else if (CountSkill == 3)
        {
            skill = SkillManager.GetSkillByID(-1, CountSkill); // Lay ky nang chuyen rong
        }
        // Hien thi thong tin
        ThucHienTaiLenThongTinKyNang(skill);
    }

    private void ThucHienTaiLenThongTinKyNang(SkillController skill){
        // Ten ky nang
        this.TenKyNang.text = "Kỹ Năng "+(CountSkill+1).ToString() + ": " + skill.Name;
        // Cap do duoc yeu cau
        this.YeuCauCapDo.text = ThucHienLayThongtinYeuCau(skill.ID,skill.Relationship);
        // Cap do cua ky nang
        this.CapDo.text = skill.Level.Current.ToString() + "/" + skill.Level.Max.ToString();
        this.NhanDuoc.text = skill.Receive.Current.ToString();
        this.ThoiGianTonTai.text = skill.TimeLive.Current.ToString();
        this.ThoiGianHoiChieu.text = skill.TimeCountDown.Current.ToString();
        // Trang thai
        if (skill.Status == 0)
        {
            this.TrangThai.text = "Chưa được mở khóa";
            panelLeft.color = new Color(1, 0.743f, 0.5896226f, 1);
        }
        else
        {
            this.TrangThai.text = "Mở";
            panelLeft.color = new Color(0.6559555f, 0.9811321f, 0.6340334f,1);
        }

        this.MoTa.text = skill.Description;

        // Thuc hien khoa nut nang cap
        ThuHienKhoaNutNangCap(skill);
        // Hien thi thong cap tiep theo
        TaiLenCapDoKeTiepCuaKyNang(skill.NextLevel());
        // An mot so thong tin voi ky nang
        ThucHienAnVaDoiTenDoiTuongKhongCanThiet(skill);
    }

    private string ThucHienLayThongtinYeuCau(int id, int thuoc)
    {
        if (skill.IDNgoc != -1)
        {
            if (skill.IDNgoc == 1)
            {
                // Hien hinh
                ShowBook(0);
            }
            else if (skill.IDNgoc == 3)
            {
                // Hien hinh
                ShowBook(2);
            }
            else
            {
                // Hien hinh
                ShowBook(1);
            }
            //
            return skill.ConditionLevelPlayer.ToString() + " và " + VatPhamController.GetItemByID(skill.IDNgoc).Name;  
        }
        imageBook.enabled = false;
        return skill.ConditionLevelPlayer.ToString(); 
    }

    private void ShowBook(int book)
    {
        imageBook.enabled = true; // Hien anh
        imageBook.sprite = ListBook[book]; // Chon anh
    }

    private void ThucHienAnVaDoiTenDoiTuongKhongCanThiet(SkillController skill)
    {
        SetActive(2, true);
        SetActive(3, true);
        SetActive(1, true);
        // Thuc hien an doi tuong khong can thiet
        ThucHienAnThuocTinhDanhThuongCuaHaiNhanVat(skill);

        //// Thuc hien doi ten doi tuong cua ky nang tan hinh
        EditTitleSkillAddHealth(skill);

        //// An thuoc tinh sat thuong voi ky nang tan hinh cua nhan vat
        ThucHienAnThuocTinhKyNangTanHinhHiepSi(skill);

        //// An thuoc tinh sat thuong va thoi gian ton tai cua chuyen doi
        ThucHienAnThuocTinhKyNangHoanDoi(skill);

        //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">1 sat thuong | 2 Ton tai | 3 Hoi Chieu</param>
    /// <param name="value"></param>
    private void SetActive(int index, bool value)
    {
        if (index == 1)
        {
            // Sat thuong co ban
            LabelNhanDuoc.gameObject.SetActive(value);
            NhanDuoc.gameObject.SetActive(value);
            // Sat thuong co ban cap tiep theo
            NLabelNhanDuoc.gameObject.SetActive(value);
            NNhanDuoc.gameObject.SetActive(value);

        }
        else if (index == 2)
        {
            ThoiGianTonTai.gameObject.SetActive(value);
            LabelThoiGianTonTai.gameObject.SetActive(value);

            NThoiGianTonTai.gameObject.SetActive(value);
            NLabelThoiGianTonTai.gameObject.SetActive(value);
        }
        else if (index == 3)
        {
            LabelThoiGianHoiChieu.gameObject.SetActive(value);
            ThoiGianHoiChieu.gameObject.SetActive(value);
            NLabelThoiGianHoiChieu.gameObject.SetActive(value);
            NThoiGianHoiChieu.gameObject.SetActive(value);
        }
    }

    private void ThucHienAnThuocTinhKyNangHoanDoi(SkillController skill)
    {
        if (skill.ID == 3) // Hoan doi
        {
            SetActive(1,false);
            SetActive(2, false);
        }
    }


    private void ThucHienAnThuocTinhKyNangTanHinhHiepSi(SkillController skill)
    {
        if (skill.ID == 2 && NhanVat == 0) // An than
        {
            SetActive(1, false);
        }
    }

    private void EditTitleSkillAddHealth(SkillController skill)
    {
        if (skill.ID == 1 && NhanVat == 0) // Hoi mau
        {
            LabelNhanDuoc.text = "Lượng máu hồi/0.5s:";
            NLabelNhanDuoc.text = "Lượng máu hồi/0.5s:";
        }
        else
        {
            LabelNhanDuoc.text = lableSatThuong;
            NLabelNhanDuoc.text = lableSatThuong;
        }
    }

    private void ThucHienAnThuocTinhDanhThuongCuaHaiNhanVat(SkillController skill)
    {
        if (skill.ID == 0)
        {
            SetActive(2,false);
            SetActive(3,false);
        } 
    }

    private void TaiLenCapDoKeTiepCuaKyNang(SkillController skill)
    {
        // Cap do duoc yeu cau
        this.NYeuCauCapDo.text = skill.ConditionLevelPlayer.ToString();
        // Cap do cua ky nang
        this.NCapDo.text = skill.Level.Current.ToString() + "/" + skill.Level.Max.ToString();
        this.NNhanDuoc.text = skill.Receive.Current.ToString();
        this.NThoiGianTonTai.text = skill.TimeLive.Current.ToString();
        this.NThoiGianHoiChieu.text = skill.TimeCountDown.Current.ToString();
        // Trang thai
        this.NTrangThai.text = "Chưa mở khóa";

        // An mot so thong tin voi ky nang

    }

    public void MovingLeft()
    {
        if (CountSkill > 0)
        {
            CountSkill--;
            sound.Playsound("Click");
        }
    }

    public void MoingRight()
    {
        if (CountSkill < 3)
        {
            CountSkill++;
            sound.Playsound("Click");
        }
    }

    public void ThucHienNangCapKyNang()
    {
        // Kiem tra diem chuyen can
        if (PlayerHealth.control._Player.PointSkill > 0)
        {
            // Ky nang dang bi khoa
            if (skill.Status == 0)
            {
               
                // Kiem tra dieu kien cap cua nhan vat, co dat yeu cau
                if (skill.ConditionLevelPlayer <= PlayerHealth.control._Player.Level)
                {
                   
                    // Kiem tra cac dieu dien khac
                    if (skill.IDNgoc == -1)
                    {
                        skill.Status = 1; // Mo khoa ky nang
                        PlayerHealth.control._Player.PointSkill--;
                        sound.Playsound("UpSkill");
                        //// Hien thong bao chua du diem
                        //MessageBoxx.control.ShowSmall("Bạn đã nâng cấp thành công kỹ năng này.", "CHẤP NHẬN", new Color(1, 0.7163402f, 0.4627451f, 1));
                    }
                    else if (skill.IDNgoc != -1)
                    {
                       
                        VatPham vp = VatPhamController.GetItemByID(skill.IDNgoc);
                        // Can kiem tra nguoi choi da co ngoc hay chua      
                        if (vp.Quality > 0)
                        {
                            skill.Status = 1; // Mo khoa ky nang
                            PlayerHealth.control._Player.PointSkill--;
                            // Giam so luong ngoc
                            vp.Quality--;
                            VatPhamController.UpdateItem(vp);
                        }
                        else{
                            sound.Playsound("Error");
                            // Hien thong bao chua du diem
                            MessageBoxx.control.ShowSmall("Bạn chưa tìm thấy " + vp.Name + " để nâng cấp kỹ năng này. \nHãy quay lại sau nhé!", "CHẤP NHẬN");
                        }
                      
                    }
                }
                else 
                {
                    sound.Playsound("Error");
                    // Hien thong bao chua du diem
                    MessageBoxx.control.ShowSmall("Có vẻ như, trình độ bạn chưa đủ để học kỹ năng này.\n Hãy rèn thêm và quay lại!!", "CHẤP NHẬN");
                }
            }
            else if (skill.Level.Max > 1)
            {

                // Ky nang nang cap da dat max
                if (skill.Level.Current < skill.Level.Max)
                {
                  
                    // Ky nang da mo, nang len cap moi
                    if (SkillManager.IncreasingLevel(skill.NextLevel()))
                    {
                        sound.Playsound("UpSkill");
                        PlayerHealth.control._Player.PointSkill--;
                        //MessageBoxx.control.ShowSmall("Bạn đã nâng cấp thành công kỹ năng này.\n Tăng sức mạnh của mình lên gấp bội!!", "CHẤP NHẬN", new Color(1, 0.7163402f, 0.4627451f, 1));
                    }
                }
            }
        }
        else
        {
            sound.Playsound("Error");
            // Hien thong bao chua du diem
            MessageBoxx.control.ShowSmall("Điểm chuyên cần của bạn chưa đủ để nâng cấp, bạn hãy quay lại sau nhé!!", "CHẤP NHẬN");
        }
    }
}
