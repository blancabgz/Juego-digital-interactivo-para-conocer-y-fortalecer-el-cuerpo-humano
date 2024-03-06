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

        private void Awake(){
            restante = (min * 60) + seg;
            enMarcha = true;
        }

    
        void Update()
        {
            restante -= Time.deltaTime;
            if(restante <= 0 ){
                restante = 0;
                enMarcha = false;
            }
            tempMin = Mathf.FloorToInt(restante/60);
            tempSeg = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);

        }
    }
