using System.Collections.Generic;
using UnityEngine;

public class ProfileGridControl : MonoBehaviour
{
    public ProfileButtonControl ProfileButtonPrefab;
    private List<ProfileButtonControl> _profileButtonControls;

    public void InitializeProfileButtons()
    {
        ClearExistingProfiles();
        foreach (var profile in Managers.ProfileManager.profiles)
        {
            var profileButton = Instantiate(ProfileButtonPrefab, transform);
            profileButton.AssignProfile(profile);
        }
    }

    private void ClearExistingProfiles()
    {
        var childCount = transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
