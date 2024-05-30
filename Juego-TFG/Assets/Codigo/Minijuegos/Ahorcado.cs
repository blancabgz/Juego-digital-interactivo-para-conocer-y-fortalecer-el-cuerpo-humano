using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Ahorcado : Minijuego {

    private char[] letras;
    // numero de intentos para acertar la palabra
    public char[] palabraDividida;
    public int letrasPalabraDividida;
    public char[] palabraOculta;
    public PalabrasAhorcado[] palabras;


    private GameObject panelTeclado;
    private GameObject panelPalabra;
    private GameObject panelImagenes;
    public GameObject panelVolverIntentar;


    private TextMeshProUGUI textoPalabraOculta;
    public TextMeshProUGUI pista;
    public TextMeshProUGUI intentos;
    private Button[] botones;
    public Image[] imagenesFallo;


    void Awake(){
        ControlMusica.EstadoMusica();
    }
    void Start(){
        base.puntos = 10;
        base.numIntentos = 6;
        // Obtener los componentes GameObject
        panelTeclado = GameObject.Find("Teclado");
        panelPalabra = GameObject.Find("Palabra");
        panelImagenes = GameObject.Find("PanelFallos");

        // Actualizar intentos disponibles en pantalla
        ActualizarIntentos(base.numIntentos);

        // Cargar letras de teclado
        AsignarLetrasDelTeclado();

        // Mezcla los elementos
        Utilidades.MezclarElementos(palabras);

        // Seleccionar palabra oculta
        SeleccionarPalabra();
    }

    /**
     * @brief Asigna las letras al teclado al inicio del juego
     * 
     * Esta función asigna las letras del abecedario al teclado que aparece por pantalla.
     * @note Se asignan con el orden del teclado pc español
     * 
    */
    void AsignarLetrasDelTeclado(){
        // Obtener todos los botones del panel Teclado
        botones = panelTeclado.GetComponentsInChildren<Button>();
        // Incluir en el array de letras el abecedario
        letras = "QWERTYUIOPASDFGHJKLÑZXCVBNM".ToCharArray();

        // Si coinciden el numero de botones disponibles con el numero de letras del abecedario
        if(botones.Length == letras.Length){

            for(int i = 0; i < letras.Length; i++){

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

    /**
     * @brief Selecciona una palabra aleatoria para adivinar
     * 
     * Esta función selecciona una palabra aleatoria del array de palabras 
     *
     * 
    */
    void SeleccionarPalabra(){
        // Verifica que el array de palabras tenga al menos una palabra
        if (palabras == null || palabras.Length == 0) {
            Debug.LogError("No hay palabras para adivinar");
            return;
        }
        // Obtener un indice aleatorio del array
        int indicePalabra = Random.Range(0, palabras.Length);
        
        if (indicePalabra >= palabras.Length) {
            Debug.LogError("Fuera de rango");
            return;
        }

        // Guarda la palabra seleccionada
        string palabraElegida = palabras[indicePalabra].palabra;

        if (string.IsNullOrEmpty(palabraElegida)) {
            Debug.LogError("La palabra elegida es nula");
            return;
        }

        
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

    /**
     * @brief Comprueba si el botón seleccionado coincide con alguna letra de la palabra oculta.
     * 
     * Esta función comprueba si el botón seleccionado por el jugador coincide con alguna letra de la palabra oculta. Si es así, se 
     * desbloquea por pantalla esa letra de la palabra. Si no, no se desbloquea y se decrementa el numero de intentos que tiene 
     * para conseguirlo. Si agota el número de intentos, puede volver a intentar el juego. 
     * Cuando se pulse el botón, se bloquea para no repetir la letra.  
     *
     * @param boton El GameObject del botón seleccionado.
     * @pre El GameObject del botón debe existir y no ser nulo.
     * 
    */

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
                
                textoPalabraOculta.text = new string(palabraOculta);

                // Comprobar si se han adivinado todas las letras
                if(letrasPalabraDividida == 0){
                    base.CalcularPuntuacionFinalIntentos();
                    base.GuardarPuntuacion(2);
                    base.MostrarPanelFinal();
                }
            }else{
                // Obtener la imagen segun el error cometido

                if(imagenesFallo != null || imagenesFallo.Length > 0){
        
                    colorImagen = imagenesFallo[base.numIntentos - 1].color;
                    colorImagen.a = 1f;
                    imagenesFallo[numIntentos - 1].color = colorImagen;

                }

                base.numIntentos--;

                // Actualizar los intentos disponibles en pantalla
                ActualizarIntentos(base.numIntentos);

                // Comprobar si se han agotado los intentos
                if(base.numIntentos == 0){
                    panelVolverIntentar.SetActive(true);

                }
            }
        }else{
            Debug.LogError("El componente Text no ha sido encontrado");
            return; 
        }
    }

    /**
     * @brief Actualiza el numero de intentos por pantalla 
     * 
     * Función utilizada para actualizar en el panel vista el numero de intentos que tiene el usuario
     * 
     * @param intentos Numero de intentos actuales
     * 
    */
    
    private void ActualizarIntentos(int intentosNum){
        intentos.text = intentosNum.ToString();
    }

    [System.Serializable]
    public class PalabrasAhorcado{
        public string palabra;
        public string definicion;
    }
}
