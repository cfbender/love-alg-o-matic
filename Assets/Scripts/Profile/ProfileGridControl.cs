using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProfileGridControl : MonoBehaviour
{
    public int maxProfileCount = 20;
    public ProfileButtonControl ProfileButtonPrefab;
    private Dictionary<Profile, ProfileButtonControl> _profileButtonControls;

    public Transform ProfileGridUI;
    
    [Header("Chat Bubble")] public ChatBubbleControl ChatBubblePrefab;
    private Transform _canvasTransform;

    private void Awake()
    {
        _canvasTransform = GameObject.Find("Canvas").transform;
    }
    
    public void InitializeProfileButtons()
    {
        ClearExistingProfiles();
        
        for (var i = 0; i < maxProfileCount; i++)
        {
            var profile = new Profile();
            var profileButton = Instantiate(ProfileButtonPrefab, ProfileGridUI);
            profileButton.AssignProfile(profile);
            _profileButtonControls.Add(profile, profileButton);
        }
    }

    public void ActivateProfileButton(Profile profile, bool active)
    {
        _profileButtonControls[profile].SetTileActive(active);
    }

    public void RemoveAndReplaceMatchedPair(Profile profile1, Profile profile2)
    {
       var newProfile1 = new Profile(); 
       var newProfile2 = new Profile();

       _profileButtonControls[newProfile1] = _profileButtonControls[profile1];
       _profileButtonControls[newProfile1].AssignProfile(newProfile1);
       _profileButtonControls[newProfile2] = _profileButtonControls[profile2];
       _profileButtonControls[newProfile2].AssignProfile(newProfile2);
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
        
        var childCount = ProfileGridUI.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(ProfileGridUI.GetChild(i).gameObject);
        }
    }
    
    #region ChatBubbles
    public void DisplayChatBubbleForRandomProfile()
    {
        var profile = _profileButtonControls.Keys.ToList()[Random.Range(0, _profileButtonControls.Count)];
        var profileButton = _profileButtonControls[profile];
        var interest = profile.Interests[Random.Range(0, profile.Interests.Count)];
        var calloutText = interest.uniqueCallouts[Random.Range(0, interest.uniqueCallouts.Length)];

        var chatBubble = Instantiate(ChatBubblePrefab, _canvasTransform);
        chatBubble.transform.position = profileButton.transform.position;
        chatBubble.AssignText(calloutText);
    }
     #endregion
}
