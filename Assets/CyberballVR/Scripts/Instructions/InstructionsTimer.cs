using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTimer : MonoBehaviour
{
    float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 7f)
        {
            timer += Time.deltaTime;
        }
        if(timer >= 7f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
