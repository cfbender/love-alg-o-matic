using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubbleControl : MonoBehaviour
{
    public Vector2 BackgroundOffset;
    private Image _backgroundImage;
    private TextMeshProUGUI _chatTextMesh;

    private void Start()
    {
        _backgroundImage = GetComponentInChildren<Image>();
        _chatTextMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void AssignText(string text)
    {
        _chatTextMesh.SetText(text);
        _chatTextMesh.ForceMeshUpdate();

        Vector2 textSize = _chatTextMesh.GetRenderedValues(false);
        _backgroundImage.rectTransform.sizeDelta = textSize + BackgroundOffset;
    }
}
