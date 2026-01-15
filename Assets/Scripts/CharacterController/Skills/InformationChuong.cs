using UnityEngine;
using System;

[Serializable]
public class InformationChuong
{
	public GameObject DanGraphics; // Hinh anh vien dan
	public Transform GunTip; // Vi tri xuat hien vien dan
	public float Damage = 0f;
	private bool chuonged = false;
	public float timeNextAttack = 0.5f; // // Thoi gian thuc hien chuong, dung de set co dinh
	public float timeLive = 5f;
	public float speedDan = 50f;

	public bool GetActived(){
		return chuonged;
	}

	public float GetDamage(){
		return Damage;
	}

	public void SetActived(bool value){
		this.chuonged = value;
	}
}
