using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterestManager : MonoBehaviour
{
    public List<InterestTemplate> Interests;

    public List<InterestTemplate> GetInterests()
    {
       return Interests.OrderBy(a => Guid.NewGuid()).Take(5).ToList();
    }
}
