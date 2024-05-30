using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TirarDado : MonoBehaviour
{
    public int nivel;
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
    private int puntuacion = 10;
    public GameObject panelFinal;
    public AudioSource audioSource;

    void Awake(){
        GameObject controlMusica = GameObject.Find("ControlMusica");
        if(PlayerPrefs.GetString("estadoMusica", "null") == "OFF"){
            if(controlMusica != null){
                Destroy(controlMusica);
            }   
        }

        CargarPuntuacion();
        CargarContadorMedallas();
        GuardarContadorMedallas();
        SubirIntensidadImagen();
    }

    
    void Start(){
        if(audioSource != null){
            audioSource.Stop();
            audioSource.enabled = false;
        }
        imagenDado = GameObject.Find("Dado").GetComponent<Image>();
        tituloDado = GameObject.Find("TituloDado").transform.GetComponent<TextMeshProUGUI>();
    }


    public void Pulsar(){
        if(!rueda){
            // Quito un punto por tirar al dado
            puntuacion--;
            PlayerPrefs.SetInt("Puntuacion", puntuacion);
            CargarPuntuacion();
            StartCoroutine(Dado());
            if(PlayerPrefs.GetString("estadoMusica", "null") == "ON"){
                if (audioSource != null){
                    audioSource.enabled = true;
                    audioSource.Play();
                }
            }
        }
    }
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

    private void IrEscenaTest(){
        Utilidades.EscenaJuego("TipoTestRuleta");
    }

    private void IrMovimiento(){
        Utilidades.EscenaJuego("MovimientoRuleta");
    }

    public void DadoSeleccionado(int valor){
        dadoSeleccion = musculos[valor];
        PlayerPrefs.SetString("DadoSeleccion", dadoSeleccion);
        
    }

    private void CargarPuntuacion(){
        if (PlayerPrefs.HasKey("Puntuacion")){
            puntuacion = PlayerPrefs.GetInt("Puntuacion");
        }
    }

    private void CargarContadorMedallas(){
        if (PlayerPrefs.HasKey("ContadorMedallas")){
            contadorMedallas = PlayerPrefs.GetInt("ContadorMedallas");
            
        }

        if(panelFinal != null){
            if(contadorMedallas >= 3){
                // A la puntuacion obtenida le sumo los puntos ganados 
                puntuacion += contadorMedallas;
                // Compruebo que la puntuacion es mayor de 0
                if(puntuacion < 0){
                    puntuacion = 0;
                }
                // Guardo la puntuacion del nivel
                Puntuaciones.GuardarPuntuacion(nivel, puntuacion);
                // Guardo nivel superado
                NivelCompletado.GuardarNivel(nivel,2);

                panelFinal.SetActive(true);
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
    }


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

    private void GuardarContadorMedallas(){
        if(contadorMedallas == 0){
            Utilidades.InicializarContadorMedallas();
        }
    }
    
   
    
}
