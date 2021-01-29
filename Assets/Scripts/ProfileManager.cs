using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.iOS;
using Random = System.Random;

public class ProfileManager : MonoBehaviour
{
    public int StartingProfileCount = 20;
    public List<Profile> Profiles;

    private void Awake()
    {
        var interestManager = GetComponent<InterestManager>();
        var random = new Random();
        Profiles = Enumerable.Range(1, StartingProfileCount)
            .Select(x => new Profile(interestManager, random)).ToList();
        
        // Profiles.ForEach(profile =>
        // {
        //     Debug.Log(profile.Name);
        //     profile.Interests.ForEach(interest => Debug.Log(interest));
        // });
    }
}
