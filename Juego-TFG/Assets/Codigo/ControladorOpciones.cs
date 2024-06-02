using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Menu
using UnityEngine.UI;


public class ControladorOpciones : MonoBehaviour
{
    private bool botonOn = true;
    public Text btext;
    public GameObject panelSeguridad;
    AudioSource[] audioSources;
    private string musica;
    // Load scene 
    private void Start() {
        audioSources = FindObjectsOfType<AudioSource>();
        // Debug.Log(audioSources.Length);
        musica = PlayerPrefs.GetString("estadoMusica", "null");
        if(musica == "null"){
            musica = "ON";
            PlayerPrefs.SetString("estadoMusica", musica);
            PlayerPrefs.Save();
        }
        ComprobarEstadoMusica();
        
    }


    public void ComprobarEstadoMusica(){
        if(audioSources != null){
            foreach (AudioSource audioSource in audioSources){
                if(musica == "ON"){
                    audioSource.mute = false;
                    btext.text = "ON";
                }else{
                    audioSource.mute = true;
                    btext.text = "OFF";
                }
            }
        }

    }

    public void CambiarModoBoton(){
        // cambiamos el valor del boton
        botonOn = !botonOn;
        if (audioSources != null) {
            foreach (AudioSource audioSource in audioSources){
                if(botonOn == true){
                    btext.text = "ON";
                    if(audioSource != null){
                        audioSource.mute = false;
                    }
                    musica = "ON";
                    PlayerPrefs.SetString("estadoMusica", musica);
                }else{
                    btext.text = "OFF";
                    if(audioSource != null){
                        audioSource.mute = true;
                    }
                    musica = "OFF";
                    PlayerPrefs.SetString("estadoMusica", musica);

                }
                PlayerPrefs.Save();
            }
        }
    }

    public void IntentarBorrarDatos(){
        panelSeguridad.SetActive(true);
    }

    public void CancelarAccion(){
        panelSeguridad.SetActive(false);
    }
}
