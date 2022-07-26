using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] Camera playerCamera;
    [SerializeField] RawImage E;

    private Vector3 rayStartPosition;
    private Interactable interactable;
    private Interactable previousInteractable = null;
    private int interactableLayer = 1 << 6;
    [SerializeField] private float interactableRadius;
    [SerializeField] private GameObject lightP;

    [SerializeField] public Dictionary<string, int> collectedHerbs = new Dictionary<string, int>();

    private bool hoveredHerbal = false;
    private bool hoveredNPC = false;

    void Start()
    {
        rayStartPosition = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ReleaseRay();
    }

    private void ReleaseRay()
    {
        Ray ray = playerCamera.ScreenPointToRay(rayStartPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactableRadius, interactableLayer))
        {
            interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (interactable != this && interactable != previousInteractable)
                {
                    interactable.OnHoverEnter();
                    E.gameObject.SetActive(true);
                    if(interactable.tag == "NPC")
                    {
                        hoveredHerbal = false;
                        hoveredNPC = true;
                    }
                    else
                    {
                        hoveredNPC = false;
                        hoveredHerbal = true;
                    }
                    previousInteractable = interactable;
                }

            }
            else if (previousInteractable != null)
            {
                previousInteractable.OnHoverExit();
                hoveredHerbal = false;
                hoveredNPC = false;
                E.gameObject.SetActive(false);
                previousInteractable = null;
            }
        }
        else if (previousInteractable != null)
        {
            previousInteractable.OnHoverExit();
            hoveredHerbal = false;
            hoveredNPC = false;
            E.gameObject.SetActive(false);
            previousInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House")
        {
            lightP.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "House")
        {
            lightP.SetActive(true);
        }
    }    

    public bool IsHoveredHerbal()
    {
        return hoveredHerbal;
    }

    public bool IsHoveredNPC()
    {
        return hoveredNPC;
    }

    public void InteractWithHerb()
    {
        if(previousInteractable != null)
        {
            if (collectedHerbs.TryGetValue(previousInteractable.tag, out int value))
            {
                Debug.Log("Herb " + previousInteractable.tag + " is allready exist.");
                collectedHerbs[previousInteractable.tag] = value + 1;
                Debug.Log("Herb " + previousInteractable.tag + " is increased. Now you have: " + collectedHerbs[previousInteractable.tag]);
            }
            else
            {
                collectedHerbs.Add(previousInteractable.tag, 1);
                Debug.Log("Herb " + previousInteractable.tag + " is added to the dictionary.");
                Debug.Log("Herb " + previousInteractable.tag + " is increased. Now you have: " + collectedHerbs[previousInteractable.tag]);
            }

            foreach (KeyValuePair<string, int> kvp in collectedHerbs)
            {
                Debug.LogFormat("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }

            Destroy(previousInteractable.gameObject);
            hoveredHerbal = false;
            previousInteractable = null;
            E.gameObject.SetActive(false);
        }
        
    }

    public void InteractWithNPC()
    {
        if(previousInteractable != null)
        {
            hoveredHerbal = false;
            NPC npc = previousInteractable.gameObject.GetComponent<NPC>();
            npc.Interact(collectedHerbs);
            hoveredNPC = false;
            previousInteractable = null;
            E.gameObject.SetActive(false);
        }

    }
}
