using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class InsertarCalificaciones : MonoBehaviour
{
    private GameObject[] slotsPanel;
    public GameObject calificaciones;   
    private int numSlots;
    public string mensaje = "";

    
    
    void Awake() {
        // cuenta el numero de slots de las calificaciones
        numSlots = calificaciones.transform.childCount;
        // crear array 
        slotsPanel = new GameObject[numSlots];
    }
    void Start()
    {   
        
        for(int i = 0; i < slotsPanel.Length; i++){
            slotsPanel[i] = calificaciones.transform.GetChild(i).gameObject;
        }
        Calificaciones();
        EscribirMensaje();

    }

    public void EscribirMensaje(){
        mensaje = "Notas del alumno \n";
        for(int i = 0; i < 22; i++){
            int nota = Puntuaciones.CargarPuntuacion(i+1);
            if(NivelCompletado.CargarNivel(i+1) == 2){
                mensaje += "Nivel" + (i+1) + " -- " + nota + "\n";
            }else{
                mensaje += "Nivel" + (i+1) + " --  nivel no completado \n";
            }
            
        }
    }

    private void Calificaciones(){
        GameObject slotPuntuacion;
        if(slotsPanel.Length > 0){
            for(int i = 0; i < slotsPanel.Length; i++){
                slotPuntuacion = slotsPanel[i];
                TextMeshProUGUI texto = slotPuntuacion.GetComponentInChildren<TextMeshProUGUI>();
                if(texto != null){
                    if(NivelCompletado.CargarNivel(i+1) == 2){
                        texto.text = Convert.ToString(Puntuaciones.CargarPuntuacion(i+1));
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
        mail.To.Add(new MailAddress(GuardarDatosJugador.CargarEmailJugador()));

        mail.Subject = "Calificaciones de " + GuardarDatosJugador.CargarNombreJugador();
        mail.Body = mensaje;

        SmtpServer.Credentials = new System.Net.NetworkCredential("blancabril.999", "uexg kljy ikfn ldjm") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            
            return true;
        };

        

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }


}
