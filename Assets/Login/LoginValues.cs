using UnityEngine;
using System.IO;

public class LoginValues
{
    public bool isLogined;
    public string sessionId;

    private static string filePath = "./Assets/Login/loginValues.json";
    public static LoginValues Get()
    {
        string loginValuesString = File.ReadAllText(filePath);
        return JsonUtility.FromJson<LoginValues>(loginValuesString);
    }

    public static void Set(LoginValues newValues)
    {
        string updatedJson = JsonUtility.ToJson(newValues);
        File.WriteAllText(filePath, updatedJson);
    }
}