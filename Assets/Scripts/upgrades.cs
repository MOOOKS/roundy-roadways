
using UnityEngine;
using System.Collections.Generic;

public class upgrades : MonoBehaviour
{
    

    [Header("Lane Buying")]
    public int laneCost;    
    public int defaultLaneCost;
    public float laneCostMultiplier;

    [Header("JOULES")]
    public float Joules;
    public float energyPerSecond;
    public List<float> carEps = new List<float>();
    public float energyGoal = 50000;

    public static upgrades instance;

    [Header("Audio")]
    public AudioClip cantbuy;
    public AudioClip buyLaneAud;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        laneCost = defaultLaneCost;
    }

    public bool buyLane()
    {
        if (Joules >= laneCost)
        {
            Joules -= laneCost;
            laneCost = (int)(laneCost * laneCostMultiplier);
            soundManager.instance.playSoundRandPitch(buyLaneAud, transform, 0.8f, 0.8f, 1.2f, false);
            return true;
        }
        else
        {
            soundManager.instance.playSoundRandPitch(cantbuy, transform, 0.8f, 0.8f, 1.2f, false);
            return false;
        }

    }

    private void Update()
    {
        float sum = 0;
        foreach (float car in carEps) { 
            sum += car;          
        }
        energyPerSecond = sum;
    }


}
