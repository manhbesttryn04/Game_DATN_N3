using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperienceCharacter : MonoBehaviour {

    public GameObject HieuUngTangLevel; // Hieu ung
     // Use this for initialization
    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update () {
        // Thuc hien kiem tra kinh nghiem cua nguoi choi
        CheckIncreasingLevel();
	}

    // 
    private void CheckIncreasingLevel()
    {
        // Kiem tra kinh nghiem cua nguoi choi
        if (PlayerHealth.control._Player.Experience.Current >= PlayerHealth.control._Player.Experience.Max)
        {
            ThucHienTangCapDo();
            // Hien thong bao
            MessageBoxx.control.ShowMessageLevel();
           
            // Hien thong bao mo cua
            if (PlayerHealth.control._Player.Level >= SceneManager.GetActiveScene().buildIndex * 5)
            {
                MessageBoxx.control.ShowSmall("Chúc mừng, trình độ của bạn đã đủ để đi tiếp, hãy khám phá nào!", "CHẤP NHẬN");
            }
        }
    }

    private void ThucHienTangCapDo()
    {
        // Tang cac thong so cho cap tiep theo
        PlayerHealth.control._Player = ExperienceCharacter.ThongTinCapTiepTheo();
        // Hien hieu ung tang level
        sound.Playsound("LevelUp");
        HieuUngTangLevel.SetActive(true);
    }

    public static Player ThongTinCapTiepTheo() {
        Player _player = new Player();
        // Kinh nghiem, tang 300 moi cap
        if (PlayerHealth.control._Player.Experience.Current >= PlayerHealth.control._Player.Experience.Max)
        {
            _player.Experience.Current = PlayerHealth.control._Player.Experience.Current - PlayerHealth.control._Player.Experience.Max;
        }
        else
        {
            _player.Experience.Current = 0;
        }
        //
        _player.Experience.Max = PlayerHealth.control._Player.Experience.Max + 300;
        // Tang Level len 1 bac
        _player.Level = PlayerHealth.control._Player.Level + 1;
        // Tang mau
        _player.Health.Max = PlayerHealth.control._Player.Health.Max + 200; // Tang max
        _player.Health.Current = PlayerHealth.control._Player.Health.Current + 200; // Tang mau hien tai
        // Tang diem ky nang, dung de tang ky nang
        _player.PointSkill = PlayerHealth.control._Player.PointSkill + 1;
        // No 
        _player.Infuriate.Max = 50 + PlayerHealth.control._Player.Infuriate.Max;
        // Tang luc chien
        _player.Damage = 8 + PlayerHealth.control._Player.Damage;
        // Vang 
        _player.Gold = PlayerHealth.control._Player.Gold + 1000; // 
        return _player;
    }
}
