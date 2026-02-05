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

    [Header("Hien thi vi du sach")]
    public Image imageBook;
    public Sprite[] ListBook;

    private string lableSatThuong = "Sát thương cơ bản:";
    private int NhanVat = 0;

    // Xac nhan ky nang nao dang duoc chon
    private SkillController skill;

    void Start () {
        CountSkill = 0;
        skill = SkillManager.GetSkillByID(0, 0);
        
        GameObject soundObj = GameObject.FindGameObjectWithTag("sound");
        if (soundObj != null) sound = soundObj.GetComponent<SoundManager>();
    }
    
    void Update () {
        // Kiem tra an toan Player
        if (PlayerHealth.control == null || PlayerHealth.control._Player == null) return;

        DiemChuyenCan.text = PlayerHealth.control._Player.PointSkill.ToString();
        
        LayDoiTuongNhanVatHienThi();
        ThucHienKhoaButtonLeftRight();
        ThucHienTimKyNangTheoMa();
    }

    private void ThuHienKhoaNutNangCap(SkillController skill)
    {
        if (skill == null || ButtonNangCap == null || ThuocTinh == null) return;

        if ((skill.Level.Current >= skill.Level.Max) && skill.Status == 1)
        {
            ButtonNangCap.interactable = false;
            ThuocTinh.SetActive(false);
        }
        else
        {
            ButtonNangCap.interactable = true;
            ThuocTinh.SetActive(true);
        }
    }

    private void LayDoiTuongNhanVatHienThi()
    {
        NhanVat = !ShowAnimation.Count ? 0 : 1;
    }

    private void ThucHienKhoaButtonLeftRight()
    {
        if (ButtonLeft != null) ButtonLeft.interactable = (CountSkill != 0);
        if (ButtonRight != null) ButtonRight.interactable = (CountSkill != 3);
    }

    private void ThucHienTimKyNangTheoMa()
    {
        if (CountSkill >= 0 && CountSkill <= 2)
        {
            skill = SkillManager.GetSkillByID(NhanVat, CountSkill);
        }
        else if (CountSkill == 3)
        {
            skill = SkillManager.GetSkillByID(-1, CountSkill);
        }

        if (skill != null)
        {
            ThucHienTaiLenThongTinKyNang(skill);
        }
    }

    private void ThucHienTaiLenThongTinKyNang(SkillController skill)
    {
        if (TenKyNang != null) TenKyNang.text = "Kỹ Năng " + (CountSkill + 1).ToString() + ": " + skill.Name;
        if (YeuCauCapDo != null) YeuCauCapDo.text = ThucHienLayThongtinYeuCau(skill.ID, skill.Relationship);
        if (CapDo != null) CapDo.text = skill.Level.Current.ToString() + "/" + skill.Level.Max.ToString();
        if (NhanDuoc != null) NhanDuoc.text = skill.Receive.Current.ToString();
        if (ThoiGianTonTai != null) ThoiGianTonTai.text = skill.TimeLive.Current.ToString();
        if (ThoiGianHoiChieu != null) ThoiGianHoiChieu.text = skill.TimeCountDown.Current.ToString();

        if (TrangThai != null && panelLeft != null)
        {
            if (skill.Status == 0)
            {
                TrangThai.text = "Chưa được mở khóa";
                panelLeft.color = new Color(1, 0.743f, 0.5896226f, 1);
            }
            else
            {
                TrangThai.text = "Mở";
                panelLeft.color = new Color(0.6559555f, 0.9811321f, 0.6340334f, 1);
            }
        }

        if (MoTa != null) MoTa.text = skill.Description;

        ThuHienKhoaNutNangCap(skill);
        TaiLenCapDoKeTiepCuaKyNang(skill.NextLevel());
        ThucHienAnVaDoiTenDoiTuongKhongCanThiet(skill);
    }

    private string ThucHienLayThongtinYeuCau(int id, int thuoc)
    {
        if (imageBook == null) return skill.ConditionLevelPlayer.ToString();

        if (skill.IDNgoc != -1)
        {
            if (skill.IDNgoc == 1) ShowBook(0);
            else if (skill.IDNgoc == 3) ShowBook(2);
            else ShowBook(1);

            // SUA LOI TAI DAY: Kiem tra vat pham ton tai truoc khi lay .Name
            VatPham vp = VatPhamController.GetItemByID(skill.IDNgoc);
            if (vp != null)
            {
                return skill.ConditionLevelPlayer.ToString() + " và " + vp.Name;
            }
            else
            {
                return skill.ConditionLevelPlayer.ToString() + " và Sách ID " + skill.IDNgoc;
            }
        }

        imageBook.enabled = false;
        return skill.ConditionLevelPlayer.ToString();
    }

    private void ShowBook(int book)
    {
        if (imageBook == null || ListBook == null || book >= ListBook.Length) return;
        imageBook.enabled = true;
        imageBook.sprite = ListBook[book];
    }

    private void ThucHienAnVaDoiTenDoiTuongKhongCanThiet(SkillController skill)
    {
        SetActive(1, true);
        SetActive(2, true);
        SetActive(3, true);

        ThucHienAnThuocTinhDanhThuongCuaHaiNhanVat(skill);
        EditTitleSkillAddHealth(skill);
        ThucHienAnThuocTinhKyNangTanHinhHiepSi(skill);
        ThucHienAnThuocTinhKyNangHoanDoi(skill);
    }

    private void SetActive(int index, bool value)
    {
        if (index == 1)
        {
            if (LabelNhanDuoc != null) LabelNhanDuoc.gameObject.SetActive(value);
            if (NhanDuoc != null) NhanDuoc.gameObject.SetActive(value);
            if (NLabelNhanDuoc != null) NLabelNhanDuoc.gameObject.SetActive(value);
            if (NNhanDuoc != null) NNhanDuoc.gameObject.SetActive(value);
        }
        else if (index == 2)
        {
            if (ThoiGianTonTai != null) ThoiGianTonTai.gameObject.SetActive(value);
            if (LabelThoiGianTonTai != null) LabelThoiGianTonTai.gameObject.SetActive(value);
            if (NThoiGianTonTai != null) NThoiGianTonTai.gameObject.SetActive(value);
            if (NLabelThoiGianTonTai != null) NLabelThoiGianTonTai.gameObject.SetActive(value);
        }
        else if (index == 3)
        {
            if (LabelThoiGianHoiChieu != null) LabelThoiGianHoiChieu.gameObject.SetActive(value);
            if (ThoiGianHoiChieu != null) ThoiGianHoiChieu.gameObject.SetActive(value);
            if (NLabelThoiGianHoiChieu != null) NLabelThoiGianHoiChieu.gameObject.SetActive(value);
            if (NThoiGianHoiChieu != null) NThoiGianHoiChieu.gameObject.SetActive(value);
        }
    }

    private void ThucHienAnThuocTinhKyNangHoanDoi(SkillController skill)
    {
        if (skill.ID == 3) { SetActive(1, false); SetActive(2, false); }
    }

    private void ThucHienAnThuocTinhKyNangTanHinhHiepSi(SkillController skill)
    {
        if (skill.ID == 2 && NhanVat == 0) SetActive(1, false);
    }

    private void EditTitleSkillAddHealth(SkillController skill)
    {
        if (LabelNhanDuoc == null || NLabelNhanDuoc == null) return;
        if (skill.ID == 1 && NhanVat == 0)
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
        if (skill.ID == 0) { SetActive(2, false); SetActive(3, false); }
    }

    private void TaiLenCapDoKeTiepCuaKyNang(SkillController skill)
    {
        if (skill == null) return;
        if (NYeuCauCapDo != null) NYeuCauCapDo.text = skill.ConditionLevelPlayer.ToString();
        if (NCapDo != null) NCapDo.text = skill.Level.Current.ToString() + "/" + skill.Level.Max.ToString();
        if (NNhanDuoc != null) NNhanDuoc.text = skill.Receive.Current.ToString();
        if (NThoiGianTonTai != null) NThoiGianTonTai.text = skill.TimeLive.Current.ToString();
        if (NThoiGianHoiChieu != null) NThoiGianHoiChieu.text = skill.TimeCountDown.Current.ToString();
        if (NTrangThai != null) NTrangThai.text = "Chưa mở khóa";
    }

    public void MovingLeft()
    {
        if (CountSkill > 0)
        {
            CountSkill--;
            if (sound != null) sound.Playsound("Click");
        }
    }

    public void MoingRight()
    {
        if (CountSkill < 3)
        {
            CountSkill++;
            if (sound != null) sound.Playsound("Click");
        }
    }

    public void ThucHienNangCapKyNang()
    {
        if (PlayerHealth.control == null || skill == null) return;

        if (PlayerHealth.control._Player.PointSkill > 0)
        {
            if (skill.Status == 0)
            {
                if (skill.ConditionLevelPlayer <= PlayerHealth.control._Player.Level)
                {
                    if (skill.IDNgoc == -1)
                    {
                        skill.Status = 1;
                        PlayerHealth.control._Player.PointSkill--;
                        if (sound != null) sound.Playsound("UpSkill");
                    }
                    else
                    {
                        VatPham vp = VatPhamController.GetItemByID(skill.IDNgoc);
                        if (vp != null && vp.Quality > 0)
                        {
                            skill.Status = 1;
                            PlayerHealth.control._Player.PointSkill--;
                            vp.Quality--;
                            VatPhamController.UpdateItem(vp);
                            if (sound != null) sound.Playsound("UpSkill");
                        }
                        else
                        {
                            if (sound != null) sound.Playsound("Error");
                            string itemName = (vp != null) ? vp.Name : "vật phẩm yêu cầu";
                            MessageBoxx.control.ShowSmall("Bạn chưa tìm thấy " + itemName + " để nâng cấp kỹ năng này.", "CHẤP NHẬN");
                        }
                    }
                }
                else 
                {
                    if (sound != null) sound.Playsound("Error");
                    MessageBoxx.control.ShowSmall("Cấp độ nhân vật chưa đủ để học kỹ năng này!", "CHẤP NHẬN");
                }
            }
            else if (skill.Level.Max > 1 && skill.Level.Current < skill.Level.Max)
            {
                if (SkillManager.IncreasingLevel(skill.NextLevel()))
                {
                    if (sound != null) sound.Playsound("UpSkill");
                    PlayerHealth.control._Player.PointSkill--;
                }
            }
        }
        else
        {
            if (sound != null) sound.Playsound("Error");
            MessageBoxx.control.ShowSmall("Điểm chuyên cần chưa đủ!", "CHẤP NHẬN");
        }
    }
}