using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected bool dead = false;
    public bool Dead => dead;

    protected virtual void OnKill()
    {
        dead = true;
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<Player>().Jump(true);
    }
}