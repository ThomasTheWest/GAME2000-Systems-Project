using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public GameObject objective;
    public Rigidbody2D rb;

    public SpriteRenderer renderer;
    public CircleCollider2D circleColl;
    public BoxCollider2D boxColl;

    public Sprite ball;
    public Sprite square;

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

    }

    void Update()
    {

        //Inputs for navigating between different controls
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {

            controlNo--;

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

            controlNo++;

        }

        //Limiting range of controlNo to Min and Max which can be edited in the inspector
        controlNo = Mathf.Clamp(controlNo, controlNoMin, controlNoMax);


        //Inputs for interacting with currently selected control
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {

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

            if (controlNo == 0)
            {

                mass --;

            }
            else if (controlNo == 1)
            {

                SpriteChange();

            }

        }

        rb.mass = mass;

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

        }
        else
        {
            circleColl.enabled = false;
            boxColl.enabled = true;

            renderer.sprite = square;
            spriteChange = true;

        }

    }

}
