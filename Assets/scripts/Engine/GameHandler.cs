using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    [Header("Vehicle")]
    public GameObject CarSpawn;
    public GameObject CyberTruck;
    public GameObject StarterCar;
    public GameObject StarterTruck;
    private GameObject vehicle;
    private GameObject CurrentCar;

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

    [Header("Settings")]
    public GameObject SettingsUI;
    public Slider FovSlider;
    public TMP_Text FovSliderText;
    private bool IsMenuShowing;

    // Start is called before the first frame update
    public void InstantiateCar(GameObject playerVehicle, Vector3 playerLocation, Quaternion playerRotation, Vector3 playerVelocity, Vector3 playerAngularVelocity)
    {
        Cameras[CurrentCamera].transform.SetParent(GameObject.Find("Cameras").transform, false);
        PlayerLook.transform.SetParent(null, false);
        Destroy(vehicle);
        vehicle = Instantiate(playerVehicle, playerLocation, playerRotation);
        PlayerLook.transform.SetParent(vehicle.transform, false);
        vehicle.GetComponent<CarControl>().IsEnabled = true;
        vehicle.GetComponent<CarControl>().autoLockCursor = !IsMenuShowing;
        vehicle.GetComponent<Rigidbody>().velocity = playerVelocity;
        vehicle.GetComponent<Rigidbody>().angularVelocity = playerAngularVelocity;
        SwitchCamera(CurrentCamera);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Toggle the car's controller script
    public void ToggleDriving()
    {
        vehicle.GetComponent<CarControl>().IsEnabled = !vehicle.GetComponent<CarControl>().IsEnabled;
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

    public void UpdateFOV()
    {
        FirstPersonCamera.GetComponent<Camera>().fieldOfView = FovSlider.value;
        FreeCam.GetComponent<Camera>().fieldOfView = FovSlider.value;
        ThirdPersonCamera.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = FovSlider.value;
        FovSliderText.text = FovSlider.value.ToString();
    }
    void Start()
    {
        CurrentCar = CyberTruck;
        IsMenuShowing = false;
        Cameras.Add(ThirdPersonCamera);
        Cameras.Add(FirstPersonCamera);
        Cameras.Add(DashCam);
        Cameras.Add(FreeCam);
        CurrentCamera = 0;
        InstantiateCar(CurrentCar, CarSpawn.transform.position, CarSpawn.transform.rotation, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
    }

    public void SettingOverlay()
    {
        IsMenuShowing = !IsMenuShowing;
        SettingsUI.SetActive(!SettingsUI.activeSelf);
        vehicle.GetComponent<CarControl>().autoLockCursor = !vehicle.GetComponent<CarControl>().autoLockCursor;
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && vehicle.GetComponent<CarControl>().IsEnabled)
        {
            InstantiateCar(CurrentCar, CarSpawn.transform.position, CarSpawn.transform.rotation, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
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
        if(Input.GetKeyDown("escape"))
        {
            SettingOverlay();
        }
        if(Input.GetKeyDown("j"))
        {
            CurrentCar = StarterCar;
            InstantiateCar(CurrentCar, vehicle.transform.position, vehicle.transform.rotation, vehicle.GetComponent<Rigidbody>().velocity, vehicle.GetComponent<Rigidbody>().angularVelocity);
        }
        if(Input.GetKeyDown("k"))
        {
            CurrentCar = StarterTruck;
            InstantiateCar(CurrentCar, vehicle.transform.position, vehicle.transform.rotation, vehicle.GetComponent<Rigidbody>().velocity, vehicle.GetComponent<Rigidbody>().angularVelocity);
        }
        if(Input.GetKeyDown("l"))
        {
            CurrentCar = CyberTruck;
            InstantiateCar(CurrentCar, vehicle.transform.position, vehicle.transform.rotation, vehicle.GetComponent<Rigidbody>().velocity, vehicle.GetComponent<Rigidbody>().angularVelocity);
        }

    }
}
