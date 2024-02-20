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
    private string[] nombresImagenes = {"TickA", "TickB", "TickC"};

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
    // Muestra la sombra
    //
    public void MostrarPregunta(){
        DesactivarImagenes();
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
    // Desactiva todas las imagenes tick de los botones
    //
    private void DesactivarImagenes(){
        for(int i = 0; i < nombresImagenes.Length; i++){
            // Buscamos el panel que se llama como la opciones seleccionada
            GameObject imagen = GameObject.Find(nombresImagenes[i]);
            //Verifir si ha sido encontrado el GameObject
            if(imagen != null){
                // Obtenemos la imagen del GameObject
                Image imagenTick = imagen.GetComponent<Image>();
                // Verificar si ha encontrado el Image
                if(imagenTick != null){
                    // Desactiva la imagen
                    imagenTick.enabled = false;
                }else{
                    // Mensaje de error
                    Debug.LogError("No se encontró el componente Image en el GameObject.");
                }
            }else{
                // Mensaje de error
                Debug.LogError("No se encontró el GameObject con el nombre especificado.");
            }
        }
    }

    // 
    // Muestra las respuestas posibles
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
    // Devuelve el nombre del componente Image segun la opcion clickada
    // @param {int} opcion Indice de la respuesta seleccionada por el jugador
    //

    public string ObtenerImagen(int opcion){
        // Verificar si la opcion esta en el rango del array de opciones
        if(opcion >= 0 && opcion < nombresImagenes.Length){
            // Devuelve el nombre de la imagen de la opcion
            return nombresImagenes[opcion];
        }else{
            return string.Empty;
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
                ActivarImagen(opcion);
            }
        }else{
            // Verifica si ha encontrado el GameObject
            if(panelFinal != null){
                // Muestra la ultima pregunta
                MostrarPregunta();
                // Verifica si la opción escogida es la correcta
                if(respuestas[opcion] == sombras[musculoActual].musculo){
                    if (GameObject.Find("Sombra").TryGetComponent(out Image sombra)) {
                        sombra.enabled = false;
                    }

                    // Desactiva el panel de respuestas
                    GameObject respuestasPanel = GameObject.Find("Respuestas");
                    if (respuestasPanel != null) {
                        respuestasPanel.SetActive(false);
                    }

                    // Desactiva el panel de explicación
                    GameObject explicacionPanel = GameObject.Find("Explicacion");
                    if (explicacionPanel != null) {
                        explicacionPanel.SetActive(false);
                    }

                    // Activa el panel final
                    if (panelFinal != null) {
                        panelFinal.SetActive(true);
                    }

                }else{
                    ActivarImagen(opcion);
                }   
            }
        }
    }

    //
    // Activa la imagen de la opcion seleccionada en caso de error del jugador
    // @param {int} opcion Indice de la respuesta seleccionada por el jugador
    //

    private void ActivarImagen(int opcion){
        // Obtenemos la opcion seleccionada
        string opcionBoton = ObtenerImagen(opcion);
        // Buscamos el panel que se llama como la opciones seleccionada
        GameObject imagen = GameObject.Find(opcionBoton);
        //Verifir si ha sido encontrado el GameObject
        if(imagen != null){
            // Obtenemos la imagen del GameObject
            Image imagenTick = imagen.GetComponent<Image>();
            // Verificar si ha encontrado el Image
            if(imagenTick != null){
                imagenTick.enabled = true;

            }else{
                Debug.LogError("No se encontró el componente Image en el GameObject.");
            }

        }else{
            Debug.LogError("No se encontró el GameObject con el nombre especificado.");
        }
    }



    
    // Clase que contiene el nombre del musculo y el sprite correspondiente a la sombra
    [System.Serializable] 
    public class Sombras {
        public string musculo;
        public Sprite sombra;
        
    }
}
