using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility;
using Utility.Datetime;

namespace Test
{
    [TestClass]
    public class DateTimeTest
    {
        [TestMethod]
        public void GetThisFriday_ShouldSucceed()
        {
            DateTime today = new DateTime(2015, 12, 16);
            var expectedTime = new DateTime(2015, 12, 18);
            Assert.AreEqual(expectedTime, DateTimeHelper.GetThisWeekDay(today, DayOfWeek.Friday));
            today = new DateTime(2015, 12, 18);
            Assert.AreEqual(expectedTime, DateTimeHelper.GetThisWeekDay(today, DayOfWeek.Friday));
            today = new DateTime(2015, 12, 19);
            Assert.AreEqual(expectedTime, DateTimeHelper.GetThisWeekDay(today, DayOfWeek.Friday));
        }

        [TestMethod]
        public void GetThisFriday5pm_ShouldSucceed()
        {
            DateTime today = new DateTime(2015, 12, 16, 17, 0, 0);
            var expectedTime = new DateTime(2015, 12, 18, 17, 0, 0);
            Assert.AreEqual(expectedTime, DateTimeHelper.GetThisWeekDay(today, DayOfWeek.Friday));
            today = new DateTime(2015, 12, 18, 17, 0, 0);
            Assert.AreEqual(expectedTime, DateTimeHelper.GetThisWeekDay(today, DayOfWeek.Friday));
            today = new DateTime(2015, 12, 19);
            Assert.AreEqual(expectedTime, DateTimeHelper.GetThisWeekDay(today.AddHours(17), DayOfWeek.Friday));
        }

        [TestMethod]
        public void TestWeekdayHours_ShouldSucceed()
        {
            DateTime time = new DateTime(2015, 12, 16);//wednesday
            Assert.IsFalse(DateTimeHelper.IsWeekendHours(time));
            time = new DateTime(2015, 12, 13); //sunday
            Assert.IsTrue(DateTimeHelper.IsWeekendHours(time));
            time = new DateTime(2015, 12, 19);// saturday
            Assert.IsTrue(DateTimeHelper.IsWeekendHours(time));
            time = new DateTime(2015, 12, 18, 16, 59, 59); //friday, 16pm
            Assert.IsFalse(DateTimeHelper.IsWeekendHours(time));
            time = new DateTime(2015, 12, 18, 17, 0, 1); //friday, 17pm
            Assert.IsTrue(DateTimeHelper.IsWeekendHours(time));
            time = new DateTime(2015, 12, 18, 23, 0, 1); //friday, 23pm
            Assert.IsTrue(DateTimeHelper.IsWeekendHours(time));
        }

        [TestMethod]
        public void GetBusinessDaysBetweenTwoDays_ShouldSucceed()
        {
            var days1 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 12, 1), new DateTime(2016, 12, 19));
            Assert.AreEqual(days1.Value, 13);
            var days2 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 12, 3), new DateTime(2016, 12, 19));
            Assert.AreEqual(days2.Value, 11);
            var days3 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 12, 1), new DateTime(2016, 11, 19));
            Assert.AreEqual(days3, 0);
            var days4 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 11, 28), new DateTime(2016, 12, 30));
            Assert.AreEqual(days4, 25);
            var days5 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 11, 28), new DateTime(2016, 12, 24));
            Assert.AreEqual(days5, 20);
            var days6 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 12, 3), new DateTime(2016, 12, 3));
            Assert.AreEqual(days6, 0);
            var days7 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 12, 3), new DateTime(2016, 12, 4));
            Assert.AreEqual(days7, 0);
            var days8 = DateTimeHelper.GetBusinessBaysBetween(new DateTime(2016, 12, 3), new DateTime(2016, 12, 5));
            Assert.AreEqual(days8, 1);
            var days9 = DateTimeHelper.GetBusinessBaysBetween(null, new DateTime(2016, 11, 19));
            Assert.AreEqual(days9, null);
        }

        [TestMethod]
        public void ConvertTimezone_ShouldSucceed()
        {
            //var time = DateTime.Now;
            var time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            // pst
            //var pstTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneEnum.PST.ToName()));
            var pstTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneEnum.EST.ToName()), TimeZoneInfo.FindSystemTimeZoneById(TimeZoneEnum.PST.ToName()));
            Assert.IsTrue(Math.Abs(time.Hour - pstTime.Hour) == 3);
        }
    }
}
