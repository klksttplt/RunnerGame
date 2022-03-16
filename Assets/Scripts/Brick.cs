using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public bool hasCoin;

    public GameObject CoinPrefab;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnKill()
    {
        if (hasCoin)
        {
            var coin = Instantiate(CoinPrefab);
            coin.transform.position = transform.position + new Vector3(0, 2, 0);

            var c = coin.GetComponent<Coin>();
            c.Vanish();

            GameObject.Find("Player").GetComponent<Player>().onCollectCoin();
            //Destroy(c);
        }
        
    }
}