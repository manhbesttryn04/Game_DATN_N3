using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {




	public string NameScencce = "Level 1.3";
    public GameObject Message;
    //public SoundManager sound;
    //private void Start()
    //{
    //    sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    //}
    public void PlayGame()
    {
		SceneManager.LoadScene(NameScencce);
    }

    public void GoScenceIndex(int sceneIndex)
    {
        Scene.GoSceneIndex(sceneIndex);
       
    }

    public void GoScenceName(string sceneName)
    {
        Scene.GoSceneName(sceneName);
     
    }

    public void quit()
    {
        Application.Quit(); // khi nào buil game thì mới sử dụng được
    }

    public void GoGUIPlay(int indexScene)
    {
        if (PlayerManager.LoadInformation() == null)
        {
            Scene.GoSceneName("Gui");
        }
        else
        {
            Scene.GoSceneIndex(indexScene);
        }
    }

    public void GoMenuChose()
    {
        if (!PlayerManager.HasPlayGame())
        {
            Scene.GoSceneName("Gui");
        }
        else
        {
            Message.SetActive(true);
        }
    }

    public void AllowResetGame()
    {
        // Thong tin nhan vat
        if (File.Exists(PlayerManager.pathInformation))
        {
            File.Delete(PlayerManager.pathInformation);
        }

        // Thay doi nhan vat
        if (File.Exists(PlayerManager.pathChangePlayer))
        {
            File.Delete(PlayerManager.pathChangePlayer);
        }

        // Ky nang nhan vat
        if (File.Exists(PlayerManager.pathSkillPlayer))
        {
            File.Delete(PlayerManager.pathSkillPlayer);
        }

        // Vat pham nhan vat
        if (File.Exists(PlayerManager.pathItemPlayer))
        {
            File.Delete(PlayerManager.pathItemPlayer);
        }

        // Enemy 
        // Thong tin boss
        if (File.Exists(EnemyManager.pathFile))
        {
            File.Delete(EnemyManager.pathFile);
        }


        Scene.GoSceneName("StartCutScene");
    }

    public void DisallowResetGame(){
        Message.SetActive(false);
    }
}
