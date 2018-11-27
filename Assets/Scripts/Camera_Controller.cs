using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    public Transform objetivo;
    public float suavizado = 5f;

    Vector3 cambio;

    // Use this for initialization
    void Start()
    {
        cambio = transform.position - objetivo.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 posicionObjetivo = objetivo.position + cambio;
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizado * Time.deltaTime);
    }
}
