using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngineInternal;


public class GameController : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public Text finishText;

    private int _score;
    public float restartTimer = 3f;
    public float finishTimer = 3f;

    
    private bool _finished = false;

    // Start is called before the first frame update
    private void Start()
    {
        player.onCollectCoin = OnCollectCoin;
        finishText.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.Dead)
        {
            restartTimer -= Time.deltaTime;
            if (restartTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (player.Finished)
        {
            if (!_finished)
            {
                _finished = true;
                OnFinish();
            }
            finishTimer -= Time.deltaTime;
            if (finishTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnFinish()
    {
        finishText.enabled = true;
        scoreText.enabled = false;
        finishText.text = SceneManager.GetActiveScene().name + " finished!" + "\nYour score: " + _score;
    }

    private void OnCollectCoin()
    {
        _score++;
        scoreText.text = "Score: " + _score;
    }
}