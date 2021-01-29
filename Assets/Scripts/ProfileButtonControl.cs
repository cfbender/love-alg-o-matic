using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButtonControl : MonoBehaviour
{
    public Color NormalColor;
    public Color SelectedColor;
    
    private Profile _profile;
    
    private TextMeshProUGUI _nameText;
    private Image _tileImage;

    public void AssignProfile(Profile profile)
    {
        _nameText = GetComponentInChildren<TextMeshProUGUI>();
        _tileImage = GetComponent<Image>();
        _profile = profile;
        _nameText.text = profile.Name;
    }

    public void OnProfileButtonClick()
    {
        Debug.Log($"{_profile.Name} clicked");
        Managers.GameManager.SelectProfile(_profile);
    }

    public void SetTileActive(bool active)
    {
        _tileImage.color = active ? SelectedColor : NormalColor;
    }
}
