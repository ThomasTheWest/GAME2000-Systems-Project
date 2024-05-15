using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningLauncher : MonoBehaviour
{
    float lerpDuration = 0.5f;
    bool rotating;
    bool ballCaught;

    public bool directionRight;

    private Collider2D collide;
    public GameObject ballCatcher;
    public GameObject Ball;
    public Rigidbody2D ballGrav;

    // Start is called before the first frame update
    void Start()
    {
        collide = GetComponent<Collider2D>();
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
            ballGrav.gravityScale = 0;
            collide.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ballCaught = false;
            collide.enabled = false;
            ballGrav.gravityScale = 1;
        }
    }

    IEnumerator RotatingRight()
    {
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "objective")
        {
            ballCaught = true;
        }
    }
}
