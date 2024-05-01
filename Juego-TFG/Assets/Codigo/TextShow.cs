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
    // array con los actores de la escena (niño/a)
    public Actor[] actorPanel;
    // array con los superheroes de la escena
    public Actor[] superheroePanel;
    // array con las diapositivas
    public Diapositiva[] imagenes;
    // guardar si la historia ya se ha reproducido
    // public int historiaVisionada;
    // nombre de la escena a la que vamos a mandar cuando termine la conversacion
    public string escena;

    public int nivel;
    
 

    public Image avatar;
    public Image avatarInicio;
    public Image avatarSuperheroe;
    public Image diapositiva;
    public Text conversacion;
    string selectedCharacter;
    

    int mensajeActivo = 0;

    void Start(){
        PersonajePanel();
        DesplegarMensaje();  
    }

    private void Awake(){
        string nombreEscena = SceneManager.GetActiveScene().name;
        // obtengo el personaje que ha elegido el jugador/a
        selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
        // comprueba si la escena ya ha sido visionada en la escena "Historia"
        if(nombreEscena == "Historia"){
            LoadHistoriaVisionada();
        }
        
    }

    // Funcion para que aparezca el sprite del personaje seleccionado por el jugador/a
    public void PersonajePanel(){
        if(selectedCharacter == "boy"){
            avatarInicio.sprite = actorPanel[1].sprite;
        }else{
            avatarInicio.sprite = actorPanel[0].sprite;
        }
    }

    // Funcion para controlar si la historia ha sido visionada, en el caso de que no, la muestra
    // En caso contrario, no la muestra y va directamente a la escena "Juego"
    public void LoadHistoriaVisionada(){
        if (PlayerPrefs.GetInt("HistoriaVisionada",0) != 0){
            SceneManager.LoadScene("Juego"); // going to menu 
        }else{
            PlayerPrefs.SetInt("HistoriaVisionada",1);
        }
    }

    // Funcion que despliega el mensaje junto con el avatar del personaje que habla
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
        
        // avatar del actor que dice el mensaje
        avatar.sprite = actor.sprite;
        // avatar del superheroe de la escena
        avatarSuperheroe.sprite = superheroePanel[mensaje.id_superheroe].sprite;
        // imagen de la escena
        if(diapositiva != null){
            diapositiva.sprite = imagenes[mensaje.id_imagen].sprite;
        }
        
    }

    public void SiguienteMensaje(){
        mensajeActivo++;
        string nombreEscena = SceneManager.GetActiveScene().name;
        
        // si aun hay mensajes por mostrar, continuo
        if(mensajeActivo < mensajes.Length){
            DesplegarMensaje();
        }else{
            // // si el nombre de la escena es historia, ya se ha visionado, asi que la pongo a true
            // if(nombreEscena == "Historia"){
            //     historiaVisionada = "true";
            //     PlayerPrefs.SetString("HistoriaVisionada", historiaVisionada);
            // }
            
            if(nombreEscena == "Historia"){
                SceneManager.LoadScene("Juego"); // going to menu
            }else{
                PlayerPrefs.SetInt("Nivel", nivel);
                if(ControladorNiveles.instancia != null){
                    ControladorNiveles.instancia.IncreaseLevel();
                }
                
                SceneManager.LoadScene(escena);

            }
        }
    }
}


[System.Serializable] //mostrar los mensajes en los ajustes
public class Mensaje {
    public int actorId;
    public int id_superheroe;
    public int id_imagen;
    public string text;
    
}

[System.Serializable] //mostrar los actores en los ajustes
public class Actor {
    public int id;
    public string name;
    public Sprite sprite;
}

[System.Serializable] //mostrar las diapositivas
public class Diapositiva {
    public int id;
    public string name;
    public Sprite sprite;
}


