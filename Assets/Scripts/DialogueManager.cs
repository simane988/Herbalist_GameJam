using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

	public GameObject dialogueBox;
	public TMP_Text nameText;
	public TMP_Text dialogueText;

	public int waitSeconds = 3;

	//public Animator animator;

	private Queue<string> sentences = new Queue<string>();
	public bool isDialogueActive = false;

	// Use this for initialization
	void Start()
	{
		isDialogueActive = false;
		dialogueBox.SetActive(false);
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
        if (!isDialogueActive)
        {
            isDialogueActive = true;
			dialogueBox.SetActive(true);
			sentences.Clear();

            foreach (string sentence in dialogue.sentences)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
	{
        dialogueText.text = "";
		string[] subs = sentence.Split(':');
		if(subs[0] != nameText.text)
        {
			nameText.text = "";
			foreach (char letter in subs[0].ToCharArray())
			{
				nameText.text += letter;
				yield return null;
			}
        }
		
		foreach (char letter in subs[1].ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}

		yield return new WaitForSeconds(3);
		DisplayNextSentence();
	}

	void EndDialogue()
	{
		dialogueBox.SetActive(false);
		isDialogueActive = false;
		//animator.SetBool("IsOpen", false);
	}

}
