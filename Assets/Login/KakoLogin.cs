using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class KakaoLogin : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = GetSessionId();
        string url = "https://localhost:7032/api/login/kakao?session_id=" + sessionId;

        Application.OpenURL(url);

        // LoginComplete ������ �α����� �Ϸ��ϰ� �Ѵ�
        SceneManager.LoadScene("LoginComplete");
    }

    private string GetSessionId()
    {
        string sessionId = Guid.NewGuid().ToString();  // ���� ID�� �����

        LoginValues loginValues = LoginValues.Get();

        loginValues.sessionId = sessionId;

        LoginValues.Set(loginValues);

        return sessionId;
    }
}