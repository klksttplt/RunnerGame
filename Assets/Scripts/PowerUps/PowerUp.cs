using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject model;
    public GameObject effectPrefab;

    public float rotationSpeed = 20f;


    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        model.transform.RotateAround(model.transform.position, Vector3.up, rotationSpeed * Time.deltaTime); 
        //model.transform.RotateAround(model.transform.position, Vector3.right, rotationSpeed * Time.deltaTime);
        //model.transform.RotateAround(model.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public virtual void Collect()
    {
        var effectObject = Instantiate(effectPrefab);
        effectObject.transform.SetParent(transform.parent);
        effectObject.transform.position = transform.position;
        Destroy(gameObject);
    }

    public virtual void Apply()
    {
    }
}