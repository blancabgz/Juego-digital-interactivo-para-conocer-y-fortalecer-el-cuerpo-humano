using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public string selectedCharacter = "null";

    // Metodo para cargar personaje 
    private void Awake(){
        LoadSelectedCharacter();
    }
    
    // Metodo para seleccionar un personaje, guardarlo en memoria.
    public void SelectCharacter(string gender){
        selectedCharacter = gender;
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter ); 
        Controlador.EscenaJuego("MenuPrincipal"); 
    }

    // Metodo para cargar un personaje
    public void LoadSelectedCharacter(){
        if (PlayerPrefs.HasKey("SelectedCharacter")){
            selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
        }

        if(selectedCharacter != "null"){
            Controlador.EscenaJuego("Historia"); // going to menu 
        }
    }
}
