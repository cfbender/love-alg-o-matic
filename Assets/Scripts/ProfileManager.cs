using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public int StartingProfileCount = 20;
    public List<Profile> Profiles;
    
    void Start()
    {
        Profiles = new List<Profile>();
        
        // loop to create profiles and add to list
    }
}
