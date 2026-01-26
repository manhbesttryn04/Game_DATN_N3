using System.Collections;
using System.Linq;
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
    public int index = 0;
    public bool isTyping = false;
    public bool isPlaying = false;
    public AudioSource musicBG;
    public float typingSpeed = 0.05f;
    public float delay = 1f;
    public float currentDelay = 0;
    public float speedDelay = 1f;
    public bool isDelay = true;
    

    void Start()
    { 
    }
       
    void Update()
    {
       
        EndStory();
        RunStory();
        
       //XetStory();
    }
    public void RunStory()
    {if (!isPlaying)
        {
            if (isDelay)
            {
                currentDelay += speedDelay * Time.deltaTime;

                if (currentDelay >= delay)
                {
                    index++;
                    isTyping = true;
                    NextStory();
                    currentDelay = 0;
                    isDelay = false;
                }

            }

            if (storyTextBox.text == storyText[index] && index <= storyText.Length - 1)
            {
                isDelay = true;
            }

            if (index >= storyText.Length - 1) isDelay = false;
        }
        else{
           
            storyTextBox.gameObject.SetActive(false);
            
        
        } }

    public void NextStory()
    {
        if (isTyping)
        {
            for (int i = 0; i < ojImageStory.Length; i++)
            {
                ojImageStory[i].SetActive(false);
            }
            ojImageStory[index].SetActive(true);

            StartCoroutine(TypeText(storyText[index]));
            isTyping = false;
        }
     
    }
    
    public void StartStory()
    {

        for (int i = 0; i < ojImageStory.Length; i++)
        {
            ojImageStory[i].SetActive(false);
        }
        ojImageStory[index].SetActive(true);
        StartCoroutine(TypeText(storyText[index]));
        
    }
   
    public void PlayGame(string namescene)
    { isPlaying = true;
        isTyping = false;
        musicBG.enabled = false;
        storyTextBox.text = "";
       buttonSkip.gameObject.SetActive(false);
       buttonPlay.gameObject. SetActive(false);
        SceneManager.LoadScene("Load", LoadSceneMode.Additive);
        StartCoroutine(Loading(namescene));
    }
    public IEnumerator Loading(string namescene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(namescene);
       
    }
    public void EndStory()
    {
        if(index >= storyText.Length-1)
        {if(!isPlaying)
            {
                buttonPlay.SetActive(true);
                buttonSkip.gameObject.SetActive(false);
            }
          
           // isDelay = false;
           
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
    //code cua manh
}
