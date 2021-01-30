using UnityEngine;

public class Managers : MonoBehaviour
{
    public static GameManager GameManager;
    public static InterestManager InterestManager;
    public static EvaluationManager EvaluationManager;
    public static ProfileGridControl ProfileGridControl;
    public static ScoreManager ScoreManager;
    public static GrammarManager GrammarManager;
    public static ProfileSpriteManager ProfileSpriteManager;
    
    private void Awake()
    {
        GameManager = GetComponent<GameManager>();
        InterestManager = GetComponent<InterestManager>();
        EvaluationManager = GetComponent<EvaluationManager>();
        ProfileGridControl = GetComponent<ProfileGridControl>();
        ScoreManager = GetComponent<ScoreManager>();
        GrammarManager = GetComponent<GrammarManager>();
        ProfileSpriteManager = GetComponent<ProfileSpriteManager>();
    }
}
