using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTrigger : MonoBehaviour
{
    public string ItemTypeText;
    TMP_Text textObject;

    public bool StartOn = true;

    public GameObject connectedObject;

    // Start is called before the first frame update
    void Start()
    {
        // Setup floating text
        textObject = GetComponentInChildren<TMP_Text>();
        textObject.text = ItemTypeText;

        // Turn object on or off, based on set parameters
        if(StartOn)
        {
            connectedObject.SetActive(true);
        }
        else
        {
            connectedObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger!");
        string colliderTag = other.gameObject.tag;  // Get access to the tag of the collider!

        // Process whether or not this collision should trigger a specific response!
        if(ItemTypeText == "Sphere" && colliderTag == "Sphere")
        {
            connectedObject.SetActive(!StartOn);
        }
        else if(ItemTypeText == "Cube" && colliderTag == "Cube")
        {
            connectedObject.SetActive(!StartOn);
        }
        else if(ItemTypeText == "Cyllinder" && colliderTag == "Cyllinder")
        {
            connectedObject.SetActive(!StartOn);
        }
    }

    public void TurnBackOff()
    {
         connectedObject.SetActive(StartOn);
    }
}
