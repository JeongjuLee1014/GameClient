using UnityEngine;
using UnityEngine.SceneManagement;

public class DeepLinkHandler : MonoBehaviour
{
    // Singleton 인스턴스
    public static DeepLinkHandler Instance { get; private set; }

    // 딥 링크 URL을 저장하는 변수
    public string deeplinkURL;

    private void Awake()
    {
        // Singleton 패턴 설정
        if (Instance == null)
        {
            Instance = this;

            // 딥 링크 활성화 이벤트 등록
            Application.deepLinkActivated += OnDeepLinkActivated;

            Debug.Log($"Application.absoluteURL: {Application.absoluteURL}"); // 앱 시작 시 URL 확인

            // 앱이 처음 실행될 때 딥 링크 URL 처리
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OnDeepLinkActivated(Application.absoluteURL);
            }
            else
            {
                deeplinkURL = "[none]";
            }

            // 씬 전환에도 오브젝트가 유지되도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 딥 링크 URL 처리
    private void OnDeepLinkActivated(string url)
    {
        Debug.Log($"Deep Link Activated: {url}");

        // URL 저장
        deeplinkURL = url;

        // URL 파싱 및 씬 로드
        ProcessDeepLink(url);
    }

    // 딥 링크 URL 파싱 및 처리
    private void ProcessDeepLink(string url)
    {
        try
        {
            // URL 구조: unitydl://mylink?sceneName
            string[] urlParts = url.Split('?');
            if (urlParts.Length < 2)
            {
                Debug.LogWarning("Invalid URL format. Expected: unitydl://mylink?sceneName");
                return;
            }

            //// 씬 이름 추출
            //string sceneName = urlParts[1];
            //Debug.Log($"Parsed scene name: {sceneName}");

            //// 유효한 씬 이름인지 확인 후 로드
            //if (sceneName == "LoginComplete")
            //{
            //    SceneManager.LoadScene(sceneName);
            //}
            //else
            //{
            //    Debug.LogWarning($"Invalid scene name: {sceneName}");
            //}

            // 씬 이름 추출
            string sceneName = urlParts[1].Replace("sceneName=", ""); // "LoginComplete" 추출
            Debug.Log($"Parsed scene name: {sceneName}");

            // 씬 로드
            if (!string.IsNullOrEmpty(sceneName) && sceneName == "LoginComplete")
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogWarning($"Invalid scene name: {sceneName}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to process deep link: {ex.Message}");
        }
    }
}
