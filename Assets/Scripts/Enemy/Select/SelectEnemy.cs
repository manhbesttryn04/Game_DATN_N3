using UnityEngine;
using UnityEngine.UI;

public class SelectEnemy : MonoBehaviour {

    private Slider enemySlider;
    private EnemyHealth enemyHealth;
    public static Transform Player;

    private void Start()
    {
         enemySlider = null;
        enemyHealth = null;
        // Load lai scence se huy chon
        SelectManager.OnDelete();
        Player = transform;
    }

    public static void HandlingSelectedGameObject(GameObject _enemyGraphics, Slider _enemySlider, bool value, Enemy _enemy)
    {
        //
        if (value)
        {   // Neu da co doi tuong duoc chon roi, se khong chon nua,
            // cho den khi nao huy chon doi tuong trc do
            if (!SelectManager.HasSelect())
            {
                _enemySlider.gameObject.SetActive(true); // hien thanh mau
                // Chon doi tuong
                SelectManager.OnSelect(_enemyGraphics); // Con doi tuong
                SelectManager.SetInformation(_enemyGraphics, _enemy);
            }
            else { 
                // Hien thi thong tin
                SelectManager.SetInformation(_enemyGraphics, _enemy);
            }
        }
        else {
            // Huy doi tuong da duoc chon
            if (SelectManager.CompareGame(_enemyGraphics))
            {
                SelectManager.OnDelete(); // Huy chon
                if (!_enemy.IsBoss)
                {
                    _enemySlider.gameObject.SetActive(false); // an thanh mau
                }
            }
        }
    }

    private bool HandlingGetValue(GameObject EnemyGraphics)
    {
        // hasComponenet: class dung de kiem tra ton tai cua Componenet
        // F12 de di toi no

        // Kiem tra ton tai SelectEnemy hay khong?
        if (!hasComponent.HasComponent<EnemyHealth>(EnemyGraphics))
        {
            return false;
        }
        enemyHealth = EnemyGraphics.GetComponent<EnemyHealth>(); // Lay thong tin cua doi tuong
        enemySlider = enemyHealth.enemyHealthSlider; // Lay doi tuong slider
        return true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Kiem tra lay cac doi tuong can thiet
            if (HandlingGetValue(other.gameObject) && !enemyHealth.GetTanHinh())
            {
                if(KiemTraKhoangCach(other.gameObject)){
                    if (enemyHealth._Enemy.Health.Current <= 0)
                    {
                        HandlingSelectedGameObject(other.gameObject, enemySlider, false, enemyHealth._Enemy);
                    }
                    else
                    {
                        HandlingSelectedGameObject(other.gameObject, enemySlider, true, enemyHealth._Enemy);
                    }
                }
            }
        }
    }

    private float GetScale(Transform trans)
    {
        return trans.position.x - transform.position.x;
    }

    private bool KiemTraKhoangCach(GameObject other)
    {
        if (!SelectManager.HasSelect())
        {
            return true;
        }

        if (GetScale(other.transform) < GetScale(SelectManager.GameSelected.transform))
        {
            return true;
        }
        return false;
    }

    //// Kiem tra thoat khoi pham vi
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && SelectManager.CompareGame(other.gameObject))
        {
            if (HandlingGetValue(other.gameObject))
            {
                // Huy chon
                HandlingSelectedGameObject(other.gameObject, enemySlider, false, enemyHealth._Enemy);
            }
        }
    }
}