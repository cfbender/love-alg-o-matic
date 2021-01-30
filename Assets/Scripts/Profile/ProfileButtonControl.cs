using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButtonControl : MonoBehaviour
{
    public Color NormalColor;
    public Color SelectedColor;

    [Header("Animation Settings")] 
    public float InitialDelay = .1f;
    public Vector3 SmallScale = new Vector3(.1f, .1f, 1);
    public float IntroDuration = 0.5f;
    public Ease IntroEaseType = Ease.OutBack;
    
    public float OutroDuration = 0.5f;
    public Ease OutroEaseType = Ease.OutQuint;
    
    private Profile _profile;
    
    private Image _tileImage;

    public void AssignProfile(Profile profile, int index = 0)
    {
        _tileImage = GetComponent<Image>();

        if (_profile == null)
        {
            _profile = profile;
            PerformIntroSequence(index);    
        }
        else
        {
            PerformProfileSwapAnimation(profile);
        }
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

    private void PerformIntroSequence(int index)
    {
        transform.localScale = SmallScale;

        var sequence = DOTween.Sequence();
        sequence.AppendInterval(InitialDelay * index);
        sequence.Append(transform.DOScale(Vector3.one, IntroDuration).SetEase(IntroEaseType));
    }

    private void PerformProfileSwapAnimation(Profile newProfile)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(Vector3.zero, OutroDuration).SetEase(OutroEaseType));
        sequence.AppendCallback(() => _profile = newProfile);
        sequence.Append(transform.DOScale(Vector3.one, IntroDuration).SetEase(IntroEaseType));
    }
}
