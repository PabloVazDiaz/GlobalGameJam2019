﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuestoTransformacion : Puesto
{
    public float maxPower;
    public int transTarget;
    public Image barra;
    public int EntreEventosTiempo;
    public bool automatico;
    public Sprite transformSprite;
    public bool falsoPuesto;

    private float UltimoTiempo;
    private ObjetoLlevable go;

    private GameObject objetoTareaTransformado;
    private float power;
    public float cantidadPorClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (automatico && activado)
        {
            barra.fillAmount +=  Time.deltaTime / EntreEventosTiempo ;
        }
        if (Time.time - UltimoTiempo > EntreEventosTiempo && activado && automatico)
        {
            transform.Find("cerrado").gameObject.SetActive(false);
            transform.Find("abierto").gameObject.SetActive(true);
            activado = false;
            go.gameObject.SetActive(true);
            go = null;
            barra.fillAmount = 0;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Movimiento mov = collision.gameObject.GetComponent<Movimiento>();
        if (collision.tag == "Player" && !activado) 
        {
            
            if (Input.GetButtonDown("Fire2-p" + mov.numPlayer))
            {
                
                    go = collision.gameObject.GetComponentInChildren<ObjetoLlevable>();
                    if (go.transformaciones == transTarget)
                    {
                        go.gameObject.transform.parent = null;
                        go.gameObject.SetActive(false);
                        RecibirObjeto(go);
                    }
                    else
                    {
                        go = null;
                    }

                
            }

        }
        if(collision.tag == "Player" && activado && !automatico)
        {
            if (Input.GetButtonDown($"Fire2-p"+mov.numPlayer))
            {
                barra.fillAmount += cantidadPorClick;
                if (barra.fillAmount >= 1)
                {

                    transform.Find("cerrado").gameObject.SetActive(false);
                    transform.Find("abierto").gameObject.SetActive(true);
                    activado = false;
                    go.gameObject.SetActive(true);
                    go = null;
                    barra.fillAmount = 0;
                }
            }
        }
	    if(falsoPuesto && activado)
        {
            if (Input.GetButtonDown($"Fire2-p" + mov.numPlayer))
            {
                barra.fillAmount += cantidadPorClick;
                if (barra.fillAmount >= 1)
                {
                    realizados++;
                    if (realizados >= cantidad)
                    {
                        activado = false;
                        realizados = 0;
                    }
                    transform.Find("cerrado").gameObject.SetActive(true);
                    transform.Find("abierto").gameObject.SetActive(false);
                    barra.fillAmount = 0;
                }
            }
        }
    }

    override public void RecibirObjeto( ObjetoLlevable objLlevable)
    {
        transform.Find("cerrado").gameObject.SetActive(true);
        transform.Find("abierto").gameObject.SetActive(false);
        activado = true;
        objLlevable.Transformar(transformSprite);
        UltimoTiempo = Time.time;
    }



}
