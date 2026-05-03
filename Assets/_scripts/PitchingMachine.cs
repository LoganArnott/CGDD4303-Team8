using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PitchingMachine : MonoBehaviour
{
    public Transform ballSpawn;
    public Transform ball;
    public Transform emptyRotator;

    public float timeToTurn;
    private float passedTime;

    public float timeBetweenPitch;

    private bool isPitching;
    private bool rotating;

    private Quaternion rotateTo;

    private int currentPitch;

    public Transform startingTransform;

    static List<Coroutine> coroutineList = new List<Coroutine>();

    [Serializable]
    private class PitchingInstructions
    {
        [Range(20f, 35f)]
        public float rotation;
        [Range(1.5f, 2.5f)]
        public float force;

        //5 rotation - 5 force
        //23.5 rotation - force
        //25 roation - 2.25 force
        //35 rotation - force
        //45 rotation - 2 force
    }

    [SerializeField] private PitchingInstructions[] pitches;

    private void OnEnable()
    {
        EventManager.onTogglePitch += togglePitch;
        rotating = false;
        isPitching = false;
        currentPitch = 0;
        rotating = false;
        emptyRotator.position = transform.position;
        emptyRotator.rotation = transform.rotation;
        passedTime = 0f;
    }

    private void OnDisable()
    {
        EventManager.onTogglePitch -= togglePitch;
        // StopCoroutine(SetAim());
    }

    private void Start()
    {
        isPitching = false;
        currentPitch = 0;
        rotating = false;
        emptyRotator.position = transform.position;
        emptyRotator.rotation = transform.rotation;
        passedTime = 0f;

       //enableTransform = transform;
    }

    void togglePitch(bool b)
    {
        isPitching = b;
        //rotating = false;
        transform.position = startingTransform.position;
        transform.rotation = startingTransform.rotation;
        while(coroutineList.Count > 0)
        {
            StopCoroutine(coroutineList[coroutineList.Count - 1]);
            coroutineList.Remove(coroutineList[coroutineList.Count - 1]);
        }
        Debug.Log(coroutineList.Count);
        if (isPitching) coroutineList.Add(StartCoroutine(SetAim()));
        //transform.position = emptyRotator.position;
    }

    /*void pitchOnce()
    {
        StartCoroutine(SetAim());
    }*/

    IEnumerator SetAim()
    {
        Debug.Log("Cororoutine started");
        // float rotation = pitches[currentPitch].rotation;
        float rotation = 20f;
        emptyRotator.Rotate(rotation, 0f, 0f);
        rotateTo = emptyRotator.rotation;
        rotating = true;
        yield return new WaitForSeconds(timeToTurn); //wait till at angle
        rotating = false;
        Pitch(); //pitch

        //Debug.Log("Resetting Aim");
        emptyRotator.Rotate(-rotation, 0f, 0f);
        rotateTo = emptyRotator.rotation;
        rotating = true;
        yield return new WaitForSeconds(timeToTurn); //wait till at angle
        rotating = false;
        emptyRotator.position = startingTransform.position;
        emptyRotator.rotation = startingTransform.rotation;
        // currentPitch = (currentPitch + 1) % pitches.Length;
        yield return new WaitForSeconds(2);
        if (isPitching) StartCoroutine(SetAim());
    }

    private void Pitch()
    {
        ball.gameObject.SetActive(false);
        ball.position = ballSpawn.position;
        ball.up = transform.forward;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.gameObject.SetActive(true);
        // ball.GetComponent<Rigidbody>().AddForce(transform.up * (pitches[currentPitch].force - 0.1f), ForceMode.Impulse);
        ball.GetComponent<Rigidbody>().AddForce(transform.up * 2f, ForceMode.Impulse);


        Debug.Log("Pitching at angle: " + pitches[currentPitch].rotation + " with force: " + (pitches[currentPitch].force - 0.1f));
    }

    private void Update()
    {
        if (rotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, (pitches[currentPitch].rotation/timeToTurn) * Time.deltaTime);
        }
    }
}
