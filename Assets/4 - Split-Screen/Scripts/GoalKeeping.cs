using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeping : MonoBehaviour
{
    public GameObject playerWhoScoresOnMe;
    public GameObject kickoffSpot;
    public GameObject ball;

    public float kickoffDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            // Update the score for the right player
            ScoreKeeper scoreKeeper = playerWhoScoresOnMe.GetComponent<ScoreKeeper>();
            scoreKeeper.AddOnePoint();

            // Reset ball position after delay
            Invoke("SendBallToKickoffSpot", kickoffDelay);
        }
    }

    void SendBallToKickoffSpot()
    {
        ball.transform.position = kickoffSpot.transform.position;
    }
}
