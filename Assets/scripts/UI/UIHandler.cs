using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public GameObject StartingCanvas;
    public GameObject StartingSideView;
    private GameObject currectCanvas;
    private GameObject CurrentSideView;
    public Slider FOVslider;
    public TMP_Text FOVtext;
    public GameObject errorMessageText;
    public GameObject StatusMessageText;
    public Dropdown resolutionsMenu;

    [Header("Settings")]
    public float FOV = 90f;
    public void LoadLevel()
    {
        SceneManager.LoadScene("Testing", LoadSceneMode.Single);
    }

    public void LoadMenu(GameObject canvas)
    {
        currectCanvas.SetActive(false);
        currectCanvas = canvas;
        canvas.SetActive(true);
    }
    public void LoadSideView(GameObject sideView)
    {
        CurrentSideView.SetActive(false);
        CurrentSideView = sideView;
        sideView.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayAnimation(string animation, GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().Play(animation);
    }

    public IEnumerator ShowErrorMessage(string errorMessage)
    {
        errorMessageText.SetActive(true);
        errorMessageText.GetComponentInChildren<TMP_Text>().text = errorMessage;
        yield return new WaitForSeconds(5.0f);
        yield return new WaitForSeconds(0.5f);
        errorMessageText.SetActive(false);
    }
    public IEnumerator ShowStatusMessage(string statusMessage)
    {
        StatusMessageText.SetActive(true);
        StatusMessageText.GetComponentInChildren<TMP_Text>().text = statusMessage;
        yield return new WaitForSeconds(5.0f);
        StatusMessageText.SetActive(false);
    }

    public void ChangeFOV()
    {
        FOVtext.text = string.Format("{0}", FOVslider.value);
        FOV = FOVslider.value;
    }

    void Start()
    {
        currectCanvas = StartingCanvas;
        currectCanvas.SetActive(true);
        CurrentSideView = StartingSideView;
        CurrentSideView.SetActive(true);
    }
}
