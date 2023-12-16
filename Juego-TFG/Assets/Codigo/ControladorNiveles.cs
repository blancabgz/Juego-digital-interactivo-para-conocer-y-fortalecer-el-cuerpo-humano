using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // reference to buttons

public class ControladorNiveles : MonoBehaviour
{
    public static ControladorNiveles instancia; 
    public Button[] levelBottons; // array containing level buttons
    public int unlock; // unlock level

    private void Awake() { // only one scene
        if(instancia == null){
            instancia = this;
        }   
    }


    // Start is called before the first frame update
    void Start()
    {
        if(levelBottons.Length > 0){

            // disable interaction on all buttons
            for (int i = 0; i < levelBottons.Length; i++)
            {
                levelBottons[i].interactable = false;
            }
            
            // Unlock levels up to the last level unlocked by the player
            for (int i = 0; i < PlayerPrefs.GetInt("unlockedLevels",1); i++)
            {
                levelBottons[i].interactable = true;   
            }
        }
    }

    // Increase unlocked level
    public void IncreaseLevel(){
        if (unlock > PlayerPrefs.GetInt("unlockedLevels",1))
        {
            PlayerPrefs.SetInt("unlockedLevels",unlock); // Save the value of the unlocked levels
        }
    }

    public int ActualLevel(){
        return PlayerPrefs.GetInt("unlockedLevels",1);
    }   
}
