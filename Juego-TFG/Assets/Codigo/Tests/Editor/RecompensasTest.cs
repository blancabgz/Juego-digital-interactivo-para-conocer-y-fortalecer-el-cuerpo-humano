using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class RecompensasTest
{
    private ControladorRecompensas controladorRecompensas;

    [SetUp]
    public void SetUp()
    {
        ;
        controladorRecompensas = new GameObject().AddComponent<ControladorRecompensas>();

        SceneManager.LoadScene("Medallas", LoadSceneMode.Single);
    }

    [TearDown]
    public void TearDown()
    {
        if (gameObject != null)
        {
            UnityEngine.Object.DestroyImmediate(gameObject.gameObject);
        }

    }

    [Test]
    public void LeerArchivoTest()
    {
        // Crear un archivo de prueba temporal
        string path = "Assets/Codigo/Datos/recompensas_test.csv";
        string[] lines = {
            "nivel,tipo,imagenPath",
            "1,medalla,medalla1",
            "2,objeto,objeto1"
        };
        File.WriteAllLines(path, lines);

        // Llamar al método LeerArchivo
        controladorRecompensas.LeerArchivo(path);

        // Verificar que las recompensas se hayan leído correctamente
        Assert.AreEqual(2, controladorRecompensas.recompensas.Count);
        Assert.AreEqual(1, controladorRecompensas.recompensas[0].nivel);
        Assert.AreEqual("medalla", controladorRecompensas.recompensas[0].tipo);
        Assert.AreEqual(2, controladorRecompensas.recompensas[1].nivel);
        Assert.AreEqual("objeto", controladorRecompensas.recompensas[1].tipo);

        // Eliminar el archivo de prueba temporal
        File.Delete(path);
    }

    [Test]
    public void AsignarRecomenpensaNivelTest()
    {
        GameObject imagenPremio = new GameObject();
        imagenPremio.AddComponent<Image>();
        controladorRecompensas.imagenPremio = imagenPremio;
        PlayerPrefs.SetInt("Nivel", 1);

        Sprite sprite = Sprite.Create(Texture2D.blackTexture, new Rect(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0.5f, 0.5f));
        controladorRecompensas.recompensas = new List<Recompensa> { new Recompensa(1, "medalla", sprite) };

        controladorRecompensas.AsignarRecomenpensaNivel();

        Image imagen = imagenPremio.GetComponent<Image>();
        Assert.AreEqual(sprite, imagen.sprite);
    }
}
