using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KakaoLogin : MonoBehaviour
{
    //private void OnMouseDown()
    public void OnKakaoLoginClick()
    {
        string sessionId = LoginValues.CreateSessionId();

        LoginValues loginValues = LoginValues.Get();
        loginValues.sessionId = sessionId;
        LoginValues.Set(loginValues);

        //string url = Constant.SERVER_URL + "/api/login/kakao?sessionId=" + sessionId;
        string url = $"{Constant.SERVER_URL}/api/login/kakao?session_id={Uri.EscapeDataString(sessionId)}";

        Application.OpenURL(url);

        // LoginComplete ������ �α����� �Ϸ��ϰ� �Ѵ�
        SceneManager.LoadScene("LoginComplete");
    }
}