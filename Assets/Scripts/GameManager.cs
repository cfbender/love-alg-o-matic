using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public float SelectionResetDelay = 2;
    private Profile _selectedProfile1;
    private Profile _selectedProfile2;
    private bool _allowSelection = true;

    public float ChatBubbleCooldown = 5;
    private float _chatBubbleTimer;
    private bool _chatBubbleCooldownActive;

    [Header("Ready State Settings")]
    public GameObject ReadyOverlay;
    public float ReadyDelay = 3f;
    private bool ReadyScreenActive;
    private TextMeshProUGUI ReadyCountDownText;
    private float _readyTextTimer;
    public float InitDelay = 3f;
    private bool _gameActive;

    [Header("Round Time")] 
    public TextMeshProUGUI TimerText;
    public float RoundTimeMax = 180;
    private float _roundTimer;
    private bool _roundOver;

    private void Start()
    {
       BeginNewRound();
    }

    private void Update()
    {
        if (ReadyScreenActive)
        {
            _readyTextTimer += Time.deltaTime;
            ReadyCountDownText.text = Mathf.Ceil(ReadyDelay - _readyTextTimer).ToString();
        }
        
        if (!_gameActive) return;
        
        HandleRoundTimer();

        if (_roundOver) return;
        
        HandleChatBubbles();
    }

    public void BeginNewRound()
    {
        Managers.ScoreManager.HideFinalScoreScreen();
        Managers.ScoreManager.ResetScores();
        StartCoroutine(DelayedProfileInit());
        ReadyOverlay.SetActive(true);
        ReadyScreenActive = true;
        ReadyCountDownText = ReadyOverlay.transform.Find("Ready Count Down Text").GetComponent<TextMeshProUGUI>();
        _roundTimer = 0;
        _readyTextTimer = 0;
        _roundOver = false;
    }

    private IEnumerator DelayedProfileInit()
    {
        yield return new WaitForSeconds(ReadyDelay);
        
        ReadyOverlay.SetActive(false);
        ReadyScreenActive = false;
        Managers.ProfileGridControl.InitializeProfileButtons();
        Managers.ProfileGridControl.DisableGridButtons();
        
        yield return new WaitForSeconds(InitDelay);
        
        _gameActive = true;
        Managers.ProfileGridControl.ActivateGridButtons();
    }

    #region Timer

    private void HandleRoundTimer()
    {
        _roundTimer += Time.deltaTime;
        var timeSpan = TimeSpan.FromSeconds(RoundTimeMax - _roundTimer);
        TimerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        
        if (_roundTimer >= RoundTimeMax)
        {
            _gameActive = false;
            _roundOver = true;
            Managers.ProfileGridControl.DisableGridButtons();
            Managers.ScoreManager.ShowFinalScoreScreen();
            _selectedProfile1 = null;
            _selectedProfile2 = null;
        }
    }

    #endregion

    #region ProfileGridChatBubbles

    private void HandleChatBubbles()
    {
        if (_chatBubbleCooldownActive) // Don't display too many chat bubbles at once
        {
            _chatBubbleTimer += Time.deltaTime;

            if (_chatBubbleTimer < ChatBubbleCooldown) return;

            _chatBubbleCooldownActive = false;
        }

        ;

        if (Random.Range(0f, 1f) <= .9f) return; // small chance each frame to display a new chat bubble

        Managers.ProfileGridControl.DisplayChatBubbleForRandomProfile();
        _chatBubbleCooldownActive = true;
        _chatBubbleTimer = 0;
    }

    #endregion

    #region ProfileSelection

    public void SelectProfile(Profile profile)
    {
        if (!_allowSelection) return;

        if (_selectedProfile1 == null)
        {
            _selectedProfile1 = profile;
            Managers.ProfileGridControl.ActivateProfileButton(profile, true);
            Managers.EvaluationManager.AssignEvaluationProfile(profile, true);
            Debug.Log($"{profile.Name} set to <color=blue>_selectedProfile1</color>");
            return;
        }

        // cancel selection
        if (_selectedProfile1 == profile)
        {
            _selectedProfile1 = null;
            Managers.ProfileGridControl.ActivateProfileButton(profile, false);
            Managers.EvaluationManager.ClearEvaluationProfiles();
            Debug.Log($"{profile.Name} removed from _selectedProfile1");
            return;
        }

        _selectedProfile2 = profile;
        Managers.ProfileGridControl.ActivateProfileButton(profile, true);
        Managers.EvaluationManager.AssignEvaluationProfile(profile, false);
        Debug.Log($"{profile.Name} set to <color=teal>_selectedProfile2</color>");
        _allowSelection = false;


        var matchCount = Managers.EvaluationManager.PerformEvaluation(_selectedProfile1, _selectedProfile2);

        StartCoroutine(DelayedReset(matchCount));
    }

    private IEnumerator DelayedReset(int matchCount)
    {
        yield return new WaitForSeconds(SelectionResetDelay);

        // reset selected profiles and buttons
        Managers.ProfileGridControl.ActivateProfileButton(_selectedProfile1, false);
        Managers.ProfileGridControl.ActivateProfileButton(_selectedProfile2, false);

        var successful = matchCount > 2;

        if (successful)
        {
            Managers.ProfileGridControl.RemoveAndReplaceMatchedPair(_selectedProfile1, _selectedProfile2);
        }

        Managers.SoundManager.PlaySFX("match " + (successful == true ? "success" : "fail"));

        Managers.ScoreManager.UpdateStreaks(successful);

        _selectedProfile1 = null;
        _selectedProfile2 = null;
        
        Managers.EvaluationManager.ClearEvaluationProfiles();

        _allowSelection = true;
    }

    #endregion
}