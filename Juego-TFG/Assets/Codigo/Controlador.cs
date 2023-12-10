using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Menu

public class MainMenu : MonoBehaviour
{

    // Load scene 
    public void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }

    // Exit play
    public void Salir()
    {   
        Debug.Log("Salir...");
        Application.Quit();
    }
}
