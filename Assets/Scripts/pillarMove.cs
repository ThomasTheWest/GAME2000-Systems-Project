using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarMove : MonoBehaviour
{
    [Header("Transform")]
    public float moveLimit;
    private float maxLeft, maxRight;
    private bool isMoving = false;

    [Header("Grab")]
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;
    private bool isGrabbed = false;

    [Header("Slam")]
    public float distance = 5.0f;
    public float speed = 100.0f;
    public bool isRight;
    private int isRightValue;
    public bool isUp;
    private int isUpValue;
    private bool isSlamming = false;
    private Rigidbody2D pillar;
    private Vector2 posStart;

    [Header("Cursor")]
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public Texture2D cursorGrabH, cursorGrab, cursorOpen;

    private void Start()
    {
        maxLeft = moveLimit * -1;
        maxRight = moveLimit;

        pillar = GetComponent<Rigidbody2D>();
        posStart = transform.position;

        Cursor.SetCursor(cursorOpen, hotSpot, cursorMode);
        selectedObject = null;

        if (isRight)
        {
            isRightValue = 1;
        }
        else
        {
            isRightValue = -1;
        }
    }
    void Update()
    {
        Debug.Log("is it moving? " + isMoving);
        Debug.Log("is it slamming? " + isSlamming);

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //slam logic
        if (Input.GetMouseButtonDown(0) && isMoving == false)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>() == null || GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                isMoving = true;
                isSlamming = true;
            }
        }
        /*
        //grab logic
        if (Input.GetMouseButtonDown(1) && !isMoving)
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                Cursor.SetCursor(cursorGrabH, hotSpot, cursorMode);

                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePosition;
                isMoving = true;
                isGrabbed = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
            Cursor.SetCursor(cursorOpen, hotSpot, cursorMode);
            isMoving = false;
            isGrabbed = false;
            posStart = transform.position;
        }
        */
    }
    void FixedUpdate()
    {
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }

        if (isSlamming)
        {
            //moving dang fast (multiplied by isRightValue to change direction)
            Vector3 displacement = pillar.position - posStart;

            if (!isUp && Mathf.Abs(displacement.x) >= distance)
            {
                isSlamming = false;
            }
            else if (isUp && Mathf.Abs(displacement.y) >= distance)
            {
                isSlamming = false;
            }
            else
            {
                if (!isUp)
                {

                    Vector2 targetPosition = pillar.position + Vector2.left * speed * Time.deltaTime * isRightValue;
                    pillar.MovePosition(targetPosition);

                }
                else if (isUp)
                {

                    Vector2 targetPosition = pillar.position + Vector2.up * speed * Time.deltaTime;
                    pillar.MovePosition(targetPosition);

                }
            }
        }

        if (isMoving && !isSlamming && !isGrabbed && Vector3.Distance(pillar.position, posStart) > 0.1f)
        {
            // sloooowwwwly move it back to where it was.
            Vector2 targetPosition = Vector2.MoveTowards(pillar.position, posStart, speed / 4 * Time.deltaTime);
            pillar.MovePosition(targetPosition);

            if (Vector3.Distance(pillar.position, posStart) < 0.25f)
            {
                isMoving = false;
            }
        }
    }
}
