using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ProfileGridControl : MonoBehaviour
{
    public int maxProfileCount = 20;
    public ProfileButtonControl ProfileButtonPrefab;
    public Dictionary<Profile, ProfileButtonControl> ProfileButtonControls;

    public Transform ProfileGridUI;

    [Header("Chat Bubble")] public Vector2 ChatBubbleOffset;
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
            ProfileButtonControls.Add(profile, profileButton);
        }
    }

    public void ActivateProfileButton(Profile profile, bool active)
    {
        if (profile == null) return;
        ProfileButtonControls[profile].SetTileActive(active);
        Managers.SoundManager.PlaySFX("profile " + (active == true ? "select" : "deselect"));
    }

    public void RemoveAndReplaceMatchedPair(Profile profile1, Profile profile2)
    {
        var newProfile1 = new Profile();
        var newProfile2 = new Profile();

        var button1 = ProfileButtonControls[profile1];
        var button2 = ProfileButtonControls[profile2];

        ProfileButtonControls.Remove(profile1);
        ProfileButtonControls.Remove(profile2);

        button1.AssignProfile(newProfile1);
        button2.AssignProfile(newProfile2);

        ProfileButtonControls.Add(newProfile1, button1);
        ProfileButtonControls.Add(newProfile2, button2);
    }

    public void ActivateGridButtons()
    {
        foreach (var kvp in ProfileButtonControls)
        {
            kvp.Value.GetComponent<Button>().interactable = true;
        }
    }

    public void DisableGridButtons()
    {
        foreach (var kvp in ProfileButtonControls)
        {
            kvp.Value.GetComponent<Button>().interactable = false;
        }
    }

    private void ClearExistingProfiles()
    {
        if (ProfileButtonControls == null)
        {
            ProfileButtonControls = new Dictionary<Profile, ProfileButtonControl>();
        }
        else
        {
            ProfileButtonControls.Clear();
        }

        var childCount = ProfileGridUI.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(ProfileGridUI.GetChild(i).gameObject);
        }
    }

    #region ChatBubbles

    public (Profile, ChatBubbleControl chatBubble) DisplayChatBubbleForRandomProfile(
        IEnumerable<Profile> bubbleProfiles)
    {
        var bubbleList = bubbleProfiles.ToList();
        var profile = bubbleList.ToList()[Random.Range(0, bubbleList.Count)];
        var profileButton = ProfileButtonControls[profile];
        var interest = profile.Interests[Random.Range(0, profile.Interests.Count)];
        var calloutText = interest.uniqueCallouts[Random.Range(0, interest.uniqueCallouts.Length)];
        var chatBubble = Instantiate(ChatBubblePrefab, _canvasTransform);
        chatBubble.transform.position = (Vector2) profileButton.transform.position + ChatBubbleOffset;
        chatBubble.AssignText(calloutText);
        chatBubble.AssignProfile(profile);

        return (profile, chatBubble);
    }

    #endregion
}