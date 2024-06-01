using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControladorDialogo : MonoBehaviour
{
    public string escena;
    public int nivel;
    // array con los mensajes de la escena
    public Mensaje[] mensajes;
    // array con los actores que participan en la escena
    public Personaje[] actores;
    // array con los actores de la escena (niño/a)
    public Personaje[] actorPanel;
    // array con los superheroes de la escena
    public Personaje[] superheroePanel;
    // array con las diapositivas
    public Diapositiva[] imagenes;
    // guardar si la historia ya se ha reproducido
    // public int historiaVisionada;
    // nombre de la escena a la que vamos a mandar cuando termine la conversacion
   
    public Image avatar;
    public Image avatarInicio;
    public Image avatarSuperheroe;
    public Image diapositiva;
    public Text conversacion;
    string selectedCharacter;
    

    int mensajeActivo = 0;  

    private void Awake(){
        string nombreEscena = SceneManager.GetActiveScene().name;
        GameObject controlMusica = GameObject.Find("ControlMusica");
        if(PlayerPrefs.GetString("estadoMusica", "null") == "OFF"){
            if(controlMusica != null){
                Destroy(controlMusica);
            }   
        }

        if(nombreEscena == "Historia"){
            if (PlayerPrefs.GetInt("HistoriaVisionada",0) == 1){
                Controlador.EscenaJuego("Juego"); 
            }
        }
        
        // obtengo el personaje que ha elegido el jugador/a
        selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
        // comprueba si la escena ya ha sido visionada en la escena "Historia"

    }
    void Start(){
        PersonajePanel();
        DesplegarMensaje();  
    }

    // Funcion para que aparezca el sprite del personaje seleccionado por el jugador/a
    public void PersonajePanel(){
        if(selectedCharacter == "boy"){
            avatarInicio.sprite = actorPanel[1].sprite;
        }else{
            avatarInicio.sprite = actorPanel[0].sprite;
        }
    }
    

    // Funcion que despliega el mensaje junto con el avatar del personaje que habla
    public void DesplegarMensaje(){
        Mensaje mensaje = mensajes[mensajeActivo];
        conversacion.text = mensaje.text;


        Personaje actor;
        int id = 0;
    
        //si el personaje seleccionado por el jugador es un chico y contiene el nombre del id la palabra niña
        if(selectedCharacter == "boy" && actores[mensaje.actorId].name.Contains("niña")){

            string nameActorSeleccionado = actores[mensaje.actorId].name.Replace("niña","niño"); // reemplazo la palabra niña por niño

            foreach (Personaje actorNombre in actores) //busco el id de los actores que sea igual al nombre
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
            if(nombreEscena == "Historia"){
                PlayerPrefs.SetInt("HistoriaVisionada",1);
                Controlador.EscenaJuego("Juego"); // going to menu
            }else{
                PlayerPrefs.SetInt("Nivel", nivel);
                if(ControladorNiveles.instancia != null){
                    ControladorNiveles.instancia.AumentarNivel();
                }
                
                Controlador.EscenaJuego(escena);

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
public class Personaje {
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


