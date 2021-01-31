using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationProfileControl : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private Profile _profile;

    private void Start()
    {
        ClearProfile();
    }

    public void AssignProfile(Profile profile)
    {
        _profile = profile;
        nameText.text = profile.Name;
        AssignProfileSprites();
    }

    public void ClearProfile()
    {
        _profile = null;
        nameText.text = string.Empty;
        SetProfileSpritesActive(false);
    }


    #region ProfileImage

    public Image BackgroundImageComponent;
    public Image BodyImageComponent;
    public Image EyesImageComponent;
    public Image FacialHairImageComponent;
    public Image HairImageComponent;
    public Image HatImageComponent;
    public Image HeadImageComponent;
    public Image MouthImageComponent;

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

    #endregion
}