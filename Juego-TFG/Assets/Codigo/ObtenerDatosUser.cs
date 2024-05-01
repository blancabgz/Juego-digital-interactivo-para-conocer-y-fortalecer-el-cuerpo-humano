using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Menu

public class ObtenerDatosUser : MonoBehaviour
{
    public TMP_InputField nombre;
    public TMP_InputField email;
    void Start(){
        if(PlayerPrefs.GetInt("Datos",0) == 0){
            PlayerPrefs.SetInt("Datos",1);
        }else{
            SceneManager.LoadScene("MenuPrincipal");
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Si el campo de entrada nombre tiene el foco, cambiar al campo de entrada email
            if (nombre.isFocused)
            {
                email.Select();
            }
            // Si el campo de entrada email tiene el foco, cambiar al campo de entrada nombre
            else if (email.isFocused)
            {
                nombre.Select();
            }
        }  
    }

    public void GuardarDato(TMP_InputField campo){
        // Compruebo si es un mail
        if(Utilidades.EsEmail(campo.text)){
            GuardarDatosJugador.GuardarEmail(campo.text.ToLower());
            // Debug.Log(GuardarDatosJugador.CargarEmailJugador());
        }else{
            GuardarDatosJugador.GuardarNombre(campo.text.ToLower());
            // Debug.Log(GuardarDatosJugador.CargarNombreJugador());
        } 
    }

    
}
