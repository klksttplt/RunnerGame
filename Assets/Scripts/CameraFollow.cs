using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;
    public Vector3 offset;
    public float speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (!player.Dead && !player.Finished )
        {
            //Following the player
            Vector3 targetPosition= new Vector3(
                player.transform.position.x + offset.x,
                player.transform.position.y + offset.y, 
                transform.position.z 
            );
            //Smooth follow
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
        }
       
    }
}
