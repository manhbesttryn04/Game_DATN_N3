using System.Collections.Generic;
using System;

public enum TypeEnemy { DungYen = 1, DanhGan = 2, DanhXa = 3, Bay = 4, LaoVao = 5,None = 6};
public enum LevelEnemy { Level1,Level2,Level3,Level4,Level5,Level6,Level7,Level8,Level9,
	Level10,Level11,Level12,Level13,Level14,Level15,Level16,Level17,Level18,Level19,
	Level20,Level21,Level22,Level23,Level24,Level25,Level26,Level27,Level28,Level29,
	Level30,Level31,Level32,Level33,Level34,Level35,Level36,Level37,Level38,Level39,
    Level40, Level41, Level42, Level43, Level44, Level45, Level46, Level47, Level48, Level49, Level50,
    Level51, Level52, Level53, Level54, Level55,
};

// Thong tin quai vat
[Serializable]
public class Enemy {
	public Range Health = new Range(300f,300f);
	private float Damage = 20f;
	public TypeEnemy type;
	public LevelEnemy Level;
	public IndexBoss boss;
    public bool IsBoss = false;

	public float GetDamage(){
		return this.Damage;
	}

    public int GetIntLevel()
    {
        return Convert.ToInt32(this.Level.ToString().Split('l')[1]);
    }

	//lv1: mau 350 | damage: 30
	/// <summary>
	/// Sets the health and damage by level.
	/// </summary>
    public void SetHealthAndDamageByLevel(int intLevel)
    {
		// Moi cap tang 50 mau
		float HPStand = 300f;
		if (intLevel < 10) {
			this.Health.Max = HPStand + (50 * intLevel);
		}else if (intLevel < 20) {
			this.Health.Max = HPStand + (150 * intLevel);
		}
		else if (intLevel < 30) {
			this.Health.Max = HPStand + (250 * intLevel);
		}else{
			this.Health.Max = HPStand + (350 * intLevel);
        }
		this.Health.Current = this.Health.Max;
		// ST
		this.Damage = 20f + 10*intLevel;
	}
	public void SaveInformationBoss(){
		List<IndexBoss> lstBoss = EnemyManager.LoadBoss ();
		if (lstBoss == null) {
			lstBoss = new List<IndexBoss> ();
		}
		if (!EnemyManager.CheckIndexBoss (lstBoss,this.boss)) {
			lstBoss.Add (this.boss);
			EnemyManager.SaveBoss (lstBoss);
		}
	}
}
