using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TirarDado : Minijuego
{
    // public int nivel;
    public Sprite[] dados;
    public GameObject[] medallas;
    private GameObject dadoObjecto;
    private Image imagenDado;
    private bool rueda = false;
    private int valorDado;
    public string[] musculos;
    private string dadoSeleccion = "null";
    private TextMeshProUGUI tituloDado;
    public Image medalla;
    public Color medallaSeleccionada;
    private int contadorMedallas = 0;
    public AudioSource audioSource;

    void Awake(){
        ControlMusica.EstadoMusica();
        base.puntos = 10;
        CargarPuntuacion();
        CargarContadorMedallas();
        GuardarContadorMedallas();
        SubirIntensidadImagen();
    }


    /**
    *  @brief Función de inicialización
    * 
    *  Busca y asigna los componentes Image y TextMeshProUGUI de los objetos "Dado" y "TituloDado", respectivamente.
    */
    
    void Start(){
        if(audioSource != null){
            audioSource.Stop();
            audioSource.enabled = false;
        }
        imagenDado = GameObject.Find("Dado").GetComponent<Image>();
        tituloDado = GameObject.Find("TituloDado").transform.GetComponent<TextMeshProUGUI>();
    }

    /**
    *  @brief Función para reproducir el sonido del dado y crear la animación de girar el dado.
    * 
    *  Esta función inicia el giro del dado. Verifica si la música está activada en los ajustes del jugador y, si es así, habilita y reproduce el audio.
    */

    public void Pulsar(){
        if(!rueda){
            StartCoroutine(Dado());
            if(PlayerPrefs.GetString("estadoMusica", "null") == "ON"){
                if (audioSource != null){
                    audioSource.enabled = true;
                    audioSource.Play();
                }
            }
        }
    }

    /**
    *  @brief Función que controla el giro del dado
    * 
    *  Esta función controla el giro del dado y dependiendo de la selección, envía al jugador a la escena que le corresponde. 
    */
    IEnumerator Dado(){
        rueda = true;
        int iteraciones = Random.Range(10, 20);
        for(int i = 0; i < iteraciones ; i++){
            int randomIndice = Random.Range(0, dados.Length);
            imagenDado.sprite = dados[randomIndice];
            tituloDado.text = musculos[randomIndice];
            yield return new WaitForSeconds(0.1f);
        }

        valorDado = Random.Range(0, dados.Length);
        imagenDado.sprite = dados[valorDado];
        tituloDado.text = musculos[valorDado];
        rueda = false;
        DadoSeleccionado(valorDado);
        if (!string.Equals(dadoSeleccion, "Movimiento")) {
            Invoke("IrEscenaTest",1f);
        }else{
            Invoke("IrMovimiento",1f);  
        }
          
    }

    /**
    *  @brief Función que lleva al jugador a la escena Tipo Test
    */

    private void IrEscenaTest(){
        Controlador.EscenaJuego("TipoTestRuleta");
    }

    /**
    *  @brief Función que lleca al jugador a la escena Movimiento
    */

    private void IrMovimiento(){
        Controlador.EscenaJuego("MovimientoRuleta");
    }

    /**
    *  @brief Función que almacena el dado seleccionado para filtrar las preguntas
    */

    public void DadoSeleccionado(int valor){
        dadoSeleccion = musculos[valor];
        PlayerPrefs.SetString("DadoSeleccion", dadoSeleccion);
        
    }

    /**
    *  @brief Función para obtener la puntuación
    */

    private void CargarPuntuacion(){
        if (PlayerPrefs.HasKey("Puntuacion")){
            base.puntos = PlayerPrefs.GetInt("Puntuacion");
        }
    }

    /**
    *  @brief Función para cargar el contador de medallas y verificar finalización del juego
    * 
    *  Esta función verifica si existe una clave para el contador de medallas en las preferencias del jugador.
    *  Si existe, carga el valor. Si el contador de medallas es igual a 3, el juego ha terminado y almacena la puntuación. 
    */

    private void CargarContadorMedallas(){
        if (PlayerPrefs.HasKey("ContadorMedallas")){
            contadorMedallas = PlayerPrefs.GetInt("ContadorMedallas");   
        }

        if(contadorMedallas >= 3){
            // Compruebo que la puntuacion es mayor de 0
            if(base.puntos < 0){
                base.puntos = 0;
            }

            base.GuardarPuntuacion(2);
            base.MostrarPanelFinal();

            // Restablezco los valores
            PlayerPrefs.SetInt("ContadorMedallas", 0);
            PlayerPrefs.SetInt("Puntuacion", 10);

            for(int i = 0; i < contadorMedallas; i++){
                medalla = medallas[i].GetComponent<Image>();
                Color nuevaOpacidad = medalla.color;
                nuevaOpacidad.a = 0.5f;
                medalla.color = nuevaOpacidad;
            }
        }

    }

    /**
    *  @brief Función para aumentar la intensidad de la imagen por medallas desbloqueadas. 
    * 
    *  Esta función verifica el numero de medallas desbloqueadas y aumenta la intensidad de la imagen.  
    */

    private void SubirIntensidadImagen(){
        if(contadorMedallas != 0){
            for(int i = 0; i < contadorMedallas; i++){
                medalla = medallas[i].GetComponent<Image>();
                Color nuevaOpacidad = medalla.color;
                nuevaOpacidad.a = 1f;
                medalla.color = nuevaOpacidad;
                
            }
        }
    }

    /**
    *  @brief Función para inicializar el contador de medallas
    */

    private void GuardarContadorMedallas(){
        if(contadorMedallas == 0){
            base.InicializarContadorMedallas();
        }
    }
    
   
    
}
