using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LuchaContraElVillano : MonoBehaviour
{

    public Preguntas[] preguntas;
    public Image barraVida;
    private float vidaActual = 4;
    private float vidaMaxima = 4;
    private int indicePregAct = 0;
    private GameObject pregunta;
    private TextMeshProUGUI textoPregunta;
    private int numPreguntasFalladas = 0;
    // Start is called before the first frame update
    void Start()
    {
        Utilidades.MezclarElementos(preguntas);
        MostrarPregunta();
        
    }


    public void MostrarPregunta(){
        pregunta = GameObject.Find("Pregunta"); // obtengo el contenedor del texto
        if(pregunta != null){
            textoPregunta = pregunta.GetComponent<TextMeshProUGUI>(); // obtienes el componente texto
            if(textoPregunta != null){
                if(indicePregAct < preguntas.Length){ // si el componente no es nulo y el indice de la pregunta < max
                    textoPregunta.text = preguntas[indicePregAct].pregunta; // muestras la pregunta
                }else{
                    Utilidades.MezclarElementos(preguntas); // Volvemos a mezclar las preguntas
                    indicePregAct = 0; // volvemos al principio
                }
            }
            
        }
        
    }

    public void ComprobarRespuesta(bool respuesta){
        if(respuesta == preguntas[indicePregAct].verdadera){ // si coincide la respuesta
            vidaActual--; // bajamos la vida 
            barraVida.fillAmount = vidaActual / vidaMaxima; // actualizamos la barra de vida
            if(vidaActual == 0){
                SceneManager.LoadScene("Nivel3.1");
            }
        }else{
            numPreguntasFalladas++;
        }
        indicePregAct++; // siguiente pregunta
        MostrarPregunta();
    }

}


    [System.Serializable]
    public class Preguntas
    {
        public string pregunta;
        public bool verdadera;
    }

