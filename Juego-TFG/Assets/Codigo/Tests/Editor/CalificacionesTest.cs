using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CalificacionesTest
{
    private Calificaciones calificaciones;

    [SetUp]
    public void SetUp(){
        calificaciones = new Calificaciones();
    }

    [Test]
    public void EnviarMensajeTest()
    {
        // Configurar el usuario y el mensaje
        Usuario usuario = new Usuario { nombre = "Alumno", email_tutor = "blancabrilgonzalez@gmail.com" };
        ControladorUsuario.GuardarUsuario(usuario);
        calificaciones.usuario = usuario;
        calificaciones.mensaje = "Mensaje de prueba";

        // Llamar al método EnviarMensaje
        calificaciones.EnviarMensaje();

        // No podemos verificar directamente el envío del correo, pero podemos asegurarnos de que no haya errores
        Assert.Pass("El método EnviarMensaje se ejecutó sin errores.");
    }

    [Test]
    public void GuardarYCargarPuntuacionTest()
    {
        int nivel = 1;
        int puntuacion = 7;
        Calificaciones.GuardarPuntuacion(nivel, puntuacion);

        int puntuacionCargada = Calificaciones.CargarPuntuacion(nivel);
        Assert.AreEqual(puntuacion, puntuacionCargada);
    }
}
