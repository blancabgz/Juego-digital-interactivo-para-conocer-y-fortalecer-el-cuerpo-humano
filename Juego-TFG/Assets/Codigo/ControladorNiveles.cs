using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // reference to buttons
using UnityEngine.SceneManagement;

public class ControladorNiveles : MonoBehaviour
{
    public static ControladorNiveles instancia; 
    public Nivel[] levelBottons; // array containing level buttons
    public int unlock; // unlock level
    public int nivelSeleccionado;
    private const string NIVEL_ESTADO = "EstadoNivel_";

    void Awake() { 
        if(instancia == null){
            instancia = this;
        }   
    }


    // La funcion start se encarga de desactivar todos los botones inicialmente y después activar los botones correspondientes
    // a los niveles desbloqueados 
    public void Start(){
        
        if(levelBottons.Length > 0){
            DesactivarBotones();
            for(int i = 0; i < levelBottons.Length; i++){
                if(i < PlayerPrefs.GetInt("unlockedLevels",1)){
                    levelBottons[i].btnNivel.interactable = true;
                }
            }

            for(int i = 0; i < levelBottons.Length; i++){
                levelBottons[i].Inicializar(this);
            }
        }
    }

    // Funcion que desactiva todos los botones
    public void DesactivarBotones(){
        for (int i = 0; i < levelBottons.Length; i++){
            levelBottons[i].btnNivel.interactable = false;
        }

    }

    // Funcion para aumentar un nivel superado y guardarlo
    public void AumentarNivel(){
        if (unlock > PlayerPrefs.GetInt("unlockedLevels",1))
        {
            PlayerPrefs.SetInt("unlockedLevels",unlock); 
        }
    }

    // Funcion para obtener el nivel actual
    public int NivelActual(){
        return PlayerPrefs.GetInt("unlockedLevels",1);
    }


    // estado = 0 --> nivel no completado
    // estado = 1 --> pantalla 1 completada
    // estado = 2 --> pantalla 2 completada
    // Funcion para guardar el nivel y su estado

    public static void GuardarNivel(int nivel, int estado){
        string estado_nivel = NIVEL_ESTADO + nivel.ToString();
        PlayerPrefs.SetInt(estado_nivel, estado);
        PlayerPrefs.Save();
    }

    // Funcion para cargar el estado de un nivel
    public static int CargarNivel(int nivel){
        string estado_nivel = NIVEL_ESTADO + nivel.ToString();
        return(PlayerPrefs.GetInt(estado_nivel, 0)); // Si no hay valor guardado, devuelve 0
    }

    // Funcion para cargar la escena 

    public void CargarEscena(int nivel, string escena) {
        PlayerPrefs.SetInt("nivelSeleccionado", (nivel)); // Guardar el nivel seleccionado
        PlayerPrefs.Save();
        SceneManager.LoadScene(escena); // Cargar la escena
    }

    // Funcion que gestiona la ventana de juego que se muestra dependiendo del numero de niveles desbloqueados
    public static void MenuNiveles(){
        if(PlayerPrefs.GetInt("unlockedLevels",1) > 20){
            Controlador.EscenaJuego("Juego2");
        }else{
            Controlador.EscenaJuego("Juego");
        }
    }



    [System.Serializable] 
    public class Nivel {
        public int nivel;
        public Button btnNivel;
        public string escena;
        
        public void Inicializar(ControladorNiveles controlador) {
            if (btnNivel != null){
                btnNivel.onClick.AddListener(() => controlador.CargarEscena(nivel, escena));
            }
        }
    }
}

