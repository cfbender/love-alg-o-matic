using UnityEngine;

public class Managers : MonoBehaviour
{
    public static ProfileManager ProfileManager;
    public static InterestManager InterestManager;

    private void Awake()
    {
        ProfileManager = GetComponent<ProfileManager>();
        InterestManager = GetComponent<InterestManager>();
    }
}
