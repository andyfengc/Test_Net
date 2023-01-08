using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public enum TimeZoneEnum
    {
        // canada
        NST,
        NDT,
        AST,
        ADT,
        EST,
        EDT,
        CST,
        CDT,
        MST,
        MDT,
        PST,
        PDT,
        YST,
        YDT,
        // us
        AKST,
        AKDT,
        HST,
        HAST,
        HADT,
        SST,
        SDT,
        CHST
    }

    public static class TimeZoneEnumExtensions
    {
        public static TimeZoneEnum ToEnum(string timezoneStr)
        {
            switch (timezoneStr.ToUpper())
            {
                // ca
                case "NST":
                    return TimeZoneEnum.NST;
                case "NDT":
                    return TimeZoneEnum.NDT;
                case "AST":
                    return TimeZoneEnum.AST;
                case "ADT":
                    return TimeZoneEnum.ADT;
                case "EST":
                    return TimeZoneEnum.EST;
                case "EDT":
                    return TimeZoneEnum.EDT;
                case "CDT":
                    return TimeZoneEnum.CDT;
                case "CST":
                    return TimeZoneEnum.CST;
                case "MST":
                    return TimeZoneEnum.MST;
                case "MDT":
                    return TimeZoneEnum.MDT;
                case "PST":
                    return TimeZoneEnum.PST;
                case "PDT":
                    return TimeZoneEnum.PDT;
                case "YST":
                    return TimeZoneEnum.YST;
                case "YDT":
                    return TimeZoneEnum.YDT;
                // us
                case "AKST":
                    return TimeZoneEnum.AKST;
                case "AKDT":
                    return TimeZoneEnum.AKDT;
                case "HST":
                    return TimeZoneEnum.HST;
                case "HAST":
                    return TimeZoneEnum.HAST;
                case "HADT":
                    return TimeZoneEnum.HADT;
                case "SST":
                    return TimeZoneEnum.SST;
                case "SDT":
                    return TimeZoneEnum.SDT;
                case "CHST":
                    return TimeZoneEnum.CHST;
                default:
                    throw new NotSupportedException("undefined timezone string: " + timezoneStr);
            }
        }
        public static string ToCode(this TimeZoneEnum timezone)
        {
            switch (timezone)
            {
                // ca
                case TimeZoneEnum.NST:
                    return "NST";
                case TimeZoneEnum.NDT:
                    return "NDT";
                case TimeZoneEnum.AST:
                    return "AST";
                case TimeZoneEnum.ADT:
                    return "ADT";
                case TimeZoneEnum.EST:
                    return "EST";
                case TimeZoneEnum.EDT:
                    return "EDT";
                case TimeZoneEnum.CDT:
                    return "CDT";
                case TimeZoneEnum.CST:
                    return "CST";
                case TimeZoneEnum.MST:
                    return "NST";
                case TimeZoneEnum.MDT:
                    return "NST";
                case TimeZoneEnum.PST:
                    return "PST";
                case TimeZoneEnum.PDT:
                    return "PDT";
                case TimeZoneEnum.YST:
                    return "YST";
                case TimeZoneEnum.YDT:
                    return "YDT";
                // us
                case TimeZoneEnum.AKST:
                    return "AKST";
                case TimeZoneEnum.AKDT:
                    return "AKDT";
                case TimeZoneEnum.HST:
                    return "HST";
                case TimeZoneEnum.HAST:
                    return "HAST";
                case TimeZoneEnum.HADT:
                    return "HADT";
                case TimeZoneEnum.SST:
                    return "SST";
                case TimeZoneEnum.SDT:
                    return "SDT";
                case TimeZoneEnum.CHST:
                    return "CHST";
                default:
                    throw new NotSupportedException("undefined timezone: " + timezone);
            }
        }

        public static string ToName(this TimeZoneEnum timezone)
        {
            switch (timezone)
            {
                // ca
                case TimeZoneEnum.NST:
                case TimeZoneEnum.NDT:
                    return "Newfoundland Standard Time";
                case TimeZoneEnum.AST:
                case TimeZoneEnum.ADT:
                    return "Atlantic Standard Time";
                case TimeZoneEnum.EST:
                case TimeZoneEnum.EDT:
                    return "Eastern Standard Time";
                case TimeZoneEnum.CST:
                case TimeZoneEnum.CDT:
                    return "Central Standard Time";
                case TimeZoneEnum.MST:
                case TimeZoneEnum.MDT:
                    return "Mountain Standard Time";
                case TimeZoneEnum.PST:
                case TimeZoneEnum.PDT:
                    return "Pacific Standard Time";
                // us
                case TimeZoneEnum.AKST:
                case TimeZoneEnum.AKDT:
                    return "Alaskan Standard Time";
                case TimeZoneEnum.HST:
                case TimeZoneEnum.HAST:
                case TimeZoneEnum.HADT:
                    return "Hawaiian Standard Time";
                case TimeZoneEnum.SST:
                case TimeZoneEnum.SDT:
                    return "Samoa Standard Time";
                default:
                    throw new NotSupportedException("undefined timezone: " + timezone);
            }
        }
    }
}
