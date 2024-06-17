using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TMPro;

public class SombrasTest
{
    private Sombras juegoSombras;
    private Sombras.Sombra[] sombrasArray;



    [SetUp]
    public void Setup()
    {
        juegoSombras = new GameObject().AddComponent<Sombras>();

        sombrasArray = new Sombras.Sombra[] { 
            new Sombras.Sombra() {musculo = "biceps", sombra = null},
            new Sombras.Sombra() {musculo = "triceps", sombra = null},
            new Sombras.Sombra() {musculo = "deltoides", sombra = null},
        };

        juegoSombras.sombras = sombrasArray;
        juegoSombras.respuestas = new string[] {"biceps", "triceps", "deltoides"};

        
    }

    [Test]
    public void inicializar()
    {
        Assert.AreEqual(0, juegoSombras.musculoActual);
    }

    [Test]
    public void TestObtenerImagen()
    {
        Assert.AreEqual("TickA", juegoSombras.ObtenerImagen(0));
        Assert.AreEqual("TickB", juegoSombras.ObtenerImagen(1));
        Assert.AreEqual("TickC", juegoSombras.ObtenerImagen(2));
        Assert.AreEqual(string.Empty, juegoSombras.ObtenerImagen(3));
    }

    
}
