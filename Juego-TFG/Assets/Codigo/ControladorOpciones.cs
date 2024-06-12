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
    public AudioSource[] audioSources;
    private string musica;
    // Load scene 
    public void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        musica = PlayerPrefs.GetString("estadoMusica", "ON");
        ComprobarEstadoMusica();

    }


    public void ComprobarEstadoMusica()
    {
        if (audioSources != null)
        {
            bool musicaEncendida = (musica == "ON");
            string estadoTexto = (musicaEncendida) ? "ON" : "OFF";

            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = !musicaEncendida;
            }

            btext.text = estadoTexto;
        }
    }

    public void CambiarModoBoton()
    {
        // cambiamos el valor del boton
        botonOn = !botonOn;
        if (audioSources != null)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (botonOn == true)
                {
                    btext.text = "ON";
                    if (audioSource != null)
                    {
                        audioSource.mute = false;
                    }
                    musica = "ON";
                    PlayerPrefs.SetString("estadoMusica", musica);
                }
                else
                {
                    btext.text = "OFF";
                    if (audioSource != null)
                    {
                        audioSource.mute = true;
                    }
                    musica = "OFF";
                    PlayerPrefs.SetString("estadoMusica", musica);

                }
                PlayerPrefs.Save();
            }
        }
    }

    public void IntentarBorrarDatos()
    {
        panelSeguridad.SetActive(true);
    }

    public void CancelarAccion()
    {
        panelSeguridad.SetActive(false);
    }
}
