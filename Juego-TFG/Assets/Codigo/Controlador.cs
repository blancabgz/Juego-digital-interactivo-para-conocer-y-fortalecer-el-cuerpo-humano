using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class Controlador : MonoBehaviour
{
    
    private void Awake() {
        ControlMusica.EstadoMusica();
    }
    // Funcion para redirigir a una escena 
    // opcion -> Nombre de la escena a redirigir
    public static void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
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
            EscenaJuego("Juego2");
        }else{
            EscenaJuego("Juego");
        }
    }
    public static void Salir(){   
        // Debug.Log("Salir...");
        Application.Quit();
    }
    
}