using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Ahorcado : MonoBehaviour
{

    public int nivel;
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
    private int puntos;
    public PalabrasAhorcado[] palabras;
    public char[] palabraDividida;
    public int letrasPalabraDividida;
    public char[] palabraOculta;
    private TextMeshProUGUI textoPalabraOculta;
    public TextMeshProUGUI pista;
    public TextMeshProUGUI intentos;
    void Start()
    {   
        // Obtener los componentes GameObject
        panelTeclado = GameObject.Find("Teclado");
        panelPalabra = GameObject.Find("Palabra");
        panelImagenes = GameObject.Find("PanelFallos");
        // Actualizar intentos disponibles en pantalla
        intentos.text = numIntentos.ToString();
        // Cargar letras de teclado
        AsignarLetrasDelTeclado();
        // Mezcla los elementos
        Utilidades.MezclarElementos(palabras);
        // Seleccionar palabra oculta
        SeleccionarPalabra();
        puntos = 10;
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
        palabraDividida = new char[palabraElegida.Length];
        palabraOculta = new char[palabraElegida.Length];

        // Divide la palabra en letras y rellena el array 
        for(int i = 0; i < palabraElegida.Length; i++){
            palabraDividida[i] = palabraElegida[i];
            palabraOculta[i] = '_';
        }

        // Verifica que el componente no es nulo y que el indice esté dentro del rango
        if(pista != null && indicePalabra > 0){
            // Muestra la definicion de la palabra seleccionada
            pista.text = palabras[indicePalabra].definicion;
            textoPalabraOculta.text = new string(palabraOculta);
        }
    }

    public void Comprobar(GameObject boton){
        Color colorImagen;
        // Comprobar que el boton seleccionado no es nulo
        if(boton == null){
            Debug.LogError("El GameObject no ha sido encontrado");
            return;
        }

        // Obtener el componente TextMeshProUGUI del botón
        TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();
        if (textoBoton != null){
            // Obtener la letra asociada al boton
            char letra = textoBoton.text[0];
            // Variable para controlar si se ha encontrado una letra en la palabra oculta
            bool letraEncontrada = false;
            // desactivamos el boton para no volver a repetir letra
            boton.GetComponentInChildren<Button>().interactable = false;

            // Iterar sobre la palabra para comprobar si la letra seleccionada se encuentra 
            for(int i = 0; i < palabraDividida.Length; i++){
                // Si se encuentra la letra
                if(palabraDividida[i] == letra){
                    // Marcar la letra como encontrada
                    letrasPalabraDividida--;
                    // Actualizar la palabra oculta con la letra encontrada
                    palabraOculta[i] = letra;
                    letraEncontrada = true;
                }
            }

            // Si se ha encontrado una letra oculta
            if(letraEncontrada){
                // Volver a cargar la palabra 
                textoPalabraOculta.text = new string(palabraOculta);
                // Comprobar si se han adivinado todas las letras
                if(letrasPalabraDividida == 0){
                    // Calcular los puntos en función del número de intentos
                    puntos -= (6 - numIntentos) * 2;  
                    // Nivel completado
                    NivelCompletado.GuardarNivel(nivel,2);
                    // Guardo la media de los puntos conseguidos en ambas pantallas
                    Puntuaciones.GuardarPuntuacion(nivel, puntos);
                    // Activo el panel final
                    panelFinal.SetActive(true);
                }
            }else{
                if(imagenesFallo != null || imagenesFallo.Length > 0){
                    // Obtener la imagen segun el error cometido
                    colorImagen = imagenesFallo[numIntentos - 1].color;
                    colorImagen.a = 1f;
                    imagenesFallo[numIntentos - 1].color = colorImagen;
                }
                //Actualizar el numero de intentos
                numIntentos--;
                // Actualizar los intentos disponibles en pantalla
                intentos.text = numIntentos.ToString();
                // Comprobar si se han agotado los intentos
                if(numIntentos == 0){
                    panelVolverIntentar.SetActive(true);

                }
            }
        }else{
            Debug.LogError("El componente Text no ha sido encontrado");
            return; 
        }
    }


    [System.Serializable]
    public class PalabrasAhorcado
    {
        public string palabra;
        public string definicion;
    }
}
