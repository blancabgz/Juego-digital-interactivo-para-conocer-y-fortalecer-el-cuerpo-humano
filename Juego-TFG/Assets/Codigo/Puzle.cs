using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Puzle : Minijuego{
    private int posImagen;
    private int posPuzzle;
    private int imagenesCorrectas = 0;
    private int[] imagenesGuardadas;

    public List<Pieza> piezas;
    private string[] lineas;

    public GameObject cajonImagenes;
    public GameObject cajonPuzzle;
    public GameObject btnCambiarEscena;
    private Button botonPulsadoImagenes;

    private static int MAX_FALLOS = 66;


    void Awake(){
        ControlMusica.EstadoMusica();
        if (base.nivel == 2){
            LeerArchivo("Assets/Codigo/Datos/puzlenivel2.csv");
        }else if(base.nivel == 4){
            LeerArchivo("Assets/Codigo/Datos/puzlenivel4.csv");
        }
        
    }
    

    void Start() {
        base.MezclarElementos(piezas);
        ColocarImagenesAleatorias();
        base.numFallos = 0;
    }

    public void ColocarImagenesAleatorias(){
        Image imagen;
        imagenesGuardadas  = new int [cajonImagenes.transform.childCount];
        for (int i = 0; i < cajonImagenes.transform.childCount; i++){
            imagen = cajonImagenes.transform.GetChild(i).GetComponent<Image>();
            if (imagen != null) {
                imagen.sprite = piezas[i].sprite;
                imagenesGuardadas[i] = piezas[i].posicion;
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
            imagen.sprite = piezas[posImagen].sprite;
            botonPulsadoImagenes.transform.Find("Borde").gameObject.SetActive(false);
            botonPulsadoImagenes.interactable = false;
            
            //imagen correcta
            imagenesCorrectas++;

            // comprobamos si se ha terminado el puzzle
            if (imagenesCorrectas == piezas.Count){
                CalcularPuntuacion();
                base.MostrarPanelFinal();
                btnCambiarEscena.SetActive(true);   
            }
        }else{
            base.AumentarNumeroFallos();
        }
    }

    public void CalcularPuntuacion(){
        base.puntos = base.CalcularPuntuacionProporcion(MAX_FALLOS);
        base.GuardarPuntuacion(2);
    }

    private void LeerArchivo(string rutaArchivo){
        piezas = new List<Pieza>();
        lineas = File.ReadAllLines(rutaArchivo);
        for (int i = 1; i < lineas.Length; i++){
            string[] valores = lineas[i].Split(',');
            if(valores.Length >= 2){
                int posicion = int.Parse(valores[0]);
                string archivoName = valores[1];
                string spriteName = valores[2];
                Sprite sprite = base.CargarSprite(archivoName, spriteName);
                if(sprite != null){
                    Pieza pieza = new Pieza(posicion, sprite);
                    piezas.Add(pieza);
                }

            }
        }
    }
}


    [System.Serializable]
    public class Pieza
    {
        public int posicion { get; set; }
        public Sprite sprite { get; set; }

        public Pieza(int posicion, Sprite sprite){
            this.posicion = posicion;
            this.sprite = sprite;
        }
    }
