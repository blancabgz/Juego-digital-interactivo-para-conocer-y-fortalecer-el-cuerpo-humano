using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AdivinanzaMusculo : Minijuego
{
    // Variables de modelo
    private string musculo;

    void Awake(){
        ControlMusica.EstadoMusica();
    }

    void Start(){
        musculo = PlayerPrefs.GetString("MusculoSeleccionado");
  
        if(musculo == null){
            Debug.LogError("No se ha seleccionado musculo");
            return;
        }

        base.puntos = 10;
        base.numFallos = 0;
        base.multiplicador = 1;
    }

    /**
     * @brief Comprueba si el botón pulsado (músculo seleccionado) coincide con el músculo correcto. 
     * 
     * Esta función se utiliza para comprobar si el texto del músculo seleccionado con el botón coincide con el músculo generado
     * de forma aleatoria. 
     * Si el jugador falla, se despliega una imagen de fallo y aumenta el numero de fallos. 
     * Si el jugador acierta, se guarda la puntuación generada y muestra la puntuación final.
     * 
     * @param boton El GameObject del botón seleccionado.
     * @pre El GameObject del botón debe existir y no ser nulo.
     * @note El numero de fallos no puede ser negativo 
     * 
    */
    public void ComprobarMusculo(GameObject boton){
        GameObject imagen;

        if(boton == null){
            Debug.LogError("El GameObject no ha sido encontrado");
            return;
        }

        Transform hijo = boton.transform.Find("Fallo");
        if(hijo != null){
            imagen = hijo.gameObject;
        }else{
            Debug.LogError("El GameObject 'Fallo' no ha sido encontrado");
            return;
        }

        TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();

        if (textoBoton != null){
            string texto = textoBoton.text;
            if(string.Equals(musculo, texto)){
               base.CalcularPuntuacionFinal(multiplicador);
               base.GuardarPuntuacion(2);
                base.MostrarPanelFinal();
                  
            }else{
                base.AumentarNumeroFallos();
                // Debug.Log("Numero de fallos despues " + base.numFallos);
                imagen.SetActive(true);
            }
        }else{
            Debug.LogError("El componente Text no ha sido encontrado");
            return;
        }

    }
   

    
}
