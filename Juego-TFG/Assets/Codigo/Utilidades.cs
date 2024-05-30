using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class Utilidades : MonoBehaviour
{
    
    // Funcion que mezcla los elementos de un array 
    // Array -> tipo de array para mezclar
    public static void MezclarElementos<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int indiceAleatorio = Random.Range(0, array.Length);
            T temp = array[indiceAleatorio];
            array[indiceAleatorio] = array[i];
            array[i] = temp;
        }
    }

    // Funcion para redirigir a una escena 
    // opcion -> Nombre de la escena a redirigir
    public static void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }

    // Funcion para inicial el contador de medallas conseguidas
    public static void InicializarContadorMedallas(){
        PlayerPrefs.SetInt("ContadorMedallas", 0);
    }


    // Funcion que redondea el numero flotante al entero mas cercano
    // puntuacion --> El numero float para redondear
    // return int --> El numero redondeada
    public static int Redondear(float puntuacion) {
        int parteEntera = (int) puntuacion;
        float parteDecimal = puntuacion - parteEntera;

        if(parteDecimal >= 0.5){
            return parteEntera + 1;
        }else{
            return parteEntera;
        }

    }


    // ! BLANCA QUITARLO CUANDO HAGAS PUZZLE QUE ESTA YA EN MINIJUEGO
    // Funcion que calcula la puntuacion basada en la proporcion de fallos en relacion con el maximo de fallos posibles
    /// numFallos --> El numero de fallos cometidos por el jugador.
    /// max_fallos --> El maximo de fallos posibles.
    /// int return --> La puntuacion calculada, redondeada al entero mas cercano.
    public static int CalcularPuntuacionProporcion(int numFallos, int max_fallos){
        float proporcion = (float)numFallos / max_fallos;
        float puntuacion = 10.0f - proporcion * 10.0f;
        return Redondear(puntuacion);
    }

    public static bool EsEmail(string email){
        // Expresión regular para validar el formato de un correo electrónico
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

    /**
     * @brief Metodo para cargar el menú de niveles según desbloqueado
     *
     * Este metodo comprueba el último nivel nivel desbloqueado para cargar la primera escena o la segunda
    */

    public static void MenuNiveles(){
        if(PlayerPrefs.GetInt("unlockedLevels",1) >= 20){
            Utilidades.EscenaJuego("Juego2");
        }else{
            Utilidades.EscenaJuego("Juego");
        }
    }
    public static void Salir(){   
        Debug.Log("Salir...");
        Application.Quit();
    }
    
}