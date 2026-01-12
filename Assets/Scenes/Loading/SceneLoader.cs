using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour {

    public static int IndexScene = -1;
    public static string NameScene = "";
    //
    public Text textSlider;
    public Slider slider;
    private bool Load = false;
    private AsyncOperation operation;
    private void Update(){
        if (IndexScene != -1 && !Load)
        {
            StartCoroutine(LoadIndexScene(IndexScene));
            Load = true;
        }
        else if (NameScene != "" && !Load)
        {
            StartCoroutine(LoadNameScene(NameScene));
            Load = true;
        }
    }

    private IEnumerator LoadNameScene(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            textSlider.text = Math.Round((slider.value * 100),2) + "%";
            NameScene = "";
            yield return null;
        }
        
    }

    private IEnumerator LoadIndexScene(int sceneIndex)
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            textSlider.text = (slider.value * 100) + "%";
            IndexScene = -1;
            yield return null;
        }
    }
}
