﻿namespace FarsiLibrary.Localization;

public class ENLocalizer : BaseLocalizer
{
    public override string GetLocalizedString(StringID id)
    {
        return id switch
            {
                StringID.Empty => string.Empty,
                StringID.Numbers_0 => "0",
                StringID.Numbers_1 => "1",
                StringID.Numbers_2 => "2",
                StringID.Numbers_3 => "3",
                StringID.Numbers_4 => "4",
                StringID.Numbers_5 => "5",
                StringID.Numbers_6 => "6",
                StringID.Numbers_7 => "7",
                StringID.Numbers_8 => "8",
                StringID.Numbers_9 => "9",
                StringID.FADateTextBox_Required => "Mandatory field",
                StringID.FAMonthView_None => "Empty",
                StringID.FAMonthView_Today => "Today",
                StringID.PersianDate_InvalidDateFormat => "Invalid date format",
                StringID.PersianDate_InvalidDateTime => "Invalid date/time value",
                StringID.PersianDate_InvalidDateTimeLength => "Invalid date time format",
                StringID.PersianDate_InvalidDay => "Invalid Day value",
                StringID.PersianDate_InvalidEra => "Invalid Era value",
                StringID.PersianDate_InvalidFourDigitYear => "Invalid four digit Year value",
                StringID.PersianDate_InvalidHour => "Invalid Hour value",
                StringID.PersianDate_InvalidLeapYear => "Not a leap year. Correct the day value.",
                StringID.PersianDate_InvalidMinute => "Invalid Minute value",
                StringID.PersianDate_InvalidMonth => "Invalid Month value",
                StringID.PersianDate_InvalidMonthDay => "Invalid Month/Day value",
                StringID.PersianDate_InvalidSecond => "Invalid Second value",
                StringID.PersianDate_InvalidTimeFormat => "Invalid Time format",
                StringID.PersianDate_InvalidYear => "Invalid Year value.",
                StringID.Validation_Cancel => "Cancel",
                StringID.Validation_NotValid => "Entered value is not in valid range.",
                StringID.Validation_Required => "This is a mandatory field. Please fill it in.",
                StringID.Validation_NullText => "[Empty Value]",
                StringID.MessageBox_Ok => "Ok",
                StringID.MessageBox_Abort => "Abort",
                StringID.MessageBox_Cancel => "Cancel",
                StringID.MessageBox_Ignore => "Ignore",
                StringID.MessageBox_No => "No",
                StringID.MessageBox_Retry => "Retry",
                StringID.MessageBox_Yes => "Yes",
                _ => string.Empty
            };
    }

