using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Inventario : MonoBehaviour
{   

    // numero de slots en escena
    private int numSlots; 
    //hueco del inventario
    public GameObject[] slots;
    // contenedor del inventario de los slots
    public GameObject inventario;
    public ObjetoInventario[] premios;
    

    void Awake(){
        ControlMusica.EstadoMusica();
    }

    void Start(){
        // cuenta el numero de slots del inventario
        numSlots = inventario.transform.childCount;
        // crear un array de slots 
        slots = new GameObject[numSlots];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = inventario.transform.GetChild(i).gameObject;
            Image imagenSlot = slots[i].GetComponent<Image>();
            imagenSlot.sprite = premios[i].sprite;

            // si existe la instancia ControladorNiveles y el nivel actual es mas alto que alguna medalla, se desbloquea esa medalla aumentando su opacidad
            // -1 porque se desbloquea hasta el nivel pero esta completado hasta el anterior
            if(ControladorNiveles.instancia != null && (ControladorNiveles.instancia.ActualLevel() - 1) >= premios[i].nivel ){
                // Como el color es de solo lectura, hay que hacer un cambio creando una nueva 
                // instancia
                Color nuevaOpacidad = imagenSlot.color;
                nuevaOpacidad.a = 1f;
                imagenSlot.color = nuevaOpacidad;
            }
        }
    }


}




