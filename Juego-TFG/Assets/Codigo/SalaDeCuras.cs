using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SalaDeCuras : MonoBehaviour
{
    public string respuesta;
    public string[] curasOpciones;
    public GameObject panelFinal;
    
    
    void Start()
    {
        // Mezcla las opciones de las curas
        Utilidades.MezclarElementos(curasOpciones);
        // Muestra las opciones posibles
        MostrarCuras();
    }

    //
    // Obtiene el numero de opcion del boton y comprueba si la respuesta seleccionada es correcta
    //
    public void RecogerRespuesta(int opcion){
        // Comprueba si la respuesta es correcta
        if(respuesta == curasOpciones[opcion]){
            // Activa el panel final tras comprobar que la respuesta es correcta
            if (panelFinal != null) {
                panelFinal.SetActive(true);
            }
        // Si la respuesta no es correcta
        }else{
            // Obtiene el componente Curas
            GameObject curas = GameObject.Find("Curas");
            if(curas != null){
                // Obtiene el componente cura según el indice
                GameObject cura = curas.transform.GetChild(opcion).gameObject;
                if(cura != null){
                    // Obtiene el componente Image de cura
                    Image falloImage = cura.transform.Find("Fallo").GetComponent<Image>();
                    if(falloImage != null){
                        // Activa la imagen
                        falloImage.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    //
    // Empareja las opciones a las curas
    //

    public void MostrarCuras(){
        TextMeshProUGUI textCura;
        // Se recorren todas las opciones de curas
        for(int i = 0; i < curasOpciones.Length; i++){
            // Obtiene el componente que contiene todas las curas
            GameObject curas = GameObject.Find("Curas");
            if(curas != null){
                // Obtiene el componente cura por el indice
                GameObject cura = curas.transform.GetChild(i).gameObject;
                // Obtiene el componente TextMeshProGUI del boton
                textCura = cura.GetComponentInChildren<TextMeshProUGUI>();
                // Verifica si lo ha encontrado
                if(textCura != null){
                    // Asigna el nombre de la cura
                    textCura.text = curasOpciones[i];
                }
                
            }
        }
    }
}
