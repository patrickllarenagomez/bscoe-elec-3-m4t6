using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour {


    [SerializeField] float speed = 15f;
    [SerializeField] float xRange = 9f;
    [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    MeshCollider meshCollider;
    GameObject playerShip;
    GameObject terrain;
    ParticleSystem particleSystemExplosion;
    ParticleSystem particleSystemLaser;

    // Use this for initialization
    void Start()
{
        playerShip = GameObject.Find("PlayerShip");
        particleSystemExplosion = GetComponentInChildren<ParticleSystem>();
        particleSystemLaser = GetComponentInChildren<ParticleSystem>();

        terrain = GameObject.Find("Terrain");
}

// Update is called once per frame
    void Update()
    {
        HorizontalOrVerticalMovement();
        Rotation();
    }

    private void Rotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void HorizontalOrVerticalMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void OnTriggerEnter(Collider other)
    {
        particleSystemExplosion.Play();
        Invoke("DestroyObjectAndReload", .5f);
    }


    void DestroyObjectAndReload()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(1);
    }

   


}