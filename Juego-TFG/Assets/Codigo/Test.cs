using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public Preguntas[] preguntas;
    TextMeshProUGUI textPregunta;
    GameObject respuestas;

    int indicePreguntaAleatoria;
    public GameObject panelFinal;
    
    void Start()
    {   
        Utilidades.MezclarElementos(preguntas);
        MostrarPreguntaRespuestas();
        
    }
    public void MostrarPreguntaRespuestas(){
        textPregunta = GameObject.Find("Pregunta").transform.GetComponent<TextMeshProUGUI>();
        
        if(textPregunta != null && preguntas != null && preguntas.Length > 0){
            indicePreguntaAleatoria = Random.Range(0, preguntas.Length);
            textPregunta.text = preguntas[indicePreguntaAleatoria].pregunta;

            respuestas = GameObject.Find("Respuestas");
            if(respuestas != null){
                Preguntas preguntaActual = preguntas[indicePreguntaAleatoria];
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
            if(preguntas[indicePreguntaAleatoria].respuestaCorrecta == preguntas[indicePreguntaAleatoria].respuestas[indice]){
                visible = panelFinal.transform.Find("Acierto")?.gameObject;
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
