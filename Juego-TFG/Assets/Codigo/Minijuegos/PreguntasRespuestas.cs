using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreguntasRespuestas : Minijuego
{
    
    public Respuestas[] respuestas;
    public Imagenes[] imagenes;

    public GameObject resp1;
    public GameObject resp2;
    public GameObject resp3;

    public GameObject validate1;
    public GameObject validate2;
    public GameObject validate3;

    public string musculo;

    public GameObject imagenPanel;
    public GameObject botonSiguiente;
    public string escenaSiguiente;


    void Awake(){
        GameObject controlMusica = GameObject.Find("ControlMusica");
        if(PlayerPrefs.GetString("estadoMusica", "null") == "OFF"){
            if(controlMusica != null){
                Destroy(controlMusica);
            }   
        }
    }
    void Start(){

        // Inicializar la variable de control de errores
        base.numFallos = 0;
        // Mezclar las posibles respuestas del array
        Utilidades.MezclarElementos(respuestas);
        // Obtener los componentes y establecer una imagen
        resp1.GetComponent<Image>().sprite = respuestas[0].imagen;
        resp2.GetComponent<Image>().sprite = respuestas[1].imagen;
        resp3.GetComponent<Image>().sprite = respuestas[2].imagen;

        resp1.GetComponent<Button>().onClick.AddListener(() => VerificarRespuesta(respuestas[0].musculo,1));
        resp2.GetComponent<Button>().onClick.AddListener(() => VerificarRespuesta(respuestas[1].musculo,2));
        resp3.GetComponent<Button>().onClick.AddListener(() => VerificarRespuesta(respuestas[2].musculo,3));
        
        base.ObtenerPuntuacionNivel();
    }

    public void VerificarRespuesta(string opcion, int indice){
        if(opcion == musculo){
            ActivarImagen(indice, 1);
            // Si la primera pantalla no ha sido completada
            if(base.puntos < 0){
                base.CalcularPuntuacionRonda();
                base.GuardarPuntuacion(1);
            // Si la primera pantalla ha sido completada
            }else{
                
                base.CalcularPuntuacionSegundaRonda();
                base.puntos /= 2;
                base.GuardarPuntuacion(2);
            }
        }else{
            // Activo la imagen de error
            ActivarImagen(indice, 0);
            // Aumento el contador en 1 de errores cometidos
            base.AumentarNumeroFallos();
        }
    }

    void ActivarImagen(int indice, int correcta){
        GameObject imagenActivar = null;
        switch (indice)
        {
            case 1:
                imagenActivar = validate1;
                
                break;
            case 2:
                imagenActivar = validate2;
                
                break;
            case 3:
                imagenActivar = validate3;
                break;
            default:
                break;
        }

        

        if(imagenActivar != null){
            Image imagen = imagenActivar.GetComponent<Image>();
            Image imagenIntentalo = imagenPanel.GetComponent<Image>();
            imagenPanel.SetActive(true);
            

            if(correcta == 1){
                // activa la imagen verdad
                imagen.sprite = imagenes[0].sprite;
                // desactiva la imagen 
                imagenPanel.SetActive(false);
                // desactivo los botones 
                resp1.GetComponent<Button>().interactable = false;
                resp2.GetComponent<Button>().interactable = false;
                resp3.GetComponent<Button>().interactable = false;

                botonSiguiente.SetActive(true);
                botonSiguiente.GetComponent<Button>().onClick.AddListener(() => Utilidades.EscenaJuego(escenaSiguiente)); 

            }else{
                // activa la imagen fallo
                imagen.sprite = imagenes[1].sprite;
                // activa la imagen intentalo de nuevo
                imagenIntentalo.sprite = imagenes[2].sprite;
            }

            imagenActivar.SetActive(true);
        }
    }

    [System.Serializable] //mostrar los mensajes en los ajustes
    public class Respuestas {
        public int id;
        public string musculo;
        public Sprite imagen;
        
    }

    [System.Serializable] //mostrar los mensajes en los ajustes
    public class Imagenes {
        public int id;
        public Sprite sprite;
        
    }
    
}



