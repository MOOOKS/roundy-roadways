using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI joulesText;
    public TextMeshProUGUI newLaneText;
    public Button newLaneButton;
    public TextMeshProUGUI epsText;

    public AudioClip canBuy;
    bool played = false;
    public AudioClip winAudio;
    bool playedWin = false;

    public GameObject winObj;
    private void Update()
    {
        joulesText.text = Mathf.Floor(upgrades.instance.Joules).ToString() + " Energy";
        newLaneText.text = "New Lane - " + upgrades.instance.laneCost.ToString() + " Energy";
        epsText.text = "+" + Mathf.Round(upgrades.instance.energyPerSecond*10)*0.1f + " EPS";
        if(upgrades.instance.Joules < upgrades.instance.laneCost)
        {
            newLaneButton.interactable = false;
            played = false;
        }
        else
        {
            newLaneButton.interactable = true;
            if (!played)
            {
                soundManager.instance.playSoundRandPitch(canBuy, transform, 0.8f, 0.8f, 1.2f, false);
                played = true;
            }
        }

        if(upgrades.instance.Joules >= upgrades.instance.energyGoal && winObj != null)
        {
            winObj.SetActive(true);
            if (!playedWin)
            {
                soundManager.instance.playSoundRandPitch(winAudio, transform, 0.8f, 0.8f, 1.2f, false);
                playedWin = true;
            }
        }
        
    }

    public void endlessMode()
    {
        Destroy(winObj);
    }

    public void backToMenu()
    {
        StartCoroutine(music.instance.transition(0));
    }
}
