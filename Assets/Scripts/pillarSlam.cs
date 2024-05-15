using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarSlam : MonoBehaviour
{
    private Vector3 originalPos;
    public float distance = 5.0f; 
    public float speed = 100.0f; 
    public bool movingBack = false;
    private bool movingFast = false;
    private Rigidbody2D rb;

    
    public bool ifLeft = false;
    private float mult = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //this is just for this demo. In future versions where it this objects be shifted left and right as well,
        //its original position might have to be reset every time the player stops moving it
        originalPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1) && movingBack == false)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>() == null || GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                movingFast = true;
                movingBack = false;
            }
        }

        if (movingFast)
        {
            // moving dang fast (to the left in this specific scenario)!!
            Vector2 targetPosition = rb.position + Vector2.left * speed * Time.deltaTime;
            rb.MovePosition(targetPosition);

            if (Vector3.Distance(rb.position, originalPos - Vector3.right * distance) < 0.1f)
            {
                movingFast = false;
                movingBack = true;
            }
        }

        if (movingBack)
        {
            // sloooowwwwly (hopefully) moving back to where it was.
            Vector2 targetPosition = Vector2.MoveTowards(rb.position, originalPos, speed / 4 * Time.deltaTime);
            rb.MovePosition(targetPosition);

            if (Vector3.Distance(rb.position, originalPos) < 0.1f)
            {
                movingBack = false;
            }
        }
    }

    private void Update()
    {
        //this decides whether the block is on the left side of the map, changing the direction multiplier to -1, inverting the direction.
        //be sure to check off whether or not the specific pillar is on the left or right when setting up levels.
        if (ifLeft) 
        {
            mult = -1;
        }
    }
}
