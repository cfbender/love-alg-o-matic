using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubbleControl : MonoBehaviour
{
    public Vector2 BackgroundOffset;
    public float DestroyDelay = 3f;
    private Image _backgroundImage;
    private TextMeshProUGUI _chatTextMesh;

    private void Start()
    {
        StartCoroutine(DelayedDestroy());
        Managers.GameManager.ChatBubbleAdded();
    }

    public void AssignText(string text)
    {
        _backgroundImage = GetComponentInChildren<Image>();
        _chatTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        
        _chatTextMesh.SetText(text);
        _chatTextMesh.ForceMeshUpdate();

        Vector2 textSize = _chatTextMesh.GetRenderedValues(false);
        _backgroundImage.rectTransform.sizeDelta = textSize + BackgroundOffset;
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(DestroyDelay);

        Managers.GameManager.ChatBubbleRemoved();
        Destroy(gameObject);
    }
}
