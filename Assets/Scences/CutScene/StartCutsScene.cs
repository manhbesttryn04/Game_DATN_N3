using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCutsScene : MonoBehaviour
{
   [Header("Story Cut Scene")]
    public GameObject[] ojImageStory;
    public string[] storyText;
    public TextMeshProUGUI storyTextBox;
    public Button buttonSkip;
    public GameObject buttonPlay;
    public AudioSource musicBG;
    public int index = 0;
    public bool isTyping = false;
    public float typingSpeed = 0.05f;
    

    void Start()
    {
        StartStory();
    }
    void Update()
    {
       
        EndStory();
       XetStory();
    }

    public void NextStory()
    {
        if (isTyping)
        {
            for(int i = 0; i < ojImageStory.Length; i++)
            {
                ojImageStory[i].SetActive(false);
            }
            ojImageStory[index].SetActive(true);
           
            StartCoroutine(TypeText(storyText[index]));
            isTyping = false;
        } }
    public void XetStory()
    {
        if(storyTextBox.text == storyText[index])
        {
           buttonSkip.gameObject.SetActive(true);
        }else {
            buttonSkip.gameObject.SetActive(false); } }
    //
    public void StartStory()
    {

        for (int i = 0; i < ojImageStory.Length; i++)
        {
            ojImageStory[i].SetActive(false);
        }
        ojImageStory[index].SetActive(true);
        StartCoroutine(TypeText(storyText[index]));
        
    }
    public void SkipStory()
    {
        index++;
        isTyping = true;
        NextStory();



    }
    public void PlayGame(string namescene)
    { musicBG.enabled = false;
        storyTextBox.gameObject.SetActive(false);
        
        buttonPlay.SetActive(false);
        SceneManager.LoadScene("Load",LoadSceneMode.Additive);
        StartCoroutine(Loading(namescene));
        
        

    }
    public IEnumerator Loading(string namescene)
    {
       yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(namescene);
    }
    public void EndStory()
    {
        if(index >= storyText.Length-1)
        {
            buttonPlay.SetActive(true);
            buttonSkip.gameObject.SetActive(false);
           
        }
    }
    public IEnumerator TypeText(string text)
    {
        storyTextBox.text = "";
        foreach (char c in text)
        {
            storyTextBox.text += c;
            yield return new WaitForSeconds(typingSpeed);
            buttonSkip.interactable = true;


        }
    }
}
