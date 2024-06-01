using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorUsuario : MonoBehaviour {
    
    private const string USUARIO = "Usuario";

    public static void GuardarUsuario(Usuario usuario) {
        string usuarioStr = usuario.nombre + "," + usuario.email_tutor;
        PlayerPrefs.SetString(USUARIO, usuarioStr);
        PlayerPrefs.Save();
    }

    public static Usuario CargarUsuario() {
        string usuarioStr = PlayerPrefs.GetString(USUARIO, "");
        if (string.IsNullOrEmpty(usuarioStr)) {
            return null;
        }
        string[] datos = usuarioStr.Split(',');
        return new Usuario { nombre = datos[0], email_tutor = datos[1] };
    }
}

    [System.Serializable]
    public class Usuario
    {
        public string nombre;
        public string email_tutor;
        
    }

