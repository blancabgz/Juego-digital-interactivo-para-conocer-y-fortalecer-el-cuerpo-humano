using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Puzzle : MonoBehaviour
{
    public Pregunta[] preguntas;
    public GameObject cajonImagenes;
    public GameObject cajonPuzzle;
    private int[] imagenesGuardadas;

    private Button botonPulsadoImagenes;
    private int posImagen;
    private int posPuzzle;
    

    void Start() {
        MezclarElementos(preguntas);
        ColocarImagenesAleatorias();
        
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
    public void MezclarElementos(Pregunta[] array){
        for(int i = 0; i < array.Length; i++){
            int indiceAleatorio = Random.Range(0, array.Length); // se obtiene un elemento aleatorio
            Pregunta temp = array[indiceAleatorio]; // guarda el valor en una variable temporal 
            array[indiceAleatorio] = array[i]; // lo intercambio
            array[i] = temp; // se intercambia el elemento original en la posicion aleatoria
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
        }else{
            Debug.Log("No");
        }
        
    }
}


    [System.Serializable]
    public class Pregunta
    {
        public int posicion;
        public Sprite sprite;
    }
