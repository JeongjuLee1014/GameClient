using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    void Start()
    {
        // loginValues.json�� �� �о����
        LoginValues loginValuesJson = LoginValues.Get();

        // �α����� ���� ������ Home ������ �̵�, �׷��� ������ Login ������ �̵�
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