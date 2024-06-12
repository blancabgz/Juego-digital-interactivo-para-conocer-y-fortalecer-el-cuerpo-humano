using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNivelesTest{
    private GameObject controladorGameObject;
    private ControladorNiveles controladorNiveles;

    [SetUp]
    public void Setup(){

        controladorGameObject = new GameObject();
        controladorNiveles = controladorGameObject.AddComponent<ControladorNiveles>();

        // Crear botones de nivel ficticios para pruebas
        controladorNiveles.levelBottons = new ControladorNiveles.Nivel[3];
        for (int i = 0; i < controladorNiveles.levelBottons.Length; i++){
            controladorNiveles.levelBottons[i] = new ControladorNiveles.Nivel{
                nivel = i + 1,
                btnNivel = new GameObject().AddComponent<Button>(),
                escena = "Escena" + (i + 1)
            };
        }

        PlayerPrefs.DeleteAll(); 
    }

    [TearDown]
    public void Teardown(){
        if (controladorNiveles != null){
            Object.DestroyImmediate(controladorNiveles.gameObject);
        }
        
        PlayerPrefs.DeleteAll(); 
    }

    // test para comprobar que se desactivan todos los botones al inicio del nivel
    [Test]
    public void DesactivarTodosBotones(){
        controladorNiveles.DesactivarBotones();
        foreach (var nivel in controladorNiveles.levelBottons){
            Assert.IsFalse(nivel.btnNivel.interactable);
        }
    }

    // test para comprobar que se desactivan los botones al desbloquear el nivel
    [Test]
    public void DesbloquearNiveles(){

        PlayerPrefs.SetInt("unlockedLevels", 2);
        controladorNiveles.Start();

        Assert.IsTrue(controladorNiveles.levelBottons[0].btnNivel.interactable);
        Assert.IsTrue(controladorNiveles.levelBottons[1].btnNivel.interactable);
        Assert.IsFalse(controladorNiveles.levelBottons[2].btnNivel.interactable);
    }

    // test para comprobar que se puede desbloquear un nivel
    [Test]
    public void AumentarNivelCorrecto(){
    
        controladorNiveles.unlock = 3;
        controladorNiveles.AumentarNivel();
        Assert.AreEqual(3, PlayerPrefs.GetInt("unlockedLevels"));
    }

    // test para comprobar que devuelve el nivel correcto

    [Test]
    public void NivelActualCorrecto(){

        PlayerPrefs.SetInt("unlockedLevels", 2);

        int nivelActual = controladorNiveles.NivelActual();

        Assert.AreEqual(2, nivelActual);
    }

    // Test que comprueba si se guarda el nivel de forma correcta
    [Test]
    public void GuardarNivelCompletado(){
        ControladorNiveles.GuardarNivel(1, 2);
        Assert.AreEqual(2, PlayerPrefs.GetInt("EstadoNivel_1"));
    }

    [UnityTest]
    public IEnumerator CargarEscenaCorrecta(){
        controladorNiveles.levelBottons[0].nivel = 1;
        controladorNiveles.levelBottons[0].escena = "Nivel1";

        controladorNiveles.CargarEscena(1, "Nivel1");

        // Esperar un frame para asegurar que la escena se carga correctamente
       yield return new WaitForSeconds(0.1f);

        Assert.AreEqual("Nivel1", SceneManager.GetActiveScene().name);
    }



    
}
