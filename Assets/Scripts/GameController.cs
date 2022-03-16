using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public Player player;
    public Text scoreText;

    private int _score;
    private float _restartTimer = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        player.onCollectCoin = OnCollectCoin;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.Dead)
        {
            _restartTimer -= Time.deltaTime;
            if (_restartTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnCollectCoin()
    {
        _score++;
        scoreText.text = "Score: " + _score;
    }
}