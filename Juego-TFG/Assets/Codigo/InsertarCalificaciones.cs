using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class InsertarCalificaciones : MonoBehaviour
{
    private GameObject[] slotsPanel;
    public GameObject calificaciones;
    private int numSlots;
    
    
    void Awake() {
        // cuenta el numero de slots de las calificaciones
        numSlots = calificaciones.transform.childCount;
        // crear array 
        slotsPanel = new GameObject[numSlots];
    }
    void Start()
    {   
        
        for(int i = 0; i < slotsPanel.Length; i++){
            slotsPanel[i] = calificaciones.transform.GetChild(i).gameObject;
        }
        Calificaciones();
    }

    private void Calificaciones(){
        GameObject slotPuntuacion;
        if(slotsPanel.Length > 0){
            Debug.Log(slotsPanel.Length);
            for(int i = 0; i < slotsPanel.Length; i++){
                slotPuntuacion = slotsPanel[i];
                TextMeshProUGUI texto = slotPuntuacion.GetComponentInChildren<TextMeshProUGUI>();
                if(texto != null){
                    if(NivelCompletado.CargarNivel(i+1) == 2){
                        texto.text = Convert.ToString(Puntuaciones.CargarPuntuacion(i+1));
                    }else{
                        texto.text = "-";
                    }
                    
                }else{
                    Debug.LogWarning("No se encontró el componente TextMeshPro en el GameObject " + slotPuntuacion.name);
                }
            } 
        }
    }
}
