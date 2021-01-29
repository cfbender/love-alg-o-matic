using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ProfileManager : MonoBehaviour
{
    public int startingProfileCount = 20;
    public ProfileGridControl ProfileGridControl;
    public readonly List<Profile> profiles = new List<Profile>();
    public readonly Random Random = new Random();
    
    [Header("Chat Bubble")]
    public ChatBubbleControl ChatBubblePrefab;
    private Transform _canvasTransform;

    private void Awake()
    {
        _canvasTransform = GameObject.Find("Canvas").transform;
    }

    public void GenerateInitialProfiles()
    {
        for (var i = 0; i < startingProfileCount; i++)
        {
            profiles.Add(new Profile());
        }

        ProfileGridControl.InitializeProfileButtons();
    }

    #region ChatBubbles

    public void DisplayChatBubbleForRandomProfile()
    {
        var profile = profiles[UnityEngine.Random.Range(0, profiles.Count)];
        var interest = profile.Interests[UnityEngine.Random.Range(0, profile.Interests.Count)];
        var calloutText = interest.uniqueCallouts[UnityEngine.Random.Range(0, interest.uniqueCallouts.Length)];

        var chatBubble = Instantiate(ChatBubblePrefab, _canvasTransform);
        chatBubble.transform.position = ProfileGridControl.GetPositionOfProfileButton(profile);
        chatBubble.AssignText(calloutText + profile.Name);
    }

    #endregion
}