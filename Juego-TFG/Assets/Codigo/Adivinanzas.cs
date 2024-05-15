using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Adivinanzas : MonoBehaviour
{
    public Adivinanza[] adivinanzas;
    private GameObject panelAdivinanzas;
    private TextMeshProUGUI adivinanzaText;
    
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
    void Start()
    {   
        // Mezcla elementos
        Utilidades.MezclarElementos(adivinanzas);
        panelAdivinanzas = GameObject.Find("Pregunta");
        SeleccionarAdivinanza();
    }

    void SeleccionarAdivinanza(){
        string musculo;
        string adivinanzaSeleccionada;
        // Obtiene el componente TextMeshProUGUI
        adivinanzaText= panelAdivinanzas.GetComponentInChildren<TextMeshProUGUI>();
        if(adivinanzaText != null){
            int indiceAdivinanza = Random.Range(0, adivinanzas.Length);
            if(indiceAdivinanza <= adivinanzas.Length){
                musculo = adivinanzas[indiceAdivinanza].musculo;
                adivinanzaSeleccionada = adivinanzas[indiceAdivinanza].adivinanza;
                if(string.IsNullOrEmpty(musculo) || string.IsNullOrEmpty(adivinanzaSeleccionada)){
                    Debug.LogError("La palabra elegida es nula");
                    return;
                }else{
                    adivinanzaText.text = adivinanzaSeleccionada;
                    PlayerPrefs.SetString("MusculoSeleccionado", musculo);
                    Invoke("CargarRespuestas", 15f);
                }
            }else{
                Debug.LogError("Fuera de rango");
                return;
            }
        }else{
            Debug.LogError("No se encontró el componente");
            return;
        }
    }

    public void CargarRespuestas(){
        SceneManager.LoadScene("AdivinanzaSeleccion");
    }



    [System.Serializable]
    public class Adivinanza{
        public string musculo;
        public string adivinanza;
    }
}
