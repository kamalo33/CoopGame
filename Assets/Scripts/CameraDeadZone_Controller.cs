using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeadZone_Controller : MonoBehaviour {

    //Inicializas el tamaño de la camara al 70%
    [Range(0.5f, 0.9f)] public float deadZoneSizeFactor = 0.7f;

    //Inicializas las variables de la DeadZone
    private float dZH, dZW;

    //Player
    public Transform Megildo;

    public float smooth = 1;


    private void Start()
    {
        dZH = Camera.main.orthographicSize * deadZoneSizeFactor;
        dZW = dZH * Camera.main.aspect;
    }

    private void Update()
    {
         if (Megildo.position.x > transform.position.x + dZW)
         {
             float xoffset = Megildo.position.x - (transform.position.x + dZW);
             Vector3 newPosition = new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z);
             transform.position = Vector3.Lerp(transform.position, newPosition, smooth);
        }

        if (Megildo.position.x < transform.position.x - dZW)
         {
             float xoffset = Megildo.position.x - (transform.position.x - dZW);
             Vector3 newPosition = new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z);
             transform.position = Vector3.Lerp(transform.position, newPosition, smooth);
        }

        if (Megildo.position.y > transform.position.y + dZH)
         {
             float yoffset = Megildo.position.y - (transform.position.y + dZH);
             Vector3 newPosition = new Vector3(transform.position.x , transform.position.y + yoffset, transform.position.z);
             transform.position = Vector3.Lerp(transform.position, newPosition, smooth);
        }

        if (Megildo.position.y < transform.position.y - dZH)
        {
            float yoffset = Megildo.position.y - (transform.position.y - dZH);
            Vector3 newPosition = new Vector3(transform.position.x , transform.position.y + yoffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smooth);
        }
    }
}
