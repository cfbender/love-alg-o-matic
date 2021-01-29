using TMPro;
using UnityEngine;

public class ProfileButtonControl : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    private Profile _profile;

    public void AssignProfile(Profile profile)
    {
        _profile = profile;
        NameText.text = profile.Name;
    }

    public void OnProfileButtonClick()
    {
        Debug.Log($"{_profile.Name} clicked");
        Managers.GameManager.SelectProfile(_profile);
    }
}
