﻿using System;
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
    public Text invincibleText;

    private int _score;
    public float restartTimer = 3f;
    public float finishTimer = 3f;


    private bool _finished = false;

    // Start is called before the first frame update
    private void Start()
    {
        player.onCollectCoin = OnCollectCoin;
        player.onCollectInvincibility = OnCollectInvincibility;
        player.onEndInvincibility = OnEndInvincibility;
        finishText.enabled = false;
        invincibleText.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.Dead)
        {
            restartTimer -= Time.deltaTime;
            if (restartTimer <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                if (SceneManager.GetActiveScene().buildIndex != 7)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                else SceneManager.LoadScene(0);
            }
        }
    }

    private void OnFinish()
    {
        finishText.enabled = true;
        scoreText.enabled = false;
        finishText.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1) + " finished!" + "\nYour score: " +
                          _score;
    }

    private void OnCollectCoin()
    {
        _score++;
        scoreText.text = "Score: " + _score;
    }

    private void OnCollectInvincibility()
    {
        invincibleText.enabled = true;
    }

    private void OnEndInvincibility()
    {
        invincibleText.enabled = false;
    }
}