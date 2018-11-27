using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour {

    /*Inicializo las variables y constantes tanto de int
    como de clase Texto para guardar aquí el objeto en canvas*/
    public Text PuntuationController;
    public int InitialPuntuation = 0;
    public int ActualPuntuation;

    private float TimeAcross = 0f;
    [HideInInspector]public bool TimeAccount = false; 

    private void Update()
    {
        /*if (TimeAccount)
        {
            TimeAcross += Time.deltaTime;

            if (TimeAcross > 0.5f)
            {
                PuntuationController.color = Color.black;
                TimeAccount = false;
            }
        



        if (Input.anyKeyDown)
        {
            ActualPuntuation++;
            ImpressPuntuation(ActualPuntuation);
        }}*/
    }

    /*void ImpressPuntuation(int InitialPuntuation)
    {
        ActualPuntuation = InitialPuntuation;
        PuntuationController.text = "Puntuacion: " + ActualPuntuation;
        PuntuationController.color = Color.red;
        TimeAcross = 0f;
        TimeAccount = true;
    }*/
	
}
