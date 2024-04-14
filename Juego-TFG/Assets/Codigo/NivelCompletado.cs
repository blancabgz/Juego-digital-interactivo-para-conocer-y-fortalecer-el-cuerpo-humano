using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NivelCompletado : MonoBehaviour 
{
    private const string NIVEL_ESTADO = "EstadoNivel_";
    private const int NUM_NIVELES = 22;

    // estado = 0 --> nivel no completado
    // estado = 1 --> pantalla 1
    // estado = 2 --> pantalla 2
    public static void GuardarNivel(int nivel, int estado){
        string estado_nivel = NIVEL_ESTADO + nivel.ToString();
        PlayerPrefs.SetInt(estado_nivel, estado);
        PlayerPrefs.Save();
    }

    public static int CargarNivel(int nivel){
        string estado_nivel = NIVEL_ESTADO + nivel.ToString();
        return(PlayerPrefs.GetInt(estado_nivel, 0)); // Si no hay valor guardado, devuelve 0
    }
}