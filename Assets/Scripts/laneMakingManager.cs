using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class laneMakingManager : MonoBehaviour
{
    public static laneMakingManager instance;
    public GameObject lane;
    public float laneSizeAdd = 1;
    public float laneSize = 2;    
    public float carLimitMult = 2;
    public int defaultCarLimit = 15;

    public List<GameObject> lanes = new List<GameObject>();

    public AudioClip[] stems;
    public float stemVolume = 0.4f;
    public int currentStem = 0;
    public AudioMixer audioMixer;
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
        for (int i = 0; i < stems.Length; i++)
        {
            string mixerGroup = "Instrument " + i;
            soundManager.instance.playSound(stems[i], transform, stemVolume, 1f, true, mixerGroup);                                              
            audioMixer.SetFloat("Vol" + i, -80f);                       
        }                         
    }

    public void newLane()
    {
        if (upgrades.instance.buyLane())
        {
            laneSize += laneSizeAdd;
            GameObject laneObj = Instantiate(lane, Vector3.zero, Quaternion.identity, transform);
            if(currentStem < stems.Length)
            {                
                audioMixer.SetFloat("Vol" + currentStem, 0f);
                currentStem++;
            }            
        }
    }
}
