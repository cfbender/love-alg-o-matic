using UnityEngine;

public class Managers : MonoBehaviour
{
    public static ProfileManager ProfileManager;

    private void Awake()
    {
        ProfileManager = GetComponent<ProfileManager>();
    }
}
