using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Utilidades
{
    public static void MezclarElementos<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int indiceAleatorio = Random.Range(0, array.Length);
            T temp = array[indiceAleatorio];
            array[indiceAleatorio] = array[i];
            array[i] = temp;
        }
    }

    public static void EscenaJuego(string opcion)
    {
        SceneManager.LoadScene(opcion);
    }

    public static void InicializarContadorMedallas(){
        PlayerPrefs.SetInt("ContadorMedallas", 0);
    }

    
}