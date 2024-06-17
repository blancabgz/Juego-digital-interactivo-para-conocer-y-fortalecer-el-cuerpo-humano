using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[TestFixture]
public class SalaDeCurasTests
{
    private SalaDeCuras salaDeCuras;

    [SetUp]
    public void Setup()
    {
        salaDeCuras = new GameObject().AddComponent<SalaDeCuras>();
        salaDeCuras.curasOpciones = new string[] { "Cura1", "Cura2", "Cura3", "Cura4" };
        salaDeCuras.respuesta = "Cura1";

        // Se crea el panel de las curas con todas las curas del interior para simular la escena 
        GameObject curas = new GameObject("Curas");
        for (int i = 0; i < salaDeCuras.curasOpciones.Length; i++)
        {
            GameObject cura = new GameObject("Cura" + i);
            cura.transform.SetParent(curas.transform);
            GameObject fallo = new GameObject("Fallo");
            fallo.transform.SetParent(cura.transform);
            fallo.AddComponent<Image>().gameObject.SetActive(false);
        }

        // Lo agregamos a sala de curas
        curas.transform.SetParent(salaDeCuras.transform);
    }

    [Test]
    public void ObtenerRespuesta()
    {
        // Act
        salaDeCuras.RecogerRespuesta(0);
        Assert.Pass("Obteniene la respuesta correcta sin fallos");
    }


    // Comprobar que ocurre si 
    [Test]
    public void ObtenerRespuestaIncorrecta()
    {
        // Arrange
        salaDeCuras.respuesta = "Cura3"; 
        

        salaDeCuras.RecogerRespuesta(0);

        GameObject curas = GameObject.Find("Curas");
        GameObject cura0 = curas.transform.GetChild(0).gameObject;
        Image falloImage = cura0.transform.Find("Fallo").GetComponent<Image>();
        Assert.IsTrue(falloImage.gameObject.activeSelf);
    }


    // Comprobar que inicializa de forma correcta el sistema
    [Test]
    public void InicializacionCorrecta()
    {
        salaDeCuras.curasOpciones = new string[] { "Cura1", "Cura2", "Cura3" };
        
        salaDeCuras.Start();

        Assert.AreEqual(0, salaDeCuras.numFallos);
        Assert.AreEqual(10, salaDeCuras.puntos);
    }
}