using UnityEngine;
using UnityEngine.UI;
using System;

// Lop de kiem tra ton tai cua component
public static class hasComponent {
	public static bool HasComponent<T>(this GameObject flag)where T : Component{
		return flag.GetComponent<T> () != null;
	}
    public static bool HasComponentInChildren<T>(this GameObject flag) where T : Component
    {
        return flag.GetComponentInChildren<T>() != null;
    }
}

public class CanvasController : MonoBehaviour {

	public static CanvasController control;
    [Header("Thong tin thanh mau")]
	// Doi tuong nhan vat
	//Thanh mau
	public Slider sliderHealth;
    [Header("Thong tin thanh no")]
	// mana
    public Slider SliderInfuriate;
    [Header("Thong tin vang")]
	// Vang
	public Text textGold;
    [Header("Thong tin quai vat")]
    // Thong tin quai
    public Text textEnemy;
    [Header("Thong tin kinh nghiem")]
    public Slider sliderExperience;
    public Text textLevel;
	// Mot so bien xu ly
	private string strMoney;

	public void SetInformation ()
	{
		PlayerHealth _player = PlayerHealth.control;
		// Create slider health
		sliderHealth.maxValue = _player._Player.Health.Max;
		sliderHealth.value = _player._Player.Health.Current;
		// Create slider mana
        SliderInfuriate.maxValue = _player._Player.Infuriate.Max;
        SliderInfuriate.value = _player._Player.Infuriate.Current;
		// Create gold
		textGold.text = convertCurrent (_player._Player.Gold);
        // 
        textEnemy.text = SelectManager.GetInformation();
        // Thuc hien hien thi thanh kinh nghiem
        sliderExperience.maxValue = _player._Player.Experience.Max;
        sliderExperience.value = _player._Player.Experience.Current;
        // Level player
        textLevel.text = "Lv" + _player._Player.Level.ToString();
	}

	private void Update (){
		// Thuc hien thay doi thong tin
		SetInformation ();
	}

	// Chuyen doi chuoi thanh dang tien te
	private string convertCurrent(double money){
        string strMoney = "";
		if (money == 0) {
			return "0";
		}else 
			if (money < 1000000) {
				strMoney = String.Format ("{0:0,00}", money);
			} else if (money < 1000000000) {
				// Hien thi chu "M" khi tien tren 1 trieu
				strMoney = getDecimal(money,1000000) + "M";
			}else {
				// Hien thi chu "B" khi tien tren 1 trieu
				strMoney = getDecimal(money,1000000000) + "B";
			}
		return strMoney;
	}

	// Chi lay 1 so cuoi sau dau cham
	private string getDecimal(double money,double unit){
		if (Math.Round ((money / unit), 1) - Math.Round ((money / money), 0) != 0) {
			return Math.Round ((money / unit), 1).ToString ();
		}
		return Math.Round ((money / unit), 0).ToString ();
	}
}
