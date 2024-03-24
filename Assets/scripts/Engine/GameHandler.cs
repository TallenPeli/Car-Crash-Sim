using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [Header("Vehicle")]
    public GameObject CarSpawn;
    public GameObject CyberTruck;
    public GameObject StarterCar;
    public GameObject StarterTruck;

    [Header("Camera")]
    public GameObject PlayerLook;
    public GameObject ThirdPersonCamera;
    public GameObject FirstPersonCamera;
    public GameObject DashCam;
    public int CurrentCamera;

    // Start is called before the first frame update
    public void InstantiateCar()
    {
        Destroy(GameObject.Find("CyberTruck(Clone)"));
        Instantiate(CyberTruck, CarSpawn.transform.position, CarSpawn.transform.rotation);
        PlayerLook.transform.SetParent(GameObject.Find("CyberTruck(Clone)").transform, false);
    }
    void Start()
    {
        InstantiateCar();
    }

    void SwitchCamera()
    {
        if(Input.GetKeyDown("f"))
        {
            ThirdPersonCamera.SetActive(!ThirdPersonCamera.activeInHierarchy);
            FirstPersonCamera.SetActive(!FirstPersonCamera.activeInHierarchy);
        }
    }

    void Update()
    {
        SwitchCamera();
        if(FirstPersonCamera.activeInHierarchy)
        {
            FirstPersonCamera.transform.SetParent(GameObject.Find("CyberTruck(Clone)").transform, false);
        }
        if(Input.GetKeyDown("space"))
        {  
            InstantiateCar();
        }
    }
}
