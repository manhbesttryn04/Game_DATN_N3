using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SelectManager {

    private static string strEnemy = "";
    public static int ID = -1;
    public static GameObject GameSelected;
    public static Slider SliderSelected;

    /// <summary>
    /// Kiem tra ton tai da chon doi tuong nao hay chua
    /// </summary>
    /// <returns></returns>
    public static bool HasSelect() {
        if (ID != -1)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Lay thong tin hien thi len bang thong bao, da qua dinh dang
    /// </summary>
    /// <returns></returns>
    public static string GetInformation() {
        // Mota: Lay thong tin de hien thi len bang thong bao
        if (SelectManager.strEnemy.Length != 0)
        {
            return SelectManager.strEnemy;
        }
        return "";
    }

    /// <summary>
    /// Thay doi thong tin
    /// </summary>
    /// <param name="enemy">Doi tuong enemy</param>
    /// <param name="enemyHealth">Thong tin mau cua enemy</param>
    public static void SetInformation(GameObject enemyGraphics,Enemy _enemy) {

        if (SelectManager.HasSelect() && SelectManager.CompareGame(enemyGraphics))
        {
            // Gan thong tin
            SelectManager.strEnemy = String.Format("Quái cấp độ {0}: {1}/{2}", _enemy.GetIntLevel(), _enemy.Health.Current, _enemy.Health.Max);
        }
    }

    /// <summary>
    /// Thuc hien chon doi tuong
    /// </summary>
    /// <param name="enemy">Doi tuong</param>
    public static void OnSelect(GameObject enemy)
    {
        try
        {
            if (!SelectManager.HasSelect())
            {
                // Lay ma cua doi tuong
                SelectManager.ID = enemy.GetInstanceID();
                SelectManager.GameSelected = enemy;
                SelectManager.SliderSelected = enemy.GetComponent<EnemyHealth>().enemyHealthSlider;
            }
        }
        catch {
            Debug.Log("Select incorrect.");
        }
    }

    /// <summary>
    /// Thuc hien so sach doi tuong dang duoc chon voi moi doi tuong enemy khac
    /// </summary>
    /// <param name="enemy">Doi tuong enemy khac</param>
    /// <returns></returns>
    public static bool CompareGame(GameObject enemy) {
        if (enemy != null)
        {
            // So sanh
            if (SelectManager.ID == enemy.GetInstanceID())
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Huy chon doi tuong
    /// </summary>
    public static void OnDelete()
    {
        // Huy chon
        SelectManager.strEnemy = "";
        SelectManager.ID = -1;
        GameSelected = null;
        SliderSelected = null;
    } 
}
