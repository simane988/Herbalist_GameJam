using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{

    [SerializeField] GameObject dialogueObject;
    private bool isDialogueStarted = false;
    private bool isDialogueEnded = false;

    DialogueManager dialogueManager;
    DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        isDialogueStarted = false;
        isDialogueEnded = false;
        dialogueManager = dialogueObject.GetComponent<DialogueManager>();
        dialogueTrigger = dialogueObject.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            dialogueTrigger.TriggerDialogue();
        }
        if (!dialogueManager.isDialogueActive && isDialogueStarted)
        {
            SceneManager.LoadScene("Level1");
        }
    }


}
