using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Loading : MonoBehaviour
{
    void Start()
    {
        // loginValues.json의 isLogined 값 읽어오기
        const string filePath = "./Assets/login/loginValues.json";
        string loginValuesString = File.ReadAllText(filePath);
        LoginValues loginValuesJson = JsonUtility.FromJson<LoginValues>(loginValuesString);

        // 로그인한 적이 있으면 Home 씬으로 이동, 그렇지 않으면 Login 씬으로 이동
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