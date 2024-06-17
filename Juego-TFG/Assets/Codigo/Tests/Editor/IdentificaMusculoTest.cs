using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class IdentificaMusculoTest
{
    private IdentificaMusculo identificaMusculo;


    [SetUp]
    public void Setup()
    {
        
        GameObject gameObject = new GameObject();
        identificaMusculo = gameObject.AddComponent<IdentificaMusculo>();

        identificaMusculo.resp1 = new GameObject();
        identificaMusculo.resp1.AddComponent<Button>();
        identificaMusculo.resp1.AddComponent<Image>();

        identificaMusculo.resp2 = new GameObject();
        identificaMusculo.resp2.AddComponent<Button>();
        identificaMusculo.resp2.AddComponent<Image>();

        identificaMusculo.resp3 = new GameObject();
        identificaMusculo.resp3.AddComponent<Button>();
        identificaMusculo.resp3.AddComponent<Image>();

        identificaMusculo.validate1 = new GameObject();
        identificaMusculo.validate1.AddComponent<Image>();

        identificaMusculo.validate2 = new GameObject();
        identificaMusculo.validate2.AddComponent<Image>();

        identificaMusculo.validate3 = new GameObject();
        identificaMusculo.validate3.AddComponent<Image>();

        identificaMusculo.imagenPanel = new GameObject();
        identificaMusculo.imagenPanel.AddComponent<Image>();

        identificaMusculo.botonSiguiente = new GameObject();
        identificaMusculo.botonSiguiente.AddComponent<Button>();
        identificaMusculo.botonSiguiente.SetActive(false);

        identificaMusculo.panelPregunta = new GameObject();
        identificaMusculo.panelPregunta.SetActive(true);

        identificaMusculo.respuestas = new IdentificaMusculo.OpcionesMusculo[]
        {
            new IdentificaMusculo.OpcionesMusculo { musculo = "biceps", imagen = null },
            new IdentificaMusculo.OpcionesMusculo { musculo = "triceps", imagen = null },
            new IdentificaMusculo.OpcionesMusculo { musculo = "deltoids", imagen = null }
        };

        identificaMusculo.imagenes = new IdentificaMusculo.Imagenes[]
        {
            new IdentificaMusculo.Imagenes { sprite = null },
            new IdentificaMusculo.Imagenes { sprite = null },
            new IdentificaMusculo.Imagenes { sprite = null }
        };


        identificaMusculo.musculo = "biceps";
        identificaMusculo.escenaSiguiente = "Nivel3.1";
    }

    [Test]
    public void VerificarRespuestaCorrecta()
    {
        identificaMusculo.VerificarRespuesta("biceps",1);
        Assert.IsFalse(identificaMusculo.resp1.GetComponent<Button>().interactable);
        Assert.IsTrue(identificaMusculo.botonSiguiente.activeSelf);
    }

    [Test]
    public void VerificarRespuestaIncorrecta()
    {
        identificaMusculo.VerificarRespuesta("triceps",2);
        Assert.IsTrue(identificaMusculo.resp1.GetComponent<Button>().interactable);
        Assert.IsFalse(identificaMusculo.botonSiguiente.activeSelf);
    }



}
