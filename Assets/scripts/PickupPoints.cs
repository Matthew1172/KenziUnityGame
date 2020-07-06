using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoints : MonoBehaviour
{
    public int scoreToGive;
    private ScoreManager theScoreManager;
    private AudioSource coinSound;

    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("coinSound").GetComponent<AudioSource>();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            theScoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);
            if(coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
        }
    }
}
