using UnityEngine;
using UnityEngine.SceneManagement;

public class DeepLinkHandler : MonoBehaviour
{
    // Singleton �ν��Ͻ�
    public static DeepLinkHandler Instance { get; private set; }

    // �� ��ũ URL�� �����ϴ� ����
    public string deeplinkURL;

    private void Awake()
    {
        // Singleton ���� ����
        if (Instance == null)
        {
            Instance = this;

            // �� ��ũ Ȱ��ȭ �̺�Ʈ ���
            Application.deepLinkActivated += OnDeepLinkActivated;

            Debug.Log($"Application.absoluteURL: {Application.absoluteURL}"); // �� ���� �� URL Ȯ��

            // ���� ó�� ����� �� �� ��ũ URL ó��
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OnDeepLinkActivated(Application.absoluteURL);
            }
            else
            {
                deeplinkURL = "[none]";
            }

            // �� ��ȯ���� ������Ʈ�� �����ǵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �� ��ũ URL ó��
    private void OnDeepLinkActivated(string url)
    {
        Debug.Log($"Deep Link Activated: {url}");

        // URL ����
        deeplinkURL = url;

        // URL �Ľ� �� �� �ε�
        ProcessDeepLink(url);
    }

    // �� ��ũ URL �Ľ� �� ó��
    private void ProcessDeepLink(string url)
    {
        try
        {
            // URL ����: unitydl://mylink?sceneName
            string[] urlParts = url.Split('?');
            if (urlParts.Length < 2)
            {
                Debug.LogWarning("Invalid URL format. Expected: unitydl://mylink?sceneName");
                return;
            }

            //// �� �̸� ����
            //string sceneName = urlParts[1];
            //Debug.Log($"Parsed scene name: {sceneName}");

            //// ��ȿ�� �� �̸����� Ȯ�� �� �ε�
            //if (sceneName == "LoginComplete")
            //{
            //    SceneManager.LoadScene(sceneName);
            //}
            //else
            //{
            //    Debug.LogWarning($"Invalid scene name: {sceneName}");
            //}

            // �� �̸� ����
            string sceneName = urlParts[1].Replace("sceneName=", ""); // "LoginComplete" ����
            Debug.Log($"Parsed scene name: {sceneName}");

            // �� �ε�
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
