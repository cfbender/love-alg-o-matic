using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoveMeterControl : MonoBehaviour
{
    public List<MeterInterestRow> MeterInterestRows;

    public void UpdateLoveMeter(EvaluationResult result, int matchCount)
    {
        const int maxInterests = 5;

        var max = matchCount > 2 ? maxInterests : matchCount;

        for (var i = 0; i < max; i++)
        {
            var row = MeterInterestRows[i];
            // display all successful matches
            if (i < matchCount)
            {
                row.DisplayMatch(result.Matches[i]);
            }
            else
            {
                // if a successful match, display all interests
                var currMiss = i - matchCount;
                var p1Interest = result.P1Misses[currMiss];
                var p2Interest = result.P2Misses[currMiss];
                row.DisplayMismatch(p1Interest, p2Interest);
            }
        }
    }

    public void Reset()
    {
        MeterInterestRows.ForEach(row => row.Reset());
    }
}