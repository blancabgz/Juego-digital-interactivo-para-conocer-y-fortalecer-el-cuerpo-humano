using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // para manipular el menu

public class MainJuego : MonoBehaviour
{
    // Cargar la escena de home
    public void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }


}
