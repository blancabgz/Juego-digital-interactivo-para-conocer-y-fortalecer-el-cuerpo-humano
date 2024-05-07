using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Menu
using UnityEngine.UI;


public class Opciones : MonoBehaviour
{
    private bool botonOn = true;
    public Text btext;
    public GameObject panelSeguridad;


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

    public void IntentarBorrarDatos(){
        Debug.Log("borrar");
        panelSeguridad.SetActive(true);
    }

    public void BorrarDatos(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        EscenaJuego("Datos");
    }

    public void CancelarAccion(){
        panelSeguridad.SetActive(false);
    }
}
