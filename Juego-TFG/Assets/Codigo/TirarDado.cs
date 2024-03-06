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
        Debug.Log(valorDado);
    }
    

    
}
