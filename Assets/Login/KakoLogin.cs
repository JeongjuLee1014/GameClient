using UnityEngine;
using UnityEngine.SceneManagement;

public class KakaoLogin : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = LoginValues.CreateSessionId();

        LoginValues loginValues = LoginValues.Get();
        loginValues.sessionId = sessionId;
        LoginValues.Set(loginValues);

        string url = Constants.SERVER_URL + "/api/login/kakao?sessionId=" + sessionId;

        Application.OpenURL(url);

        // LoginComplete ������ �α����� �Ϸ��ϰ� �Ѵ�
        SceneManager.LoadScene("LoginComplete");
    }
}