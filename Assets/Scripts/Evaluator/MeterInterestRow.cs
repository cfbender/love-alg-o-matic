using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeterInterestRow : MonoBehaviour
{
    public TextMeshProUGUI Profile1InterestTextObject;
    public Image HeartImage;
    public TextMeshProUGUI Profile2InterestTextObject;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public string defaultText = "?????";

    public void DisplayMatch(string match)
    {
        HeartImage.sprite = fullHeart;
        Profile1InterestTextObject.text = match;
        Profile2InterestTextObject.text = match;
    }

    public void DisplayMismatch(string interest1, string interest2)
    {
        HeartImage.sprite = emptyHeart;
        Profile1InterestTextObject.text = interest1;
        Profile2InterestTextObject.text = interest2;
    }

    public void Reset()
    {
        Profile1InterestTextObject.text = defaultText;
        Profile2InterestTextObject.text = defaultText;
        HeartImage.sprite = emptyHeart;
    }
}