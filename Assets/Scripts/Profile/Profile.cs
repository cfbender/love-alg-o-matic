using System;
using System.Collections.Generic;

public class Profile
{
    public Guid Id { get; }
    public string Name { get; }
    public List<InterestTemplate> Interests { get; }

    public Profile()
    {
        Id = Guid.NewGuid();
        Interests = Managers.InterestManager.GetInterests();
        Name = Managers.GrammarManager.GetRandomName();
    }
}