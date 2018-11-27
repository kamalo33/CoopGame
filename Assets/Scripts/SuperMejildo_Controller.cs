//---------------------------------------------------------------------------------------------------------------------------------------------------//
//                                                            ---                  ---                                                               //
//---------------------------------------------------------------Inicio de programa------------------------------------------------------------------//
//                                                            ---                  ---                                                               //
//---------------------------------------------------------------------------------------------------------------------------------------------------//


                    //-------------------------------------Importación de librerias del mismo Unity--------------------------------------//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

                    //-------------------------------------------------------------------------------------------------------------------//
                    //-------------------------------------Elementos obligatorios pre-inicialización-------------------------------------//

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

                    //-------------------------------------------------------------------------------------------------------------------//

    
public class SuperMejildo_Controller : MonoBehaviour /*Clase heredera de MonoBehaviour, necesaria para importar elementos como los "Transform", "position", "Text", "Imports",...*/
{
    /* Comenzamos la incialización de los elementos que aparecerán en 
       este proyecto en lo referente al jugador. Se detallará individualmente 
       cada concepto y elemento que aparezca en las proximas lineas*/

                     //-------------------------------------Inicialización de Elementos-------------------------------------//

                    private Rigidbody2D rb2d;   /*----->Se inicializa el elemento de RigidBody2D para poder usarlo en el protagonista, lo que le otorgará fisicas*/
                    private SpriteRenderer sr;  /*----->Inicizalizamos elemento de SpriteRenderer para permitirnos añadir y modificar la capa grafica del personaje, que está compuesta por "Sprites"*/
                    private Animator anim;      /*----->Iniciamos el elemento "Animator" para así habilitar a posteriori la posibilidad de modificar la animación y las transiciones correspondientes del personaje*/
                    public Canvas canvas;       /*----->Añadimos el elemento "Canvas" para permitirnos modificar dicho componente desde este "Controller", pese a que lo correcto sería inicializarlo en el "GameController"*/
                    public Text endgame;        /*----->Seguimos con el "Text" para así permitirnos habilitar esta capa del "Canvas" con el texto de "Game Over" que aparece cuando el PJ cae al vacio*/
                    private bool grounded;      /*----->Este no obstante nos permite saber si está en el suelo*/
                    private bool inAir;      /*----->Boleano que define si la animación del aire se activa, o no*/
                    private float hmovement;    /*----->Este float nos permite conocer la situación a la que se está moviendo el jugador, que luego nos permitirá conocer si vá hacia adelante o hacia atrás*/
                    private float speedX;       /*----->Este float nos permite modificar el valor del "Animator"*/
                    public float speed;         /*----->A diferencia del SpeedX que hemos tocado con anterioridad, este es uno de los objetos que lo conformarán, en conjunto con "hmovement" y una vez más, determina la velocidad a la que nuestro PJ se desplaza*/
                    public float groundDetectionRadius = 0.2f;  /*----->Este elemento nos permite modificar el radio de detección de suelo*/
                    private bool gameOver = false;              /*----->Este Boleano nos permite saber si el disparador de "Game Over" se ha accionado*/
                    public float airFactor = 0.2f;              /*----->Este elemento nos permitirá modificar la "Espesura" o "Resistencia" que ofrece el aire al saltar el personaje*/
       [Range(0,90)]public float maxSlope;                      /*----->¿¿¿¿¿??????*/
                    private float minYwalkeable;                /*----->¿¿¿¿¿??????*/
                    public LayerMask lm;                        /*----->Este selector de Layer nos permite elegir "QUE" elementos catalogamos como suelo, y que por tanto usaremos como base para nuestro personaje*/
                    public float jumpImpulse;                   /*----->Este float nos permitirá conocer y modificar el impulso con el que nuestro jugador se mueve y desplaza verticalmente nivel através*/
                    private bool jumpPressed = false;           /*----->Este Bool por el contrario nos permite conocer si el jugador "ESTÁ" saltando*/
                    private bool jump;                          /*----->Este Bool nos permite conocer si el jugador ha saltado con anterioridad*/
                    private RaycastHit2D[] results = new RaycastHit2D[14];          /*----->Creamos un Array de tipo "RayCast" que nos permitirá detectar la distancia y el elemento de suelo bajo nosotros*/
                                                                                    
