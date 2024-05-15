using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // para manipular el menu

public class MainJuego : MonoBehaviour
{
    AudioSource[] audioSources;
    private string musica;

    void Awake(){
        audioSources = FindObjectsOfType<AudioSource>();
        musica = PlayerPrefs.GetString("estadoMusica", "null");
        if(musica != null){
            if(musica == "OFF"){
                if(audioSources != null){
                    foreach (AudioSource audioSource in audioSources){
                        audioSource.mute = false;
                    }
                }
            }
        }
    }
    
    public void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }


}
