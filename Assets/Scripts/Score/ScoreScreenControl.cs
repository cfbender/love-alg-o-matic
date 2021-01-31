using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreScreenControl : MonoBehaviour
{
    public float profileDisplayDelay = 5;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI bestStreakText;
    public TextMeshProUGUI worstStreakText;
    public EvaluationProfileControl evaluationProfileControl1;
    public EvaluationProfileControl evaluationProfileControl2;
    public LoveMeterControl loveMeterControl;

    private bool _scoreScreenActive = false;
    private float _profileDisplayTimer;

    public void InitializeScoreScreen()
    {
        bestStreakText.text = $"Best Streak: {Managers.ScoreManager.longestSuccessStreak} matches";
        worstStreakText.text = $"Worst Streak: {Managers.ScoreManager.longestMissedStreak} mismatches";
        gameObject.SetActive(true);
        var successfulMatches = Managers.ScoreManager.matchCount > 0;
        if (successfulMatches)
        {
            DisplayMatchedPair();
        }

        evaluationProfileControl1.gameObject.SetActive(successfulMatches);
        evaluationProfileControl2.gameObject.SetActive(successfulMatches);
        loveMeterControl.gameObject.SetActive(successfulMatches);
        _scoreScreenActive = true;
        _profileDisplayTimer = 0;
    }

    public void DisableScoreScreen()
    {
        _scoreScreenActive = false;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_scoreScreenActive) return;

        _profileDisplayTimer += Time.deltaTime;

        if (_profileDisplayTimer >= profileDisplayDelay)
        {
            _profileDisplayTimer = 0;
            DisplayMatchedPair();
        }
    }

    public void DisplayMatchedPair()
    {
        var match = Managers.ScoreManager.matches[Random.Range(0, Managers.ScoreManager.matches.Count)];

        evaluationProfileControl1.AssignProfile(match.Profile1);
        evaluationProfileControl2.AssignProfile(match.Profile2);

        loveMeterControl.UpdateLoveMeter(match.EvaluationResult, match.EvaluationResult.Matches.Count);
    }
}