using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    // Information Management
    public string NPCName = "Unknown";

    // State management
    bool ableToTalkTo = false;

    // UI Management
    public TMP_Text interactPrompt;
    public UIManager npcTextUI;
    public TMP_Text npcText;
    public TMP_Text nameText;
    public UnityEngine.UI.Button continueButton;
    public UnityEngine.UI.Image convoBackground;
    public UnityEngine.UI.Image nameBackground;

    // Interaction Management
    public string interactionKey = "e";

    // Story management
    public Conversation[] conversations;
    int currentConversationState = 0;

    // Player manager
    ClickPlayerController player;

    // Start is called before the first frame update
    void Awake()
    {
        // Snag reference to the NPC text UI component
        npcText = npcTextUI.npcText;
        nameText = npcTextUI.nameText;
        continueButton = npcTextUI.continueButton;
        convoBackground = npcTextUI.convoBackground;
        nameBackground = npcTextUI.nameBackground;

        interactPrompt.text = "Press " + interactionKey + " to talk";
        interactPrompt.gameObject.SetActive(false);

        player = GameObject.Find("Player").GetComponent<ClickPlayerController>();

        DisableConversationUI();
        
        // Disable conversations
        foreach(Conversation convo in conversations)
        {
            convo.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ableToTalkTo = true;

            // Turn on world UI talk prompt
            interactPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ableToTalkTo = false;
            
            // Turn off world UI talk prompts
            interactPrompt.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(ableToTalkTo && Input.GetKeyDown(interactionKey))
        {
            Debug.Log("I interacted!");
            EnableConversationUI();
            // Disable player controls
            player.TurnOffControl();
            // Turn on and start current convo
            conversations[currentConversationState].enabled = true;
            conversations[currentConversationState].StartConversation();
        }
    }

    void EnableConversationUI()
    {
        npcText.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        convoBackground.gameObject.SetActive(true);
        nameBackground.gameObject.SetActive(true);
    }
    void DisableConversationUI()
    {
        npcText.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        convoBackground.gameObject.SetActive(false);
        nameBackground.gameObject.SetActive(false);
    }

    public void IncrementConversationState()
    {
        DisableConversationUI();
        //if(currentConversationState < conversations.Length-1) // If we aren't already at the final conversation, toggle the next one!
        //{
        //    currentConversationState++;
        //}
    }
}
