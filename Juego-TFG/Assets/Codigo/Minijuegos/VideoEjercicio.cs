using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
// using UnityEngine.Networking;
// using System.IO;

public class VideoEjercicio : MonoBehaviour
{
    public GameObject videoPlayerObject;
    public VideoClip[] videos;

    void Awake(){
        ControlMusica.EstadoMusica();
    }

    void Start(){
        VideoPlayer videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();

        if(videos.Length > 0){
            // Genera un indice aleatorio dentro del rango del array
            int randomIndex = Random.Range(0, videos.Length);
            // Accede al video aleatorio del array
            VideoClip randomVideo = videos[randomIndex];
            videoPlayer.clip = randomVideo;
            videoPlayer.Play();
        }else{
            Debug.LogError("No hay videos en el array");
        }
    }
}