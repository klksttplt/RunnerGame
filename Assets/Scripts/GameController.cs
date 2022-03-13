using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Player player;
    public Text scoreText;

    private int _score;

    // Start is called before the first frame update
    private void Start()
    {
        player.onCollectCoin = OnCollectCoin;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollectCoin()
    {
        _score++;
        scoreText.text = "Score: " + _score;
    }
}