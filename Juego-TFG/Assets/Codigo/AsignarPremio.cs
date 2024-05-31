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
    }
    void Start()
    {   
        Image imagen = imagenPremio.GetComponent<Image>();
        
        imagen.sprite = premios[nivelActual - 1].sprite;
        if(ControladorNiveles.instancia != null){
            ControladorNiveles.instancia.IncreaseLevel();
        }
    }

    /**
     * @brief Metodo para cargar el menú de niveles según desbloqueado
    */
    public void IrMenu(){
        Controlador.MenuNiveles();
    }
    
}

    [System.Serializable]
    public class ObjetoInventario
    {
        public int nivel;
        public string nombre;
        public Sprite sprite;
    }



