using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GuardarDatosJugador : MonoBehaviour 
{
    private const string NOMBRE_JUGADOR = "NombreJugador";
    private const string EMAIL_JUGADOR = "EmailJugador";

    public static void GuardarNombre(string nombre){
        PlayerPrefs.SetString(NOMBRE_JUGADOR, nombre);
        PlayerPrefs.Save();
    }

    public static void GuardarEmail(string email){
        PlayerPrefs.SetString(EMAIL_JUGADOR, email);
        PlayerPrefs.Save();
    }

    public static string CargarNombreJugador()
    {
        return PlayerPrefs.GetString(NOMBRE_JUGADOR, "");
    }

    public static string CargarEmailJugador()
    {
        return PlayerPrefs.GetString(EMAIL_JUGADOR, "");
    }

}

    [System.Serializable]
    public class Usuario
    {
        string nombre;
        string email_tutor;
        
    }

