using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarMove : MonoBehaviour
{
    [Header("Transform")]
    public float moveLimit;
    private float maxLeft, maxRight;
    private bool isMoving = false;
    private Vector2 posOriginal, posLimited;

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
        posOriginal = transform.position;
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
        /*Debug.Log("is it moving? " + isMoving);
        Debug.Log("is it slamming? " + isSlamming);*/
        //Debug.Log("What is s pos? " + posStart.y);

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
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            //this array just looks for whatever has the Mover tag and is a collider
            Collider2D[] targetObjects = Physics2D.OverlapPointAll(mousePosition);

            foreach (Collider2D targetObject in targetObjects)
            {
                if (targetObject.CompareTag("mover"))
                {
                    Cursor.SetCursor(cursorGrabH, hotSpot, cursorMode);

                    selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                    offset = selectedObject.transform.position - mousePosition;
                    isMoving = true;
                    isGrabbed = true;
                }
            }
        }

        if (isGrabbed)
        {
            posLimited = mousePosition + offset;
            posLimited.y = Mathf.Clamp(posOriginal.y, maxLeft, maxRight);
            selectedObject.transform.position = posLimited;
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

            if (Mathf.Abs(displacement.x) >= distance)
            {
                isSlamming = false;
            }
            else
            {
                Vector2 targetPosition = pillar.position + Vector2.left * speed * Time.deltaTime * isRightValue;
                pillar.MovePosition(targetPosition);
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
