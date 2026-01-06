using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    private static List<SkillController> lstSkill = new List<SkillController>();
    public bool Load = true;
    void Awake()
    {
        if (Load)
        {
            if (PlayerManager.LoadSkill() == null)
            {
                KhoiTaoDanhSachKyNang();
                // Lưu lại thong tin
            }
            else
            {
                lstSkill = PlayerManager.LoadSkill();
            }
        }
        else
        {
            KhoiTaoDanhSachKyNang();
        }
    }

    private static void KhoiTaoDanhSachKyNang()
    {
        // Ky nang hiep si
        // Danh thuong
        lstSkill.Add(new SkillController(0, "KIẾM SẮT", new Offset(23, 13), new Offset(0, 0), new Offset(0, 0), 0, 1, new Level(1,5), "Hiệp sĩ chém vào quái vật một lượng sát thương cơ bản.\n Khi kỹ năng đạt cấp 5, hiệp sĩ được nhận thêm hiệu ứng chém gió, với mỗi lần chém tăng thêm 5 - 10 sát thương cơ bản.", 1,-1));
        // Hoi mau
        lstSkill.Add(new SkillController(1, "TRỊ THƯƠNG", new Offset(20, 22f), new Offset(25, 1f), new Offset(5, 0.5f), 0, 17, new Level(1, 5), "Xung quanh bản thân xuất hiện một luồng gió, giúp cho bản thân được cường hóa, hồi một lượng máu lớn với mỗi 0.5s cho cơ thể trong một khoảng thời gian nhất định.", 0, -1));
        // Tan hinh
        lstSkill.Add(new SkillController(2, "ẨN THÂN", new Offset(0, 0), new Offset(30, 1.5f), new Offset(2, 1.5f), 0, 22, new Level(1, 5), "Hiệp sĩ cường hóa bản thân, giúp cho mọi thứ trên cơ thể hòa mình với xung quanh. Khiến cho kẻ địch không nhận ra sự tồn tại của bản thân, mà bản thân có thể di chuyển qua chúng một cách dễ dàng. ", 0, 2));



        // Ky nang rong
        lstSkill.Add(new SkillController(0, "HỎA LONG", new Offset(50, 22), new Offset(0, 0), new Offset(0, 0), 1, 1, new Level(1, 5), "Bản thân nổi lên cơn thịnh nộ, bắn tới phía trước một viên lửa gây sát thương cơ bản đến với kẻ địch gặp phải.", 1, -1)); ;
        lstSkill.Add(new SkillController(1, "PHONG THẦN", new Offset(100, 12), new Offset(25, 0.2f), new Offset(1, 0.1f), 1, 27, new Level(1, 5), "Rồng cường hóa bản thân, phóng tới phía trước trong trạng thái bất động. Lướt qua kẻ địch gây lượng sát thương khủng lên kẻ địch.", 0, -1));
        lstSkill.Add(new SkillController(2, "ÁNH SÁNG HỦY DIỆT", new Offset(120, 14), new Offset(30, 1.5f), new Offset(3, 0.3f), 1, 32, new Level(1, 5), "Rồng vận dụng sức mạnh phóng ra một luồng ánh sáng, gây một lượng sát thương thiêu cháy kẻ địch trong mỗi 0.5s.", 0, 3));
        // Ky nang chuyen rong
        lstSkill.Add(new SkillController(3, "HOÁN ĐỔI", new Offset(0, 0), new Offset(35, 1.5f), new Offset(3, 0.2f), -1, 12, new Level(1, 5), "Hiệp sĩ cường hóa bản thân, lấy sức mạnh từ bên trong cơ thể giải phong ấn cho linh hồn Rồng Vương, biến đổi bản thân thành Rồng Vương để có thể sử dụng những kỹ năng tìm ẩn bên trong sức mạnh của Rồng Vương. ", 0, 1));
    }
	
	// Update is called once per frame
	void Update () {
        PlayerManager.SaveSkill(lstSkill);//
	}

    public static void AddSkill(SkillController skill){
        lstSkill.Add(skill);
    }

    /// <summary>
    /// Thuc hien lay thong tin ky nang
    /// </summary>
    /// <param name="relationship">-1: khong cua ai| 0: hiep si| 1:rong</param>
    /// <param name="id">Ma ky nang</param>
    /// <returns></returns>
    public static SkillController GetSkillByID(int relationship, int id)
    {
        if (lstSkill.Count > 0)
        {
            // Tim trong danh sach
            foreach (SkillController skill in lstSkill)
            {
                // Kiem tra  cua nhan vat nao
                if (relationship == skill.Relationship)
                {
                    // kiem tra loai ky nang
                    if (id == skill.ID)
                    {
                        return skill;
                    }
                }
            }
        }
        return null;
    }

    public static bool IncreasingLevel(SkillController skill)
    {
        if (lstSkill.Count > 0)
        {
            // Tim trong danh sach
            for (int i = 0; i < lstSkill.Count; ++i)
            {
                // Kiem tra  cua nhan vat nao
                if (skill.Relationship == lstSkill[i].Relationship)
                {
                    // kiem tra loai ky nang
                    if (skill.ID == lstSkill[i].ID)
                    {
                        skill.Status = 1; // Mo khoa
                        lstSkill[i] = skill;
                        return true; // Dung
                    }
                }
            }
        }
        return false;
    }
}
