using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeDoor : MonoBehaviour {

	// tao bien static

    public int Levelload = 1;
    public IndexBoss boss;
	public Direction direc;
    public Transform Bong;
	private void OnCollisionEnter2D(Collision2D col)
    {
		if (col.collider.CompareTag("Player"))
        {
            if (CheckBoss())
            {
                // Kiem tra di chuyen
                if (CheckRulesPlayer())
                {
                    SetLoadScene(direc, Levelload); // Thuc hien chuyen huong
                    SelectManager.OnDelete(); // Huy con enemy
                }
            }
        }
    }

    // Thuc hien kiem tra cac dieu kien
    private bool CheckBoss()
    {
        // Thuc hien kiem tra cac dieu kien de chan chuyen man
        if (boss != IndexBoss.None && !EnemyManager.CheckIndexBoss(EnemyManager.LoadBoss(),boss))
        {
            // Hien thong bao
            MessageBoxx.control.ShowSmall("Bạn cần đánh bại kẻ cai trị ở đây, mới có thể qua trang mới được.\nHãy quay lại sau nhé!", "ĐỒNG Ý!");
            // Day nhan vat ra mot khoang 
            PlayerHealth.control.pushBack(PlayerMoving.myBody.gameObject.transform, 3f,0f);
            return false;
        }
        return true; // Chan chuyen scene
    }

    // Thuc hien kiem tra cac dieu kien
    private bool CheckRulesPlayer()
    {
        // Thuc hien kiem tra cac dieu kien de chan chuyen man
        if(PlayerHealth.control._Player.Level < GetLevelByScene()){
            // Hien thong bao
            MessageBoxx.control.ShowSmall("Bạn cần đạt cấp độ " + GetLevelByScene() + " mới có thể qua trang mới?", "ĐỒNG Ý!");
            // Day nhan vat ra mot khoang 
            if (direc == Direction.Top)
            {
                PlayerMoving.myBody.gameObject.transform.position = Bong.position;
            }
            else
            {
                PlayerHealth.control.pushBack(PlayerMoving.myBody.gameObject.transform, 3f, 0f);
            }
            return false;
        }
        return true; // Chan chuyen scene
    }

    public int GetLevelByScene()
    {
        // Lay vi tri scene hien tai
        return (Levelload - 1) * PlayerHealth.nextLevel;
    }

    /// <summary>
    /// Thuc hien load scence
    /// </summary>
    /// <param name="_direc">Dung de dieu huong xuat hien trong scene</param>
    /// <param name="_Levelload">Dieu huong den scene build</param>
    public static void SetLoadScene(Direction _direc, int _Levelload)
    {
        DoorCharacter._player.direction = _direc; // Thay doi huong di chuyen
        SelectManager.OnDelete(); // Huy con enemy

        Scene.GoSceneIndex(_Levelload);// Chuyen man

        Destroy(GameObject.FindGameObjectWithTag("SystemPlayer").gameObject); // Huy doi tuong
        // Luu vi tri
        PlayerPrefs.SetString("Direction", _direc.ToString());
    }
}
