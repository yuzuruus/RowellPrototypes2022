using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName;

    Renderer renderer;
    Color baseColor;
    public Color pickupRangeColor = Color.green;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        baseColor = renderer.material.color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            renderer.material.color = pickupRangeColor;
            ClickPlayerController player = other.gameObject.GetComponent<ClickPlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            renderer.material.color = baseColor;
        }
    }
}
