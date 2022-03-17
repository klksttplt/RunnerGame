﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUp : PowerUp
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Apply()
    {
        base.Apply();
        GameObject.Find("Player").GetComponent<Player>().hasSizeUp = true;
        Debug.Log("received size up ");
    }
}