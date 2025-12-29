using TMPro;
using UnityEngine;

public class panelManager : MonoBehaviour
{
    public static panelManager instance;
    public GameObject speedPanel;
    public GameObject carPanel;

    public TextMeshProUGUI carCostT;
    public TextMeshProUGUI carLimitT;

    public TextMeshProUGUI carSpeedT;
    public TextMeshProUGUI carSpeedCostT;
    public TextMeshProUGUI plusSpeedText;

    public laneManager _laneManager;
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

    private void Update()
    {
        if (_laneManager != null)
        {
            if (upgrades.instance.Joules >= _laneManager.gameObject.GetComponent<SpeedUpgrades>().carCost)
            {
                carCostT.color = Color.green;
            }
            else
            {
                carCostT.color = Color.red;
            }
            if (upgrades.instance.Joules >= _laneManager.gameObject.GetComponent<SpeedUpgrades>().speedCost)
            {
                carSpeedCostT.color = Color.green;
            }
            else
            {
                carSpeedCostT.color = Color.red;
            }
            if (_laneManager.carCount >= _laneManager.carLimit)
            {
                carCostT.text = "FULL";
                carCostT.color = Color.white;
            }
            else
            {
                carCostT.text = "Cost \n" + _laneManager.gameObject.GetComponent<SpeedUpgrades>().carCost + " Energy";
            }

            carLimitT.text = _laneManager.carCount + "/" + _laneManager.carLimit + " Cars";

            carSpeedT.text = "Speed: " + _laneManager.carSpeed +"m/s";
            carSpeedCostT.text = "Cost \n" + _laneManager.gameObject.GetComponent<SpeedUpgrades>().speedCost + " Energy";
            plusSpeedText.text = "+" + _laneManager.carSpeedAdd + "m/s";
        }
    }

    public void showPanels()
    {
        speedPanel.SetActive(true); 
        carPanel.SetActive(true);
    }

    public void hidePanels()
    {
        speedPanel.SetActive(false);
        carPanel.SetActive(false);
    }
}
