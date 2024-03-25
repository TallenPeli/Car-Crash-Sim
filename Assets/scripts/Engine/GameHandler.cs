using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [Header("Vehicle")]
    public GameObject CarSpawn;
    public GameObject CyberTruck;
    public GameObject StarterCar;
    public GameObject StarterTruck;

    [Header("Camera")]
    public GameObject CameraHolder;
    public GameObject PlayerLook;
    public int TotalCameras;
    public int CurrentCamera;
    public GameObject ThirdPersonCamera;
    public GameObject FirstPersonCamera;
    public GameObject DashCam;
    public GameObject FreeCam;
    public RawImage DashViewPort;
    List<GameObject> Cameras = new List<GameObject>();

    // Start is called before the first frame update
    public void InstantiateCar()
    {
        Cameras[CurrentCamera].transform.SetParent(GameObject.Find("Cameras").transform, false);
        PlayerLook.transform.SetParent(null, false);
        Destroy(GameObject.Find("CyberTruck(Clone)"));
        GameObject vehicle = Instantiate(CyberTruck, CarSpawn.transform.position, CarSpawn.transform.rotation);
        PlayerLook.transform.SetParent(vehicle.transform, false);
        vehicle.GetComponent<CarControl>().IsEnabled = true;
        SwitchCamera(CurrentCamera);
    }

    // Toggle the car's controller script
    public void ToggleDriving()
    {
        GameObject.Find("CyberTruck(Clone)").GetComponent<CarControl>().IsEnabled = !GameObject.Find("CyberTruck(Clone)").GetComponent<CarControl>().IsEnabled;
    }
    void Start()
    {
        CurrentCamera = 0;
        Cameras.Add(ThirdPersonCamera);
        Cameras.Add(FirstPersonCamera);
        Cameras.Add(DashCam);
        Cameras.Add(FreeCam);
        InstantiateCar();
    }

    // Used to change the Dash Cam Viewport to 4:3
    private void UpdateDashCamView()
    {
        DashViewPort.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.currentResolution.height * (4f / 3f), Screen.currentResolution.height);
        Debug.Log(Screen.currentResolution.height*(4/3));
    }

    public void SwitchCamera(int camera)
    {
        // For free cam
        if(CurrentCamera == 3)
        {
            ToggleDriving();
        }

        if(camera == 3)
        {
            Cameras[CurrentCamera].transform.SetParent(null, false);
            Cameras[CurrentCamera].SetActive(false);
            CurrentCamera = camera;
            Cameras[CurrentCamera].SetActive(true);
            Cameras[CurrentCamera].transform.position = PlayerLook.transform.position + new Vector3(0f, 2f ,0f);
            Cameras[CurrentCamera].transform.rotation = PlayerLook.transform.rotation;
            ToggleDriving();
        }
        else
        {
            // For regular Cameras
            Cameras[CurrentCamera].transform.SetParent(null, false);
            Cameras[CurrentCamera].SetActive(false);
            CurrentCamera = camera;
            Cameras[CurrentCamera].SetActive(true);
            Cameras[CurrentCamera].transform.SetParent(PlayerLook.transform, false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && GameObject.Find("CyberTruck(Clone)").GetComponent<CarControl>().IsEnabled)
        {
            InstantiateCar();
        }

        if(Input.GetKeyDown("1"))
        {
            SwitchCamera(0);
        }

        if(Input.GetKeyDown("2"))
        {
            SwitchCamera(1);
        }
        if(Input.GetKeyDown("3"))
        {
            SwitchCamera(2);
            UpdateDashCamView();
        }
        if(Input.GetKeyDown("v"))
        {
            SwitchCamera(3);
            UpdateDashCamView();
        }

    }
}
