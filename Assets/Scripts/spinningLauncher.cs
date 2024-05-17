using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningLauncher : MonoBehaviour
{
    float lerpDuration = 0.5f;
    bool rotating;
    bool ballCaught;

    AudioSource audio;

    public bool directionRight;

    private Collider2D collide;
    public GameObject ballCatcher;
    public GameObject Ball;
    public Rigidbody2D ballGrav;

    // Start is called before the first frame update
    void Start()
    {
        collide = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        if (ballCaught == true && directionRight == true)
        {
            StartCoroutine(RotatingRight());
        }

        if (ballCaught == true && directionRight != true)
        {
            StartCoroutine(RotatingLeft());
        }

        if (ballCaught == true)
        {
            Ball.transform.position = transform.position;
            Ball.transform.rotation = ballCatcher.transform.rotation;
            ballGrav.gravityScale = 0;
            collide.enabled = false;
        }

        if (Input.GetMouseButtonDown(0) && ballCaught == true)
        {
            ballCaught = false;
            ballGrav.gravityScale = 1;
            audio.Play();

            if (directionRight == true)
            {
                StartCoroutine(launchRight());
            }

            if (directionRight != true)
            {
                StartCoroutine(launchLeft());
            }
        }
    }

    IEnumerator RotatingRight()
    {
        ballGrav.velocity = Vector3.zero;
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = ballCatcher.transform.rotation;
        Quaternion targetRotation = ballCatcher.transform.rotation * Quaternion.Euler(0, 0, 180);

        while (timeElapsed < lerpDuration)
        {
            ballCatcher.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ballCatcher.transform.rotation = targetRotation;
        rotating = false;
    }

    IEnumerator RotatingLeft()
    {
        ballGrav.velocity = Vector3.zero;
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = ballCatcher.transform.rotation;
        Quaternion targetRotation = ballCatcher.transform.rotation * Quaternion.Euler(0, 0, -180);

        while (timeElapsed < lerpDuration)
        {
            ballCatcher.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ballCatcher.transform.rotation = targetRotation;
        rotating = false;
    }

    IEnumerator launchRight()
    {
        float timeElapsed = 0;

        while (timeElapsed < 300000)
        {
            Ball.transform.Translate (new Vector3(8,9,0) * Time.deltaTime);
            timeElapsed += 1;
            yield return null;
        }
    }

    IEnumerator launchLeft()
    {
        float timeElapsed = 0;

        while (timeElapsed < 300000)
        {
            Ball.transform.Translate (new Vector3(-8, 9, 0) * Time.deltaTime);
            timeElapsed += 1;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "objective")
        {
            ballCaught = true;
        }
    }
}
