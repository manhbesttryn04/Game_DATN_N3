using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
	// Thong tin mac dinh cua quai vat, co the thay doi theo tung quai
	public Enemy _Enemy;
	// Lay thong tin thanh mau cua enemy
	public Slider enemyHealthSlider; // Su dung: using UnityEngine.UI;
	public Image Fill;
	//
	public GameObject EnemyParent;
	// Thoi gian khoi tao lai doi tuong
	private float timeInstantiate = 40f;
	// Xac nhan hien lai
	public bool Show = true;
	// Xac nhan da chet
	private bool Die = false;

	// So lan bi tan cong
	private int BiDanh = 0;
    // So sat thuong nhan vao
    private float NhanSatThuong = 0;
    private bool TanHinh = false; // Thuc hien tan hinh
	public bool Delete = false;
	public float scaleBoss = 3f;

	// Xu ly hieu ung mau roi
	public EnemyAnimation enemyBlood;
	// Xu ly hieu ung chet
	public EnemyAnimation enemyDead;

	//
	private Vector2 psitionStart; 
	//
	private void Awake(){
        // Lay vi tri hien tai
		if (EnemyParent != null) {
			psitionStart = EnemyParent.transform.position;
		}
	}

    // HienThiAmThanh
    public SoundManager sound;


    // Use this for initialization
    private void Start()
    {
        SaveLoadEnemy();
        // Thay doi thong tin mau va damage theo level
        if (!_Enemy.IsBoss)
        {
            _Enemy.Level = (LevelEnemy)(SceneManager.GetActiveScene().buildIndex * 3);
            _Enemy.SetHealthAndDamageByLevel((SceneManager.GetActiveScene().buildIndex * 3));
        }
        else
        {
            _Enemy.Level = (LevelEnemy)((SceneManager.GetActiveScene().buildIndex * 3) + 4);
            _Enemy.SetHealthAndDamageByLevel((SceneManager.GetActiveScene().buildIndex * 3) + 4);
        }
        // Cai dat thanh mau cho enemy
        SettingEnemyHealthSlider();
        // An doi tuong thanh mau
        if (!_Enemy.IsBoss)
        {
            enemyHealthSlider.gameObject.SetActive(false); // an thanh mau
        }
        // Set thoi gan hoi lai mac dinh la 40s
        timeInstantiate = 40f;
        // Cap nhat thong tin mau va luc chien thong qua level

        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }

    private void Update()
    {
        SaveLoadEnemy();
        SetColorSlider();
    }

    private void SettingEnemyHealthSlider()
    {
        // Gan gia tri can thiet
        ChangeValueForVariable();
        // Lay thong tin doi duong 
        if (enemyHealthSlider != null)
        {
            // Thong tin mau cua enemy
            enemyHealthSlider.maxValue = _Enemy.Health.Max;
            enemyHealthSlider.value = _Enemy.Health.Current;
        }
    }

    private void ChangeValueForVariable()
    {
        if (_Enemy.IsBoss)
        {
            // Gan gia tri cho boss
            enemyHealthSlider = BossHealth.control.enemySlider;
            Fill = BossHealth.control.Fill;
        }
    }

	/// <summary>
	/// Saves the load enemy.
	/// </summary>
	private void SaveLoadEnemy ()
	{
		// Neu no la boss
        if (_Enemy.IsBoss)
        {
			// Khi da co thong tin duoc luu lai
			if(EnemyManager.LoadBoss () != null){
				//
				if (!Delete) {
					if (EnemyManager.CheckIndexBoss (EnemyManager.LoadBoss (), _Enemy.boss)) {
						this.gameObject.SetActive (false);
						EnemyParent.SetActive (false);
                        this.Die = false;
					}
				} else {
					EnemyManager.DeleteIndexBoss (EnemyManager.LoadBoss (), _Enemy.boss);
				}
			}
		}
	}

	/// <summary>
	/// Contructions the color health.
	/// </summary>
	/// <param name="type">Type.</param>
	private void SetColorSlider(){
        float Mau = _Enemy.Health.Current;
        if (Mau > 6000)
        {
            Fill.color = Color.red; // Mau do
        }
        else if (Mau > 5000)
        {
            Fill.color = Color.blue; // Mau xanh
		} else if (Mau > 4000) {
            Fill.color = new Color(0.6839032f, 0, 1, 1); // Mau tim
		} else if (Mau > 3000) {
            Fill.color = Color.yellow;
        }
        else if (Mau > 2000)
        {
            Fill.color = Color.green;
        }
        else if (Mau > 1000)
        {
            Fill.color = new Color(0.1529412f, 0.9840662f, 1, 1);
        } 
        else
        {
			Fill.color = Color.gray; // Mau do
		}
	}
    private bool Attack = false;
    private void HandlingShowInformation(){
        if (!Attack)
        {
            Attack = true;
        }
        // Kiem tra ton tai SelectEnemy hay khong?
        if (SelectManager.HasSelect() && !SelectManager.CompareGame(this.gameObject))
        {
            // Huy chon doi tuong hien tai
            SelectEnemy.HandlingSelectedGameObject(SelectManager.GameSelected, SelectManager.SliderSelected, false,_Enemy);
        }
        
        if (_Enemy.Health.Current > 0)
        {
            //

            SelectEnemy.HandlingSelectedGameObject(this.gameObject, enemyHealthSlider, true, this._Enemy);
            // Sau thoi gian nao do thi huy
           Invoke("InvokeCancelShowInformation", 2.5f);
        }
        else
        {
            // Sau thoi gian nao do thi huy
            Invoke("InvokeCancelShowInformation", 0f);
        }
    }

    private void InvokeCancelShowInformation()
    {
        if (transform.position.x + 4 <= SelectEnemy.Player.position.x || transform.position.x - 4 >= SelectEnemy.Player.position.x)
        {
            // Huy chon doi tuong hien tai
            SelectEnemy.HandlingSelectedGameObject(SelectManager.GameSelected, SelectManager.SliderSelected, false, _Enemy);
        }
    }

    // Thuc hien truyen cho nhan vat mot luong kinh nghiem
    private void SetExpriencePlayer()
    {
        if (_Enemy.type != TypeEnemy.DungYen)
        {
            // Quai vat cap 1 mac dinh khi bi danh trung nguoi choi se co 5 kinh nghiep
            PlayerHealth.control._Player.Experience.Current += _Enemy.GetIntLevel() * 0.08673f;
        }
    }

	// Xu ly bi tan cong
	public void addDamage(float damage){
        if (!TanHinh)
        {
            if (damage < 0)
            {
                return;
            }
            //sound.Playsound("QuaiBiDanh");
            // Thuc hien tang kinh nghiem cho nhan vat khi nhan vat danh trung
            SetExpriencePlayer();
            // Nhan tac dong bi danh
            this.BiDanh++;
            // Nhan sat thuong nhan vao
            this.NhanSatThuong += damage;
            // Giam mau 
            _Enemy.Health.Current -= damage;
            enemyHealthSlider.value = _Enemy.Health.Current;
            // Day lui quai lai mot doan
            if (EnemyParent != null && !_Enemy.IsBoss)
            {
                pushBack(EnemyParent.transform);
            }
            // Chet
            // Hien thong tin khi bi danh
            HandlingShowInformation();
            //
            if (_Enemy.Health.Current <= 0f)
            {
                Dead();
            }
            else
            {
                // Hien animation mau khi bi tan cong
                displayAnimation(enemyBlood.Graphicss, enemyBlood.GunTips); // Hien mau roi
            }
        }
	}

	/// <summary>
	/// Gets the bi danh.
	/// </summary>
	/// <returns>The bi danh.</returns>
	public int GetBiDanh(){
		return this.BiDanh;
	}

	/// <summary>
	/// Sets the bi danh.
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetBiDanh(int value){
		this.BiDanh = value;
	}

    /// <summary>
    /// Gets the bi danh.
    /// </summary>
    /// <returns>The bi danh.</returns>
    public float GetSatThuong()
    {
        return this.NhanSatThuong;
    }

    /// <summary>
    /// Sets the bi danh.
    /// </summary>
    /// <param name="value">Value.</param>
    public void SetSatThuong(float value)
    {
        this.NhanSatThuong = value;
    }

    public bool GetTanHinh()
    {
        return this.TanHinh;
    }

    public void SetTanHinh(bool value)
    {
        this.TanHinh = value;
    }

	/// <summary>
	/// Thay vi Destroy luon gameObject thi o day se an enemy sau thoi gian se hien lai
	/// </summary>
	public void Dead(){
        //sound.Playsound("Bomb");
		this.BiDanh = 0;
        this.NhanSatThuong = 0;
		this.Die = true;
		// Hien animation chet
		displayAnimation (enemyDead.Graphicss,enemyDead.GunTips); // Hien hieu ung chet
		this.gameObject.SetActive (false);
		enemyHealthSlider.gameObject.SetActive (false); // An thanh mau
		// Luu thong tin boss
        if (_Enemy.IsBoss && _Enemy.boss != IndexBoss.None)
        {
			_Enemy.SaveInformationBoss ();
        }
		// Xoa doi tuong duoc chon khi chet
        if (SelectManager.CompareGame(this.gameObject))
        {
            SelectEnemy.HandlingSelectedGameObject(SelectManager.GameSelected, SelectManager.SliderSelected, false, _Enemy);
        }
		// Khoi tao moi quai khi chet
		if (Show && !_Enemy.IsBoss) {
            // Hoi sinh
			Invoke("Instancex",timeInstantiate);
		}
	}

    private void DestroyBoss()
    {
        Destroy(EnemyParent);
    }

	/// <summary>
	/// Instancex this instance.
	/// </summary>
	private void Instancex(){
		this.Die = false; // Xac nhan song lai
		// Set lai vi tri cua enemy lkhi duoc khoi tao lai
		EnemyParent.transform.position = psitionStart;
		// Gan thanh mau day lai 
		_Enemy.Health.Current = _Enemy.Health.Max; 
		enemyHealthSlider.value = _Enemy.Health.Current; 
		this.gameObject.SetActive (true);
	}

	/// <summary>
	/// Gets the dead.
	/// </summary>
	/// <returns><c>true</c>, if dead was gotten, <c>false</c> otherwise.</returns>
	public bool GetDead(){
		return this.Die;
	}

	/// <summary>
	/// Displaies the animation.
	/// </summary>
	/// <param name="Graphics">Graphics.</param>
	/// <param name="GunTip">Gun tip.</param>
	public void displayAnimation(GameObject Graphics,Transform GunTip){
		if (Graphics != null && GunTip != null) {
            if (_Enemy.IsBoss)
            {
				Graphics.transform.localScale = new Vector2 (scaleBoss, scaleBoss);
			} else {
				Graphics.transform.localScale = new Vector2 (1f, 1f);
			}
			Instantiate (Graphics, GunTip.position, Quaternion.Euler (0, 0, 0));
		}
	}

	private enemyMovingWalk GetEnemyMovingWalk (GameObject enemy)
	{
		if (enemy.GetComponent<enemyMovingWalk> () != null) {
			return EnemyParent.GetComponent<enemyMovingWalk> ();
		}
		return null;
	}

	private void AddToForce (Transform pushObject, enemyMovingWalk enemyWalk)
	{
		// Tim toi mot Rigidbody2D
		Rigidbody2D myBody = pushObject.gameObject.GetComponent<Rigidbody2D> ();
		if (myBody != null) {
			// Gan luc dung yen
			myBody.linearVelocity = Vector2.zero;
		}
	}

	/// <summary>
	/// Pushs the back.
	/// </summary>
	/// <param name="pushObject">Push object.</param>
	public void pushBack(Transform pushObject){
		if (_Enemy.type != TypeEnemy.DungYen) {
			// Lay script EnemyMovingWalk de lay pham vi di chuyen
			enemyMovingWalk enemyWalk = GetEnemyMovingWalk (EnemyParent);
			// Thuc hien day lui enemy
			AddToForce (pushObject, enemyWalk);
		}
	}
}