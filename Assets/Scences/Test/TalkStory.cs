using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkStroy : MonoBehaviour
{
    public string[] talkStory;
    public Sprite[] imageChatacters;
    public TextMeshProUGUI textTalk;
    public GameObject imageCharacter;
    public GameObject talkOject;
    public int index = 0;
    public bool isActive= false;
    public bool isNext = false;
    public GameObject boss;
    void Start()
    { boss = GameObject.FindGameObjectWithTag("Boss")   ;
        if (boss != null)
        {   talkOject.SetActive(true);
            isActive = true;
            isNext = true;
            LoadTalk();
        }else 
        {
            talkOject.SetActive(false);
            isActive = false;
            Debug.Log("No Boss");


        } }

    // Update is called once per frame
  
    public void LoadTalk()
    {
        if (isNext)
        {
            imageCharacter.gameObject.GetComponent<Image>().sprite = imageChatacters[index];
            textTalk.text = talkStory[index];
            isNext = false;
        }
    }
    public void SetActiveTalk()
    {
       
    }
    public void NextTalk()
    {
        index++;
        isNext = true;

        LoadTalk();
        EndTalk();
    }
    public void EndTalk()
    {
        if(index >= imageChatacters.Length || index >= talkStory.Length)
        {
            isActive = false;
        }

        if(!isActive)
        {
            talkOject.SetActive(false);
        }
    }
    
}
