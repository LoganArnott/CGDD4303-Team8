using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTimer : MonoBehaviour
{
    float timer;
    float maxTime;
    public GameManager gameManager;
    bool readButton;
    public GameObject okButton;
    public GameObject waitingForPlayers;
    public BallManager ballManager;
    public AudioSource audioRoundInstructions;

    void OnEnable()
    {
        ballManager.DisableBall();
        timer = 0f;
        maxTime = UnityEngine.Random.Range(5f, 12f);
        readButton = false;
        okButton.SetActive(true);
        waitingForPlayers.SetActive(false);
        gameManager.AlternateHandRays(true);
        audioRoundInstructions.PlayOneShot(audioRoundInstructions.clip);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        if(timer >= maxTime && readButton)
        {
            gameManager.AlternateHandRays(false);
            gameManager.SetUpBallReciever();
            this.gameObject.SetActive(false);
        }
    }

    public void buttonPressed()
    {
        readButton = true;
        okButton.SetActive(false);
        waitingForPlayers.SetActive(true);
    }
}
