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
    int EnemyHealth = 2;
    Text HealthText;
    void Start () {
        canvas = GameObject.Find("Canvas");
        scoreText = canvas.GetComponentInChildren<Text>();
        explosion = GameObject.Find("explosionsfx");
        explodeSFX = explosion.GetComponent<AudioSource>();
        laserSFX = GetComponent<AudioSource>();
        part = GetComponent<ParticleSystem>();
        HealthText = canvas.transform.Find("HealthText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        if(!laserSFX.isPlaying)
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
                // increment if the same ship was hits
                counterShip++;
                if(EnemyHealth > 0)
                    EnemyHealth -= 1;
                HealthText.text = "Enemy Health: " + EnemyHealth;
            }
            else
            {
                //0 if the last ship hit wasn't the previous one
                counterShip = 0;
                EnemyHealth = 2;
                HealthText.text = "Enemy Health: " + EnemyHealth;
            }

            if (counterShip > 1)
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
