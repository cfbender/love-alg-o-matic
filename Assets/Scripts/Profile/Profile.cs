using System;
using System.Collections.Generic;

public class Profile
{
    public Guid Id { get; }
    public string Name { get; }
    public List<InterestTemplate> Interests { get; }

    public int BackgroundId;
    public int BodyId;
    public int EyesId;
    public int FacialHairId;
    public int HairId;
    public int HatId;
    public int HeadId;
    public int MouthId;

    public Profile()
    {
        Id = Guid.NewGuid();
        Interests = Managers.InterestManager.GetInterests();
        Name = Managers.GrammarManager.GetRandomName();
        Managers.ProfileSpriteManager.AssignProfilePicture(this);
    }
}