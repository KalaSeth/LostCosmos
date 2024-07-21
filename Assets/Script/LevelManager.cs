using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

     
    [Tooltip(" Map UI Panel")]
    [SerializeField] public GameObject MapPanel;

     
    [Tooltip(" Pause UI Panel")]
    [SerializeField] public GameObject PausePanel;

     
    [Tooltip(" HUD UI Panel")]
    [SerializeField] public GameObject HUDPanel;
    
    [Tooltip(" Dead UI Panel")]
    [SerializeField] public GameObject DeadPanel;
    [SerializeField] public GameObject alertpanel;

    [Tooltip(" Dialogue UI Panel")]
    [SerializeField] public GameObject DialoguePanel;

    [Tooltip(" if true => Paused, false => Unpaused")]
    [SerializeField] public bool IsPaused;

     
    [Tooltip(" if true => Player is dead, false => Player is not Dead")]
    [SerializeField] public bool IsDead;

     
    [Tooltip(" if Cutscene is playing or not, true = yes, false = no")]
    [SerializeField] public bool IsCutscene;

    [SerializeField]public string[] QuestText;
    public int QuestIndex;

    public Text QuestUItext;
    public Text DialogueUItext;
    public float Dialoguetime;
    [SerializeField]public string[] DialogueText;
    public int DialogueIndex;

    public GameObject[] dialoButtons;

    public float typingSpeed = 0.05f; // Speed at which characters are displayed

    private string currentDialogue;
    private Coroutine typingCoroutine;

    public bool[] Task;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;
        IsPaused = false;

        for (int i = 0; i <= Task.Length - 1; i++)
        {
            Task[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        QuestUItext.text = QuestText[0]; 
        
    }

    public void onclickDisplaynext()
    {
        switch (DialogueIndex)
        {
            case 0:
                dwriter();
                break;

            case 1:
                dwriter();
                IsCutscene = false;
                alertpanel.SetActive(false);
                break;

            case 2:
                HUDPanel.SetActive(true);
                dwriter();
                break;

            case 3:
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 4:
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 5:
                dwriter();
                break;

            case 6:
                dialoButtons[1].SetActive(true);
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 7:
                dialoButtons[1].SetActive(true);
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 8:
                dwriter();
                break;

            case 9:
                dwriter();
                break;

            case 10:
                dwriter();
                break;
        }
    }

    void DelayDialoguebox()
    {
        DialoguePanel.SetActive(true);
    }

    public void OnClickNextDialogue()
    {
        dialoButtons[0].SetActive(false);
        dialoButtons[1].SetActive(false);
        DialogueIndex++;
        StopAllCoroutines();
    }

    public void OnclickDied()
    {
        IsDead = true;
        
    }

    public void OnClickCoordinate(int i)
    {
        PlayerMovement.instance.PlanetIndex = i;
    }

    public void dwriter()
    {
        DialogueUItext.text = " ";
        StartCoroutine(TypeDialogue());
    }

     private IEnumerator TypeDialogue()
     {
        DialogueUItext.text = " ";
        foreach (char letter in DialogueText[DialogueIndex])
        {
            DialogueUItext.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        dialoButtons[0].SetActive(true);
        if (DialogueIndex == 6 || DialogueIndex == 7)
        {
            dialoButtons[1].SetActive(true);
        }
    }
}
