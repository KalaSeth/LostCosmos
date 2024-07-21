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

    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;
        IsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        QuestUItext.text = QuestText[0];
        DialogueUItext.text = DialogueText[DialogueIndex];
    }

    public void ShowDialogue()
    {
        switch (DialogueIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
        }
    }
    
    public void onclickDisplaynext()
    {
        switch (DialogueIndex)
        {
            case 0:
                
                Invoke("DelayDialoguebox", Dialoguetime);
                
                break;

            case 1:
                IsCutscene = false;
                Invoke("DelayDialoguebox", Dialoguetime);
                break;

            case 2:
                Invoke("DelayDialoguebox", Dialoguetime);
                break;

            case 3:
                Invoke("DelayDialoguebox", Dialoguetime);
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                break;

            case 8:
                break;

            case 9:
                break;

            case 10:
                break;
        }
    }

    void DelayDialoguebox()
    {
        DialoguePanel.SetActive(true);
    }

    public void OnClickNextDialogue()
    {
        DialogueIndex++;
    }

    public void OnclickDied()
    {
        IsDead = true;
        
    }

    public void OnClickCoordinate(int i)
    {
        PlayerMovement.instance.PlanetIndex = i;
    }

}
