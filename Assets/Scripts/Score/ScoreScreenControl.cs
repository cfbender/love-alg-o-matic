using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreScreenControl : MonoBehaviour
{
    public float profileDisplayDelay = 5;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI bestStreakText;
    public TextMeshProUGUI worstStreakText;
    public EvaluationProfileControl evaluationProfileControl1;
    public EvaluationProfileControl evaluationProfileControl2;
    public LoveMeterControl loveMeterControl;

    public void InitializeScoreScreen()
    {
        bestStreakText.text = $"Best Streak: {Managers.ScoreManager.longestSuccessStreak} matches";
        worstStreakText.text = $"Worst Streak: {Managers.ScoreManager.longestMissedStreak} mismatches";
        gameObject.SetActive(true);
        DisplayMatchedPair();
    }

    public void DisplayMatchedPair()
    {
        var match = Managers.ScoreManager.matches[Random.Range(0, Managers.ScoreManager.matches.Count)];

        evaluationProfileControl1.AssignProfile(match.Profile1);
        evaluationProfileControl2.AssignProfile(match.Profile2);

        loveMeterControl.UpdateLoveMeter(match.EvaluationResult, match.EvaluationResult.Matches.Count);

        StartCoroutine(DelayedDisplay());
    }

    private IEnumerator DelayedDisplay()
    {
        yield return new WaitForSeconds(profileDisplayDelay);

        DisplayMatchedPair();
    }
}