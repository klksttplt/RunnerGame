using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player player = target.GetComponent<Player>();
        if (player.Dead == false)
        {
            //Following the player
            Vector3 targetPosition= new Vector3(
                target.transform.position.x + offset.x,
                target.transform.position.y + offset.y, 
                transform.position.z 
            );
            //Smooth follow
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
        }
    }
}
