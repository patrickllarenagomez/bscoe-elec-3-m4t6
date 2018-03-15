using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class laserScript : MonoBehaviour {

    // Use this for initialization
    ParticleSystem part;
    public GameObject lastShipHit;
    public GameObject explosionEnemy;
    Text scoreText;
    GameObject canvas;
    int score = 0;
    int counterShip;
    AudioSource explodeSFX;
    GameObject explosion;
    AudioSource laserSFX;
    void Start () {
        canvas = GameObject.Find("Canvas");
        scoreText = canvas.GetComponentInChildren<Text>();
        explosion = GameObject.Find("explosionsfx");
        explodeSFX = explosion.GetComponent<AudioSource>();
        laserSFX = GetComponent<AudioSource>();
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update ()
    {
            laserSFX.Play();
    }

    void AddScore()
    {
        score++;
        scoreText.text = "Score :" + score;
    }   

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(lastShipHit == other)
            {
                // increment if the same ship was hit
                counterShip++;
            }
            else
            {
                //0 if the last ship hit wasn't the previous one
                counterShip = 0;
            }

            if(counterShip > 3)
            {
                Instantiate(Resources.Load("explosionEnemy"), other.transform.position, other.transform.rotation);
                AddScore();
                if (!explodeSFX.isPlaying)
                {
                    explodeSFX.Play();
                }
                Destroy(other);
                Destroy(other.transform.parent.gameObject);
            }
        }
        lastShipHit = other;
    }
}
