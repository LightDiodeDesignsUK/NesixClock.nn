using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Clockv2.nn
{
    public struct ClockGlobals
    {
        public static Icon? appIcon = null  ;
        public static System.Media.SoundPlayer? soundPlayer;
        public static FileStream? wavFS;
        public static Color[] colorStripes; 
        public static readonly string SchedFileName = @"D:\neals\ClockSchedule.txt";
        public static readonly  char[] schedFileDelimiter = new char[] { (char)27 , (char)9 };
        public static Bitmap? MainBMP;

        public static SchedEvent[]? SchedEventList;
        public static void doSchedListLoad()
        {

            string sF = "";
            List<SchedEvent> tmpList = new List<SchedEvent>();
            try
            {
                sF = File.ReadAllText(ClockGlobals.SchedFileName);
            }   
            catch { sF = "";  }
            string sDelim = new string(ClockGlobals.schedFileDelimiter);
            string[] ss = sF.Split(sDelim);

            foreach (string s in ss)
            {
                if (s != "")
                {
                    SchedEvent cevl = new SchedEvent();
                    string[] sSplit = s.Split(",");
                    cevl.Time = TimeOnly.Parse(sSplit[0]);
                    cevl.Day = DateOnly.Parse(sSplit[1]).Day;
                    cevl.Month = DateOnly.Parse(sSplit[1]).Month;
                    cevl.Year = DateOnly.Parse(sSplit[1]).Year;

                    string sRemDT = sSplit[2].Replace("_"," ");
                    string sDay = sRemDT.Split("/")[0];
                    string sMonth = sRemDT.Split("/")[1];
                    string sYear = sRemDT.Split("/")[2].Split(" ")[0];
                    string sTime = sRemDT.Split("/")[2].Split(" ")[1];
                    cevl.ReminderIsOn = (sSplit[3] == "Onn");
                    DateTime dtREm = new DateTime();
                    dtREm = DateTime.Parse(sDay + " " + sMonth + " " + sYear + " " + sTime);
                    

                    cevl.ReminderDT = dtREm;
                    cevl.ReminderDateString = cevl.ReminderDT.ToShortDateString();
                    cevl.LastTriggered = DateTime.Now.AddMinutes(-20);

                    cevl.Description = sSplit[4];
                    cevl.Description = Uri.UnescapeDataString(cevl.Description);
                    cevl.Description = cevl.Description.Replace("!^&", ",");
                    tmpList.Add(cevl);
                }
            }
            SchedEventList = tmpList.ToArray();
            if (SchedEventList.Length == 0)
            {
                SchedEventList = new SchedEvent[]
                {
                    new SchedEvent() { Time = TimeOnly.Parse("09:00"), Day = 13, Month = 4, Year = 2029, Description = "Apophis!!\r\nMASSIVE\r\nASTEROID!!!!!", ReminderDT = DateTime.Parse("08-04-29 09:00"), ReminderIsOn = true, LastTriggered = DateTime.Now }
                };
            }
        }
        static string getCalEvtString(SchedEvent cEvent)
        {
            string sRet = "";
            sRet += cEvent.Time.ToShortTimeString() + ",";
            sRet += cEvent.Day.ToString() + "-";
            sRet += cEvent.Month.ToString() + "-";
            sRet += cEvent.Year.ToString() + ",";

            sRet += cEvent.ReminderDT.ToShortDateString() + " " + cEvent.ReminderDT.ToShortTimeString();
            sRet += "," + (cEvent.ReminderIsOn?"Onn":"Off") + ",";

            sRet += Uri.EscapeDataString((cEvent?.Description?.Replace(",", "!^&")));

            sRet += new string(ClockGlobals.schedFileDelimiter);
            ;
            return sRet;
        }
        public static void SaveCalendatEvents()
        {
            string sRet = "";
            foreach (SchedEvent ce in SchedEventList)
            {
                sRet += getCalEvtString(ce);
            }
            File.WriteAllText("d:\\neals\\cal.txt", sRet);
        }

        public static int DaysInMonth(int month, int year)
        {
            try { return DateTime.DaysInMonth(year, month); }
            catch { return 28; }
        }

        public static alarmStruct[]? alarmList;
        public static string AlarmsToString()
        {
            string sRet = "";
            if (alarmList != null)
            {
                foreach (alarmStruct alarm in alarmList)
                {
                    sRet += alarm.ToAlarmString();
                    sRet += ",";
                }
                while (sRet.EndsWith(",")) sRet = sRet.Substring(0, sRet.Length - 1);
                sRet += (char)27 + (char)9;
                return sRet;
            }
            else
                return "";
        }

        public static bool doAlarmsLoad()
        {
            if (alarmList == null)
            {
                List<alarmStruct> tmpAlarmList = new List<alarmStruct>();
                try
                {
                    string sTmp = "";
                    string[] sAlarms = ClockSettings.Default.AlarmTimes.Split(",");
                    if (sAlarms.Length < 1) return true;
                    foreach (string sAlarm in sAlarms)
                    {
                        sTmp = sAlarm.Split("-")[0].Trim();
                        alarmStruct newAlarm = new alarmStruct();

                        newAlarm.Description = sTmp.Split("@")[0];
                        newAlarm.AlarmTime = sTmp.Split("@")[1];
                        newAlarm.ShortDaysString = sAlarm.Split("-")[1];
                        newAlarm.IsEnabled = !(sAlarm.Contains("OFF"));

                        tmpAlarmList.Add(newAlarm);
                    };

                }
                catch
                {
                    alarmList = new alarmStruct[0];
                    return false;
                }
                ClockGlobals.alarmList = tmpAlarmList.ToArray();
                tmpAlarmList = null;
                return true;
            }
            return true;
        }
    }
    public struct alarmStruct
    {
        public String AlarmTime; public string Description; public byte DayMask;
        public bool IsEnabled;
        public DateTime lastTriggered;
        public string ToAlarmString()
        {
            return Description + "@" + AlarmTime + "-" + ShortDaysString;
        }
        public string ShortDaysString
        {
            get
            {
                string sDays = "";
                if ((DayMask & 1) != 0) sDays += "Mon ";
                if ((DayMask & 2) != 0) sDays += "Tue ";
                if ((DayMask & 4) != 0) sDays += "Wed ";
                if ((DayMask & 8) != 0) sDays += "Thu ";
                if ((DayMask & 16) != 0) sDays += "Fri ";
                if ((DayMask & 32) != 0) sDays += "Sat ";
                if ((DayMask & 64) != 0) sDays += "Sun ";
                if (IsEnabled) sDays += "ON ";
                else sDays += "OFF ";
                return sDays;
            }
            set
            {
                if (value.Contains("mon", StringComparison.InvariantCultureIgnoreCase)) DayMask |= 1;
                if (value.Contains("tue", StringComparison.InvariantCultureIgnoreCase)) DayMask |= 2;
                if (value.Contains("wed", StringComparison.InvariantCultureIgnoreCase)) DayMask |= 4;
                if (value.Contains("thu", StringComparison.InvariantCultureIgnoreCase)) DayMask |= 8;
                if (value.Contains("fri", StringComparison.InvariantCultureIgnoreCase)) DayMask |= 16;
                if (value.Contains("sat", StringComparison.InvariantCultureIgnoreCase)) DayMask |= 32;
                if (value.Contains("sun", StringComparison.InvariantCultureIgnoreCase)) DayMask |= 64;
                IsEnabled = !(value.Contains("OFF"));
            }
        }
    }
    public class SchedEvent
    {
        public TimeOnly Time; public int Day; public int Month; public int Year; public string? Description;
        public bool ReminderIsOn = false;
        DateOnly reminder_Date = DateOnly.MinValue;
        TimeOnly reminder_Time = TimeOnly.MinValue;
        TimeOnly ReminderTime
        {
            get { return reminder_Time; }   
            set { reminder_Time = value;    reminderDT = DateTime.Parse(reminder_Date.ToString() + " " + value.ToString()); }
        }
        DateOnly reminderDate
        {
            get { return reminder_Date; }
            set { reminder_Date = value; reminderDT = DateTime.Parse(value.ToShortDateString() + " " + reminder_Time.ToString()); }
        }
        DateTime reminderDT ;
        public DateTime ReminderDT 
        {
            get { return reminderDT; } 
            set {
                reminderDT = value;  // ReminderTime = TimeOnly.FromDateTime(reminderDT);
                }
        }

        public DateTime LastTriggered = DateTime.FromBinary(0);
        public string DateString
        {
            get { return (Day.ToString() + "/" + Month.ToString() + "/" + Year.ToString()); }
            set
            {
                try
                {
                    Day = DateOnly.Parse(value).Day;
                    Month = DateOnly.Parse(value).Month;
                    Year = DateOnly.Parse(value).Year;
                }
                catch { Day = 1; Month = 1; Year = 2000; }
            }
        }
        public string ReminderDateString
        {
            get { return ReminderDT.ToShortDateString(); }
            set
            {
                try
                {   
                    ReminderDT = DateTime.Parse(value + " " + reminderDT.ToShortTimeString());
                }
                catch 
                {
                    ReminderDT = DateTime.FromBinary(0);
                    LastTriggered = DateTime.FromBinary(0);
                    ReminderIsOn = false;
                }
            }
        }

    }
}