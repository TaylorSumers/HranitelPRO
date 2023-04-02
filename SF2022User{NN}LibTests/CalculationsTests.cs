using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF2022User_NN_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User_NN_Lib.Tests
{
    [TestClass()]
    public class CalculationsTests
    {


        [TestMethod()]
        public void AvaliblePeriods_5consultaitions_Returns14Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("10:00"), TimeSpan.Parse("11:00"), TimeSpan.Parse("15:00"), TimeSpan.Parse("15:30"), TimeSpan.Parse("16:50") };
            int[] durations = { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("8:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("18:00");
            int consultationTime = 30;
            string[] expected =
                {
                    "08:00-08:30",
                    "08:30-09:00",
                    "09:00-09:30",
                    "09:30-10:00",
                    "11:30-12:00",
                    "12:00-12:30",
                    "12:30-13:00",
                    "13:00-13:30",
                    "13:30-14:00",
                    "14:00-14:30",
                    "14:30-15:00",
                    "15:40-16:10",
                    "16:10-16:40",
                    "17:30-18:00",
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_ConsultaitionInMorning_Returns14Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("8:00"), TimeSpan.Parse("11:00"), TimeSpan.Parse("15:00"), TimeSpan.Parse("15:30"), TimeSpan.Parse("16:50") };
            int[] durations = { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("8:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("18:00");
            int consultationTime = 30;
            string[] expected =
                {
                    "09:00-09:30",
                    "09:30-10:00",
                    "10:00-10:30",
                    "10:30-11:00",
                    "11:30-12:00",
                    "12:00-12:30",
                    "12:30-13:00",
                    "13:00-13:30",
                    "13:30-14:00",
                    "14:00-14:30",
                    "14:30-15:00",
                    "15:40-16:10",
                    "16:10-16:40",
                    "17:30-18:00",
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_ConsultaitionInEvening_Returns14Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("10:00"), TimeSpan.Parse("11:00"), TimeSpan.Parse("15:00"), TimeSpan.Parse("15:30"), TimeSpan.Parse("17:20") };
            int[] durations = { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("8:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("18:00");
            int consultationTime = 30;
            string[] expected =
                {
                    "08:00-08:30",
                    "08:30-09:00",
                    "09:00-09:30",
                    "09:30-10:00",
                    "11:30-12:00",
                    "12:00-12:30",
                    "12:30-13:00",
                    "13:00-13:30",
                    "13:30-14:00",
                    "14:00-14:30",
                    "14:30-15:00",
                    "15:40-16:10",
                    "16:10-16:40",
                    "16:40-17:10",
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_ConsultaitionsInMorningAndEvening_Returns14Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("8:00"), TimeSpan.Parse("11:00"), TimeSpan.Parse("15:00"), TimeSpan.Parse("15:30"), TimeSpan.Parse("17:20") };
            int[] durations = { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("8:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("18:00");
            int consultationTime = 30;
            string[] expected =
                {
                    "09:00-09:30",
                    "09:30-10:00",
                    "10:00-10:30",
                    "10:30-11:00",
                    "11:30-12:00",
                    "12:00-12:30",
                    "12:30-13:00",
                    "13:00-13:30",
                    "13:30-14:00",
                    "14:00-14:30",
                    "14:30-15:00",
                    "15:40-16:10",
                    "16:10-16:40",
                    "16:40-17:10",
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_1consultaition_Returns8Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("10:00") };
            int[] durations = { 60 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("8:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("13:00");
            int consultationTime = 30;
            string[] expected =
                {
                    "08:00-08:30",
                    "08:30-09:00",
                    "09:00-09:30",
                    "09:30-10:00",
                    "11:00-11:30",
                    "11:30-12:00",
                    "12:00-12:30",
                    "12:30-13:00",
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_3consultaitions_Returns6Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("10:00"), TimeSpan.Parse("13:00"), TimeSpan.Parse("15:00") };
            int[] durations = { 90, 10, 5 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("8:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("18:00");
            int consultationTime = 60;
            string[] expected =
                {
                    "08:00-09:00",
                    "09:00-10:00",
                    "11:30-12:30",
                    "13:10-14:10",
                    "15:05-16:05",
                    "16:05-17:05"
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_2consultaitions_Returns5Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("10:30"), TimeSpan.Parse("11:00")  };
            int[] durations = { 30, 60, };
            TimeSpan beginWorkingTime = TimeSpan.Parse("7:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("12:00");
            int consultationTime = 40;
            string[] expected =
                {
                    "07:00-07:40",
                    "07:40-08:20",
                    "08:20-09:00",
                    "09:00-09:40",
                    "09:40-10:20"
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_1consultaition_Returns4Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("20:00") };
            int[] durations = { 30 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("16:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("21:00");
            int consultationTime = 60;
            string[] expected =
                {
                    "16:00-17:00",
                    "17:00-18:00",
                    "18:00-19:00",
                    "19:00-20:00",
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_4shortConsultaitions_Returns4Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("10:00"), TimeSpan.Parse("10:20"), TimeSpan.Parse("10:40"), TimeSpan.Parse("11:00") };
            int[] durations = { 10, 10, 10, 10 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("9:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("11:10");
            int consultationTime = 15;
            string[] expected =
                {
                    "09:00-09:15",
                    "09:15-09:30",
                    "09:30-09:45",
                    "09:45-10:00"
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }


        [TestMethod()]
        public void AvaliblePeriods_2longConsultaitions_Returns14Spans()
        {
            // Arrange.
            TimeSpan[] startTimes = { TimeSpan.Parse("13:00"), TimeSpan.Parse("19:00") };
            int[] durations = { 300, 240 };
            TimeSpan beginWorkingTime = TimeSpan.Parse("10:00");
            TimeSpan endWorkingTime = TimeSpan.Parse("23:30");
            int consultationTime = 90;
            string[] expected =
                {
                    "10:00-11:30",
                    "11:30-13:00"
                };
            // Act.
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // Assert.
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }
    }
}