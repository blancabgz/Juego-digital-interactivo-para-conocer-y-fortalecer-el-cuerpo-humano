using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TestTools;

public class LuchaVillanoTest
{
    private TextMeshProUGUI txtpregunta;
    private GameObject pregunta;
    private Image barraVida;
    private Pregunta[] preguntas;
    private LuchaContraElVillano controlador;

    [SetUp]

    public void SetUp(){
        controlador = new GameObject().AddComponent<LuchaContraElVillano>();
        pregunta = new GameObject("Pregunta");
        txtpregunta = pregunta.AddComponent<TextMeshProUGUI>();
        barraVida = new GameObject().AddComponent<Image>();

        controlador.textoPregunta = txtpregunta;
        controlador.barraVida = barraVida;
        controlador.preguntas = new Pregunta[]
        {
            new Pregunta { pregunta = "Pregunta 1", verdadera = true },
            new Pregunta { pregunta = "Pregunta 2", verdadera = false }
        };
        controlador.indicePregAct = 0;
        controlador.vidaActual = 4;
        controlador.vidaMaxima = 4;

    }

    [Test]
    public void MostrarPreguntaCorrectamente(){
        controlador.MostrarPregunta();
        Assert.AreEqual("Pregunta 1", txtpregunta.text);
    }

    [Test]
    public void AciertoJugador(){
        controlador.MostrarPregunta();
        // respuesta verdadera
        controlador.ComprobarRespuesta(true);
        // resta vida
        Assert.AreEqual(3, controlador.vidaActual);
        // se mantiene numero fallos
        Assert.AreEqual(0, controlador.GetNumFallos());
        // baja barra vida
        Assert.AreEqual(3/4f, controlador.barraVida.fillAmount);
    }

    [Test]

    public void FalloJugador(){
        controlador.MostrarPregunta();
        controlador.ComprobarRespuesta(false);
        // se mantiene la vida del villano
        Assert.AreEqual(4, controlador.vidaActual);
        // aumenta el numero de fallos
        Assert.AreEqual(1, controlador.GetNumFallos());
    }





}
