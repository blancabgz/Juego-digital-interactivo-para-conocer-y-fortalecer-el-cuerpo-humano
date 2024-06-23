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
    public TextMeshProUGUI mensajeError;
    public Usuario usuario;
 
    void Start(){
        if(PlayerPrefs.GetInt("Datos",0) != 0){
            Controlador.EscenaJuego("MenuPrincipal");   
        }
        mensajeError.text = "";
        usuario = new Usuario();
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

        if(campo.name == "Correo"){
            if(Controlador.EsEmail(campo.text)){
                usuario.email_tutor = campo.text.ToLower();
                mensajeError.text = "";
            }else{
                mensajeError.text = "El correo electrónico no es correcto";
            }
        }

        if(campo.name == "Nombre"){
            if(!string.IsNullOrEmpty(campo.text)){
                usuario.nombre = campo.text.ToLower();
            }
        }
        
    }


    public void Continuar(string escena){
        if(string.IsNullOrEmpty(nombre.text) || string.IsNullOrEmpty(email.text) || !Controlador.EsEmail(email.text)){
            mensajeError.text = "Por favor, complete todos los campos correctamente.";
        }else{
            usuario.nombre = nombre.text;
            usuario.email_tutor = email.text;
            ControladorUsuario.GuardarUsuario(usuario);
            PlayerPrefs.SetInt("Datos",1);
            Controlador.EscenaJuego(escena);
        }
    }

    
}
