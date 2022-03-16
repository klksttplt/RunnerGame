using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEnemy : MovingEnemy
{
    public GameObject shellPrefab;
    public float rotationSpeed = 3f;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnKill()
    {
        base.OnKill();

        var shellObject = Instantiate(shellPrefab);
        shellObject.transform.position = transform.position;
        shellObject.transform.parent = transform.parent;
        shellObject.transform.RotateAround(transform.position, Vector3.up, rotationSpeed*Time.deltaTime);
    }
}