using System;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Profile _selectedProfile1;
    private Profile _selectedProfile2;

    private void Start()
    {
        Managers.ProfileManager.GenerateInitialProfiles();
    }

    public void SelectProfile(Profile profile)
    {
        if (_selectedProfile1 == null)
        {
            _selectedProfile1 = profile;
            Debug.Log($"{profile.Name} set to <color=blue>_selectedProfile1</color>");
            return;
        }

        // cancel selection
        if (_selectedProfile1 == profile)
        {
            _selectedProfile1 = null;
            Debug.Log($"{profile.Name} removed from _selectedProfile1");
            return;
        }

        _selectedProfile2 = profile;
        Debug.Log($"{profile.Name} set to <color=teal>_selectedProfile2</color>");
        var matches = Evaluator.GetMatches(_selectedProfile1, _selectedProfile2);
        
        Debug.Log($"{_selectedProfile1.Name} matched with {_selectedProfile2.Name} on {matches.Count()} interests.");
        Debug.Log($"Matching interests: {String.Join(", ",matches)}");

        _selectedProfile1 = null;
        _selectedProfile2 = null;
    }
}
