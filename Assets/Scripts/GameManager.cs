using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Ready State Settings")] public GameObject ReadyOverlay;
    public float ReadyDelay = 3f;
    private bool ReadyScreenActive;
    private TextMeshProUGUI ReadyCountDownText;
    private float _readyTextTimer;
    public float InitDelay = 3f;
    private bool _gameActive;

    [Header("Round Time")] public TextMeshProUGUI TimerText;
    public float RoundTimeMax = 180;
    private float _roundTimer;
    private bool _roundOver;

    public GameObject titleScreenObject;

    private void Start()
    {
        titleScreenObject.SetActive(true);
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
        StartCoroutine(CountdownSounds());
        ReadyOverlay.SetActive(true);
        ReadyScreenActive = true;
        ReadyCountDownText = ReadyOverlay.transform.Find("Ready Count Down Text").GetComponent<TextMeshProUGUI>();
        _roundTimer = 0;
        _readyTextTimer = 0;
        _roundOver = false;
    }

    private IEnumerator CountdownSounds()
    {
        Managers.SoundManager.StopMusic();

        Managers.SoundManager.PlaySFX("tick tock");
        yield return new WaitForSeconds(1.0f);
        Managers.SoundManager.PlaySFX("tick tock");
        yield return new WaitForSeconds(1.0f);
        Managers.SoundManager.PlaySFX("tick tock");


        yield return new WaitForSeconds(0.3f);
        Managers.SoundManager.PlayVA("quip");

        yield return new WaitForSeconds(0.7f);
        Managers.SoundManager.PlaySFX("tick tock", 3.0f);

        yield return new WaitForSeconds(0.5f);
        Managers.SoundManager.PlayMusic("during");
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
            RemoveAllChatBubbles();
            Managers.ScoreManager.ShowFinalScoreScreen();
            _selectedProfile1 = null;
            _selectedProfile2 = null;
        }
    }

    #endregion

    #region ProfileGridChatBubbles

    [Header("Chat Bubble Settings")] public int MaxChatBubbles = 4;
    [Range(0, 1)] public float ChatBubbleChancePerFrame = 0.9f;
    private Dictionary<Profile, ChatBubbleControl> chatBubbles = new Dictionary<Profile, ChatBubbleControl>();

    private void HandleChatBubbles()
    {
        if (chatBubbles.Count >= MaxChatBubbles) // Don't display too many chat bubbles at once
        {
            return;
        }

        if (Random.Range(0f, 1f) <= ChatBubbleChancePerFrame)
            return; // small chance each frame to display a new chat bubble

        var profilesWithoutBubbles =
            Managers.ProfileGridControl.ProfileButtonControls.Keys.Where(profile => !chatBubbles.ContainsKey(profile));
        var (bubbleId, chatBubble) =
            Managers.ProfileGridControl.DisplayChatBubbleForRandomProfile(profilesWithoutBubbles);
        chatBubbles.Add(bubbleId, chatBubble);
        _chatBubbleCooldownActive = true;
        _chatBubbleTimer = 0;
    }

    public void RemoveAllChatBubbles()
    {
        var bubblesClone = new Dictionary<Profile, ChatBubbleControl>(chatBubbles);
        foreach (var bubble in bubblesClone.Values)
        {
            if (bubble != null)
            {
                bubble.Destroy();
            }
        }
    }

    public void RemoveChatBubble(Profile profile)
    {
        chatBubbles.Remove(profile);
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

        Managers.SoundManager.TryPlayVA("evaluate");

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
        Managers.SoundManager.TryPlayVA(successful == true ? "success" : "fail");

        Managers.ScoreManager.UpdateStreaks(successful);

        _selectedProfile1 = null;
        _selectedProfile2 = null;

        Managers.EvaluationManager.ClearEvaluationProfiles();

        _allowSelection = true;
    }

    #endregion
}