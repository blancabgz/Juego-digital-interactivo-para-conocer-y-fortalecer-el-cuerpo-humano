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
    

    void Start() {
        MezclarElementos(preguntas);
        ColocarImagenesAleatorias();
        
    }

    public void ColocarImagenesAleatorias(){
        Image imagen;
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

    public void BotonPulsado(Button botonPulsado){

        // obtenemos todos los objetos que tienen el nombre "Borde"
        GameObject[] bordes = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == "Borde").ToArray();
        // desactivamos los bordes
        foreach (GameObject borde in bordes)
        {
            borde.SetActive(false);
        }
        // activar el borde del boton
        botonPulsado.transform.Find("Borde").gameObject.SetActive(true); 
    }

    public void PosicionBotonPulsado(int posicion){
        Debug.Log(posicion);
    }

    public void Comprobar(int posicion){
        Debug.Log(posicion);
        
    }
}


    [System.Serializable]
    public class Pregunta
    {
        public int posicion;
        public Sprite sprite;
    }
