using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMusicaHistoria : MonoBehaviour
{
    private void Start()
    {
        // Buscar el GameObject "ControlMusica" en la escena
        GameObject controlMusicaObject = GameObject.Find("ControlMusica");

        // Si se encuentra el GameObject "ControlMusica", destruirlo
        if (controlMusicaObject != null)
        {
            Destroy(controlMusicaObject);
        }
    }
}

