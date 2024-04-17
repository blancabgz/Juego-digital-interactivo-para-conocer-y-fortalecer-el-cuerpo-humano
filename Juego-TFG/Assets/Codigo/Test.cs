using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public Preguntas[] preguntas;
    public Preguntas[] preguntasSeleccionadas;
    TextMeshProUGUI textPregunta;
    GameObject respuestas;

    int indicePreguntaAleatoria;
    public GameObject panelFinal;

    private string dadoSeleccion = "null";

    private int contadorMedallas = 0;

    
    private void Awake(){
        CargarSeleccionDado();
        CargarContadorMedallas();
    }

    void Start()
    {   
        FiltrarPreguntas();
        Utilidades.MezclarElementos(preguntasSeleccionadas);
        MostrarPreguntaRespuestas();
    }

    private void FiltrarPreguntas(){
        int contador = 0;
        if(dadoSeleccion != null){
            preguntasSeleccionadas = new Preguntas[preguntas.Length]; 
            for(int i = 0; i < preguntas.Length; i++){
                if(string.Equals(preguntas[i].musculo, dadoSeleccion)){
                    preguntasSeleccionadas[contador] = preguntas[i];
                    contador++;
                }
            }
            // Redimensionar el Array
            System.Array.Resize(ref preguntasSeleccionadas, contador);
        }else{
            Debug.LogWarning("La seleccion del dado es null");
        }
    }
    

    private void CargarSeleccionDado(){
        if (PlayerPrefs.HasKey("DadoSeleccion")){
            dadoSeleccion = PlayerPrefs.GetString("DadoSeleccion");
            Debug.Log("Parte del cuerpo seleccionada es " + dadoSeleccion);
        }
    }

    private void CargarContadorMedallas(){
        if (PlayerPrefs.HasKey("ContadorMedallas")){
            contadorMedallas = PlayerPrefs.GetInt("ContadorMedallas");
            Debug.Log("Contador de medallas en la pregunta =  " + contadorMedallas);
        }
    }


    public void MostrarPreguntaRespuestas(){
        textPregunta = GameObject.Find("Pregunta").transform.GetComponent<TextMeshProUGUI>();
        if(textPregunta != null && preguntasSeleccionadas != null && preguntasSeleccionadas.Length > 0){
            // Obtener un numero de pregunta aleatoria
            indicePreguntaAleatoria = Random.Range(0, preguntasSeleccionadas.Length);
            // Mezclar las respuestas
            Utilidades.MezclarElementos(preguntasSeleccionadas[indicePreguntaAleatoria].respuestas);
            textPregunta.text = preguntasSeleccionadas[indicePreguntaAleatoria].pregunta;

            respuestas = GameObject.Find("Respuestas");
            if(respuestas != null){
                Preguntas preguntaActual = preguntasSeleccionadas[indicePreguntaAleatoria];
                for(int i = 0; i < preguntaActual.respuestas.Length; i++){
                    Text textoRespuesta = respuestas.transform.GetChild(i).GetComponentInChildren<Text>();
                    textoRespuesta.text = preguntaActual.respuestas[i];

                }
            }else{
                Debug.LogWarning("No se encuentra el GameObject 'Respuestas'");
            }
        }else{
            Debug.LogWarning("El array de preguntas esta vacío");
        }
    }

    public void ComprobarRespuesta(int indice){
        GameObject visible;
        if(panelFinal != null){
            panelFinal.SetActive(true);
            if(preguntasSeleccionadas[indicePreguntaAleatoria].respuestaCorrecta == preguntasSeleccionadas[indicePreguntaAleatoria].respuestas[indice]){
                visible = panelFinal.transform.Find("Acierto")?.gameObject;
                contadorMedallas++;
                Debug.Log("Contador de medallas despues de acertar =" + contadorMedallas);
                PlayerPrefs.SetInt("ContadorMedallas", contadorMedallas);

            }else{
                visible = panelFinal.transform.Find("Fallo")?.gameObject;
            }
            if(visible != null){
                visible.SetActive(true);
            }

            Invoke("IrAlDado",1f); 


        }else{
            Debug.LogWarning("No se encuentra el GameObject PanelFinal");
        }
        
    }



    private void IrAlDado() {
        Utilidades.EscenaJuego("Ruleta");
    }

    [System.Serializable] //mostrar los actores en los ajustes
    public class Preguntas {
        public string musculo;
        public string pregunta;
        public string[] respuestas;
        public string respuestaCorrecta;
        
    }
}
