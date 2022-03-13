using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destructable : MonoBehaviour
{
    public GameObject target;

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
        if (otherCollider.transform.GetComponent<Player>() != null) Destroy(target.gameObject);
    }
}