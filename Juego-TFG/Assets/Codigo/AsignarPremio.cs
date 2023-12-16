using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsignarPremio : MonoBehaviour
{
    public GameObject imagenPremio;
    public ObjetoInventario[] premios;
    private int nivelActual;
    
    void Awake()
    {    
        nivelActual = PlayerPrefs.GetInt("Nivel", 1);
        Debug.Log(nivelActual);
        Debug.Log(ControladorNiveles.instancia.ActualLevel());
        // if(nivelActual != ControladorNiveles.instancia.ActualLevel()){
        //     SceneManager.LoadScene("Juego");
        // }
    }
    void Start()
    {   
        Image imagen = imagenPremio.GetComponent<Image>();
        
        imagen.sprite = premios[nivelActual - 1].sprite;
        if(ControladorNiveles.instancia != null){
            ControladorNiveles.instancia.IncreaseLevel();
        }
    }
    
}

    [System.Serializable]
    public class ObjetoInventario
    {
        public int nivel;
        public string nombre;
        public Sprite sprite;
    }



