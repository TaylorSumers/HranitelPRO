using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User_NN_Lib
{
    public class Calculations
    {
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {

            ///<param name = "startTimes">Начало занятого промежутка времени</param>
            ///<param name = "durations">Длительность занятого промежутка времени</param>
            ///<param name = "consultationTime">Минимальное необходимое время посетителя</param>
            ///<param name = "beginWorkingTime">Начало рабочего дня</param>
            ///<param name = "beginWorkingTime">Конец рабочего дня</param>
            var avalibalePeriods = new List<string>();
            var startEndBusySpans = new List<List<TimeSpan>>();
            for(int i = 0; i<startTimes.Length; i++)
            {
                startEndBusySpans.Add(new List<TimeSpan> { startTimes[i], startTimes[i]+TimeSpan.FromMinutes(durations[i]) });
            }
            for (TimeSpan i = beginWorkingTime; i < endWorkingTime; )
            {
                TimeSpan freeTimeStart = i;
                TimeSpan freeTimeEnd = i + TimeSpan.FromMinutes(consultationTime);
                bool isNotBusy = true;
                foreach(var span in startEndBusySpans)
                {
                    if (freeTimeStart >= span[0] && freeTimeStart < span[1])
                    {
                        i = span[1];
                        isNotBusy = false;
                        break;
                    }

                    if (freeTimeEnd > span[0] && freeTimeEnd <= span[1])
                    {
                        i = span[1];
                        isNotBusy = false;
                        break;
                    }

                }
                if(isNotBusy)
                {
                    avalibalePeriods.Add($"{freeTimeStart.ToString().Substring(0, 5)}-{freeTimeEnd.ToString().Substring(0, 5)}");
                    i += TimeSpan.FromMinutes(consultationTime);
                }
            }
            return avalibalePeriods.ToArray();
            ///<returns name = "avalibalePeriods">Свободные промежктки времени</returns>
        }
    }
}
