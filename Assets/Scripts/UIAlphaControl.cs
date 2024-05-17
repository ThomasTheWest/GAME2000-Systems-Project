using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlphaControl : MonoBehaviour
{

    public AlphaObject UIAlpha;

    public SpriteRenderer myRenderer;

    public Color myColor;

    public float myAlpha;

    // Start is called before the first frame update
    void Start()
    {

        //myRenderer = GameObject.GetComponent<Renderer>();

        myColor = myRenderer.color;

    }

    // Update is called once per frame
    void Update()
    {

        myAlpha = UIAlpha.alpha;

        myColor.a = UIAlpha.alpha;

        myRenderer.color = myColor;


    }
}
