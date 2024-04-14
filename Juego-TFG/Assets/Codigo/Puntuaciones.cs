using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Puntuaciones : MonoBehaviour 
{
    private const string PUNTUACION_NIVEL = "PuntuacionNivel_";
    private const int NUM_NIVELES = 22;

    public static void GuardarPuntuacion(int nivel, int puntos){
        string clave = PUNTUACION_NIVEL + nivel.ToString();
        PlayerPrefs.SetInt(clave, puntos);
        PlayerPrefs.Save();
    }

    public static int CargarPuntuacion(int nivel){
        string clave = PUNTUACION_NIVEL + nivel.ToString();
        return(PlayerPrefs.GetInt(clave, -1)); // Si no hay valor guardado, devuelve 0
    }
}