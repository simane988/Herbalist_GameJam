using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{

    [SerializeField] float timeToDie;
    [SerializeField] TMP_Text time;
    [SerializeField] NPC npc;
    [SerializeField] GameManager gm;

    private bool started = false;

    private void Start()
    {
        time.gameObject.SetActive(false);
        started = false;
    }

    public void startTimer()
    {
        time.gameObject.SetActive(true);
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if(timeToDie > 0)
            {
                timeToDie -= Time.deltaTime;
                TimeSpan timeFormat = TimeSpan.FromSeconds(timeToDie);
                time.text = timeFormat.ToString("mm':'ss");
            }
            else
            {
                npc.DTF();
                started = false;
                gm.loose = true;
            }
        }
        
    }
}
