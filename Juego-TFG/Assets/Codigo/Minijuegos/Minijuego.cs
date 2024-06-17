using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego : MonoBehaviour{

    public int nivel;
    public int puntos;   
    public int numFallos;
    public int numIntentos;
    public int multiplicador;
    public GameObject panelFinal;
    
    private const int NUM_NIVELES = 22;

    
    public void MezclarElementos<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int indiceAleatorio = Random.Range(0, array.Length);
            T temp = array[indiceAleatorio];
            array[indiceAleatorio] = array[i];
            array[i] = temp;
        }
    }

    public void MezclarElementos<T>(List<T> lista){
        for (int i = 0; i < lista.Count; i++){
            int indiceAleatorio = Random.Range(0, lista.Count);
            T temp = lista[indiceAleatorio];
            lista[indiceAleatorio] = lista[i];
            lista[i] = temp;
        }
    }

   

    /**
     * @brief Metodo para incrementar el numero de fallos
     * Este método se utiliza para aumentar el número de fallos registrado.
    */

    public void AumentarNumeroFallos(){
        // Debug.Log("Numero de fallos antes" + numFallos);
        numFallos++;
    }

    /**
     * @brief Metodo para decrementar el numero de fallos
     * Este método se utiliza para disminuir el número de fallos registrado.
    */

    public void DisminuirNumeroFallos(){
        // Debug.Log("Numero de fallos antes" + numFallos);
        numFallos--;
    }

     /**
     * @brief Metodo para almacenar que el nivel ha sido completado y almacenar la puntuacion obtenida
     *
     * Este método almacena el nivel completado y la puntuacion obtenida por el jugador 
     *
    */

    public void GuardarPuntuacion(int pantalla){
        ControladorNiveles.GuardarNivel(nivel,pantalla);
        Calificaciones.GuardarPuntuacion(nivel, puntos);
    }    

    /**
     * @brief Metodo para mostrar el panel final 
     *
     * Este método muestra el panel final una vez el juego ha finalizado
    */
    public void MostrarPanelFinal(){
        if (panelFinal != null){
            panelFinal.SetActive(true);
        }
    }

    
    /**
     * @brief Metodo para calcular la puntuación final obtenida por el jugador
     *
     * Este método calcula la puntuación final obtenida por el jugador dependiendo de los fallos cometidos durante el juego
    */
    public void CalcularPuntuacionFinal(int multiplicador){
        puntos -= numFallos * multiplicador;
        if(puntos < 0){
            puntos = 0;
        }
    }

    /**
     * @brief Metodo para calcular la puntuación final obtenida por el jugador
     *
     * Este método calcula la puntuación final obtenida por el jugador dependiendo del numero de intentos que le queden
     * 
     * @param intentos Numero de intentos actuales
    */

    public void CalcularPuntuacionFinalIntentos(){
        puntos -= (6 - numIntentos) * 2;  
    }    

    // Funcion que calcula la puntuacion para la segunda ronda de juego. 

    public void CalcularPuntuacionRonda(){
        puntos = 10;
        if(numFallos == 2){
            puntos -= 10;
        }else if(numFallos == 1){
            puntos -= 5;
        }else{
            puntos -= 0;
        }
    }

    // Funcion que calcula la puntuacion para la segunda ronda de juego. 

    public void CalcularPuntuacionSegundaRonda(){
        if(numFallos == 0){
            puntos += 10;
        }else if(numFallos == 1){
            puntos += 5;
        }else{
            puntos += 0;
        }
    }

    // Funcion calcular puntuacion con 4 opciones

    public void CalcularPuntuacion3Opciones(){
        if(numFallos == 3){
            puntos -= 10;
        }else if(numFallos == 2){
            puntos -= 6;
        }else if(numFallos == 1){
            puntos -= 4;
        }
    }


    // Funcion que calcula la puntuacion basada en la proporcion de fallos en relacion con el maximo de fallos posibles
    /// numFallos --> El numero de fallos cometidos por el jugador.
    /// max_fallos --> El maximo de fallos posibles.
    /// int return --> La puntuacion calculada, redondeada al entero mas cercano.
    public int CalcularPuntuacionProporcion(int max_fallos){
        float proporcion = (float)numFallos / max_fallos;
        float puntuacion = 10.0f - proporcion * 10.0f;
        return Redondear(puntuacion);
    }

    // Funcion para inicial el contador de medallas conseguidas
    public void InicializarContadorMedallas(){
        PlayerPrefs.SetInt("ContadorMedallas", 0);
    }

    // Funcion que obtiene la puntuacion 
    public void ObtenerPuntuacionNivel(){

        // Si el nivel ya ha sido completado 
        if(ControladorNiveles.CargarNivel(nivel) == 2){
            // reinicio los puntos
            puntos = -1;
        // Si el menu solo ha sido completado la primera fase
        }else if(ControladorNiveles.CargarNivel(nivel) == 1){
            // Obtengo los puntos conseguidos en la primera fase
            puntos = Calificaciones.CargarPuntuacion(nivel);
        // Si no ha sido completado
        }else{
            // Reinicio los puntos
            puntos = -1;
        }
    }

    // Funcion que redondea el numero flotante al entero mas cercano
    // puntuacion --> El numero float para redondear
    // return int --> El numero redondeada
    public int Redondear(float puntuacion) {
        int parteEntera = (int) puntuacion;
        float parteDecimal = puntuacion - parteEntera;

        if(parteDecimal >= 0.5){
            return parteEntera + 1;
        }else{
            return parteEntera;
        }

    }

    

    

    public Sprite CargarSprite(string archivo, string spriteName){
        Sprite[] sprites = Resources.LoadAll<Sprite>(archivo);
        // Cargar un Sprite desde la ruta de archivo en la carpeta Resources
        foreach(var sprite in sprites){
            if(sprite.name == spriteName){
                return sprite;
            }
        }
        return null;
    }

}