using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Menu
using UnityEngine.UI;


public class Opciones : MonoBehaviour
{
    private bool botonOn = true;
    public Text btext;

    // Load scene 
    public void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }

    public void CambiarPersonaje(){
        // Cambiamos el valor guardado a null
        PlayerPrefs.SetString("SelectedCharacter", "null");
        // Nos aseguramos que los cambios se realizan
        PlayerPrefs.Save();
        
        // Cargamos la escena "EleccionPersonaje"
        EscenaJuego("EleccionPersonaje");
    }

    public void CambiarModoBoton(){
        // cambiamos el valor del boton
        botonOn = !botonOn;
        


        if(botonOn == true){
            btext.text = "ON";  
        }else{
            btext.text = "OFF";
        }
    }
}
