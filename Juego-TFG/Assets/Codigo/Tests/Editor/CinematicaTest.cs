using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;

public class CinematicaTest
{
    private GameObject controladorDialogoCSVObject;
    private ControladorDialogoCSV controladorDialogoCSV;


    [SetUp]
    public void SetUp()
    {
        controladorDialogoCSVObject = new GameObject();
        controladorDialogoCSV = controladorDialogoCSVObject.AddComponent<ControladorDialogoCSV>();

        // Mocking required components
        controladorDialogoCSV.avatar = new GameObject().AddComponent<Image>();
        controladorDialogoCSV.avatarInicio = new GameObject().AddComponent<Image>();
        controladorDialogoCSV.avatarSuperheroe = new GameObject().AddComponent<Image>();
        controladorDialogoCSV.diapositiva = new GameObject().AddComponent<Image>();
        controladorDialogoCSV.conversacion = new GameObject().AddComponent<Text>();

        PlayerPrefs.DeleteAll();
    }

    [TearDown]
    public void TearDown()
    {
        if (controladorDialogoCSVObject != null)
        {
            UnityEngine.Object.DestroyImmediate(controladorDialogoCSVObject.gameObject);
        }
    }


    // AVATAR

    [Test]
    public void ColocarAvatarInicio()
    {
        controladorDialogoCSV.actorPanel.Add(new PersonajeCSV(0, "girl", Sprite.Create(Texture2D.blackTexture, new Rect(), new Vector2())));
        controladorDialogoCSV.actorPanel.Add(new PersonajeCSV(1, "boy", Sprite.Create(Texture2D.whiteTexture, new Rect(), new Vector2())));

        controladorDialogoCSV.selectedCharacter = "boy";

        controladorDialogoCSV.PersonajePanel();

        Assert.AreEqual(controladorDialogoCSV.actorPanel[1].sprite, controladorDialogoCSV.avatarInicio.sprite);
    }


    // Desplegar mensaje en la pantalla
    [Test]
    public void DesplegarMensajeEnPantalla()
    {
        // Arrange
        controladorDialogoCSV.mensajes = new List<MensajeCSV>
        {
            new MensajeCSV(0, 0, 0, "Mensaje")
        };

        controladorDialogoCSV.actores = new List<PersonajeCSV>
        {
            new PersonajeCSV(0, "actor", Sprite.Create(Texture2D.blackTexture, new Rect(), new Vector2()))
        };

        controladorDialogoCSV.superheroePanel = new List<PersonajeCSV>
        {
            new PersonajeCSV(0, "superhero", Sprite.Create(Texture2D.whiteTexture, new Rect(), new Vector2()))
        };

        controladorDialogoCSV.imagenes = new List<PersonajeCSV>
        {
            new PersonajeCSV(0, "image", Sprite.Create(Texture2D.grayTexture, new Rect(), new Vector2()))
        };

        controladorDialogoCSV.mensajeActivo = 0;

        controladorDialogoCSV.DesplegarMensaje();
        Assert.AreEqual("Mensaje", controladorDialogoCSV.conversacion.text);
        Assert.AreEqual(controladorDialogoCSV.actores[0].sprite, controladorDialogoCSV.avatar.sprite);
        Assert.AreEqual(controladorDialogoCSV.superheroePanel[0].sprite, controladorDialogoCSV.avatarSuperheroe.sprite);
        Assert.AreEqual(controladorDialogoCSV.imagenes[0].sprite, controladorDialogoCSV.diapositiva.sprite);
    }

    // Mostrar siguiente mensaje por pantalla

    [Test]
    public void SiguienteMensajeYDesplegar()
    {
        // Arrange
        controladorDialogoCSV.mensajes = new List<MensajeCSV>
        {
            new MensajeCSV(0, 0, 0, "Mensaje 1"),
            new MensajeCSV(0, 0, 0, "Mensage 2")
        };

        controladorDialogoCSV.mensajeActivo = 0;

        controladorDialogoCSV.conversacion = new GameObject().AddComponent<Text>();

        controladorDialogoCSV.SiguienteMensaje();

        Assert.AreEqual(1, controladorDialogoCSV.mensajeActivo, "El mensaje activo debe actualizarse a 1.");
        Assert.AreEqual("Mensage 2", controladorDialogoCSV.mensajes[controladorDialogoCSV.mensajeActivo].texto, "El texto del mensaje activo debe ser 'Mensaje 2'.");
    }




}
