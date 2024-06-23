using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test : Minijuego
{
    private int contadorMedallas = 0;
    int indicePreguntaAleatoria;
    private string dadoSeleccion = "null";

    public PreguntaT[] preguntas;
    public PreguntaT[] preguntasSeleccionadas;
    TextMeshProUGUI textPregunta;
    GameObject respuestas;
    public GameObject panelFinal;


    private void Awake(){
        ControlMusica.EstadoMusica();
        CargarSeleccionDado();
        CargarContadorMedallas();
    }

    void Start(){   
        FiltrarPreguntas();
        base.MezclarElementos(preguntasSeleccionadas);
        MostrarPreguntaRespuestas();
    }

    /** 
    * @brief Función para filtrar las preguntas por músculo
    * Esta función filtra las preguntas según la selección del músculo del dado
    */
    private void FiltrarPreguntas(){
        int contador = 0;
        if(dadoSeleccion != null){
            preguntasSeleccionadas = new PreguntaT[preguntas.Length]; 
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
    

    /** 
    * @brief Obtener una pregunta aleatoria, mezcla las respuesta y las muestra por pantalla
    */
    public void MostrarPreguntaRespuestas(){
        textPregunta = GameObject.Find("Pregunta").transform.GetComponent<TextMeshProUGUI>();
        if(textPregunta != null && preguntasSeleccionadas != null && preguntasSeleccionadas.Length > 0){
            // Obtener un numero de pregunta aleatoria
            indicePreguntaAleatoria = Random.Range(0, preguntasSeleccionadas.Length);
            // Mezclar las respuestas
            base.MezclarElementos(preguntasSeleccionadas[indicePreguntaAleatoria].respuestas);
            textPregunta.text = preguntasSeleccionadas[indicePreguntaAleatoria].pregunta;

            respuestas = GameObject.Find("Respuestas");
            if(respuestas != null){
                PreguntaT preguntaActual = preguntasSeleccionadas[indicePreguntaAleatoria];
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

    /**
    * @brief Comprueba si la respuesta introducida es correcta o no
    * La función comprueba si la respuesta es correcta, si es así, suma una nueva medalla
    * Si es incorrecta, vuelve al dado pero sin sumar medalla y le baja puntuacion
    */
    public void ComprobarRespuesta(int indice){
        GameObject visible;
        if(panelFinal != null){
            panelFinal.SetActive(true);
            if(preguntasSeleccionadas[indicePreguntaAleatoria].respuestaCorrecta == preguntasSeleccionadas[indicePreguntaAleatoria].respuestas[indice]){
                visible = panelFinal.transform.Find("Acierto")?.gameObject;
                contadorMedallas++;
                PlayerPrefs.SetInt("ContadorMedallas", contadorMedallas);

            }else{
                visible = panelFinal.transform.Find("Fallo")?.gameObject;
                base.CargarPuntuacion();
                PlayerPrefs.SetInt("Puntuacion", base.puntos - 1);
            }
            if(visible != null){
                visible.SetActive(true);
            }

            Invoke("IrAlDado",1f); 


        }else{
            Debug.LogWarning("No se encuentra el GameObject PanelFinal");
        }
        
    }

    private void CargarSeleccionDado(){
        if (PlayerPrefs.HasKey("DadoSeleccion")){
            dadoSeleccion = PlayerPrefs.GetString("DadoSeleccion");
        }
    }

    private void CargarContadorMedallas(){
        if (PlayerPrefs.HasKey("ContadorMedallas")){
            contadorMedallas = PlayerPrefs.GetInt("ContadorMedallas");
        }
    }


    private void IrAlDado() {
        Controlador.EscenaJuego("Ruleta");
    }

    [System.Serializable] //mostrar los actores en los ajustes
    public class PreguntaT {
        public string musculo;
        public string pregunta;
        public string[] respuestas;
        public string respuestaCorrecta;
        
    }
}
