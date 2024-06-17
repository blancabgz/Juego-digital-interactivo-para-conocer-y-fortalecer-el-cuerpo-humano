using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class Calificaciones : MonoBehaviour{
    private GameObject[] slotsPanel;
    public GameObject calificaciones;   
    private int numSlots;
    public string mensaje = "";
    private const string PUNTUACION_NIVEL = "PuntuacionNivel_";
    private const int NUM_NIVELES = 22;
    public Usuario usuario;

    
    
    void Awake() {
        // cuenta el numero de slots de las calificaciones
        numSlots = calificaciones.transform.childCount;
        // crear array 
        slotsPanel = new GameObject[numSlots];
        usuario = ControladorUsuario.CargarUsuario();
        ControlMusica.EstadoMusica();
    }
    void Start()
    {   
        
        for(int i = 0; i < slotsPanel.Length; i++){
            slotsPanel[i] = calificaciones.transform.GetChild(i).gameObject;
        }
        ObtenerCalificaciones();
        EscribirMensaje();

    }

    public void EscribirMensaje(){
        mensaje = "Notas del alumno \n";
        for(int i = 0; i < NUM_NIVELES; i++){
            int nota = CargarPuntuacion(i+1);
            if(ControladorNiveles.CargarNivel(i+1) == 2){
                mensaje += "Nivel" + (i+1) + " -- " + nota + "\n";
            }else{
                mensaje += "Nivel" + (i+1) + " --  nivel no completado \n";
            }
            
        }
    }

    private void ObtenerCalificaciones(){
        GameObject slotPuntuacion;
        if(slotsPanel.Length > 0){
            for(int i = 0; i < slotsPanel.Length; i++){
                slotPuntuacion = slotsPanel[i];
                TextMeshProUGUI texto = slotPuntuacion.GetComponentInChildren<TextMeshProUGUI>();
                if(texto != null){
                    if(ControladorNiveles.CargarNivel(i+1) == 2){
                        texto.text = Convert.ToString(CargarPuntuacion(i+1));
                    }else{
                        texto.text = "-";
                    }
                    
                }else{
                    Debug.LogWarning("No se encontró el componente TextMeshPro en el GameObject " + slotPuntuacion.name);
                }
            } 
        }
    }

    public void EnviarMensaje(){
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){
            SocialShare(mensaje, "Calificaciones");
        }else{
            EnviarEmail();
        }
    }

    void SocialShare(string mensaje, string asunto){
        SocialShareHelper.ShareText(mensaje, asunto);
    }

    public void EnviarEmail(){
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress("blancabril.999@gmail.com");
        mail.To.Add(new MailAddress(usuario.email_tutor.ToLower()));

        mail.Subject = "Calificaciones de " + usuario.nombre.ToLower();
        mail.Body = mensaje;

        SmtpServer.Credentials = new System.Net.NetworkCredential("blancabril.999", "uexg kljy ikfn ldjm") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            
            return true;
        };

        

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }

    public static void GuardarPuntuacion(int nivel, int puntos){
        string clave = PUNTUACION_NIVEL + nivel.ToString();
        PlayerPrefs.SetInt(clave, puntos);
        PlayerPrefs.Save();
    }

    public static int CargarPuntuacion(int nivel){
        string clave = PUNTUACION_NIVEL + nivel.ToString();
        return(PlayerPrefs.GetInt(clave, -1)); // Si no hay valor guardado, devuelve 0
    }




}
