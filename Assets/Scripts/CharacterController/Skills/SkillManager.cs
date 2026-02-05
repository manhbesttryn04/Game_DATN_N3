using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    private static List<SkillController> lstSkill = new List<SkillController>();
    public bool Load = true;

    void Awake()
    {
        // Khởi tạo danh sách mới
        lstSkill = new List<SkillController>();

        if (Load)
        {
            List<SkillController> dataLoaded = PlayerManager.LoadSkill();
            if (dataLoaded == null || dataLoaded.Count == 0)
            {
                KhoiTaoDanhSachKyNang();
            }
            else
            {
                lstSkill = dataLoaded;
            }
        }
        else
        {
            KhoiTaoDanhSachKyNang();
        }
    }

    private static void KhoiTaoDanhSachKyNang()
    {
        // Luôn xóa danh sách cũ trước khi Add để tránh trùng lặp ID
        lstSkill.Clear();

        // --- KHỞI TẠO KỸ NĂNG HIỆP SĨ (Relationship = 0) ---
        lstSkill.Add(new SkillController(0, "KIẾM SẮT", new Offset(23, 13), new Offset(0, 0), new Offset(0, 0), 0, 1, new Level(1,5), "Hiệp sĩ chém vào quái vật một lượng sát thương cơ bản.", 1,-1));
        lstSkill.Add(new SkillController(1, "TRỊ THƯƠNG", new Offset(20, 22f), new Offset(25, 1f), new Offset(5, 0.5f), 0, 17, new Level(1, 5), "Hồi một lượng máu lớn cho cơ thể.", 0, -1));
        lstSkill.Add(new SkillController(2, "ẨN THÂN", new Offset(0, 0), new Offset(30, 1.5f), new Offset(2, 1.5f), 0, 22, new Level(1, 5), "Khiến kẻ địch không nhận ra sự tồn tại.", 0, 2));

        // --- KHỞI TẠO KỸ NĂNG RỒNG (Relationship = 1) ---
        lstSkill.Add(new SkillController(0, "HỎA LONG", new Offset(50, 22), new Offset(0, 0), new Offset(0, 0), 1, 1, new Level(1, 5), "Bắn tới phía trước một viên lửa.", 1, -1));
        lstSkill.Add(new SkillController(1, "PHONG THẦN", new Offset(100, 12), new Offset(25, 0.2f), new Offset(1, 0.1f), 1, 27, new Level(1, 5), "Lướt qua kẻ địch gây sát thương khủng.", 0, -1));
        lstSkill.Add(new SkillController(2, "ÁNH SÁNG HỦY DIỆT", new Offset(120, 14), new Offset(30, 1.5f), new Offset(3, 0.3f), 1, 32, new Level(1, 5), "Phóng ra luồng ánh sáng thiêu cháy kẻ địch.", 0, 3));

        // --- KỸ NĂNG ĐẶC BIỆT (Relationship = -1) ---
        lstSkill.Add(new SkillController(3, "HOÁN ĐỔI", new Offset(0, 0), new Offset(35, 1.5f), new Offset(3, 0.2f), -1, 12, new Level(1, 5), "Biến đổi bản thân thành Rồng Vương.", 0, 1));

        // Lưu dữ liệu mặc định lần đầu
        PlayerManager.SaveSkill(lstSkill);
    }

    public static void AddSkill(SkillController skill){
        if(lstSkill == null) lstSkill = new List<SkillController>();
        lstSkill.Add(skill);
    }

    public static SkillController GetSkillByID(int relationship, int id)
    {
        if (lstSkill != null && lstSkill.Count > 0)
        {
            foreach (SkillController skill in lstSkill)
            {
                if (relationship == skill.Relationship && id == skill.ID)
                {
                    return skill;
                }
            }
        }
        return null;
    }

    public static bool IncreasingLevel(SkillController skill)
    {
        if (lstSkill != null && lstSkill.Count > 0)
        {
            for (int i = 0; i < lstSkill.Count; ++i)
            {
                if (skill.Relationship == lstSkill[i].Relationship && skill.ID == lstSkill[i].ID)
                {
                    // Cập nhật trạng thái và lưu vào danh sách static
                    skill.Status = 1; 
                    lstSkill[i] = skill;
                    
                    // Quan trọng: Lưu lại vào file sau khi nâng cấp
                    PlayerManager.SaveSkill(lstSkill);
                    return true;
                }
            }
        }
        return false;
    }
}