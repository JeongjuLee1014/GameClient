using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text starText;
    [SerializeField] private TMP_Text energyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 초기 데이터 설정
        //User.Instance.UpdateUserData("1", "Player", "Session123", 12345, 67, 89);
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        // 텍스트 업데이트
        coinText.text = User.Instance.numCoins.ToString();
        starText.text = User.Instance.numStars.ToString();
        energyText.text = User.Instance.numEnergies.ToString();
    }

    //예시
    public void AddItem(string itemType)
    {
        if (itemType == "Coin")
        {
            User.Instance.numCoins++;
        }
        else if (itemType == "Star")
        {
            User.Instance.numStars++;
        }
        else if (itemType == "Energy")
        {
            User.Instance.numEnergies++;
        }
        UpdateUI();
    }
}
