using UnityEngine;
// Thuc hien luu thng tin can them thu vien sau:
using System.Runtime.Serialization.Formatters.Binary; // De co the thuc hien luu du lieuj xuong file
using System.IO;
using System.Collections.Generic;

public class PlayerManager{

	// Thong tin duong dan file
	public static string pathInformation = Application.persistentDataPath + "/DataPlayer.dat";
    public static string pathChangePlayer = Application.persistentDataPath + "/ChangePlayer.dat";
    public static string pathSkillPlayer = Application.persistentDataPath + "/SkillPlayer.dat";
    public static string pathItemPlayer = Application.persistentDataPath + "/ItemPlayer.dat"; 
	/// <summary>
	/// Saves the information.
	/// </summary>
	/// <param name="_player">Player.</param>
	public static void SaveInformation (Player _player){
		// Tao doi tuong ket noi
		BinaryFormatter bf = new BinaryFormatter ();
		// Tao file de luu lai thong tin
		FileStream file = File.Create (pathInformation);
		// Luu thong tin xuong file
		bf.Serialize (file,_player);
		// Dong file
		file.Close ();
	}

    public static bool HasPlayGame()
    {
        if (File.Exists(pathInformation))
        {
            return true;
        }
        return false;
    }

	/// <summary>
	/// Loads the information.
	/// </summary>
	/// <returns>The information.</returns>
	public static Player LoadInformation(){
		if (File.Exists (pathInformation)) {
			// Tao doi tuong ket noi
			BinaryFormatter bf = new BinaryFormatter ();
			// Mo file luu tru
			FileStream file = File.Open (pathInformation,FileMode.Open);
			// Lay thong tin player
			Player player = (Player)bf.Deserialize (file);
			// Dong file
			file.Close ();
			return player;
		}
		return null;
	}

	/// <summary>
	/// Saves the change player.
	/// </summary>
	/// <param name="_change">If set to <c>true</c> change.</param>
	public static void SaveChangePlayer (bool _change){
		// Tao doi tuong ket noi
		BinaryFormatter bf = new BinaryFormatter ();
		// Tao file de luu lai thong tin
		FileStream file = File.Create (pathChangePlayer);
		// Luu thong tin xuong file
		bf.Serialize (file,_change);
		// Dong file
		file.Close ();
	}

	public static bool LoadChangePlayer(){
		if (File.Exists (pathChangePlayer)) {
			// Tao doi tuong ket noi
			BinaryFormatter bf = new BinaryFormatter ();
			// Mo file luu tru
			FileStream file = File.Open (pathChangePlayer,FileMode.Open);
			// Lay thong tin player
			bool knight = (bool)bf.Deserialize (file);
			// Dong file
			file.Close ();
			return knight;
		}
		return true;
	}

    public static void SaveSkill(List<SkillController> lstSkill)
    {
        // Tao doi tuong ket noi
        BinaryFormatter bf = new BinaryFormatter();
        // Tao file de luu lai thong tin
        FileStream file = File.Create(pathSkillPlayer);
        // Luu thong tin xuong file
        bf.Serialize(file, lstSkill);
        // Dong file
        file.Close();
    }

    public static List<SkillController> LoadSkill()
    {
        if (File.Exists(pathSkillPlayer))
        {
            // Tao doi tuong ket noi
            BinaryFormatter bf = new BinaryFormatter();
            // Mo file luu tru
            FileStream file = File.Open(pathSkillPlayer, FileMode.Open);
            // Lay thong tin player
            List<SkillController> lstSkill = bf.Deserialize(file) as List<SkillController>;
            // Dong file
            file.Close();
            return lstSkill;
        }
        return null;
    }

    public static void SaveItem(List<VatPham> lstVP)
    {
        // Tao doi tuong ket noi
        BinaryFormatter bf = new BinaryFormatter();
        // Tao file de luu lai thong tin
        FileStream file = File.Create(pathItemPlayer);
        // Luu thong tin xuong file
        bf.Serialize(file, lstVP);
        // Dong file
        file.Close();
    }

    public static List<VatPham> LoadItem()
    {
        if (File.Exists(pathItemPlayer))
        {
            // Tao doi tuong ket noi
            BinaryFormatter bf = new BinaryFormatter();
            // Mo file luu tru
            FileStream file = File.Open(pathItemPlayer, FileMode.Open);
            // Lay thong tin player
            List<VatPham> lstVP = bf.Deserialize(file) as List<VatPham>;
            // Dong file
            file.Close();
            return lstVP;
        }
        return null;
    }
}
