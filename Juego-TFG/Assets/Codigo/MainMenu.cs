using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // para manipular el menu

public class MainMenu : MonoBehaviour
{

    // Cargar la escena del juego al pulsar el boton
    public void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }

    // Salir del juego
    public void Salir()
    {   
        Debug.Log("Salir...");
        Application.Quit();
    }
}
