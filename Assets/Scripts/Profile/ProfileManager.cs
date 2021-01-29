using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ProfileManager : MonoBehaviour
{
    public int startingProfileCount = 20;
    public ProfileGridControl ProfileGridControl;
    public readonly List<Profile> profiles = new List<Profile>();
    public readonly Random Random = new Random();

    public void GenerateInitialProfiles()
    {
        for (var i = 0; i < startingProfileCount; i++)
        {
            profiles.Add(new Profile());
        }

        ProfileGridControl.InitializeProfileButtons();
    }
}