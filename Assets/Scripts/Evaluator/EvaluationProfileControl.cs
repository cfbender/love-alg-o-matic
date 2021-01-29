using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EvaluationProfileControl : MonoBehaviour
{
    public Sprite profilePlaceholder;
    public Sprite emptySprite;
    public Image profileImage;
    private TextMeshProUGUI _nameText;
    private Profile _profile;

    private void Start()
    {
        _nameText = GetComponentInChildren<TextMeshProUGUI>();
        ClearProfile();
    }
    
    public void AssignProfile(Profile profile)
    {
        _profile = profile;
        _nameText.text = profile.Name;
        profileImage.sprite = profilePlaceholder;
    }

    public void ClearProfile()
    {
        _profile = null;
        _nameText.text = string.Empty;
        profileImage.sprite = emptySprite;
    }
}