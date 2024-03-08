using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TirarDado : MonoBehaviour
{

    public Sprite[] dados;
    private GameObject dadoObjecto;
    private Image imagenDado;
    private bool rueda = false;
    private int valorDado;
    public string[] musculos;
    private string dadoSeleccion = "null";

    // Start is called before the first frame update
    void Start()
    {
       imagenDado = GameObject.Find("Dado").GetComponent<Image>();
    }

    
    public void Pulsar(){
        if(!rueda){
            StartCoroutine(Dado());
        }
    }
    IEnumerator Dado(){
        rueda = true;
        int iteraciones = Random.Range(10, 20);
        for(int i = 0; i < iteraciones ; i++){
            int randomIndice = Random.Range(0, dados.Length);
            imagenDado.sprite = dados[randomIndice];
            yield return new WaitForSeconds(0.1f);
        }

        valorDado = Random.Range(0, dados.Length);
        imagenDado.sprite = dados[valorDado];
        rueda = false;
        DadoSeleccionado(valorDado);
        if (!string.Equals(dadoSeleccion, "Movimiento")) {
            Invoke("IrEscenaTest",1f);  
        }else{
            Invoke("IrMovimiento",1f);  
        }
          
    }

    private void IrEscenaTest(){
        SceneManager.LoadScene("TipoTestRuleta");
    }

    private void IrMovimiento(){
        SceneManager.LoadScene("MovimientoRuleta");
    }

    public void DadoSeleccionado(int valor){
        dadoSeleccion = musculos[valor];
        PlayerPrefs.SetString("DadoSeleccion", dadoSeleccion);
        Debug.Log("El musculo seleccionado es: " + dadoSeleccion);
    }
    

    
}
