using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public enum IndexBoss { None, Chapter1, Chapter2, Chapter21, Chapter3, Chapter4, Chapter41, Chapter42};

public static class EnemyManager{
	// Thong tin duong dan file
	public static string pathFile = Application.persistentDataPath + "/DataEnemyBoss.dat"; 

	public static void SaveBoss (List<IndexBoss> lstBos){
		// Tao doi tuong ket noi
		BinaryFormatter bf = new BinaryFormatter ();
		// Tao file de luu lai thong tin
		FileStream file = File.Create (pathFile);
		// Luu thong tin xuong file
		bf.Serialize (file,lstBos);
		// Dong file
		file.Close ();
	}

	public static List<IndexBoss> LoadBoss(){
		if (File.Exists (pathFile)) {
			List<IndexBoss> lstBoss = new List<IndexBoss> ();
			// Tao doi tuong ket noi
			BinaryFormatter bf = new BinaryFormatter ();
			// Mo file luu tru
			FileStream file = File.Open (pathFile,FileMode.Open);
			// Lay thong tin player
			lstBoss = bf.Deserialize (file) as List<IndexBoss>;
			// Dong file
			file.Close ();
			return lstBoss;
		}
		return null;	
	}

	public static bool CheckIndexBoss(List<IndexBoss> lst,IndexBoss index){
		if (lst != null) {
			if (lst.Count > 0) {
				foreach (IndexBoss boss in lst) {
					if (index == boss) {
						return true;
					}
				}
			}
		}
		return false;
	}

	public static void DeleteIndexBoss(List<IndexBoss> lst, IndexBoss index){
		if (lst != null) {
			if (lst.Count > 0) {
				foreach (IndexBoss boss in lst) {
					if (index == boss) {
						lst.Remove (boss);
						SaveBoss (lst);
						break;
					}
				}
			}
		}
	}
}
