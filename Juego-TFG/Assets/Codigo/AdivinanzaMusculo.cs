using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AdivinanzaMusculo : MonoBehaviour
{
    private string musculo;
    // Start is called before the first frame update
    void Start()
    {
        musculo = PlayerPrefs.GetString("MusculoSeleccionado");
        Debug.Log(musculo);
        if(musculo == null){
            Debug.LogError("No se ha seleccionado musculo");
            return;
        }
    }

    public void ComprobarMusculo(GameObject boton){
        string texto;

        if(boton == null){
            Debug.LogError("El GameObject no ha sido encontrado");
            return;
        }

        // Obtener el componente TextMeshProUGUI del botón
        TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();
        if (textoBoton != null){
            texto = textoBoton.text;
            if(string.Equals(musculo, texto)){
                Debug.Log("Acierto");
            }else{
                Debug.Log("Fallo");
            }
        }else{
            Debug.LogError("El componente Text no ha sido encontrado");
        }

    }
}
