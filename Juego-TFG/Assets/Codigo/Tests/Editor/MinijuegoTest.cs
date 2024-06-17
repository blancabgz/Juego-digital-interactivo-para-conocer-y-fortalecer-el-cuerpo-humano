using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MinijuegoTest
{
    private Minijuego minijuego;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = new GameObject();
        minijuego = gameObject.AddComponent<Minijuego>();
    }

    [Test]
    public void TestMezclarElementos()
    {
        int [] array = {1,2,3,4,5};
        minijuego.MezclarElementos(array);
        Assert.AreNotEqual(new int[] {1,2,3,4,5}, array);   
    }

    [Test]
    public void TestAumentarNumeroFallos(){
        minijuego.AumentarNumeroFallos();
        Assert.AreEqual(1, minijuego.numFallos);
    }

    [Test]
    public void TestDisminuirNumeroFallos(){
        minijuego.AumentarNumeroFallos();
        minijuego.DisminuirNumeroFallos();
        Assert.AreEqual(0, minijuego.numFallos);
    }

    [Test]
    public void TestCalcularPuntuacionFinal(){
        minijuego.puntos = 10;
        minijuego.numFallos = 2;
        minijuego.CalcularPuntuacionFinal(1);
        Assert.AreEqual(8, minijuego.puntos);
    }

    [Test]
    public void TestCalcularPuntuacionFinalIntentos(){
        minijuego.puntos = 10;
        minijuego.numIntentos = 4;
        minijuego.CalcularPuntuacionFinalIntentos();
        Assert.AreEqual(6, minijuego.puntos);
    }

    [Test]
    public void TestCalcularPuntuacionRonda(){
        minijuego.numFallos = 2;
        minijuego.CalcularPuntuacionRonda();
        Assert.AreEqual(0, minijuego.puntos);
    }

    [Test]
    public void TestCalcularPuntuacionSegundaRonda(){
        minijuego.numFallos = 1;
        minijuego.puntos = 0;
        minijuego.CalcularPuntuacionSegundaRonda();
        Assert.AreEqual(5, minijuego.puntos);
    }

    [Test]
    public void TestCalcularPuntuacionTresOpciones(){
        minijuego.numFallos = 2;
        minijuego.puntos = 10;
        minijuego.CalcularPuntuacion3Opciones();
        Assert.AreEqual(4, minijuego.puntos);
    }

    [Test]
    public void TestRedondear(){
        int resultado = minijuego.Redondear(2.5f);
        Assert.AreEqual(3, resultado);
    }
}


