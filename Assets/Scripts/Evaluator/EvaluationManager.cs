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
    public LoveMeterControl loveMeterControl;

    public void AssignEvaluationProfile(Profile profile, bool first)
    {
        var profileControl = first ? evaluationProfileControl1 : evaluationProfileControl2;
        profileControl.AssignProfile(profile);
    }

    public void ClearEvaluationProfiles()
    {
        evaluationProfileControl1.ClearProfile();
        evaluationProfileControl2.ClearProfile();
        loveMeterControl.Reset();
        ResultsText.text = "Awaiting Selections...";
    }

    public int PerformEvaluation(Profile profile1, Profile profile2)
    {
        var result = Managers.EvaluationManager.GetMatches(profile1, profile2);
        var matchCount = result.Matches.Count;
        loveMeterControl.UpdateLoveMeter(result, matchCount);
        Managers.ScoreManager.AddMatchedPair(profile1, profile2, result, matchCount < 3);

        return matchCount;
    }

    public EvaluationResult GetMatches(Profile p1, Profile p2)
    {
        var p1Interests = p1.Interests.OrderBy(interest => interest.name).Select(i => i.name).ToList();
        var p2Interests = p2.Interests.OrderBy(interest => interest.name).Select(i => i.name).ToList();

        return new EvaluationResult(p1Interests, p2Interests);
    }
}

public class EvaluationResult
{
    public List<string> P1Misses;
    public List<string> P2Misses;
    public List<string> Matches;

    public EvaluationResult(List<string> p1Interests, List<string> p2Interests)
    {
        Matches = p1Interests.Where(p2Interests.Contains).ToList();
        P1Misses = p1Interests.Where(interest => !p2Interests.Contains(interest)).ToList();
        P2Misses = p2Interests.Where(interest => !p1Interests.Contains(interest)).ToList();
    }
}