using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProfileGridControl : MonoBehaviour
{
    public int maxProfileCount = 20;
    public ProfileButtonControl ProfileButtonPrefab;
    private Dictionary<Profile, ProfileButtonControl> _profileButtonControls;

    public Transform ProfileGridUI;

    [Header("Chat Bubble")]
    public Vector2 ChatBubbleOffset;
    public ChatBubbleControl ChatBubblePrefab;
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
            profileButton.AssignProfile(profile, i);
            _profileButtonControls.Add(profile, profileButton);
        }
    }

    public void ActivateProfileButton(Profile profile, bool active)
    {
        _profileButtonControls[profile].SetTileActive(active);
        Managers.SoundManager.PlaySFX("profile " + (active == true ? "select" : "deselect"));
    }

    public void RemoveAndReplaceMatchedPair(Profile profile1, Profile profile2)
    {
        var newProfile1 = new Profile();
        var newProfile2 = new Profile();
        
        var button1 =_profileButtonControls[profile1];
        var button2 = _profileButtonControls[profile2];

        _profileButtonControls.Remove(profile1);
        _profileButtonControls.Remove(profile2);
        
        button1.AssignProfile(newProfile1);
        button2.AssignProfile(newProfile2);
        
        _profileButtonControls.Add(newProfile1, button1);
        _profileButtonControls.Add(newProfile2, button2);
    }

    public void ActivateGridButtons()
    {
        foreach (var kvp in _profileButtonControls)
        {
            kvp.Value.GetComponent<Button>().interactable = true;
        }
    }
    
    public void DisableGridButtons()
    {
        foreach (var kvp in _profileButtonControls)
        {
            kvp.Value.GetComponent<Button>().interactable = false;
        }
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
        chatBubble.transform.position = (Vector2)profileButton.transform.position + ChatBubbleOffset;
        chatBubble.AssignText(calloutText);
    }
    #endregion
}
