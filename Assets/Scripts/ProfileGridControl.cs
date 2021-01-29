using System.Collections.Generic;
using UnityEngine;

public class ProfileGridControl : MonoBehaviour
{
    public ProfileButtonControl ProfileButtonPrefab;
    private Dictionary<Profile, ProfileButtonControl> _profileButtonControls;

    public void InitializeProfileButtons()
    {
        ClearExistingProfiles();
        
        foreach (var profile in Managers.ProfileManager.profiles)
        {
            var profileButton = Instantiate(ProfileButtonPrefab, transform);
            profileButton.AssignProfile(profile);
            _profileButtonControls.Add(profile, profileButton);
        }
    }

    public void ActivateProfileButton(Profile profile, bool active)
    {
        _profileButtonControls[profile].SetTileActive(active);
    }

    private void ClearExistingProfiles()
    {
        if (_profileButtonControls == null)
        {
            _profileButtonControls = new Dictionary<Profile, ProfileButtonControl>();
        }
        else
        {
            _profileButtonControls.Clear();    
        }
        
        var childCount = transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
