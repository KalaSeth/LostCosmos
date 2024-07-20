using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

     
    [Tooltip(" Map UI Panel")]
    [SerializeField] public GameObject MapPanel;

     
    [Tooltip(" Pause UI Panel")]
    [SerializeField] public GameObject PausePanel;

     
    [Tooltip(" HUD UI Panel")]
    [SerializeField] public GameObject HUDPanel;

     
    [Tooltip(" if true => Paused, false => Unpaused")]
    [SerializeField] public bool IsPaused;

     
    [Tooltip(" if true => Player is dead, false => Player is not Dead")]
    [SerializeField] public bool IsDead;

     
    [Tooltip(" if Cutscene is playing or not, true = yes, false = no")]
    [SerializeField] public bool IsCutscene;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
