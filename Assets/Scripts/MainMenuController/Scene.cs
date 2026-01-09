using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scene {

    public static void GoSceneName(string SceneName){
        SceneLoader.NameScene = SceneName;
        SceneManager.LoadScene("Load");
    }

    public static void GoSceneIndex(int SceneIndex)
    {
        SceneLoader.IndexScene = SceneIndex;

        SceneManager.LoadScene("Load");
    }
}
