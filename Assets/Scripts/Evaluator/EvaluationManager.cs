using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvaluationManager: MonoBehaviour
{
  public EvaluationProfileControl evaluationProfileControl1;
  public EvaluationProfileControl evaluationProfileControl2;

  public void AssignEvaluationProfile(Profile profile, bool first)
  {
    var profileControl = first ? evaluationProfileControl1 : evaluationProfileControl2;
    profileControl.AssignProfile(profile);
  }

  public void ClearEvaluationProfiles()
  {
    evaluationProfileControl1.ClearProfile();
    evaluationProfileControl2.ClearProfile();
  }
  public IEnumerable<string> GetMatches(Profile p1, Profile p2)
  {
    return p1.Interests.Where(interest => p2.Interests.Contains(interest)).Select(interest => interest.name);
  } 
}
