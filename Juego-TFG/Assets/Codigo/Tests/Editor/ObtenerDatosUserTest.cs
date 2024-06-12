using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class ObtenerDatosUserTest
{
    private GameObject gameObject;
    private ObtenerDatosUser obtenerDatosUser;
    private TMP_InputField nombreInputField;
    private TMP_InputField emailInputField;
    private TextMeshProUGUI mensajeErrorText;
    
    [SetUp]
    public void SetUp()
    {
        // Configura el entorno de prueba
        gameObject = new GameObject();
        obtenerDatosUser = gameObject.AddComponent<ObtenerDatosUser>();

        // Añade y configura los campos de entrada y el mensaje de error
        GameObject nombreObject = new GameObject();
        GameObject emailObject = new GameObject();
        GameObject mensajeErrorObject = new GameObject();

        nombreInputField = nombreObject.AddComponent<TMP_InputField>();
        emailInputField = emailObject.AddComponent<TMP_InputField>();
        mensajeErrorText = mensajeErrorObject.AddComponent<TextMeshProUGUI>();

        obtenerDatosUser.nombre = nombreInputField;
        obtenerDatosUser.email = emailInputField;
        obtenerDatosUser.mensajeError = mensajeErrorText;

        obtenerDatosUser.usuario = new Usuario();
    }

    [TearDown]
    public void TearDown()
    {
        // Limpia después de cada prueba
        Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void AlmacenaNombreCorrectamente(){
        string nombre = "Blanca";
        nombreInputField.text = nombre;

        obtenerDatosUser.GuardarDato(nombreInputField);

        Assert.AreEqual(nombre.ToLower(), obtenerDatosUser.usuario.nombre);
    }

    [Test]
    public void AlmacenaEmailCorrectamente(){
        string email = "blanca@gmail.com";
        emailInputField.text = email;

        obtenerDatosUser.GuardarDato(emailInputField);

        Assert.AreEqual(email.ToLower(), obtenerDatosUser.usuario.email_tutor);
    }

    [Test]
    public void MuestraErrorSiCamposEstanVacios(){
        nombreInputField.text = "";
        emailInputField.text = "";

        obtenerDatosUser.Continuar("Juego");

        Assert.AreEqual("Por favor, complete todos los campos.", mensajeErrorText.text);
    }

    [Test]
    public void CambiaDeEscenaSiCamposEstanLlenos()
    {
        // Arrange
        nombreInputField.text = "Blanca";
        emailInputField.text = "blanca@gmail.com";

        // Act
        obtenerDatosUser.Continuar("Juego");

        Assert.IsTrue(string.IsNullOrEmpty(mensajeErrorText.text));
    }
}
