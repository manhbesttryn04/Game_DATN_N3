using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public Enemy _Enemy;
    public Slider enemyHealthSlider; 
    public Image Fill;
    public GameObject EnemyParent;
    private float timeInstantiate = 40f;
    public bool Show = true;
    protected bool Die = false; // Đổi thành protected để lớp con sử dụng được

    private int BiDanh = 0;
    private float NhanSatThuong = 0;
    private bool TanHinh = false; 
    public bool Delete = false;
    public float scaleBoss = 3f;

    public EnemyAnimation enemyBlood;
    public EnemyAnimation enemyDead;
    private Vector2 psitionStart; 
    public SoundManager sound;

    private void Awake(){
        if (EnemyParent != null) {
            psitionStart = EnemyParent.transform.position;
        }
    }

    private void Start()
    {
        SaveLoadEnemy();
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
        SettingEnemyHealthSlider();
        if (!_Enemy.IsBoss)
        {
            enemyHealthSlider.gameObject.SetActive(false); 
        }
        
        GameObject soundObj = GameObject.FindGameObjectWithTag("sound");
        if(soundObj != null) sound = soundObj.GetComponent<SoundManager>();
    }

    protected virtual void Update() 
    {
        SaveLoadEnemy();
        SetColorSlider();
    }

    private void SettingEnemyHealthSlider()
    {
        ChangeValueForVariable();
        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.maxValue = _Enemy.Health.Max;
            enemyHealthSlider.value = _Enemy.Health.Current;
        }
    }

    private void ChangeValueForVariable()
    {
        if (_Enemy.IsBoss)
        {
            enemyHealthSlider = BossHealth.control.enemySlider;
            Fill = BossHealth.control.Fill;
        }
    }

    private void SaveLoadEnemy ()
    {
        if (_Enemy.IsBoss)
        {
            if(EnemyManager.LoadBoss () != null){
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

    private void SetColorSlider(){
        if (Fill == null) return;
        float Mau = _Enemy.Health.Current;
        if (Mau > 6000) Fill.color = Color.red;
        else if (Mau > 5000) Fill.color = Color.blue;
        else if (Mau > 4000) Fill.color = new Color(0.6839032f, 0, 1, 1);
        else if (Mau > 3000) Fill.color = Color.yellow;
        else if (Mau > 2000) Fill.color = Color.green;
        else if (Mau > 1000) Fill.color = new Color(0.1529412f, 0.9840662f, 1, 1);
        else Fill.color = Color.gray;
    }

    public void addDamage(float damage){
        if (!TanHinh)
        {
            if (damage < 0) return;
            SetExpriencePlayer();
            this.BiDanh++;
            this.NhanSatThuong += damage;
            _Enemy.Health.Current -= damage;
            enemyHealthSlider.value = _Enemy.Health.Current;

            if (EnemyParent != null && !_Enemy.IsBoss)
            {
                pushBack(EnemyParent.transform);
            }

            HandlingShowInformation();

            if (_Enemy.Health.Current <= 0f)
            {
                Dead();
            }
            else
            {
                displayAnimation(enemyBlood.Graphicss, enemyBlood.GunTips); 
            }
        }
    }

    // THÊM VIRTUAL Ở ĐÂY
    public virtual void Dead(){
        if (this.Die == false) 
        {
            this.Die = true; 

            // Logic Mission13 (như bạn đã viết trong cấu trúc cũ)
            Mission13 mission = Object.FindFirstObjectByType<Mission13>();
            if (mission != null && mission.onMission)
            {
                mission.NotifyEnemyKilled(); 
            }

            if (QuestNPC.Instance != null)
            {
                QuestNPC.Instance.OnEnemyKilled();
            }

            this.BiDanh = 0;
            this.NhanSatThuong = 0;
            
            displayAnimation(enemyDead.Graphicss, enemyDead.GunTips);
            this.gameObject.SetActive(false);
            enemyHealthSlider.gameObject.SetActive(false);

            if (_Enemy.IsBoss && _Enemy.boss != IndexBoss.None)
            {
                _Enemy.SaveInformationBoss();
            }

            if (SelectManager.CompareGame(this.gameObject))
            {
                SelectEnemy.HandlingSelectedGameObject(SelectManager.GameSelected, SelectManager.SliderSelected, false, _Enemy);
            }

            if (Show && !_Enemy.IsBoss) {
                float thoiGianHoiSinhThucTe = timeInstantiate;
                if(QuestNPC.Instance != null && QuestNPC.Instance.currentState == QuestNPC.QuestState.DoingQuest2)
                {
                    thoiGianHoiSinhThucTe = 5f;
                }
                Invoke("Instancex", thoiGianHoiSinhThucTe);
            }
        }
    }
    private void Instancex(){
        this.Die = false; 
        EnemyParent.transform.position = psitionStart;
        _Enemy.Health.Current = _Enemy.Health.Max; 
        enemyHealthSlider.value = _Enemy.Health.Current; 
        this.gameObject.SetActive (true);
    }

    public int GetBiDanh() { return this.BiDanh; }
    public void SetBiDanh(int value) { this.BiDanh = value; }
    public float GetSatThuong() { return this.NhanSatThuong; }
    public void SetSatThuong(float value) { this.NhanSatThuong = value; }
    public bool GetTanHinh() { return this.TanHinh; }
    public void SetTanHinh(bool value) { this.TanHinh = value; }
    public bool GetDead() { return this.Die; }

    private void HandlingShowInformation(){
        if (SelectManager.HasSelect() && !SelectManager.CompareGame(this.gameObject))
            SelectEnemy.HandlingSelectedGameObject(SelectManager.GameSelected, SelectManager.SliderSelected, false, _Enemy);
        
        if (_Enemy.Health.Current > 0) {
            SelectEnemy.HandlingSelectedGameObject(this.gameObject, enemyHealthSlider, true, this._Enemy);
            Invoke("InvokeCancelShowInformation", 2.5f);
        } else {
            Invoke("InvokeCancelShowInformation", 0f);
        }
    }

    private void InvokeCancelShowInformation() {
        if (SelectEnemy.Player != null && (transform.position.x + 4 <= SelectEnemy.Player.position.x || transform.position.x - 4 >= SelectEnemy.Player.position.x))
            SelectEnemy.HandlingSelectedGameObject(SelectManager.GameSelected, SelectManager.SliderSelected, false, _Enemy);
    }

    private void SetExpriencePlayer() {
        if (_Enemy.type != TypeEnemy.DungYen)
            PlayerHealth.control._Player.Experience.Current += _Enemy.GetIntLevel() * 0.08673f;
    }

    public void displayAnimation(GameObject Graphics, Transform GunTip){
        if (Graphics != null && GunTip != null) {
            Instantiate (Graphics, GunTip.position, Quaternion.identity);
        }
    }

    public void pushBack(Transform pushObject){
        if (_Enemy.type != TypeEnemy.DungYen) {
            Rigidbody2D myBody = pushObject.gameObject.GetComponent<Rigidbody2D> ();
            if (myBody != null) myBody.linearVelocity = Vector2.zero;
        }
    }
}