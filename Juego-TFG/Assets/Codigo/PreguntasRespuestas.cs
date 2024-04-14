using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreguntasRespuestas : MonoBehaviour
{
    public int nivel;
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
    private int numErrores;
    private int puntos;


    void Start(){

        // Inicializar la variable de control de errores
        numErrores = 0;
        // Mezclar las posibles respuestas del array
        Utilidades.MezclarElementos(respuestas);
        // Obtener los componentes y establecer una imagen
        resp1.GetComponent<Image>().sprite = respuestas[0].imagen;
        resp2.GetComponent<Image>().sprite = respuestas[1].imagen;
        resp3.GetComponent<Image>().sprite = respuestas[2].imagen;

        resp1.GetComponent<Button>().onClick.AddListener(() => VerificarRespuesta(respuestas[0].musculo,1));
        resp2.GetComponent<Button>().onClick.AddListener(() => VerificarRespuesta(respuestas[1].musculo,2));
        resp3.GetComponent<Button>().onClick.AddListener(() => VerificarRespuesta(respuestas[2].musculo,3));
        
        // Obtener la puntuacion del nivel
        if(nivel > 0){
            // Si el nivel ya ha sido completado 
            if(NivelCompletado.CargarNivel(nivel) == 2){
                // reinicio los puntos
                puntos = -1;
            // Si el menu solo ha sido completado la primera fase
            }else if(NivelCompletado.CargarNivel(nivel) == 1){
                // Obtengo los puntos conseguidos en la primera fase
                puntos = Puntuaciones.CargarPuntuacion(nivel);
                Debug.Log(puntos);
            // Si no ha sido completado
            }else{
                // Reinicio los puntos
                puntos = -1;
                Debug.Log(puntos);
            }
        }

        
    }

    
    

    public void VerificarRespuesta(string opcion, int indice){
        if(opcion == musculo){
            ActivarImagen(indice, 1);
            // Si la primera pantalla no ha sido completada
            if(puntos < 0){
                puntos = 10;
                if(numErrores == 0){
                    puntos -= 0;
                }else if(numErrores == 1){
                    puntos -= 5;
                }else{
                    puntos -= 10;
                }
                // Pantalla 1 completada
                NivelCompletado.GuardarNivel(nivel,1);
                // Guardar puntos conseguidos
                Puntuaciones.GuardarPuntuacion(nivel, puntos);
            // Si la primera pantalla ha sido completada
            }else{
                if(numErrores == 0){
                    puntos += 10;
                }else if(numErrores == 1){
                    puntos += 5;
                }else{
                    puntos += 0;
                }
                // Nivel completado
                NivelCompletado.GuardarNivel(nivel,2);
                // Guardo la media de los puntos conseguidos en ambas pantallas
                Puntuaciones.GuardarPuntuacion(nivel, puntos/2);
            }
        }else{
            // Activo la imagen de error
            ActivarImagen(indice, 0);
            // Aumento el contador en 1 de errores cometidos
            numErrores++;
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
                botonSiguiente.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(escenaSiguiente)); 

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



