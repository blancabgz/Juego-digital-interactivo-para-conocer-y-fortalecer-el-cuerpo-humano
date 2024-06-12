using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class MenuOpcionesTest{
    private GameObject controladorGameObject;
    private ControladorOpciones controladorOpciones;
    private AudioSource audioSource;
    private Text buttonText;
    private GameObject panelSeguridad;

    [SetUp]
    public void Setup(){
        controladorGameObject = new GameObject();
        controladorOpciones = controladorGameObject.AddComponent<ControladorOpciones>();
        audioSource = controladorGameObject.AddComponent<AudioSource>();
        controladorOpciones.audioSources = new AudioSource[] { audioSource };

        GameObject textGameObject = new GameObject();
        buttonText = textGameObject.AddComponent<Text>();
        controladorOpciones.btext = buttonText;

        panelSeguridad = new GameObject();
        controladorOpciones.panelSeguridad = panelSeguridad;

        PlayerPrefs.DeleteAll();
    }

    [TearDown]
    public void Teardown(){
        if (controladorOpciones != null){
            Object.DestroyImmediate(controladorOpciones.gameObject);
        }

        if (panelSeguridad != null){
            Object.DestroyImmediate(panelSeguridad);
        }

        if (buttonText != null){
            Object.DestroyImmediate(buttonText.gameObject);
        }

        PlayerPrefs.DeleteAll(); 
    }

    [Test]
    public void ComprobarEstadoMusicaCorrecto()
    {
        // Preparar
        PlayerPrefs.SetString("estadoMusica", "OFF");
        controladorOpciones.Start();

        // Actuar
        controladorOpciones.ComprobarEstadoMusica();

        // Afirmar
        Assert.IsTrue(audioSource.mute);
        Assert.AreEqual("OFF", controladorOpciones.btext.text);
    }


    [Test]
    public void IntentarBorrarDatosTest()
    {
        controladorOpciones.IntentarBorrarDatos();

        Assert.IsTrue(controladorOpciones.panelSeguridad.activeSelf);
    }

    [Test]
    public void CancelarAccionBorrarDatos()
    {
        controladorOpciones.panelSeguridad.SetActive(true);
        controladorOpciones.CancelarAccion();
        Assert.IsFalse(controladorOpciones.panelSeguridad.activeSelf);
    }

    
}
