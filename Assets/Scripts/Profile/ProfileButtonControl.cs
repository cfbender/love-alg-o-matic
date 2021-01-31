using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButtonControl : MonoBehaviour
{
    public Color NormalColor;
    public Color SelectedColor;

    [Header("Animation Settings")]
    public float InitialDelay = .1f;
    public float SoundInitialDelay = .0999f;
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
            AssignProfileSprites();
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

        var soundSequence = DOTween.Sequence();
        soundSequence.AppendInterval(SoundInitialDelay * index);
        soundSequence.AppendCallback(() => Managers.SoundManager.PlaySFX("profile in"));
    }

    private void PerformProfileSwapAnimation(Profile newProfile)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(Vector3.zero, OutroDuration).SetEase(OutroEaseType));
        sequence.AppendCallback(() => _profile = newProfile);
        sequence.AppendCallback(AssignProfileSprites);
        sequence.Append(transform.DOScale(Vector3.one, IntroDuration).SetEase(IntroEaseType));
    }

    #region ProfileImage

    private Image BackgroundImageComponent;
    private Image BodyImageComponent;
    private Image EyesImageComponent;
    private Image FacialHairImageComponent;
    private Image HairImageComponent;
    private Image HatImageComponent;
    private Image HeadImageComponent;
    private Image MouthImageComponent;

    private void AssignProfileSprites()
    {
        if (BackgroundImageComponent == null)
        {
            AssignComponents();
        }

        BackgroundImageComponent.sprite = Managers.ProfileSpriteManager.Backgrounds[_profile.BackgroundId];
        BodyImageComponent.sprite = Managers.ProfileSpriteManager.Bodies[_profile.BodyId];
        EyesImageComponent.sprite = Managers.ProfileSpriteManager.Eyes[_profile.EyesId];
        HeadImageComponent.sprite = Managers.ProfileSpriteManager.Heads[_profile.HeadId];
        MouthImageComponent.sprite = Managers.ProfileSpriteManager.Mouths[_profile.MouthId];

        if (_profile.FacialHairId == -1)
        {
            FacialHairImageComponent.enabled = false;
        }
        else
        {
            FacialHairImageComponent.enabled = true;
            FacialHairImageComponent.sprite = Managers.ProfileSpriteManager.FacialHairs[_profile.FacialHairId];
        }

        if (_profile.HairId == -1)
        {
            HairImageComponent.enabled = false;
        }
        else
        {
            HairImageComponent.enabled = true;
            HairImageComponent.sprite = Managers.ProfileSpriteManager.Hairs[_profile.HairId];
        }

        if (_profile.HatId == -1)
        {
            HatImageComponent.enabled = false;
        }
        else
        {
            HatImageComponent.enabled = true;
            HatImageComponent.sprite = Managers.ProfileSpriteManager.Hats[_profile.HatId];
        }
    }

    private void AssignComponents()
    {
        BackgroundImageComponent = transform.Find("Background").GetComponent<Image>();
        BodyImageComponent = transform.Find("Body").GetComponent<Image>();
        EyesImageComponent = transform.Find("Eyes").GetComponent<Image>();
        FacialHairImageComponent = transform.Find("Facial Hair").GetComponent<Image>();
        HairImageComponent = transform.Find("Hair").GetComponent<Image>();
        HatImageComponent = transform.Find("Hat").GetComponent<Image>();
        HeadImageComponent = transform.Find("Head").GetComponent<Image>();
        MouthImageComponent = transform.Find("Mouth").GetComponent<Image>();
    }

    #endregion
}
