using System.Collections.Generic;
using UnityEngine;

public class ProfileSpriteManager : MonoBehaviour
{
    [Range(0, 1)] public float FacialHairChance = 0.55f;
    [Range(0, 1)] public float BaldChance = 0.2f;
    [Range(0, 1)] public float HatChance = 0.3f;
    
    public List<Sprite> Backgrounds;
    public List<Sprite> Bodies;
    public List<Sprite> Eyes;
    public List<Sprite> FacialHairs;
    public List<Sprite> Hairs;
    public List<Sprite> Hats;
    public List<Sprite> Heads;
    public List<Sprite> Mouths;

    public void AssignProfilePicture(Profile profile)
    {
        profile.BackgroundId = Random.Range(0, Backgrounds.Count);
        profile.BodyId = Random.Range(0, Bodies.Count);
        profile.EyesId = Random.Range(0, Eyes.Count);
        profile.HeadId = Random.Range(0, Heads.Count);
        profile.MouthId = Random.Range(0, Mouths.Count);

        profile.FacialHairId = Random.Range(0f, 1f) <= FacialHairChance ? Random.Range(0, FacialHairs.Count) : -1;
        profile.HairId = Random.Range(0f, 1f) >= BaldChance ? Random.Range(0, Hairs.Count) : -1;
        profile.HatId = Random.Range(0f, 1f) >= HatChance ? Random.Range(0, Hats.Count) : -1;
    }
}
