using UnityEngine;
using UnityEngine.UI;
public class ChangeName : MonoBehaviour
{
    public InputField inputField;

    void Awake()
    {
        string userName = PlayerPrefs.GetString("Nickname", "anonymous");
        inputField.text = userName;
    }

    public void NicknameUpdated()
    {
        string newNickname = inputField.text;
        if (string.IsNullOrEmpty(newNickname))
            newNickname = "anonymous";

        PlayerPrefs.SetString("Nickname", newNickname);
        inputField.text = newNickname;

    }
}
