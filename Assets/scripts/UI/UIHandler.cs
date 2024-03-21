using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public GameObject StartingCanvas;
    public GameObject currectCanvas;
    public Slider FOVslider;
    public TMP_text FOVField;

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

    public void ChangeFOVtext()
    {
        FOVField.text = string.Format("{0:0.00}", FOVslider.value);
    }

    void Start()
    {
        currectCanvas = StartingCanvas;
        currectCanvas.SetActive(true);
    }
}
