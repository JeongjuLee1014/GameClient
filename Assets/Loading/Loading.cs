using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Loading : MonoBehaviour
{
    void Start()
    {
        // loginValues.json�� isLogined �� �о����
        const string filePath = "./Assets/login/loginValues.json";
        string loginValuesString = File.ReadAllText(filePath);
        LoginValues loginValuesJson = JsonUtility.FromJson<LoginValues>(loginValuesString);

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