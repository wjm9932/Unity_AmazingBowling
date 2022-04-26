using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public Rigidbody ball;
    public Transform firePos;
    public Slider powerSlider;
    public AudioSource shootingAudio;
    public AudioClip fireClip;
    public AudioClip chargingClip;

    public float minForce = 15f;
    public float maxForce = 35f;
    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool isFired;

    private void OnEnable()
    {
        currentForce = minForce;
        powerSlider.value = minForce;
        isFired = false;   
    }

    private void Start()
    {
        chargeSpeed = (maxForce - minForce) / chargingTime;
    }

    private void Update()
    {
        if(currentForce >= maxForce && !isFired)
        {
            currentForce = maxForce; // 여기를 currentForce = minForce로 해보기.
            Fire();
        }
        else if (Input.GetButtonDown("Fire1") == true && !isFired)
        {
            currentForce = minForce;
            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if (Input.GetButton("Fire1") && !isFired)
        {
            currentForce = currentForce + chargeSpeed * Time.deltaTime;
            powerSlider.value = currentForce;
        }
        else if(Input.GetButtonUp("Fire1") && !isFired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        isFired = true;

        Rigidbody ballInstance =Instantiate(ball, firePos.position, firePos.rotation);

        ballInstance.velocity = currentForce * firePos.transform.forward;
        shootingAudio.clip = fireClip;
        shootingAudio.Play();
        currentForce = minForce;
        powerSlider.value = currentForce;
    }
}
