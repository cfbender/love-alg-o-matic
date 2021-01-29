using System.Collections.Generic;

public class Profile
{
    public string Name { get; }
    public List<InterestTemplate> Interests { get; }

    private readonly List<string> Names = new List<string>() {"Jimmy Joey", "Tina Tuna", "Paulie Porkchop", "Chris Christie", "Lilly Lollipop", "Sean Swordfish"};
    public Profile()
    {
       Interests = Managers.InterestManager.GetInterests();
       Name = Names[Managers.ProfileManager.Random.Next(Names.Count)];
    }
}