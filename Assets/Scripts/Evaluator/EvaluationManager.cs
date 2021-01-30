using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EvaluationManager : MonoBehaviour
{
    public EvaluationProfileControl evaluationProfileControl1;
    public EvaluationProfileControl evaluationProfileControl2;
    public TextMeshProUGUI ResultsText;

    public void AssignEvaluationProfile(Profile profile, bool first)
    {
        var profileControl = first ? evaluationProfileControl1 : evaluationProfileControl2;
        profileControl.AssignProfile(profile);
    }

    public void ClearEvaluationProfiles()
    {
        evaluationProfileControl1.ClearProfile();
        evaluationProfileControl2.ClearProfile();
        ResultsText.text = "Awaiting Selections...";
    }

    public int PerformEvaluation(Profile profile1, Profile profile2)
    {
        var matches = Managers.EvaluationManager.GetMatches(profile1, profile2);
        var matchCount = matches.Count();

        ResultsText.text = $"{profile1.Name} and {profile2.Name} have {matchCount} matching interests.\n\n" +
                           $"{String.Join(" \n", matches)}";

        Managers.ScoreManager.AddMatchedPair(profile1, profile2, matchCount < 3);

        return matchCount;
    }

    public IEnumerable<string> GetMatches(Profile p1, Profile p2)
    {
        return p1.Interests.Where(interest => p2.Interests.Contains(interest)).Select(interest => interest.name);
    }
}