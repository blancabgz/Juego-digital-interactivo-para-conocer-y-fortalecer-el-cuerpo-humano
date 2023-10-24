using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // para manipular el menu

public class MainJuego : MonoBehaviour
{
    // Load scenes by name
    public void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }


}
