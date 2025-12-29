using UnityEngine;

public class SpeedUpgrades : MonoBehaviour
{
    [Header("Speed Vars")]
    public int speedCost;
    public int defaultSpeedCost;
    public float speedCostMult;
    public int level;    
    public int defaultSpeedCostMult;
    public float speedMult;

    [Header("Car Vars")]
    public int carCost;
    public int defaultCarCost;
    public float carCostMult;    
    public int defaultCarCostMult;

    [Header("Audio")]
    public AudioClip cantbuy;

    private void Start()
    {
        speedCost = defaultSpeedCost;
        carCost = defaultCarCost;
        level = 1;        
    }

    public bool buyCar()
    {
        if (upgrades.instance.Joules >= carCost)
        {
            upgrades.instance.Joules -= carCost;            
            carCost = (int)Mathf.Ceil((carCost * carCostMult));
            return true;
        }
        else
        {
            soundManager.instance.playSoundRandPitch(cantbuy, transform, 0.8f, 0.8f, 1.2f, false);
            return false;
        }
    }

    public bool buySpeed()
    {
        if (upgrades.instance.Joules >= speedCost)
        {
            upgrades.instance.Joules -= speedCost;
            level++;
            speedCost = (int)Mathf.Ceil((speedCost * speedCostMult));
            return true;
        }
        else
        {
            soundManager.instance.playSoundRandPitch(cantbuy, transform, 0.8f, 0.8f, 1.2f, false);
            return false;
        }

    }
}
