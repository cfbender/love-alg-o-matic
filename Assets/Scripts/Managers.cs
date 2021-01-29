using UnityEngine;

public class Managers : MonoBehaviour
{
    public static GameManager GameManager;
    public static InterestManager InterestManager;
    public static EvaluationManager EvaluationManager;
    public static ProfileGridControl ProfileGridControl;
    
    private void Awake()
    {
        GameManager = GetComponent<GameManager>();
        InterestManager = GetComponent<InterestManager>();
        EvaluationManager = GetComponent<EvaluationManager>();
        ProfileGridControl = GetComponent<ProfileGridControl>();
    }
}
