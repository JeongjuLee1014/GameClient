using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    void Start()
    {
        LoginValues loginValuesJson = LoginValues.Get();

        if (loginValuesJson.isLogined)
        {
            SceneManager.LoadScene("Home");
        }
        else
        {
            SceneManager.LoadScene("Login");
        }
    }
}