using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarManager : MonoBehaviour
{

    public Lexic.ProfileNameGenerator NameGen;

    public string GetRandomName(string gender = null)
    {
        return NameGen.GetNextRandomName(gender);
    }
}
