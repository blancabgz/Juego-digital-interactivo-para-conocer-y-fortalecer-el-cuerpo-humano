using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EleccionPersonajeTest{
    private Jugador jugador;

    [SetUp]
    public void Setup(){
        GameObject gameObject = new GameObject();
        jugador = gameObject.AddComponent<Jugador>();
        PlayerPrefs.DeleteAll();
    }

    [TearDown]
    public void Teardown(){
        Object.DestroyImmediate(jugador.gameObject);
        PlayerPrefs.DeleteAll();  
    }

    // Comprueba que se selecciona bien el personaje 
    [Test]
    public void PersonajeSeleccionado(){
        PlayerPrefs.SetString("SelectedCharacter", "boy");
        jugador.Awake();
        Assert.AreEqual("boy", jugador.selectedCharacter);

        PlayerPrefs.SetString("SelectedCharacter", "girl");
        jugador.Awake();
        Assert.AreEqual("girl", jugador.selectedCharacter);

    }
    
    // Comprueba que se guarda correctamente el personaje seleccionado
    [Test]
    public void GuardarPersonajeSeleccionado(){
        jugador.SelectCharacter("girl");
        Assert.AreEqual("girl", jugador.selectedCharacter);
        Assert.AreEqual("girl", PlayerPrefs.GetString("SelectedCharacter"));
    }


    // Comprueba que se carga bien el personaje seleccionado en la escena
    [Test]
    public void CargarJugadorSeleccionado(){
        PlayerPrefs.SetString("SelectedCharacter", "girl");
        jugador.LoadSelectedCharacter();
        Assert.AreEqual("girl", jugador.selectedCharacter);
    }
    
}
