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
        musica = PlayerPrefs.GetString("estadoMusica", "null");
        Debug.Log(musica);
        if (instance == null && musica == "ON")
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null || musica == "null" || musica == "OFF")
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (escenasPersiste.Contains(scene.name))
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
