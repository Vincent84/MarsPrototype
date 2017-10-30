using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{

    public enum ModuleState
    {
        DISABLED,
        ENABLED,
        BUILDED,
        PRODUCING
    }

    public string moduleName;

    public ModuleState activedState;

    public Resource[] resourcesProduced;
    public float durationProduction;
    public float timerProduction;

    public Resource[] resourcesConsumed;
    public float durationConsumption;
    public float timerConsumption;

    void Awake()
    {
        timerProduction = durationProduction;    
        timerConsumption = durationConsumption;
    }

    void Update()
    {
        if(this.activedState == ModuleState.BUILDED)
        {
            this.activedState = ModuleState.PRODUCING;

            StartCoroutine(StartTimerProduction());
            StartCoroutine(StartTimerConsumption());
        }
    }

    public IEnumerator StartTimerProduction()
    {
        while (this.timerProduction > 0 && this.activedState == ModuleState.PRODUCING)
        {
            this.timerProduction -= Time.deltaTime * 1;
            yield return null;
        }
        if (this.timerProduction <= 0)
        {
            ResourceManager.IncreasesResources(this.resourcesProduced);
            timerProduction = durationProduction;
            StartCoroutine(StartTimerProduction());
        }

    }

    public IEnumerator StartTimerConsumption()
    {
        while (this.timerConsumption > 0 && this.activedState == ModuleState.PRODUCING)
        {
            this.timerConsumption -= Time.deltaTime * 1;
            yield return null;
        }
        if (this.timerConsumption <= 0)
        {
            ResourceManager.DecreasesResources(this.resourcesConsumed);
            timerConsumption = durationConsumption;
            StartCoroutine(StartTimerConsumption());
        }

    }


}
