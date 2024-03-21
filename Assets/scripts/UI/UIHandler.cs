using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public GameObject StartingCanvas;
    public GameObject currectCanvas;
    public Slider FOVslider;
    public TMP_Text FOVtext;
    public GameObject errorMessageText;

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

    public IEnumerator ShowErrorMessage(string errorMessage)
    {
        errorMessageText.SetActive(true);
        errorMessageText.GetComponent<TMP_Text>().text = errorMessage;
        yield return new WaitForSeconds(5.0f);
        errorMessageText.SetActive(false);
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
    }
}
