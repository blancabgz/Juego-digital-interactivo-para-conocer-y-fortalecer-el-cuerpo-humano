using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreguntasRespuestas : MonoBehaviour
{
    public Respuestas[] respuestas;
    public Image resp1;
    public Image resp2;
    public Image resp3;

    void Start(){
        MezclarElementos(respuestas);
        resp1.sprite = respuestas[0].imagen;
        resp2.sprite = respuestas[1].imagen;
        resp3.sprite = respuestas[2].imagen;
    }

    
    public void MezclarElementos(Respuestas[] array){
        for(int i = 0; i < array.Length; i++){
            int indiceAleatorio = Random.Range(0, array.Length); // se obtiene un elemento aleatorio
            Respuestas temp = array[indiceAleatorio]; // guarda el valor en una variable temporal 
            array[indiceAleatorio] = array[i]; // lo intercambio
            array[i] = temp; // se intercambia el elemento original en la posicion aleatoria
        }
    }
    
}


[System.Serializable] //mostrar los mensajes en los ajustes
public class Respuestas {
    public int id;
    public string musculo;
    public Sprite imagen;
    
}
