using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ProfileManager : MonoBehaviour
{
    public int startingProfileCount = 20;
    public List<Profile> profiles = new List<Profile>();

    private void Awake()
    {
        var interestManager = GetComponent<InterestManager>();
        var random = new Random();
        for (var i = 0; i < startingProfileCount; i++)
        {
            profiles.Add(new Profile(interestManager, random));
        }
    }
}