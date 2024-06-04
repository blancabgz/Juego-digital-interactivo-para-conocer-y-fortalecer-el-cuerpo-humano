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

    private void Awake() { // only one scene
        if(instancia == null){
            instancia = this;
        }   
    }


    // Start is called before the first frame update
    void Start(){
        
        if(levelBottons.Length > 0){

            // disable interaction on all buttons
            for (int i = 0; i < levelBottons.Length; i++){
                levelBottons[i].btnNivel.interactable = false;
            }

            // Unlock levels up to the last level unlocked by the player
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

    // Increase unlocked level
    public void AumentarNivel(){
        if (unlock > PlayerPrefs.GetInt("unlockedLevels",1))
        {
            PlayerPrefs.SetInt("unlockedLevels",unlock); // Save the value of the unlocked levels
        }
    }

    public int NivelActual(){
        return PlayerPrefs.GetInt("unlockedLevels",1);
    }


    // estado = 0 --> nivel no completado
    // estado = 1 --> pantalla 1
    // estado = 2 --> pantalla 2

    public static void GuardarNivel(int nivel, int estado){
        string estado_nivel = NIVEL_ESTADO + nivel.ToString();
        PlayerPrefs.SetInt(estado_nivel, estado);
        PlayerPrefs.Save();
    }

    public static int CargarNivel(int nivel){
        string estado_nivel = NIVEL_ESTADO + nivel.ToString();
        return(PlayerPrefs.GetInt(estado_nivel, 0)); // Si no hay valor guardado, devuelve 0
    }

    public void CargarEscena(int nivel, string escena) {
        PlayerPrefs.SetInt("nivelSeleccionado", (nivel)); // Guardar el nivel seleccionado
        PlayerPrefs.Save();
        SceneManager.LoadScene(escena); // Cargar la escena
    }

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
            btnNivel.onClick.AddListener(() => controlador.CargarEscena(nivel, escena));
        }
    }
}

