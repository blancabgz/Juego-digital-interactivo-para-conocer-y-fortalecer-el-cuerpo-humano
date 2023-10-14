using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // hacer referencia a los botones

public class ControladorNiveles : MonoBehaviour
{
    public static ControladorNiveles instancia; // para llamarlo facilmente desde otros codigos
    public Button[] botonesNivel; // array que contiene los botones de nivel
    public int desbloquear; 

    private void Awake() { // si no encontramos la instancia, solo debe haber una por escena, me creas esta
        if(instancia == null){
            instancia = this;
        }   
    }


    // Start is called before the first frame update
    void Start()
    {
        if(botonesNivel.Length > 0){

            // se recorren todos los botones de niveles y se desactiva la propiedad de interaccion evitando poder pulsarlo hasta que se desbloquee
            for (int i = 0; i < botonesNivel.Length; i++)
            {
                botonesNivel[i].interactable = false;
            }

            // solo el nivel 1 se desbloquea en esta ocasion
            for (int i = 0; i < PlayerPrefs.GetInt("nivelesDesbloqueados",1); i++)
            {
                botonesNivel[i].interactable = true;   
            }
        }
    }

    // solo desbloquear niveles, cuando es mayor al numero que tengo guardado
    public void AumentarNivel(){
        if (desbloquear > PlayerPrefs.GetInt("nivelesDesbloqueados",1))
        {
            PlayerPrefs.SetInt("nivelesDesbloqueados",desbloquear); // guardo el valor de los niveles desbloqueados
        }
    }
}
