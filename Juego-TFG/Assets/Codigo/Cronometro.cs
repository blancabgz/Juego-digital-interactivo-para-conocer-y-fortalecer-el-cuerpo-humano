    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class Cronometro : MonoBehaviour
    {
        [SerializeField] int min, seg;
        [SerializeField] TextMeshProUGUI tiempo;

        private float restante;
        private bool enMarcha;
        private int tempMin, tempSeg;
        private int contadorMedallas = 0;
        private bool terminado = false;

        private void Awake(){
            CargarContadorMedallas();
            restante = (min * 60) + seg;
            enMarcha = true;
        }


        void Update()
        {
            if (!enMarcha || terminado) return; // Comprueba si ya se ha llamado IrAlDado

            restante -= Time.deltaTime;
            if(restante <= 0 ){
                restante = 0;
                enMarcha = false;
                terminado = true;

                IrRetroalimentacion();

                
            }
            tempMin = Mathf.FloorToInt(restante/60);
            tempSeg = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);

        }

        private void IrRetroalimentacion() {
            contadorMedallas++;
            PlayerPrefs.SetInt("ContadorMedallas", contadorMedallas);
            Utilidades.EscenaJuego("MovimientoRetroalimentacion");
        }

        private void CargarContadorMedallas(){
            if (PlayerPrefs.HasKey("ContadorMedallas")){
                contadorMedallas = PlayerPrefs.GetInt("ContadorMedallas");
            }
        }
    }
