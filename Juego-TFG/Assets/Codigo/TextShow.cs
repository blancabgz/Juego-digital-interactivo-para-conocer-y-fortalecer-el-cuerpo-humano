using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TextShow : MonoBehaviour
{
    // array con los mensajes de la escena
    public Mensaje[] mensajes;
    // array con los actores que participan en la escena
    public Actor[] actores;
    // array con los actores que estan en el panel del fondo
    public Actor[] actorPanel;
    // array con los actores que estan en el panel del fondo
    public Actor[] superheroePanel;
    // guardar si la historia ya se ha reproducido
    public string historiaVisionada = "null";
    // nombre de la escena activa
    
 

    public Image avatar;
    public Image avatarInicio;
    public Image avatarSuperheroe;
    public Text conversacion;
    string selectedCharacter;

    int mensajeActivo = 0;

    void Start(){
        PersonajePanel();
        DesplegarMensaje();  
    }

    private void Awake(){
        string nombreEscena = SceneManager.GetActiveScene().name;
        selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
        if(nombreEscena == "Historia"){
            LoadHistoriaVisionada();
        }
        
    }

    public void PersonajePanel(){
        if(selectedCharacter == "boy"){
            Debug.Log(selectedCharacter);
            avatarInicio.sprite = actorPanel[1].sprite;
        }else{
            avatarInicio.sprite = actorPanel[0].sprite;
        }
    }

    public void LoadHistoriaVisionada(){
        if (PlayerPrefs.HasKey("HistoriaVisionada")){
            historiaVisionada = PlayerPrefs.GetString("HistoriaVisionada");
            
        }

        if(historiaVisionada != "null"){
            SceneManager.LoadScene("Juego"); // going to menu 
        }
    }

    public void DesplegarMensaje(){
        Mensaje mensaje = mensajes[mensajeActivo];
        conversacion.text = mensaje.text;
        Actor actor;
        int id = 0;
        

        //si el personaje seleccionado por el jugador es un chico y contiene el nombre del id la palabra niña
        if(selectedCharacter == "boy" && actores[mensaje.actorId].name.Contains("niña")){

            string nameActorSeleccionado = actores[mensaje.actorId].name.Replace("niña","niño"); // reemplazo la palabra niña por niño

            foreach (Actor actorNombre in actores) //busco el id de los actores que sea igual al nombre
            {
                if(actorNombre.name == nameActorSeleccionado){
                    id = actorNombre.id;
                    break;
                }
            }
            actor = actores[id];
        }else { // si el personaje elegido por el jugador es una chica o es el superheroe obtengo el actor con el id
            actor = actores[mensaje.actorId];
        }
        
        avatar.sprite = actor.sprite;
        avatarSuperheroe.sprite = superheroePanel[mensaje.id_superheroe].sprite;
    }

    public void SiguienteMensaje(){
        mensajeActivo++;
        string nombreEscena = SceneManager.GetActiveScene().name;
        
        if(mensajeActivo < mensajes.Length){
            DesplegarMensaje();
        }else{
            if(nombreEscena == "Historia"){
                historiaVisionada = "true";
            }
            PlayerPrefs.SetString("HistoriaVisionada", historiaVisionada);
            if(nombreEscena == "Historia"){
                SceneManager.LoadScene("Juego"); // going to menu
            }else{
                //SceneManager.LoadScene();
            }
        }
    }
}


[System.Serializable] //mostrar los mensajes en los ajustes
public class Mensaje {
    public int actorId;
    public int id_superheroe;
    public string text;
}

[System.Serializable] //mostrar los actores en los ajustes
public class Actor {
    public int id;
    public string name;
    public Sprite sprite;
}


