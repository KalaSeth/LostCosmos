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
    public Image Popo;
    [TextArea]
    [SerializeField]public string[] DialogueText;
    public int DialogueIndex;

    public GameObject[] dialoButtons;

    public float typingSpeed = 0.05f; // Speed at which characters are displayed

    private string currentDialogue;
    private Coroutine typingCoroutine;

    public GameObject[] TaskCc;
    public Text WindStatus;
    public Slider GravityStatus;
    public Slider O2Status;
    public bool[] Status;
    int WindS;
    int GraS;
    public int O2Timer;
    float OwS;

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
        DialogueIndex = 0;
        OwS = O2Timer;
        Popo.color = new Color32(255, 255, 255, 255);
        for (int i = 0; i <= Task.Length - 1; i++)
        {
            Task[i] = false;
        }
        for (int j = 0; j <= Status.Length - 1; j++)
        {
            Status[j] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        QuestUItext.text = QuestText[0];
        ShipStatusCheck();
    }

    public void Taskchecker()
    {

    }

    public void ShipStatusCheck()
    {
        if (Status[0] == true)
        {
            WindStatus.text = "4.4";
        }else
        {
            WindS = Random.Range(1, 12);
            WindStatus.text = WindS.ToString();
        }

        if (Status[1] == true)
        {
            GravityStatus.value = 0.9f;
        }
        else
        {
            GravityStatus.value = Random.value;
        }

        if (Status[2] == true)
        {
            O2Status.value = 1;
        }
        else
        {
            if (OwS <= O2Timer)
            {
                OwS -= Time.deltaTime;
                if (OwS <= 0)
                {
                    IsDead = true;
                }
            }
            O2Status.value = 1;
        }
    }


    public void onclickDisplaynext()
    {
        switch (DialogueIndex)
        {
            case 0:
                DialogueIndex++;
                Popo.color = new Color32(255, 255, 255, 255);
                dwriter();
                
                break;

            case 1:
                DialogueIndex++;
                Popo.color = new Color32(255, 255, 255, 255);
                dwriter();
                IsCutscene = false;
                alertpanel.SetActive(false);
                break;

            case 2:
                DialogueIndex++;
                Popo.color = new Color32(255, 255, 255, 255);
                dwriter();
                break;

            case 3:
                HUDPanel.SetActive(true);
                Popo.color = new Color32(255, 255, 255, 255);
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 4:
                Popo.color = new Color32(255, 255, 255, 255);
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 5:
                DialogueIndex++;
                Popo.color = new Color32(255, 255, 255, 255);
                dwriter();
                break;

            case 6:
                Popo.color = new Color32(255, 255, 100, 255);
                dialoButtons[1].SetActive(true);
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 7:
                Popo.color = new Color32(255, 255, 100, 255);
                dialoButtons[1].SetActive(true);
                DialoguePanel.SetActive(false);
                dwriter();
                break;

            case 8:
                DialogueIndex++;
                Popo.color = new Color32(255, 255, 100, 255);
                dwriter();
                break;

            case 9:
                Popo.color = new Color32(255, 255, 100, 255);
                dwriter();
                break;

            case 10:
                Popo.color = new Color32(255, 255, 100, 255);
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
        else { dialoButtons[1].SetActive(false); }

    }
}
