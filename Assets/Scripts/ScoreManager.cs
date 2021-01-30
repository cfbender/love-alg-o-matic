using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public List<MatchedProfile> matches = new List<MatchedProfile>();
    public List<MatchedProfile> failures = new List<MatchedProfile>();
    public int matchCount;
    public int failureCount;
    public int maxFailures = 3;
    public int maxMatches = 10;
    
    public void AddMatchedPair(Profile profile1, Profile profile2, bool failure)
    {
        var list = failure ? failures : matches;
       list.Add(new MatchedProfile(profile1, profile2)); 
       matchCount = matches.Count;
       failureCount = failures.Count;
       GenerateScoreText();
    }

    private void GenerateScoreText()
    {
        ScoreText.text = $"Successful Matches: {matchCount} \n" +
                         $"Unsuccessful Matches: {failureCount}";
    }
    
}