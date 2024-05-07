using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AdivinanzaMusculo : MonoBehaviour
{
    private string musculo;
    public GameObject panelFinal;
    public int nivel;
    private int puntos;
    private int numFallos;
    void Start(){
        musculo = PlayerPrefs.GetString("MusculoSeleccionado");
  
        if(musculo == null){
            Debug.LogError("No se ha seleccionado musculo");
            return;
        }
        puntos = 10;
        numFallos = 0;
    }

    public void ComprobarMusculo(GameObject boton){
        string texto;
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

        // Obtener el componente TextMeshProUGUI del botón
        TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();
        // Verificación de si se ha obtenido el componente TextMeshProUGUI del botón
        if (textoBoton != null){
            // Obtener texto del boton 
            texto = textoBoton.text;
            // Comparación del texto del botón con una cadena musculo
            if(string.Equals(musculo, texto)){
                puntos -= numFallos;
                if(puntos < 0){
                    puntos = 0;
                }
                
                // Guardar nivel completado
                NivelCompletado.GuardarNivel(nivel,2);
                // Guardar puntos
                Puntuaciones.GuardarPuntuacion(nivel,puntos);

                if(panelFinal != null){
                    // Activar el panel final
                    panelFinal.SetActive(true);
                }else{
                    Debug.LogError("El GameObject no ha sido encontrado");
                    return;
                }
            }else{
                // Aumento contador fallos para calcular los puntos
                numFallos++;
                // Activar la imagen de fallo
                imagen.SetActive(true);
            }
        }else{
            Debug.LogError("El componente Text no ha sido encontrado");
            return;
        }

    }
}
