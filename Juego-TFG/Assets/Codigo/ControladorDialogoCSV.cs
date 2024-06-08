using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


public class ControladorDialogoCSV : MonoBehaviour
{
    private int nivel;
    public string escena;
    // array con los mensajes de la escena
    // public Mensaje[] mensajes;
    private List<MensajeCSV> mensajes;
    // array con los actores que participan en la escena
    private List<PersonajeCSV> actores = new List<PersonajeCSV>();
    // array con los actores de la escena (niño/a)
    private List<PersonajeCSV> actorPanel = new List<PersonajeCSV>();
    // array con los superheroes de la escena
    private List<PersonajeCSV> superheroePanel = new List<PersonajeCSV>();
    // array con las diapositivas
    private List<PersonajeCSV> imagenes = new List<PersonajeCSV>();

    private string[] lineas;
    // guardar si la historia ya se ha reproducido
    // public int historiaVisionada;
    // nombre de la escena a la que vamos a mandar cuando termine la conversacion
   
    public Image avatar;
    public Image avatarInicio;
    public Image avatarSuperheroe;
    public Image diapositiva;
    public Text conversacion;
    string selectedCharacter;
    private string nombreEscenaActual;
    

    int mensajeActivo;  

    private void Awake(){
        string nombreEscena = SceneManager.GetActiveScene().name;
        nombreEscenaActual = nombreEscena;
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

        mensajeActivo = 0;
        nivel = PlayerPrefs.GetInt("nivelSeleccionado", 1);
        Debug.Log("Nivel" + nivel);
        LeerArchivo("Assets/Codigo/Datos/historias.csv");
        LeerArchivo("Assets/Codigo/Datos/actorhabla.csv", actores);
        LeerArchivo("Assets/Codigo/Datos/jugadorpanel.csv", actorPanel);
        LeerArchivo("Assets/Codigo/Datos/superheroepanel.csv", superheroePanel);
        LeerArchivo("Assets/Codigo/Datos/imagenesDiapositivas.csv", imagenes);
        
        
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
        MensajeCSV mensaje = mensajes[mensajeActivo];
        conversacion.text = mensaje.texto;

        PersonajeCSV actor;
        int id = 0;
        // Debug.Log(actores[0].name);
        //si el personaje seleccionado por el jugador es un chico y contiene el nombre del id la palabra niña
        if(selectedCharacter == "boy" && actores[mensaje.actor_habla].name.Contains("niña")){

            string nameActorSeleccionado = actores[mensaje.actor_habla].name.Replace("niña","niño"); // reemplazo la palabra niña por niño

            foreach (PersonajeCSV actorNombre in actores) //busco el id de los actores que sea igual al nombre
            {
                if(actorNombre.name == nameActorSeleccionado){
                    id = actorNombre.id;
                    break;
                }
            }
            actor = actores[id];
        }else { // si el personaje elegido por el jugador es una chica o es el superheroe obtengo el actor con el id
            actor = actores[mensaje.actor_habla];
        }
        
        // avatar del actor que dice el mensaje
        avatar.sprite = actor.sprite;
        // avatar del superheroe de la escena
        avatarSuperheroe.sprite = superheroePanel[mensaje.superheroe].sprite;
        // imagen de la escena
        if(diapositiva != null){
            diapositiva.sprite = imagenes[mensaje.imagen].sprite;
        }
        
    }

    public void SiguienteMensaje(){
        mensajeActivo++;
        string nombreEscena = SceneManager.GetActiveScene().name;
        
        // si aun hay mensajes por mostrar, continuo
        if(mensajeActivo < mensajes.Count){
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

    private void LeerArchivo(string rutaArchivo){
        mensajes = new List<MensajeCSV>();
        lineas = File.ReadAllLines(rutaArchivo);

        for (int i = 1; i < lineas.Length; i++){
            string[] valores = lineas[i].Split('-');
            if (valores.Length >= 6) {
                int nivelL = int.Parse(valores[0]);
                string escenaL = valores[1];
                int actor_habla = int.Parse(valores[2]);
                int superheroe = int.Parse(valores[3]);
                int imagen = int.Parse(valores[4]);
                string texto = valores[5];

                if(nivelL == nivel && escenaL == nombreEscenaActual){
                    MensajeCSV mensaje = new MensajeCSV(actor_habla, superheroe, imagen, texto);
                    mensajes.Add(mensaje);
                }
            }
        }

    }

    private void LeerArchivo(string rutaArchivo, List<PersonajeCSV> personajes){
        lineas = File.ReadAllLines(rutaArchivo);

        for (int i = 1; i < lineas.Length; i++){
            string[] valores = lineas[i].Split(',');
            if (valores.Length >= 3) {
                // Debug.Log(valores[0] + " - " + valores[1] + " - " + valores[2]);
                int id = int.Parse(valores[0]);
                string name = valores[1];
                string imagenPath = valores[2];
                Sprite sprite = CargarSprite(imagenPath);
                // Debug.Log(sprite);
                PersonajeCSV personaje = new PersonajeCSV(id, name, sprite);
                personajes.Add(personaje);
                
            }
        }

    }

    private Sprite CargarSprite(string path){
        // Cargar un Sprite desde la ruta de archivo en la carpeta Resources
        return Resources.Load<Sprite>(path);
    }
}


[System.Serializable] //mostrar los mensajes en los ajustes
public class MensajeCSV {
    public int actor_habla{ get; set; }
    public int superheroe{ get; set; }
    public int imagen{ get; set; }
    public string texto{ get; set; }

    public MensajeCSV(int actor_habla, int superheroe, int imagen, string texto){
        this.actor_habla = actor_habla;
        this.superheroe = superheroe;
        this.imagen = imagen;
        this.texto = texto;
    }
}



[System.Serializable] //mostrar los actores en los ajustes
public class PersonajeCSV {
    public int id{ get; set; }
    public string name{ get; set; }
    public Sprite sprite{ get; set; }

    public PersonajeCSV(int id, string name, Sprite sprite){
        this.id = id;
        this.name = name;
        this.sprite = sprite;
    }
}




