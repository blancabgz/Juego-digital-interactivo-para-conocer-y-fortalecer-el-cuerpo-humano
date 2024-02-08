using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class JuegoSombras : MonoBehaviour
{

    public Sombras[] sombras;
    private GameObject pregunta;

    public string[] respuestas;

    private string[] nombresOpciones = {"OpcionA", "OpcionB", "OpcionC"};

    public int musculoActual = 0;

    public GameObject panelFinal;

    void Start()
    {
        // Mezcla los elementos de sombras para evitar el mismo orden
        Utilidades.MezclarElementos(sombras);
        // Muestra la primera sombra
        MostrarPregunta();
        // Muestra las opciones posibles
        MostrarRespuesta();
    }

    // 
    // Funcion que muestra la sombra
    //
    public void MostrarPregunta(){
        // Encuentra el GameObject que contiene la imagen de la sombra
        pregunta = GameObject.Find("Sombra");
        // Obtener el componente imagen
        Image sombra = pregunta.GetComponent<Image>();
        // Verifica si se encuentra el GameObject
        if(pregunta != null){
            // Verifica si el indice de la pregunta se encuentra en el rango 
            if(musculoActual < sombras.Length){
                // Asignacion de la sombra al componente
                sombra.sprite = sombras[musculoActual].sombra;
            }
            
        }
    }

    // 
    // Funcion que muestra las respuestas posibles
    //
    public void MostrarRespuesta(){
        TextMeshProUGUI textOpcion;
        // Itera en los nombres de las opciones de respuesta
        for(int i = 0; i < nombresOpciones.Length; i++){
            // Encuentra el GameObject con el nombre de la opcion
            GameObject opcion = GameObject.Find(nombresOpciones[i]);
            // Verifica si encuentra el GameObject
            if(opcion != null){
                // Obtiene el componente TextMeshProUGUI del botón
                textOpcion = opcion.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();
                // Verifica si lo ha encontrado y si hay respuesta
                if(textOpcion != null && i < respuestas.Length){
                    // Asigna el nombre al TextMeshProUGUI del botón
                    textOpcion.text = respuestas[i];
                }
            }
        }
    }
    

    // 
    // Comprueba si la respuesta seleccionada coincide con la sombra y una vez finalizada la prueba, muestra pantalla de finalización
    // @param {int} opcion Indice de la respuesta seleccionada por el jugador
    //
    public void ComprobarRespuesta(int opcion){

        // Comprueba que la sombra del musculo actual es mejor que la longitud de preguntas de sombras
        if(musculoActual < sombras.Length - 1){
            // Verifica si la respuesta seleccionada es la correcta
            if(respuestas[opcion] == sombras[musculoActual].musculo){
                // Incrementa el indice de la respuesta
                musculoActual++;
                // Muestra la siguiente pregunta
                MostrarPregunta();
                
            }else{
                Debug.Log("Incorrecto");
            }
        }else{
            // Verifica si ha encontrado el GameObject
            if(panelFinal != null){
                // Muestra la ultima pregunta
                MostrarPregunta();
                // Verifica si la opción escogida es la correcta
                if(respuestas[opcion] == sombras[musculoActual].musculo){
                    // Activa el panel final    
                    panelFinal.SetActive(true);
                }else{
                    Debug.Log("Incorrecto");
                }   
            }
        }
        
    
    }

    
    // Clase que contiene el nombre del musculo y el sprite correspondiente a la sombra
    [System.Serializable] 
    public class Sombras {
        public string musculo;
        public Sprite sombra;
        
    }
}
