using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OyunuBaslat()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OyunuKapat()
    {
        Application.Quit();
    }
}