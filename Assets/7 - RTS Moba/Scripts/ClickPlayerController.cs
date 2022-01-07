// Credit for script references from https://sharpcoderblog.com/blog/rts-moba-player-controller-script

using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]

public class ClickPlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public Vector3 cameraOffset;
    public GameObject targetIndicatorPrefab;
    NavMeshAgent agent;
    GameObject targetObject;

    bool talkAvailable = false;
    bool giveItemAvailable = false;
    bool controlActive = true;
    
    // Conversation Management
    ConversationManager npc;

    // Item management
    public List<CollectableItem> heldItems;
    public TMP_Text heldItemUI;
    CollectableItem itemInRange;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Instantiate click target prefab
        if (targetIndicatorPrefab)
        {
            targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            targetObject.SetActive(false);
        }
        UpdateHeldItemSummary();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlActive)
        {
            ProcesssMovement();
        }
        if(talkAvailable)
        {
            ProcessTalking();
        }
        if(giveItemAvailable)
        {
            ProcessGiveItem();
        }

        ProcessNPCInteractions();
        ProcessHeldItemUI();
    }

    void ProcesssMovement()
    {
        #if (UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1) && !UNITY_EDITOR
                            //Handle mobile touch input
                            for (var i = 0; i < Input.touchCount; ++i)
                            {
                                Touch touch = Input.GetTouch(i);

                                if (touch.phase == TouchPhase.Began)
                                {
                                    MoveToTarget(touch.position);
                                }
                            }
        #else
                //Handle mouse input
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    MoveToTarget(Input.mousePosition);
                }
        #endif

                //Camera follow
                playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, transform.position + cameraOffset, Time.deltaTime * 7.4f);
                playerCamera.transform.LookAt(transform);
    }

    void ProcessTalking()
    {
        if(Input.GetKeyDown("e"))
        {
            
        }
    }

    public void TargetItemInRange(CollectableItem item)
    {
        itemInRange = item;
    }

    void ProcessGiveItem()
    {

    }

    void ProcessHeldItemUI()
    {
        if(heldItemUI)
        {
            if(Input.GetKeyDown("i"))
            {
                if(heldItemUI.IsActive())
                {
                    heldItemUI.gameObject.SetActive(false);
                }
                else
                {
                    heldItemUI.gameObject.SetActive(true);
                }
            }
        }
    }

    void MoveToTarget(Vector2 posOnScreen)
    {
        //print("Move To: " + new Vector2(posOnScreen.x, Screen.height - posOnScreen.y));

        Ray screenRay = playerCamera.ScreenPointToRay(posOnScreen);

        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit, 75))
        {
            agent.destination = hit.point;

            //Show marker where we clicked
            if (targetObject)
            {
                targetObject.transform.position = agent.destination;
                targetObject.SetActive(true);
            }
        }
    }

    void ProcessNPCInteractions()
    {
        if(npc != null)
        {
            
        }
    }

    public void TurnOffControl()
    {
        controlActive = false;
    }

    public void TurnOnControl()
    {
        controlActive = true;
    }

    public void TurnOnTalk(ConversationManager toTalkTo)
    {
        talkAvailable = true;
        npc = toTalkTo;
    }
    public void TurnOffTalk()
    {
        talkAvailable = false;
        npc = null;
    }

    public void TurnOnGiveItem(ConversationManager toTalkTo)
    {
        giveItemAvailable = true;
        npc = toTalkTo;
    }
    public void TurnOffGiveItem()
    {
        giveItemAvailable = false;
    }
    
    public void PickupItem(CollectableItem item)
    {
        heldItems.Add(item);
        UpdateHeldItemSummary();
    }

    public void RemoveItem(CollectableItem item)
    {
        heldItems.Remove(item);
        UpdateHeldItemSummary();
    }

    public void UpdateHeldItemSummary()
    {
        List<string> itemSummary = new List<string>();

        // Prepare for counting individual items
        List<CollectableItem> onlyOne = heldItems.Distinct().ToList();

        foreach(CollectableItem item in heldItems)
        {
            foreach(CollectableItem subItem in onlyOne)
            {
                int count = 0;
                if(item.name == subItem.name)
                {
                    count++;
                }
                if(count == 1)
                {
                    itemSummary.Add(item.name);
                }
                else if(count > 1)
                {
                    itemSummary.Add(item.name + " x" + count);
                }
            }
        }

        // Generate report for UI
        string itemReport = "Held Items:\n";
        foreach (string entry in itemSummary)
        {
            itemReport += entry + '\n';
        }

        // Update UI
        heldItemUI.text = itemReport;

    }
}