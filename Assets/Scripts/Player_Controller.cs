using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player_Controller : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private float hmovement;
    private SpriteRenderer sr;
    private bool jump;
    private bool jumpPressed = false;
    private bool grounded;
    private Animator anim;
    private float speedX;
    public Canvas canvas;

    public Text endgame;
    private bool gameOver = false;

    public Button boton;
    public float radio = 0.2f;
    public LayerMask lm;
    public Transform groundPosition;
    public float jumpImpulse;
    public float speed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Debug.Log("play");
            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                Debug.Log("pause");
            }

        }
        hmovement = Input.GetAxis("Horizontal");
        anim.SetFloat("VelocityX", Mathf.Abs(speedX));
        anim.SetBool("Grounded", grounded);
        if (hmovement < 0)
            sr.flipX = true;
        else if (hmovement > 0)
            sr.flipX = false;

        jump = (Input.GetAxis("Jump") > 0);

        if (!jump)
            jumpPressed = false;


    }

   


        private void FixedUpdate()
    {
        speedX = hmovement * speed;

        rb2d.velocity = new Vector2(speedX, rb2d.velocity.y);

        grounded = Physics2D.OverlapCircle(groundPosition.transform.position, radio, lm.value);

        if (!grounded)
            return;
        if (jump && !jumpPressed)
        {
            rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            jumpPressed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EndGame")
        {
            Destroy(this.gameObject);
            endgame.enabled = true;
            gameOver = true;
        }
    }


    public void pausa()
    {
        Debug.Log("Funciona");
    }

}