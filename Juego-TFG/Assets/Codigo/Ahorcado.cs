using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Ahorcado : MonoBehaviour
{
    private GameObject panelTeclado;
    private GameObject panelPalabra;
    private GameObject panelImagenes;
    public GameObject panelFinal;
    public GameObject panelVolverIntentar;

    private char[] letras;
    private Button[] botones;
    public Image[] imagenesFallo;
    // numero de intentos para acertar la palabra
    private int numIntentos = 6;
    public PalabrasAhorcado[] palabras;
    public char[] palabraDividida;
    public int letrasPalabraDividida;
    public char[] palabraOculta;
    private TextMeshProUGUI textoPalabraOculta;
    public TextMeshProUGUI pista;
    public TextMeshProUGUI intentos;
    void Start()
    {   
        panelTeclado = GameObject.Find("Teclado");
        panelPalabra = GameObject.Find("Palabra");
        panelImagenes = GameObject.Find("PanelFallos");
        intentos.text = numIntentos.ToString();

    

        AsignarLetrasDelTeclado();
        // Mezcla los elementos
        Utilidades.MezclarElementos(palabras);
        SeleccionarPalabra();
    }

    void AsignarLetrasDelTeclado(){
        // Obtener todos los botones del panel Teclado
        botones = panelTeclado.GetComponentsInChildren<Button>();
        // Incluir en el array de letras el abecedario
        letras = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToCharArray();

        // si coinciden el numero de botones disponibles con el numero de letras del abecedario
        if(botones.Length == letras.Length){
            for(int i = 0; i < letras.Length; i++){
                // Obtener el componente TextMeshProUGUI del boton
                TextMeshProUGUI textoBoton = botones[i].GetComponentInChildren<TextMeshProUGUI>();
                if(textoBoton != null){
                    // Asignar al botón una letra del abecedario
                    textoBoton.text = letras[i].ToString();
                }else{
                    Debug.LogWarning("El boton no contiene componente TextMeshProUGUI");
                }
            }
        }else{
            Debug.LogWarning("Faltan botones para asignar las letras del abecedario");
        }
    }

    void SeleccionarPalabra(){
        // Verifica que el array de palabras tenga al menos una palabra
        if (palabras == null || palabras.Length == 0) {
            Debug.LogError("No hay palabras para adivinar");
            return;
        }
        // Obtener un indice aleatorio del array
        int indicePalabra = Random.Range(0, palabras.Length);
        // Comprueba que el indice obtenido este dentro del rango 
        if (indicePalabra >= palabras.Length) {
            Debug.LogError("Fuera de rango");
            return;
        }
        // Guarda la palabra seleccionada
        string palabraElegida = palabras[indicePalabra].palabra;
        // Comprueba que la palabra seleccionada no este vacia o sea nula
        if (string.IsNullOrEmpty(palabraElegida)) {
            Debug.LogError("La palabra elegida es nula");
            return;
        }

        // Obtiene el componente TextMeshProUGUI
        textoPalabraOculta = panelPalabra.GetComponentInChildren<TextMeshProUGUI>();
        if (textoPalabraOculta == null) {
            Debug.LogError("No se encontró el componente");
            return;
        }

        // Inicializo los array con la longitud de la palabra seleccionada
        letrasPalabraDividida = palabraElegida.Length;
        Debug.Log(letrasPalabraDividida);
        palabraDividida = new char[palabraElegida.Length];
        palabraOculta = new char[palabraElegida.Length];

        // Divide la palabra en letras y rellena el array 
        for(int i = 0; i < palabraElegida.Length; i++){
            palabraDividida[i] = palabraElegida[i];
            Debug.Log(palabraDividida[i]);
            palabraOculta[i] = '_';
            Debug.Log(palabraOculta[i]);
        }

        // Verifica que el componente no es nulo y que el indice esté dentro del rango
        if(pista != null && indicePalabra > 0){
            // Muestra la definicion de la palabra seleccionada
            pista.text = palabras[indicePalabra].definicion;
            textoPalabraOculta.text = new string(palabraOculta);
        }
    }

    public void Comprobar(GameObject boton){
        // Obtener el texto del botón que ha sido presionado
        TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();
        Color colorImagen;
        if (textoBoton != null){
            char letra = textoBoton.text[0];
            
            bool letraEncontrada = false;

            for(int i = 0; i < palabraDividida.Length; i++){
                if(palabraDividida[i] == letra){
                    // FALTA CONTROLAR QUE ESTO SOLO LO HAGA CUANDO NO HAYA REPETIDO YA LA LETRA
                    letrasPalabraDividida--;
                    palabraOculta[i] = letra;
                    letraEncontrada = true;
                }
            }

            if(letraEncontrada){
                textoPalabraOculta.text = new string(palabraOculta);
                if(letrasPalabraDividida == 0){
                    panelFinal.SetActive(true);
                }
            }else{
                if(imagenesFallo != null){
                    colorImagen = imagenesFallo[numIntentos - 1].color;
                    colorImagen.a = 1f;
                    imagenesFallo[numIntentos - 1].color = colorImagen;
                }
                numIntentos--;
                intentos.text = numIntentos.ToString();
                if(numIntentos == 0){
                    panelVolverIntentar.SetActive(true);

                }
            }
            
        }
    }


    [System.Serializable]
    public class PalabrasAhorcado
    {
        public string palabra;
        public string definicion;
    }
}
