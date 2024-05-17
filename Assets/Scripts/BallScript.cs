using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{

    public GameObject objective;
    public Rigidbody2D rb;

    public GameObject leftArrows;
    public GameObject rightArrows;

    public GameObject respawnPoint;

    public TMP_Text massDisplay;
    public TMP_Text shapeDisplay;

    public SpriteRenderer renderer;
    public CircleCollider2D circleColl;
    public BoxCollider2D boxColl;

    public Sprite ball;
    public Sprite square;

    public AlphaObject UIAlpha;

    //Used to check whether the objective is ball (false) or square (true)
    public bool spriteChange;
    public float mass;

    //When true, objective controls UI will be fully visible
    public bool showMenu;

    //Decides which control is being interacted with e.g. when = 0, mass is being interacted with
    public int controlNo;

    //Used to define range of possible values for controlNo and prevent it from exceeding its bounds. Can be easily changes if more controls added
    public int controlNoMin;
    public int controlNoMax;

    bool showUI;
    float hideTimer;

    void Awake()
    {

        objective = GameObject.FindGameObjectWithTag("objective");
        rb = objective.GetComponent<Rigidbody2D>();

        renderer = objective.GetComponent<SpriteRenderer>();
        circleColl = objective.GetComponent<CircleCollider2D>();
        boxColl = objective.GetComponent<BoxCollider2D>();

        spriteChange = false;
        mass = rb.mass;

        renderer.sprite = ball;

        circleColl.enabled = true;
        boxColl.enabled = false;

        HideUI();

    }

    void Update()
    {

        if (showUI)
        {

            hideTimer += Time.unscaledDeltaTime;

            if (hideTimer >= 5)
            {

                HideUI();

            }

        }

        //Inputs for navigating between different controls
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {

            ShowUI();
            controlNo--;

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

            ShowUI();
            controlNo++;

        }

        //Limiting range of controlNo to Min and Max which can be edited in the inspector
        controlNo = Mathf.Clamp(controlNo, controlNoMin, controlNoMax);

        if (controlNo == 0)
        {

            leftArrows.SetActive(true);
            rightArrows.SetActive(false);

        }
        else if (controlNo == 1)
        {

            leftArrows.SetActive(false);
            rightArrows.SetActive(true);

        }


        //Inputs for interacting with currently selected control
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {

            ShowUI();

            if (controlNo == 0)
            {

                mass ++;

            }
            else if (controlNo == 1)
            {

                SpriteChange();

            }

        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {

            ShowUI();

            if (controlNo == 0)
            {

                mass --;

            }
            else if (controlNo == 1)
            {

                SpriteChange();

            }

        }

        mass = Mathf.Clamp(mass, 1, 20);

        rb.mass = mass;

        massDisplay.text = mass.ToString();

    }

    //When called, changes the objective sprite and enables/disables colliders to match
    public void SpriteChange()
    {

        if (spriteChange)
        {

            circleColl.enabled = true;
            boxColl.enabled = false;

            renderer.sprite = ball;
            spriteChange = false;
            shapeDisplay.text = "Circle".ToString();

        }
        else
        {
            circleColl.enabled = false;
            boxColl.enabled = true;

            renderer.sprite = square;
            spriteChange = true;
            shapeDisplay.text = "Square".ToString();

        }

    }

    public void ShowUI()
    {
        showUI = true;
        UIAlpha.alpha = 0.7f;
        hideTimer = 0.0f;

    }

    public void HideUI()
    {

        showUI = false;
        UIAlpha.alpha = 0.4f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Debug.Log("Respawning");
            transform.position = respawnPoint.transform.position;
        }
    }

}
