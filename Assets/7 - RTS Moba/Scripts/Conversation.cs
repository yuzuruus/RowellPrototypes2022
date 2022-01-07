using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Conversation : MonoBehaviour
{
    public string[] initialText = { "Nice to meet you!" };
    public bool requireItem = false;

    [HideInInspector]
    public GameObject requiredItem;
    [HideInInspector]
    public string[] followUpText = { "Thanks so much!" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Conversation))]
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        Conversation script = (Conversation)target;

        // draw checkbox for the bool
        //script.requireItem = EditorGUILayout.Toggle("Require Item?", script.requireItem);
        if (script.requireItem) // if bool is true, show other fields
        {
            script.requiredItem = EditorGUILayout.ObjectField("Required Item: ", script.requiredItem, typeof(GameObject), true) as GameObject;
            //script.followUpText = EditorGUILayout.ObjectField("Follow-up text: ", script.followUpText, typeof(string[]), true) as string[];
        }
    }
}
#endif