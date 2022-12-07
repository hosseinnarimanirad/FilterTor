namespace GridEngineCore.Models;
 
using GridEngineCore.Extensions;  
using System;
using System.Collections.Generic;
using System.Text;


public class Duration
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }


    public static Duration? GetDuration(DurationType durationType, DateTime dateTime)
    {
        switch (durationType)
        {
            case DurationType.Daily:
                return new Duration()
                {
                    Start = dateTime.Date,
                    End = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59)
                };

            case DurationType.Weekly:
                return new Duration()
                {
                    Start = dateTime.GetBeginningOfThePersianWeek(),
                    End = dateTime.GetEndOfThePersianWeek()
                };

            case DurationType.Monthly:
                return new Duration()
                {
                    Start = dateTime.GetBeginningOfThePersianMonth(),
                    End = dateTime.GetEndOfThePersianMonth()
                };

            case DurationType.Quarterly:
                return new Duration()
                {
                    Start = dateTime.GetBeginningOfThePersianQuarter(),
                    End = dateTime.GetEndOfThePersianQuarter()
                };

            case DurationType.Yearly:
                return new Duration()
                {
                    Start = dateTime.GetBeginningOfThePersianYear(),
                    End = dateTime.GetEndOfThePersianYear()
                };

            case DurationType.None:
                return null;

            default:
                throw new NotImplementedException("PolicyHelper > GetDuration");
        }
    }

    public static string? GetDurationString(DurationType? durationType, DateTime? dateTime)
    {
        if (!durationType.HasValue)
            return null;

        var date = dateTime.HasValue ? dateTime.Value : DateTime.Now;

        var duration = GetDuration(durationType.Value, date);

        if (duration == null)
            return null;

        var targetValue = $"{duration.Start.Year}-{duration.Start.Month}-{duration.Start.Day} 00:00:00AM,{duration.End.Year}-{duration.End.Month}-{duration.End.Day} 23:59:59.99";

        return targetValue;
    }

}