    public override string GetFormatterString(FormatterStringID stringID)
    {
        return stringID switch
            {
                FormatterStringID.CenturyPattern => "%n %u",
                FormatterStringID.CenturyFuturePrefix => string.Empty,
                FormatterStringID.CenturyFutureSuffix => " from now",
                FormatterStringID.CenturyPastPrefix => string.Empty,
                FormatterStringID.CenturyPastSuffix => " ago",
                FormatterStringID.CenturyName => "century",
                FormatterStringID.CenturyPluralName => "centuries",
                FormatterStringID.DayPattern => "%n %u",
                FormatterStringID.DayFuturePrefix => string.Empty,
                FormatterStringID.DayFutureSuffix => " from now",
                FormatterStringID.DayPastPrefix => string.Empty,
                FormatterStringID.DayPastSuffix => " ago",
                FormatterStringID.DayName => "day",
                FormatterStringID.DayPluralName => "days",
                FormatterStringID.DecadePattern => "%n %u",
                FormatterStringID.DecadeFuturePrefix => string.Empty,
                FormatterStringID.DecadeFutureSuffix => " from now",
                FormatterStringID.DecadePastPrefix => string.Empty,
                FormatterStringID.DecadePastSuffix => " ago",
                FormatterStringID.DecadeName => "decade",
                FormatterStringID.DecadePluralName => "decades",
                FormatterStringID.HourPattern => "%n %u",
                FormatterStringID.HourFuturePrefix => string.Empty,
                FormatterStringID.HourFutureSuffix => " from now",
                FormatterStringID.HourPastPrefix => string.Empty,
                FormatterStringID.HourPastSuffix => " ago",
                FormatterStringID.HourName => "hour",
                FormatterStringID.HourPluralName => "hours",
                FormatterStringID.JustNowPattern => "%u",
                FormatterStringID.JustNowFuturePrefix => string.Empty,
                FormatterStringID.JustNowFutureSuffix => "moments from now",
                FormatterStringID.JustNowPastPrefix => "moments ago",
                FormatterStringID.JustNowPastSuffix => string.Empty,
                FormatterStringID.JustNowName => string.Empty,
                FormatterStringID.JustNowPluralName => string.Empty,
                FormatterStringID.MillenniumPattern => "%n %u",
                FormatterStringID.MillenniumFuturePrefix => string.Empty,
                FormatterStringID.MillenniumFutureSuffix => " from now",
                FormatterStringID.MillenniumPastPrefix => string.Empty,
                FormatterStringID.MillenniumPastSuffix => " ago",
                FormatterStringID.MillenniumName => "millennium",
                FormatterStringID.MillenniumPluralName => "millennia",
                FormatterStringID.MillisecondPattern => "%n %u",
                FormatterStringID.MillisecondFuturePrefix => string.Empty,
                FormatterStringID.MillisecondFutureSuffix => " from now",
                FormatterStringID.MillisecondPastPrefix => string.Empty,
                FormatterStringID.MillisecondPastSuffix => " ago",
                FormatterStringID.MillisecondName => "millisecond",
                FormatterStringID.MillisecondPluralName => "milliseconds",
                FormatterStringID.MinutePattern => "%n %u",
                FormatterStringID.MinuteFuturePrefix => string.Empty,
                FormatterStringID.MinuteFutureSuffix => " from now",
                FormatterStringID.MinutePastPrefix => string.Empty,
                FormatterStringID.MinutePastSuffix => " ago",
                FormatterStringID.MinuteName => "minute",
                FormatterStringID.MinutePluralName => "minutes",
                FormatterStringID.MonthPattern => "%n %u",
                FormatterStringID.MonthFuturePrefix => string.Empty,
                FormatterStringID.MonthFutureSuffix => " from now",
                FormatterStringID.MonthPastPrefix => string.Empty,
                FormatterStringID.MonthPastSuffix => " ago",
                FormatterStringID.MonthName => "month",
                FormatterStringID.MonthPluralName => "months",
                FormatterStringID.SecondPattern => "%n %u",
                FormatterStringID.SecondFuturePrefix => string.Empty,
                FormatterStringID.SecondFutureSuffix => " from now",
                FormatterStringID.SecondPastPrefix => string.Empty,
                FormatterStringID.SecondPastSuffix => " ago",
                FormatterStringID.SecondName => "second",
                FormatterStringID.SecondPluralName => "seconds",
                FormatterStringID.WeekPattern => "%n %u",
                FormatterStringID.WeekFuturePrefix => string.Empty,
                FormatterStringID.WeekFutureSuffix => " from now",
                FormatterStringID.WeekPastPrefix => string.Empty,
                FormatterStringID.WeekPastSuffix => " ago",
                FormatterStringID.WeekName => "week",
                FormatterStringID.WeekPluralName => "weeks",
                FormatterStringID.YearPattern => "%n %u",
                FormatterStringID.YearFuturePrefix => string.Empty,
                FormatterStringID.YearFutureSuffix => " from now",
                FormatterStringID.YearPastPrefix => string.Empty,
                FormatterStringID.YearPastSuffix => " ago",
                FormatterStringID.YearName => "year",
                FormatterStringID.YearPluralName => "years",
                _ => string.Empty
            };
    }
}