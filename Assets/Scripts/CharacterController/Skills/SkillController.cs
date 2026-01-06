using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SkillController {

    public int ID { get; set; } // Ma ky nang
    public string Name{ get; set;}  // Ten ky nang
    public Offset Receive { get; set; } // Sat thuong 
    public Offset TimeCountDown { get; set; }
    public Offset TimeLive { get; set; }
    public int Relationship { get; set; } // Ky nang nay cua ai -1: khong cua ai| 0: hiep si| 1:rong
    public int ConditionLevelPlayer{ get; set;} // Dieu kien cap do cua nhan vat
    public Level Level { get; set; }  // Cấp 1 - 5
    public string Description { get; set; } // Mo ta ky nang
    public int Status{ get; set;} // Trang thai: Khoa/Mo
    public int IDNgoc { get; set; } // true: Khong co dieu kien khac/ co dieu kien khac
    public SkillController()
    {
        this.ID = -1;
        this.Name = "";
        this.Receive = new Offset(0, 0);
        this.TimeCountDown = new Offset(0, 0);
        this.TimeLive = new Offset(0, 0);
        this.Relationship = -1;
        this.ConditionLevelPlayer = 0;
        this.Level = new Level(1,1);
        this.Description = "";
        this.Status = 0;
        this.IDNgoc = -1;
    }

    /// <summary>
    /// Thuc hien khoi tao doi tuong
    /// </summary>
    /// <param name="id">Ma ky nang</param>
    /// <param name="name">Ten ky nang</param>
    /// <param name="icon">Icon ky nang</param>
    /// <param name="damage">Sat thuong</param>
    /// <param name="damageNextLevel">Sat thuong cap tiep theo</param>
    /// <param name="relationship">Ky nang thuoc nhan vat nao. -1: khong thuoc ai | 0:hiep si | 1: rong</param>
    /// <param name="conditionLevelPlayer">Cap nhan vat co the duoc mo ky nang</param>
    /// <param name="level">Cap do cua ky nang, 1 - 6</param>
    /// <param name="description">Mo ta cach su dung</param>
    /// <param name="status">Trang thai cua ky nang. 0:Khoa | 1:Mo</param>
    public SkillController(int id, string name, Offset receive, Offset timeCountDown, Offset timeLive, int relationship, int conditionLevelPlayer, Level level, string description, int status, int idNgoc)
    {
        this.ID = id;
        this.Name = name;
        this.Receive = receive;
        this.TimeCountDown = timeCountDown;
        this.TimeLive = timeLive;
        this.Relationship = relationship;
        this.ConditionLevelPlayer = conditionLevelPlayer;
        this.Level = level;
        this.Description = description;
        this.Status = status;
        this.IDNgoc = idNgoc;
    }

    public SkillController NextLevel(){
        // Khoi tao
        SkillController skill = new SkillController();

        skill.ID = this.ID;
        skill.Name = this.Name;
        skill.ConditionLevelPlayer = this.ConditionLevelPlayer;
        skill.Relationship = this.Relationship;
        // Tang sat thuong/ mau hoi lai
        skill.Receive.Next = this.Receive.Next;
        skill.Receive.Current = this.Receive.Current + this.Receive.Next; // Tang dam/ mau
        // Thoi gian hoi chieu
        skill.TimeCountDown.Next = this.TimeCountDown.Next;
        skill.TimeCountDown.Current = this.TimeCountDown.Current - this.TimeCountDown.Next; // giam thoi gian hoi chieu
        // Thoi gian thuc hien
        skill.TimeLive.Next = this.TimeLive.Next;
        skill.TimeLive.Current = this.TimeLive.Current + this.TimeLive.Next; // Tang thoi gian thuc hien
        //
        skill.Level.Max = this.Level.Max;
        skill.Level.Current = this.Level.Current + 1;
        if (skill.Level.Current >= skill.Level.Max)
        {
            skill.Level.Current = skill.Level.Max;
        }
        //
        skill.Description = this.Description;
        skill.Status = 0; // Cap tiep theo trang thai luon bi khoa
        skill.IDNgoc = this.IDNgoc;
        return skill;
    }
}
