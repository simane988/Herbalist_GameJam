using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    List<string> expected_names;
    List<int> expected_values;
    public DialogueTrigger dt1;
    public DialogueTrigger dtt;
    public DialogueTrigger dtf;

    public Player player;
    CharacterController cc;
    Timer timer;

    private int state;

    public GameManager gm;

    private void Start()
    {

        cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        timer = player.GetComponent<Timer>();
        state = 0;

        expected_names = new List<string>();
        expected_values = new List<int>();

        expected_names.Add("Herbal 5");
        expected_values.Add(2);
        expected_names.Add("Herbal 4");
        expected_values.Add(1);

        //dt1.TriggerDialogue();
        //StartCoroutine("FirstDialogue");

    }

    IEnumerator FirstDialogue()
    {
        cc.enabled = false;
        yield return new WaitUntil(() => !dt1.dialogueManager.isDialogueActive);
        state++;
        cc.enabled = true;
        timer.startTimer();
        yield return null;
    }

    IEnumerator DttDialogue()
    {
        cc.enabled = false;
        yield return new WaitUntil(() => !dtt.dialogueManager.isDialogueActive);
        cc.enabled = true;
        yield return null;
    }

    IEnumerator DtfDialogue()
    {
        //Debug.Log("DTF");
        cc.enabled = false;
        yield return new WaitUntil(() => !dtf.dialogueManager.isDialogueActive);
        cc.enabled = true;
        yield return null;
    }

    public void Interact(Dictionary<string, int> collected)
    {
        switch (state)
        {
            case 0:
                dt1.TriggerDialogue();
                StartCoroutine("FirstDialogue");
                break;
            case 1:
                if (checkHerbs(collected))
                {
                    dtt.TriggerDialogue();
                    StartCoroutine("DttDialogue");
                }
                else
                {
                    DTF();
                }
                break;
        }            
    }

    public void DTF()
    {
        dtf.TriggerDialogue();
        StartCoroutine("DtfDialogue");
    }

    public bool checkHerbs(Dictionary<string, int> collected)
    {
        for (int i = 0; i < expected_names.Count; i++)
        {
            if (!collected.TryGetValue(expected_names[i], out int _) ||
                collected[expected_names[i]] < expected_values[i])
            {
                return false;
            }

        }

        gm.win = true;
        return true;
    }
}
