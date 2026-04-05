using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedTimer : MonoBehaviour
{
    float timer;

    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
}
