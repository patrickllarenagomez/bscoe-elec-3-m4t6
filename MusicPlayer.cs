using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicPlayer : MonoBehaviour
{
    bool thisBool = false;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);   
    }

    void Start () {
	}
	
	void LoadGameScene()
    {
        if (Input.anyKey && thisBool == false)
        {
            thisBool = true;
            SceneManager.LoadScene(1);
        }
        
    }
	void Update ()
    {
        LoadGameScene();

    }
}
