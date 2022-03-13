using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlock : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }


    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Player>() != null)
        {
            otherCollider.GetComponent<Player>().Pause();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}