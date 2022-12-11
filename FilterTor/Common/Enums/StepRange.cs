namespace FilterTor;

using System;
using System.Collections.Generic;
using System.Text;


public class StepRange
{
    public double Start { get; set; }

    public double End { get; set; }

    public double MidValue { get => (Start + End) / 2.0; }

    public StepRange(double start, double end)
    {
        Start = start;

        End = end;
    }

    public bool Overlaps(StepRange range)
    {
        if (range == null)
        {
            return false;
        }

        return Start <= range.Start && End >= range.Start ||
                Start <= range.End && End >= range.End;
    }

    public static bool Overlaps(List<StepRange> ranges)
    {
        for (int i = 0; i < ranges.Count; i++)
        {
            for (int j = 0; j < ranges.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }
                else if (ranges[i].Overlaps(ranges[j]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}