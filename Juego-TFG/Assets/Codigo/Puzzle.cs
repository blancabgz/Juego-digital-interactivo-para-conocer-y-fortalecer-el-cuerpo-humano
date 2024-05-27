using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Puzzle : MonoBehaviour
{
    public int nivel;
    public Pregunta[] preguntas;
    public GameObject cajonImagenes;
    public GameObject cajonPuzzle;
    public GameObject puzzleCompletado;
    public GameObject btnCambiarEscena;
    private int[] imagenesGuardadas;

    private Button botonPulsadoImagenes;
    private int posImagen;
    private int posPuzzle;
    private int imagenesCorrectas = 0;

    private int numFallos;
    private static int MAX_FALLOS = 66;


    void Awake(){
        GameObject controlMusica = GameObject.Find("ControlMusica");
        if(PlayerPrefs.GetString("estadoMusica", "null") == "OFF"){
            if(controlMusica != null){
                Destroy(controlMusica);
            }   
        }
    }
    

    void Start() {
        Utilidades.MezclarElementos(preguntas);
        ColocarImagenesAleatorias();
        numFallos = 0;
    }

    public void ColocarImagenesAleatorias(){
        Image imagen;
        imagenesGuardadas  = new int [cajonImagenes.transform.childCount];
        for (int i = 0; i < cajonImagenes.transform.childCount; i++){
            imagen = cajonImagenes.transform.GetChild(i).GetComponent<Image>();
            if (imagen != null) {
                imagen.sprite = preguntas[i].sprite;
                imagenesGuardadas[i] = preguntas[i].posicion;
            }
        }
    }

    public void BotonPulsado(Button boton){

        // obtenemos todos los objetos que tienen el nombre "Borde"
        GameObject[] bordes = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == "Borde").ToArray();
        // desactivamos los bordes
        foreach (GameObject borde in bordes)
        {
            borde.SetActive(false);
        }
        // activar el borde del boton
        boton.transform.Find("Borde").gameObject.SetActive(true);
        botonPulsadoImagenes = boton;

    }

    public void PosicionImagen(int posicion){
        posImagen = posicion;
    }

    public void Comprobar(int posicion){
        posPuzzle = posicion;
        Image imagen;
        if(posPuzzle == imagenesGuardadas[posImagen]){
            imagen = cajonPuzzle.transform.GetChild(posPuzzle).GetComponent<Image>();
            imagen.sprite = preguntas[posImagen].sprite;
            botonPulsadoImagenes.transform.Find("Borde").gameObject.SetActive(false);
            botonPulsadoImagenes.interactable = false;
            
            //imagen correcta
            imagenesCorrectas++;

            // comprobamos si se ha terminado el puzzle
            if (imagenesCorrectas == preguntas.Length)
            {
                CalcularPuntuacion();
                if(puzzleCompletado != null){
                    puzzleCompletado.SetActive(true);
                    btnCambiarEscena.SetActive(true);
                }
                
            }
        }else{
            numFallos++;
        }
    }

    public void CalcularPuntuacion(){
        int puntuacionFinal = Utilidades.CalcularPuntuacionProporcion(numFallos, MAX_FALLOS);
        NivelCompletado.GuardarNivel(nivel,2);
        Puntuaciones.GuardarPuntuacion(nivel, puntuacionFinal);
    }

}


    [System.Serializable]
    public class Pregunta
    {
        public int posicion;
        public Sprite sprite;
    }
