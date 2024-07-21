using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    public float timer;
    float timealert;
    public Image[] alert;
    float colorindex;
    public float alertrate;

    // Start is called before the first frame update
    void Start()
    {
        timealert = timer;
    }

    // Update is called once per frame
    void Update()
    {
        colorindex = Mathf.Lerp(colorindex, 0, alertrate);
        if (timealert <= timer)
        {
            timealert -= Time.deltaTime;
            if (timealert <= 0)
            {
                timealert = timer;
                colorindex = 100;
            }
        }

        byte a = Convert.ToByte(colorindex);
        alert[0].color = new Color32(255, 100, 0, a);
        alert[1].color = new Color32(240, 230, 125, a);
        
    }
}
