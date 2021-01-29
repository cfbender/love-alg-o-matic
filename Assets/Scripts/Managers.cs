using System;
using System.Linq;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static ProfileManager ProfileManager;
    public static InterestManager InterestManager;

    private void Awake()
    {
        ProfileManager = GetComponent<ProfileManager>();
        InterestManager = GetComponent<InterestManager>();

    }

    private void Start()
    {
        var p1 = ProfileManager.profiles.First();
        var p2 = ProfileManager.profiles.Last();

        var matches = Evaluator.GetMatches(p1, p2);
        
        Debug.Log($"{p1.Name} matched with {p2.Name} on {matches.Count()} interests.");
        Debug.Log($"Matching interests: {String.Join(", ",matches)}");
    }
}