                     //----------------------------------------------------------------------------------------------------//

    //-------------------------------------Ejecucion de elementos-------------------------------------//
    //------------------------------------------Solo inicio-------------------------------------------//

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     //"rb2d" es la variante a la que se le atañe el RigidBody//
        sr = GetComponent<SpriteRenderer>();    //"sr" es la variante a la que se le vincula el SpriteRenderer//
        anim = GetComponent<Animator>();        //"anim" es la variante a la que se le añade el Animator//
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;   //"rb2d.constraints" es una variante a la que se le añade, además un componente de "FreezeRotation"//
        minYwalkeable = Mathf.Cos(Mathf.Deg2Rad*maxSlope);          //"minYwalkeable" es una variable que permite la obtención de una ecuación capaz de ¿¿¿¿¿¿??????//
    }

    //----------------------------------------------------------------------------------------------------//
    //---------------------------------------------Cada Frame---------------------------------------------//
    private void Update()
    {
        //Condicional que nos permite activar el "LogConsole" que permite visualizar en la consola de Unity el tiempo//
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
        //------------------------------------------------------------------------------------------------------------//
        
        hmovement = Input.GetAxis("Horizontal"); //"hmovement" nos permite usar el "Input" que nos permitirá a posteriori desplazarnos horizontalmente por el nivel//
        anim.SetFloat("VelocityX", Mathf.Abs(speedX)); //Esto modifica el valor "variable" de la animación de movimiento//
        anim.SetBool("InAir", inAir); //Modifica la animación de salto//
        anim.SetBool("Grounded", grounded); //Igual que la anterior, modifica una variable de "animación", pero esta vez de salto//

                 //Condicional que nos permitirá girar al personaje si el valor de desplazamiento almacenado en "hmovement", se torna negativo//

        if (hmovement < 0)
            sr.flipX = true;
        else if (hmovement > 0)
            sr.flipX = false;

                //-----------------------------------------------------------------------------------------------------------------------------//
                //Modulo que permitirá al PJ moverse en vertical, saltando [NO FUNCIONA]//

        jump = (Input.GetAxis("Jump") > 0);
        if (!jump)
            jumpPressed = false;

        if (!grounded)
        {
            hmovement *= airFactor;
            inAir = true;
        }
        else
        {
            inAir = false;
        }
 
                //----------------------------------------------------------------------//
    }

    //--------------------------------------------------------------------------------------------------------------------//
    //---------------------------------------------Cada movimiento de fisicas---------------------------------------------//

    private void FixedUpdate()
    {
        speedX = hmovement * speed;

        //rb2d.velocity = new Vector2(speedX, rb2d.velocity.y);

        /*
         -Para el Transform position Grounded-
         grounded = Physics2D.OverlapCircle(groundPosition.transform.position, radio, lm.value);
        */

        int nresults = rb2d.Cast(Vector2.down,results, groundDetectionRadius);
        grounded = (nresults > 0);

        if (nresults > 0)
        {
            Vector2 normal = results[0].normal;
            Debug.Log(normal);
            if (normal.y < minYwalkeable)
            {

            }
            else
            {
                rb2d.velocity = new Vector2(normal.y * speedX, -normal.x * speedX);
            }
        }
        else
            rb2d.velocity = new Vector2(speedX, rb2d.velocity.y);

        if (jump && !jumpPressed && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            jumpPressed = true;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------//
    //---------------------------------------------Ejecución cuando se produce la colisión con "X" Collider---------------------------------------------//

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EndGame")
        {
            Destroy(this.gameObject);
            endgame.enabled = true;
            gameOver = true;
        }
    }
}
    //---------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                      ---                        ---                                                               //
    //---------------------------------------------------------Finalización de programa------------------------------------------------------------------//
    //                                                      ---                        ---                                                               //
    //---------------------------------------------------------------------------------------------------------------------------------------------------//
