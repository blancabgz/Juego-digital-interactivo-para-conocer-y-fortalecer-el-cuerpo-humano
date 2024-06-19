using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
public class ControladorRecompensas : MonoBehaviour
{

    // numero de slots en escena
    private int numSlots;
    //hueco del inventario
    public GameObject[] slots;
    // contenedor del inventario de los slots
    public GameObject inventario;
    public List<Recompensa> recompensas;
    public List<Recompensa> recompensasEscena;
    string nombreEscenaActual;
    private string[] lineas;
    public GameObject imagenPremio;
    private int nivelActual;


    void Awake()
    {
        ControlMusica.EstadoMusica();
        LeerArchivo("Datos/recompensas");
        nombreEscenaActual = SceneManager.GetActiveScene().name;
        // Debug.Log("El nombre de la escena actual es: " + nombreEscenaActual);
    }

    void Start()
    {

        if (imagenPremio)
        {
            AsignarRecomenpensaNivel();
        }

        if (nombreEscenaActual == "Medallas")
        {
            // Filtramos por el nombre de la escena
            recompensasEscena = recompensas.Where(recompensa => recompensa.tipo == "medalla").ToList();
        }
        else if (nombreEscenaActual == "Objetos")
        {
            recompensasEscena = recompensas.Where(recompensa => recompensa.tipo == "objeto").ToList();
        }

        if (inventario != null)
        {
            RellenarPanel();
        }
    }

    private void AsignarRecomenpensaNivel()
    {
        Image imagen = imagenPremio.GetComponent<Image>();
        nivelActual = PlayerPrefs.GetInt("Nivel", 1);
        imagen.sprite = recompensas[nivelActual - 1].sprite;
        if (ControladorNiveles.instancia != null)
        {
            ControladorNiveles.instancia.AumentarNivel();
        }
    }

    private void RellenarPanel()
    {
        // cuenta el numero de slots del inventario
        numSlots = inventario.transform.childCount;
        // crear un array de slots 
        slots = new GameObject[numSlots];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = inventario.transform.GetChild(i).gameObject;
            Image imagenSlot = slots[i].GetComponent<Image>();
            imagenSlot.sprite = recompensasEscena[i].sprite;

            // si existe la instancia ControladorNiveles y el nivel actual es mas alto, se desbloquea aumentando su opacidad
            // -1 porque se desbloquea hasta el nivel pero esta completado hasta el anterior
            if (ControladorNiveles.instancia != null && (ControladorNiveles.instancia.NivelActual() - 1) >= recompensasEscena[i].nivel)
            {
                // Como el color es de solo lectura, hay que hacer un cambio creando una nueva 
                // instancia
                Color nuevaOpacidad = imagenSlot.color;
                nuevaOpacidad.a = 1f;
                imagenSlot.color = nuevaOpacidad;
            }
        }
    }

    private void LeerArchivo(string rutaArchivo)
    {

        recompensas = new List<Recompensa>();
        TextAsset csvFile = Resources.Load(rutaArchivo) as TextAsset;
        lineas = csvFile.text.Split('\n');
        for (int i = 1; i < lineas.Length; i++)
        {
            string[] valores = lineas[i].Split(',');
            if (valores.Length >= 3)
            {

                int nivel = int.Parse(valores[0]);
                string tipo = valores[1];
                string imagenPath = valores[2];
                Sprite sprite = CargarSprite(imagenPath);
                Recompensa recompensa = new Recompensa(nivel, tipo, sprite);
                recompensas.Add(recompensa);
            }
        }
    }

    private Sprite CargarSprite(string path)
    {
        // Cargar un Sprite desde la ruta de archivo en la carpeta Resources
        return Resources.Load<Sprite>(path);
    }
}

[System.Serializable]
public class Recompensa
{
    public int nivel { get; set; }
    public string tipo { get; set; }
    public Sprite sprite { get; set; }

    public Recompensa(int nivel, string nombre, Sprite sprite)
    {
        this.nivel = nivel;
        this.tipo = nombre;
        this.sprite = sprite;
    }

}




