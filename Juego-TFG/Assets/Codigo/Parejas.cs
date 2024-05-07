using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Parejas : MonoBehaviour
{
    public int nivel;
    public Carta[] cartas;
    private GameObject mazoCartas;

    private GameObject[] objetocartas;

    private GameObject seleccion1 = null;
    private GameObject seleccion2 = null;

    private int poseleccion1;
    private int poseleccion2;
    private int cartasCorrectasEncontradas = 0;
    public GameObject panelFinal;
    private int numFallos;
    private const int MAX_FALLOS = 20;




    void Start()
    {
        // Mezclamos los elementos del array
        Utilidades.MezclarElementos(cartas);
        // Obtenemos el GameObject Cartas
        mazoCartas = GameObject.Find("Cartas");
        ColocarCartasAleatorias();

    }

    // Funcion para colocar las cartas con los músculos de forma aleatoria
    public void ColocarCartasAleatorias(){
        // Obtenemos el objeto cartas
        objetocartas = new GameObject[mazoCartas.transform.childCount];
        Image imagenCarta;
        if(objetocartas != null){
            for(int i = 0; i < objetocartas.Length; i++){
                // Almacenamos todos los objetos carta
                objetocartas[i] = mazoCartas.transform.GetChild(i).gameObject;
            }
        }

        for(int i = 0; i < objetocartas.Length; i++){
            if(i < cartas.Length){
                // Colocamos la imagen
                imagenCarta = objetocartas[i].GetComponent<Image>();
                if(imagenCarta != null){
                    imagenCarta.sprite = cartas[i].sprite;
                }

            }
        }
        
    }


    // Función que gestiona el evento de pulsar una carta.
    // Desactiva la carta seleccionada para mostrar su reverso, y la almacena dependiendo de si ya hay otra carta seleccionada o no.
    // Si hay otra carta seleccionada, se comprueba si ambas son iguales. Si lo son, las deja visibles; si no, las vuelve a ocultar.
    // @param {int} posicion La posición de la carta seleccionada. 
    public void CartaPulsada(int posicion){
        
        // Desactiva la carta pulsada para mostrar su contenido
        DesActCarta(objetocartas[posicion], false);

        // Comprueba si la segunda carta no ha sido seleccionada
        if(seleccion1 != null && seleccion2 == null){
            // Almacena la posicion del objeto carta
            seleccion2 = objetocartas[posicion];
            // Almacena la posicion de la segunda carta
            poseleccion2 = posicion;
            // Comprobacion si ambas cartas son iguales
            if(ComprobarCartas(poseleccion1, poseleccion2)){
                // Restablecer las variables
                poseleccion1 = -1;
                poseleccion2 = -1;
                seleccion1 = null;
                seleccion2 = null;
                ComprobarFinNivel();
                // Salir de la función si se encontraron dos cartas iguales
                return; 
            } else {
                // Esperas 2s antes de ocultar las cartas
                Invoke("OcultarCartas", 2f);
                numFallos++;
                BloquearInteraccionCartas(true);
            }
            
        }

        // Comprueba si se ha seleccionado la primera carta
        if(seleccion1 == null){
            // Almacenar el objeto 
            seleccion1 = objetocartas[posicion];
            // Almacenar la posicion
            poseleccion1 = posicion;
        }
        
        
    }

    // Función para comprobar si las cartas seleccionadas son iguales.
    // @param {int} carta1 La posición de la primera carta seleccionada.
    // @param {int} carta2 La posición de la segunda carta seleccionada.
    // @returns Devuelve true si las cartas son iguales, de lo contrario devuelve false.
    private bool ComprobarCartas(int carta1, int carta2){
        if(cartas[carta1].musculo == cartas[carta2].musculo){
            cartasCorrectasEncontradas += 2;
            return true;
        }

        return false;
    }

    // Funcion para ocultar los objetos cartas seleccionados cuando no coincidan
    private void OcultarCartas(){
        DesActCarta(seleccion1, true);
        DesActCarta(seleccion2, true);
        seleccion1 = null;
        seleccion2 = null;
        BloquearInteraccionCartas(false);
    }

    // Funcion para mostrar u ocultar la carta con la imagen del musculo
    // @param {GameObject} carta Objecto carta seleccionado
    // @param {bool} modo False: muestra la carta oculta, True: muestra la carta ?
    private void DesActCarta(GameObject carta, bool modo){
        // Encontrar el GameObject "Visible"
        GameObject visible = carta.transform.Find("Visible")?.gameObject;
        if (visible != null)
        {
            // Desactivar el GameObject "Visible"
            visible.SetActive(modo);
        }
    }

    private void BloquearInteraccionCartas(bool bloquear){
        foreach (GameObject carta in objetocartas)
        {
            Button button = carta.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = !bloquear; // Desactivar la interacción si bloquear es true
            }
        }
    }

    private bool ComprobarFinNivel(){
        if(cartasCorrectasEncontradas == cartas.Length){
            // Calcular puntuacion segun el numero de fallos cometidos
            int puntuacionFinal = Utilidades.CalcularPuntuacionProporcion(numFallos, MAX_FALLOS);

            NivelCompletado.GuardarNivel(nivel,2);
            Puntuaciones.GuardarPuntuacion(nivel, puntuacionFinal);
            // Activa el panel final
            if (panelFinal != null) {
                panelFinal.SetActive(true);
            }
            return true;
        }

        return false;
    }


    [System.Serializable]
    public class Carta
    {
        public string musculo;
        public Sprite sprite;
    }


}
