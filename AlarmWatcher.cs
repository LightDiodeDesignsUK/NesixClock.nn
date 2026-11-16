using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clockv2.nn
{
    public static class AlarmWatcher
    {

        // 
        // notifyIcon1
        // 
        static System.ComponentModel.Container components = new System.ComponentModel.Container();            


        static System.Threading.Timer _daliesTimer;
        static System.Threading.Timer _schedTimer;
        static AlarmWatcher()
        { }
        private static NotifyIcon NotifyIcon;
        
        
        public static void Start()
        {
            if (ClockGlobals.alarmList == null) ClockGlobals.doAlarmsLoad();
            if (ClockGlobals.SchedEventList == null) ClockGlobals.doSchedListLoad();
            if (_daliesTimer == null) _daliesTimer = new System.Threading.Timer(DaliesCallBack, null, 1000, 1000);
            if (_schedTimer == null) _schedTimer = new System.Threading.Timer(schedCallBack, null, 1000, 1000);
        }
        static RingAlarmForm rf;
        static System.Threading.Thread? alarmingThread;
        static void schedCallBack(object? sender)
        {
            for (int i = 0; i < ClockGlobals.SchedEventList.Length; i++)
            {
                SchedEvent assS = ClockGlobals.SchedEventList[i];
                string st = DateTime.Now.ToString("HH:mm");
                string sd = DateTime.Now.ToShortDateString();
                string asssTime = assS.ReminderDT.ToShortTimeString();
                string asssDate = assS.ReminderDT.ToShortDateString();
                if (asssTime == st)
                {
                    if (asssDate == sd)
                    {
                        if (assS.ReminderIsOn)
                        {
                            ClockGlobals.SchedEventList[i].LastTriggered = DateTime.Now;
                            ClockGlobals.SchedEventList[i].ReminderIsOn = false;
                            //await new TaskFactory().StartNew(alarmer)  ;
                            ShowAlarm(assS.Description);
                        }
                    }
                }
            }
        }
        static void DaliesCallBack(object? sender)
        {
            if (ClockGlobals.alarmList != null)
            {
                for (int i = 0; i < ClockGlobals.alarmList.Length; i++)
                {
                    alarmStruct ass = ClockGlobals.alarmList[i];
                    string st = DateTime.Now.ToString("HH:mm");
                    string assTime = (DateTime.Parse(ass.AlarmTime)).ToShortTimeString();
                    if (assTime == st)
                    {
                        if (ass.lastTriggered.Date < DateTime.Now.Date)
                        {
                            string sDays = ass.ShortDaysString.ToLower();
                            string sDay = DateTime.Now.ToString("ddd").ToLower();
                            if (ass.ShortDaysString.ToLower().Contains(sDay))
                            {
                                if (ass.IsEnabled)
                                {
                                    ClockGlobals.alarmList[i].lastTriggered = DateTime.Now;
                                    //await new TaskFactory().StartNew(alarmer)  ;
                                    ShowAlarm(ClockGlobals.alarmList[i].Description);
                                }
                            }
                        }
                    }
                }
            }
        }
        //private static System.ComponentModel.IContainer components = null;

        public static void ShowAlarm(string sAlarm)
        {
            //if (rf == null)
            rf = new RingAlarmForm(sAlarm);

            
            Application.Run(rf);
            rf = null;
        }
    }
}
