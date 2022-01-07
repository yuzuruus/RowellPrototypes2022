using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    // UI Management
    public TMP_Text interactPrompt;
    public TMP_Text giveItemPrompt;
    public Canvas npcTextUI;

    // Interaction Management
    public string interactionKey = "e";
    public string giveItemKey = "f";

    // Story management
    public Conversation[] storyManager; 
    

    // Start is called before the first frame update
    void Awake()
    {
        // Snag reference to the NPC text UI component
        npcTextUI = GameObject.Find("UICanvas").GetComponent<Canvas>();
        npcTextUI.gameObject.SetActive(false);

        interactPrompt.text = "Press " + interactionKey + " to talk";
        giveItemPrompt.text = "Press " + giveItemPrompt + " to give item!";

        interactPrompt.gameObject.SetActive(false);
        giveItemPrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ClickPlayerController player = other.gameObject.GetComponent<ClickPlayerController>();
            if(player)
            {
                interactPrompt.gameObject.SetActive(true);
                player.TurnOnTalk(this);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ClickPlayerController player = other.gameObject.GetComponent<ClickPlayerController>();
            if (player)
            {
                interactPrompt.gameObject.SetActive(false);
                player.TurnOffTalk();
            }
        }
    }
}
