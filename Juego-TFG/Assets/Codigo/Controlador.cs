using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class Controlador : MonoBehaviour
{
    
    private static void Awake() {
        ControlMusica.EstadoMusica();
    }
    // Funcion para redirigir a una escena 
    // opcion -> Nombre de la escena a redirigir
    public static void EscenaJuego(string opcion){
        SceneManager.LoadScene(opcion);
    }


    public static bool EsEmail(string email){
        // Expresión regular para validar el formato de un correo electrónico
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        // Debug.Log(regex.IsMatch(email));
        return regex.IsMatch(email);
    }

    /**
     * @brief Metodo para cargar el menú de niveles según desbloqueado
     *
     * Este metodo comprueba el último nivel nivel desbloqueado para cargar la primera escena o la segunda
    */

    
    public static void Salir(){   
        // Debug.Log("Salir...");
        Application.Quit();
    }

    public static void CambiarPersonaje(){
        // Cambiamos el valor guardado a null
        PlayerPrefs.SetString("SelectedCharacter", "null");
        // Nos aseguramos que los cambios se realizan
        PlayerPrefs.Save();
        // Cargamos la escena "EleccionPersonaje"
        EscenaJuego("EleccionPersonaje");
    }

    public static void CambiarDatos(){
        PlayerPrefs.SetInt("Datos",0);
        PlayerPrefs.Save();
        EscenaJuego("Datos");
    }

    public static void BorrarDatos(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        EscenaJuego("Datos");
    }
    
}