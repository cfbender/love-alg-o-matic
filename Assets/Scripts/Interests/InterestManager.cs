using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterestManager : MonoBehaviour
{
    public List<InterestTemplate> interests;

    public List<InterestTemplate> GetInterests()
    {
       return interests.OrderBy(a => Guid.NewGuid()).Take(5).ToList();
    }
}
