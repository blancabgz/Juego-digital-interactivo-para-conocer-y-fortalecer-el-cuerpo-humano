using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public string selectedCharacter = "null";

    // Method to load the character already selected by the player
    private void Awake(){
        LoadSelectedCharacter();
    }
    
    // Method to select a character, save it and going to menu 
    public void SelectCharacter(string gender){
        selectedCharacter = gender;
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter ); // key and value
        SceneManager.LoadScene("Juego"); // going to menu 
    }

    // Method to load the character, if this character exit, going to the level menu without selecting the character again
    public void LoadSelectedCharacter(){
        if (PlayerPrefs.HasKey("SelectedCharacter")){
            selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
            Debug.Log("El genero seleccionado es " + selectedCharacter);
        }

        if(selectedCharacter != "null"){
            SceneManager.LoadScene("Historia"); // going to menu 
        }
    }



}
