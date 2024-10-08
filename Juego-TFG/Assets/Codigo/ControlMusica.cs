using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMusica : MonoBehaviour
{
    private static ControlMusica instance;
    public List<string> escenasPersiste;
    private string musica;

    void Start()
    {
        if(instance != null && instance != this){
            Destroy(gameObject);
            return;
        }

        instance = this;

        musica = PlayerPrefs.GetString("estadoMusica", "null");

        if(musica == "OFF"){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }

        // if (instance == null && musica == "ON"){
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }else if (instance != null || musica == "null" || musica == "OFF"){
        //     Destroy(gameObject);
        //     return;
        // }
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if (escenasPersiste.Contains(scene.name)){
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public static void EstadoMusica(){
        GameObject controlMusica = GameObject.Find("ControlMusica");
        if(PlayerPrefs.GetString("estadoMusica", "null") == "OFF"){

            if(controlMusica != null){
                Destroy(controlMusica);
            }   
            
        }else{
            if(controlMusica != null){
                AudioSource audioSource = controlMusica.GetComponent<AudioSource>();
                if(audioSource != null){
                    audioSource.volume = 0.5f;
                }
            } 
        }
    }

}
