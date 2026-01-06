using System;

[Serializable]
public class Player {
	// Nhung phuong thuc o day khong duoc de public ra ben ngoai
	// Thuc hien tinh dong goi cua Unity
	public Range Health = new Range(100000f,100000f); // Máu
	public Range Experience = new Range(50f,0); // Kinh nghiệm
	public Range Infuriate = new Range(250f,0); // Phẩn nộ
	public int Level = 99; // Cấp độ
	public double Gold = 10000; // So vang nhan duoc;
	public float Damage = 80f; // Lực chiến
    public float PointSkill = 0;
    public string BuildNameScence;
    public Player()
    {

    }

    public void Add(Range _health,float _damage)
    {
        this.Health.Max += _health.Max;
        this.Health.Current += _health.Current;

        this.Damage += _damage;
    }

    public void Sub(Range _health, float _damage)
    {
        this.Health.Max -= _health.Max;
        this.Health.Current -= _health.Current;

        this.Damage -= _damage;
    }

	public void SetDefault(){
		Health.Current = Health.Max;
		this.SaveInformation ();
	}

	// Thuc hien tang, giam mau cho nhan vat
	/// <summary>
	/// Sets the health.
	/// </summary>
	/// <param name="key">Key: 1 = Max, 0 = Current</param>
	/// <param name="value">Value.</param>
	public void SetHealth(int key,float value){
		if (key == 0) {
			this.Health.Current = value;
			// kiem tra dieu kien
			if (this.Health.Current < 0) {
				this.Health.Current = 0;
			}else if (this.Health.Current > this.Health.Max) {
				this.Health.Current = this.Health.Max;
			}
		} 
		else
			if (key == 1) {
			this.Health.Max = value;
		}
	}

	// Thuc hien tang, giam mana cho nhan vat
	/// <summary>
	/// Sets the mana.
	/// </summary>
	/// <param name="key">Key: 1 = Max, 0 = Current</param>
	/// <param name="value">Value.</param>
    public void SetInfuriate(int key, float value)
    {
		if (key == 0) {
            this.Infuriate.Current = value;
			// kiem tra dieu kien
            if (this.Infuriate.Current < 0)
            {
                this.Infuriate.Current = 0;
            }
            else if (this.Infuriate.Current > this.Infuriate.Max)
            {
                this.Infuriate.Current = this.Infuriate.Max;
			}
		} 
		else
			if (key == 1) {
                this.Infuriate.Max = value;
			}
	}

	public void SaveInformation(){
		PlayerManager.SaveInformation (this);
	}

	public Player LoadInformation(){
		if (this.hasExist ()) {
			return PlayerManager.LoadInformation ();
		}
		return null;
	}

	public bool hasExist(){
		if (PlayerManager.LoadInformation () != null) {
			return true;
		}
		return false;
	}
}
