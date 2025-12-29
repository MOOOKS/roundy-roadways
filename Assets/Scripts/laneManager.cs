using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class laneManager : MonoBehaviour
{
    #region Variables

    private Camera _mainCamera;
    private SpeedUpgrades _speedUpgrades;
    public Transform car;    
    public float carSpeed = 1f;
    public float carSpeedAdd = 0.2f;
    public int carLimit;
    public int carCount = 0;
    

    public setRoadSize road;
    public GameObject spawnPoint;

    [HideInInspector]
    public float roadRadius;

    [Header("Audio")]
    public AudioClip pop;
    public AudioClip speedAudio;
    
    

    #endregion

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        
        _speedUpgrades = gameObject.GetComponent<SpeedUpgrades>();
        laneMakingManager.instance.lanes.Add(gameObject);
        road.roadSize(laneMakingManager.instance.laneSize);        
        roadRadius = laneMakingManager.instance.laneSize;

        float x = Mathf.Cos(285 * Mathf.Deg2Rad) * roadRadius;
        float y = Mathf.Sin(285 * Mathf.Deg2Rad) * roadRadius;
        spawnPoint.transform.position = new Vector3(x, y, 0);

        carCount = 0;
        int myIndex = laneMakingManager.instance.lanes.IndexOf(gameObject);

        if (myIndex > 0)
        {
            GameObject previousLane = laneMakingManager.instance.lanes[myIndex - 1];
            laneManager prevLaneManager = previousLane.GetComponent<laneManager>();

            if (prevLaneManager != null)
            {
                carLimit = (int)(prevLaneManager.carLimit + laneMakingManager.instance.carLimitMult);
                //carSpeed = prevLaneManager.carSpeed;
                _speedUpgrades.defaultSpeedCost = prevLaneManager._speedUpgrades.defaultSpeedCost * _speedUpgrades.defaultSpeedCostMult;
                _speedUpgrades.defaultCarCost = prevLaneManager._speedUpgrades.defaultCarCost * _speedUpgrades.defaultCarCostMult;
                carSpeedAdd = Mathf.Ceil(prevLaneManager.carSpeedAdd * _speedUpgrades.speedMult);
            }
            else
            {
                Debug.LogWarning("Previous lane does not have a laneManager component.");
                carLimit = laneMakingManager.instance.defaultCarLimit;
            }
        }
        else
        {
            carLimit = laneMakingManager.instance.defaultCarLimit;
            Debug.Log("default limit");
        }
    }


    public void OnClick(InputAction.CallbackContext context)
    {        
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        
        if (rayHit.collider.transform.IsChildOf(transform) && (carCount < carLimit) && _speedUpgrades.buyCar())
        {
            Instantiate(car, Vector3.zero, Quaternion.identity, transform);
            carCount++;
            soundManager.instance.playSoundRandPitch(pop, transform, 0.8f, 0.7f, 1.1f, false);
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {        
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        if (rayHit.collider.transform.IsChildOf(transform) && _speedUpgrades.buySpeed())
        {
            print("speed up");
            carSpeed += carSpeedAdd;            
            foreach (carMove car in transform.GetComponentsInChildren<carMove>()) {
                
                car.speed = carSpeed;
            }
            soundManager.instance.playSoundRandPitch(speedAudio, transform, 0.8f, 0.8f, 1.2f, false);
        }
    }

    private void Update()
    {
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (rayHit.collider)
        {
            if (rayHit.collider.transform.IsChildOf(transform))
            {
                panelManager.instance._laneManager = this;
                panelManager.instance.showPanels();
            }

        }
        else
        {
            panelManager.instance.hidePanels();
        }
    }
}
