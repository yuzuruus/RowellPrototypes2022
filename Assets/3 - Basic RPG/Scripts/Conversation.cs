using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Conversation : MonoBehaviour
{
    // Text management
    public List<string> initialText;
    List<string> currentText;

    // Code to manage custom data entry for quests w items
    public bool requireItem = false;
    public List<string> needItemText;
    public CollectableItem requiredItem;
    public int numberOfItems;
    public List<string> receiveItemText;
    public List<string> followUpText;

    // Item turn-in management
    InventoryManager player;
    bool continueConversation = false;
    bool waitingForItems = false;
    bool receivedItems = false;

    // Conversation Management References
    ConversationManager manager;
    int currentConvoPhase = 0;
    bool inConvo = false;

    // Player Manager
    ClickPlayerController playerManager;

    // Consequence processing
    public bool questCompletionChange;
    public GameObject objectToChange;
    public bool turnOnOrOff;
    bool changeComplete = false;

    private void Start()
    {
        currentText = initialText;
        player = GameObject.Find("Player").GetComponent<InventoryManager>();
        playerManager = GameObject.Find("Player").GetComponent<ClickPlayerController>();
        manager = GetComponentInParent<ConversationManager>();

        // Setup button
        manager.continueButton.onClick.AddListener(ContinueConversation);

    }

    // Update is called once per frame
    void Update()
    {
        if(continueConversation)
        {
            continueConversation = false;  // ensure you need a button press to continue the conversation

            if (!inConvo) // Start a new conversation based on current states
            {
                inConvo = true;
                if (requireItem)
                {
                    if (receivedItems)
                    {
                        currentText = followUpText;
                    }
                    else if (waitingForItems)
                    {
                        Debug.Log("Waiting for items");
                        if (CheckPlayerItems())
                        {
                            Debug.Log("Have enough items!");
                            ReceiveItems();
                            currentText = receiveItemText;
                            ProcessQuestChange();
                        }
                        else
                        {
                            Debug.Log("Not enough items ... ");
                            currentText = needItemText;
                        }
                    }
                    else  // No items needed, just talking!
                    {
                        Debug.Log("Waiting");
                        currentText = initialText;
                        waitingForItems = true;
                    }
                }
                else if(currentText != initialText)
                {
                    currentText = initialText;
                }
            }
            ApplyCurrentText();
        }
    }

    void ProcessQuestChange()
    {
        if (questCompletionChange && !changeComplete)
        {
            if(turnOnOrOff)
            {
                objectToChange.SetActive(true);
            }
            else
            {
                objectToChange.SetActive(false);
            }
            changeComplete = true;
        }
    }

    bool CheckPlayerItems()
    {
        int itemCounter = 0;
        foreach(CollectableItem item in player.heldItems)
        {
            if(item.itemName == requiredItem.itemName)
            {
                itemCounter++;
            }
        }
        return itemCounter >= numberOfItems;
    }

    void ApplyCurrentText()
    {
        manager.npcText.text = currentText[currentConvoPhase];
    }

    // TODO still complete this logic!
    void ReceiveItems()
    {
        waitingForItems = false;
        receivedItems = true;
        player.RemoveItem(requiredItem);
    }

    public void StartConversation()
    {
        inConvo = false;
        continueConversation = true;
        currentConvoPhase = 0;
    }

    public void ContinueConversation()
    {
        Debug.Log("Convo continued");
        continueConversation = true;
        currentConvoPhase++;
        if (currentConvoPhase >= currentText.Count)
        {
            currentConvoPhase = 0;
            //inConvo = false;
            playerManager.TurnOnControl();
            MoveOnToNextConversation();
        }
    }

    void MoveOnToNextConversation()
    {
        manager.IncrementConversationState();
    }

}