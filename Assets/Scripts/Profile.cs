using System;
using System.Collections.Generic;

public class Profile
{
    public string Name { get; }
    public List<InterestTemplate> Interests { get; }

    private readonly List<string> Names = new List<string>() {"Jimmy Joey", "Tina Tuna", "Paulie Porkchop", "Chris Christie", "Lilly Lollipop", "Sean Swordfish"};
    public Profile(InterestManager interestManager, Random random)
    {
       Interests = interestManager.GetInterests();
       Name = Names[random.Next(Names.Count)];
    }
}