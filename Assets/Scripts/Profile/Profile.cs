using System;
using System.Collections.Generic;

public class Profile
{
    public Guid Id { get; }
    public string Name { get; }
    public List<InterestTemplate> Interests { get; }

    private readonly List<string> Names = new List<string>()
        {"Jimmy Joey", "Tina Tuna", "Paulie Porkchop", "Chris Christie", "Lilly Lollipop", "Sean Swordfish"};

    public Profile()
    {
        Id = Guid.NewGuid();
        Interests = Managers.InterestManager.GetInterests();
        Name = Names[UnityEngine.Random.Range(0, Names.Count)];
    }
}