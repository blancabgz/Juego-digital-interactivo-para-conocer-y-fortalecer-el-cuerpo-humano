using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Adivinanzas : Minijuego
{
    public Adivinanza[] adivinanzas;
    private GameObject panelAdivinanzas;
    private TextMeshProUGUI adivinanzaText;


    void Awake(){
        ControlMusica.EstadoMusica();
    }
    void Start(){
        // Comprueba que el array de adivinanzas no este vacio   
        if(adivinanzas == null || adivinanzas.Length == 0){
            Debug.LogError("No hay adivinanzas");
            return;
        }

        // Mezclar elementos adivinanzas
        base.MezclarElementos(adivinanzas);
        panelAdivinanzas = GameObject.Find("Pregunta");

        if(panelAdivinanzas == null){
            Debug.LogError("No se ha encontrado el panel de Adivinanzas");
            return;
        }

        // Obtener el panel de la adivinanza
        adivinanzaText= panelAdivinanzas.GetComponentInChildren<TextMeshProUGUI>();

        if (adivinanzaText == null){
            Debug.LogError("No se encontró el componente TextMeshProUGUI en el panel de adivinanzass");
            return;
        }

        SeleccionarAdivinanza();
    }

    /**
     * @brief Selecciona una adivinanza de forma aleatoria
     * 
     * Esta función selecciona una adivinanza de forma aleatoria, muestra la adivinanza por pantalla y almacena el musculo seleccionado 
     * 
    */
    void SeleccionarAdivinanza(){
    
        int indiceAdivinanza = Random.Range(0, adivinanzas.Length);

        // Obtenemos la adivinanza generada de forma aleatoria
        Adivinanza adivinanzaSeleccionada = adivinanzas[indiceAdivinanza];

        if(string.IsNullOrEmpty(adivinanzaSeleccionada.musculo) || string.IsNullOrEmpty(adivinanzaSeleccionada.adivinanza)){
            Debug.LogError("La palabra elegida es nula");
            return;
        }else{
            // Mostramos la adivinanza y guardamos el musculo
            adivinanzaText.text = adivinanzaSeleccionada.adivinanza;
            PlayerPrefs.SetString("MusculoSeleccionado", adivinanzaSeleccionada.musculo);
            Invoke("CargarRespuestas", 15f);
        }        
    }

    /**
    * @brief Carga la escena del minijuego para adivinar el musculo
    */ 
    public void CargarRespuestas(){
        Controlador.EscenaJuego("AdivinanzaSeleccion");
    }

    [System.Serializable]
    public class Adivinanza{
        public string musculo;
        public string adivinanza;
    }
}
