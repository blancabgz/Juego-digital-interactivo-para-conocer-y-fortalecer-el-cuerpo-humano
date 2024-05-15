using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LuchaContraElVillano : MonoBehaviour
{
    public int nivel;
    public Preguntas[] preguntas;
    public string pagina;
    public Image barraVida;
    private float vidaActual = 4;
    private float vidaMaxima = 4;
    private int indicePregAct = 0;
    private GameObject pregunta;
    private TextMeshProUGUI textoPregunta;
    private int numPreguntasFalladas = 0;
    private int puntos;
    private int numErrores;
    // Start is called before the first frame update

    AudioSource[] audioSources;
    private string musica;

    void Awake(){
        audioSources = FindObjectsOfType<AudioSource>();
        musica = PlayerPrefs.GetString("estadoMusica", "null");
        if(musica != null){
            if(musica == "OFF"){
                if(audioSources != null){
                    foreach (AudioSource audioSource in audioSources){
                        audioSource.mute = false;
                    }
                }
            }
        }
    }
    
    void Start()
    {
        Utilidades.MezclarElementos(preguntas);
        MostrarPregunta();
        
        if(nivel > 0){
            puntos = 10;
        }
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
            // Si ha vencido
            if(vidaActual == 0){
                // Calculo la puntuacion segun el numero de preguntas falladas
                puntos -= numPreguntasFalladas*2;
                // Si la puntuacion es negativa
                if(puntos < 0){
                    // Puntuacion a 0
                    puntos = 0;
                }
                // Guarda el nivel completado
                NivelCompletado.GuardarNivel(nivel,2);
                // Guarda los puntos del nivel
                Puntuaciones.GuardarPuntuacion(nivel, puntos);
                // Carga la escena
                SceneManager.LoadScene(pagina);
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

