using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationProfileControl : MonoBehaviour
{
    public Sprite profilePlaceholder;
    public Sprite emptySprite;
    private TextMeshProUGUI _nameText;
    private Profile _profile;

    private void Start()
    {
        _nameText = GetComponentInChildren<TextMeshProUGUI>();
        AssignComponents();
        ClearProfile();
    }

    public void AssignProfile(Profile profile)
    {
        _profile = profile;
        _nameText.text = profile.Name;
        AssignProfileSprites();
    }

    public void ClearProfile()
    {
        _profile = null;
        _nameText.text = string.Empty;
        SetProfileSpritesActive(false);
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

        SetProfileSpritesActive(true);
    }

    private void SetProfileSpritesActive(bool active)
    {
        BackgroundImageComponent.gameObject.SetActive(active);
        BodyImageComponent.gameObject.SetActive(active);
        EyesImageComponent.gameObject.SetActive(active);
        HeadImageComponent.gameObject.SetActive(active);
        MouthImageComponent.gameObject.SetActive(active);
        FacialHairImageComponent.gameObject.SetActive(active);
        HairImageComponent.gameObject.SetActive(active);
        HatImageComponent.gameObject.SetActive(active);
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