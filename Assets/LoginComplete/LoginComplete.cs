using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginComplete : MonoBehaviour
{
    private User user;
    private bool isLoginCompleted = false;

    public void OnMouseDown()
    {
        StartCoroutine(HandleLoginCompletion());
    }

    private IEnumerator HandleLoginCompletion()
    {
        while (isLoginCompleted == false)
        {
            Debug.Log("로그인 처리 중입니다.");
            yield return StartCoroutine(CheckLoginCompleted());
        }

        SetLogined();

        if (string.IsNullOrEmpty(user.nickName))
        {
            SceneManager.LoadScene("NickName");
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }

    public void SetLogined()
    {
        LoginValues loginValues = LoginValues.Get();

        loginValues.isLogined = true;

        LoginValues.Set(loginValues);
    }

    public IEnumerator CheckLoginCompleted()
    {
        yield return StartCoroutine(GetUser());
        isLoginCompleted = (user != null);
    }

    public IEnumerator GetUser()
    {
        LoginValues loginValues = LoginValues.Get();

        string url = Constants.SERVER_URL + "/api/users/session/" + loginValues.sessionId;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                user = JsonUtility.FromJson<User>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("GET request failed: " + request.error);
                user = null;
            }
        }
    }
}