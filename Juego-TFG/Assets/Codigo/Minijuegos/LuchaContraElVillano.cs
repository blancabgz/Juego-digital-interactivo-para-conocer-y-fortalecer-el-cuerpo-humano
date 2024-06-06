using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LuchaContraElVillano : Minijuego
{
    // public int nivel;
    public Pregunta[] preguntas;
    public string pagina;
    public Image barraVida;
    private float vidaActual = 4;
    private float vidaMaxima = 4;
    private int indicePregAct = 0;
    private GameObject pregunta;
    private TextMeshProUGUI textoPregunta;
    private Animator villano;


    void Awake(){
        ControlMusica.EstadoMusica();
        villano = GameObject.Find("PersonajeV").GetComponent<Animator>();
        villano.enabled = false;
        
    }
    
    void Start()
    {
    
        base.MezclarElementos(preguntas);
        MostrarPregunta();
        
        if(nivel > 0){
            base.puntos = 10;
            base.numFallos = 0;
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
                    base.MezclarElementos(preguntas); // Volvemos a mezclar las preguntas
                    indicePregAct = 0; // volvemos al principio
                }
            }
            
        }
        
    }

    public void ComprobarRespuesta(bool respuesta){
        if(respuesta == preguntas[indicePregAct].verdadera){ // si coincide la respuesta
            vidaActual--; // bajamos la vida 
            barraVida.fillAmount = vidaActual / vidaMaxima; // actualizamos la barra de vida
            villano.enabled = true;
            PlayAnimacion();
            
            

            // Si ha vencido
            if(vidaActual == 0){
                base.CalcularPuntuacionFinal(multiplicador);
                base.GuardarPuntuacion(2);
                // Carga la escena
                Controlador.EscenaJuego(pagina);
            }
        }else{
            base.numFallos++;
        }
        indicePregAct++; // siguiente pregunta
        MostrarPregunta();
        
    }

    // Funcion que activa la animación de golpe al enemigo cada vez que se acierta la pregunta
    // Rebind y Update(0f) aseguran poner el animador en estado inicial
    // Play, activa la animacion
    void PlayAnimacion(){
        villano.Rebind();
        villano.Update(0f);
        villano.Play("GolpeEnemigo");
    }

}


    [System.Serializable]
    public class Pregunta {
        public string pregunta;
        public bool verdadera;
    }

