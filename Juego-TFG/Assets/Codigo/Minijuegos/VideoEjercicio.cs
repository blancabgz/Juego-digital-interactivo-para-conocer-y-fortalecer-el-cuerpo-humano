using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;
using System.IO;

public class VideoEjercicio : MonoBehaviour
{
    public GameObject videoPlayerObject;

    void Awake()
    {
        ControlMusica.EstadoMusica();
    }

    void Start()
    {
        VideoPlayer videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
        string[] videoFiles = Directory.GetFiles(Application.streamingAssetsPath, "*.mp4");

        if (videoFiles.Length > 0)
        {
            // Genera un índice aleatorio dentro del rango del array de archivos
            int randomIndex = Random.Range(0, videoFiles.Length);
            // Accede al video aleatorio del array de archivos
            string randomVideoPath = videoFiles[randomIndex];

            // Para Android, utiliza una ruta especial
            if (Application.platform == RuntimePlatform.Android)
            {
                StartCoroutine(PlayVideoOnAndroid(randomVideoPath, videoPlayer));
            }
            else
            {
                videoPlayer.url = randomVideoPath;
                videoPlayer.Prepare();
                videoPlayer.prepareCompleted += (vp) => vp.Play();
            }
        }
        else
        {
            Debug.LogError("No hay videos en la carpeta StreamingAssets");
        }
    }

    IEnumerator PlayVideoOnAndroid(string videoPath, VideoPlayer videoPlayer)
    {
        string url = Path.Combine(Application.streamingAssetsPath, Path.GetFileName(videoPath));

        // UnityWebRequest to access video from StreamingAssets
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SendWebRequest();

            while (!request.isDone)
            {
                yield return null;
            }

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al cargar el video: " + request.error);
            }
            else
            {
                string temporaryPath = Path.Combine(Application.persistentDataPath, Path.GetFileName(videoPath));
                File.WriteAllBytes(temporaryPath, request.downloadHandler.data);
                videoPlayer.url = temporaryPath;
                videoPlayer.Prepare();
                while (!videoPlayer.isPrepared)
                {
                    yield return null;
                }
                videoPlayer.Play();
            }
        }
    }
}