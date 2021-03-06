﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class SpawnTareas : MonoBehaviour
{
    public int EntreEventosTiempo;
    public List<Puesto> puestos;
    public UIController UIController;

    private float UltimoTiempo;
    private float LosePercent;

    // Start is called before the first frame update
    void Start()
    {
        UltimoTiempo = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float puestoSinActivar = puestos.Where(x => !x.activado).ToList().Count;
        LosePercent = (float)((puestos.Count - puestoSinActivar) / puestos.Count);
        UIController.UpdateSlider(LosePercent);

        UIController.FillClock(Time.deltaTime/EntreEventosTiempo);
        if (Time.time - UltimoTiempo > EntreEventosTiempo)
        {
            if(puestos.Where(x => x.activado == false).Any())
            {
                puestos.Where(x => x.activado == false).ToList()[UnityEngine.Random.Range(0, puestos.Where(x => x.activado == false).ToList().Count)].SoltarObjetos();
                UltimoTiempo = Time.time;
                
            }
            else
            {
                GameOver();
            }
            
        }
        if (UIController.win)
        {
            SceneManager.LoadScene(3);
            Debug.Log("Has ganado");
        }
    }

    private void GameOver()
    {
        Debug.Log("Has perdido");
        
        
        SceneManager.LoadScene(2);
        
    }

    
}