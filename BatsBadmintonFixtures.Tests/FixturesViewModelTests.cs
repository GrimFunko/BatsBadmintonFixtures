using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xunit;
using Xamarin.Forms;
using Newtonsoft.Json;
using MvvmHelpers;

using BatsBadmintonFixtures.ViewModels;
using BatsBadmintonFixtures.Models;
using BatsBadmintonFixtures.Config;

namespace BatsBadmintonFixtures.Tests
{ 
    public class FixturesViewModelTests
    {
        #region DATA
        Fixture[] fixtures =
        {
            new Fixture{
                ID = "aaaa-aaaa",
                League = "Brighton Division 3",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-01-16",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = "abbb-bbbb",
                League = "Brighton Division 2",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-02-20",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = "abbb-bbbc",
                League = "Brighton Division 3",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-02-20",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = "abbb-bbcc",
                League = "Brighton Division 3",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-03-23",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = "abbb-bccc",
                League = "Brighton Division 1",
                BatsTeam = "Mixed Doubles",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-03-25",
                Time = "20:00:00"
            }
        };
        #endregion

        [Fact]
        public void GetListOfDistinctDates_ShouldReturnCorrectList()
        {

            // Arrange
            var fvm = new FixturesViewModel();

            List<string> expected = new List<string>
            {
                "2019-01-16",
                "2019-02-20",
                "2019-03-23",
                "2019-03-25"
            };

            // Actual
            var actual = fvm.GetListOfDistinctDates(fixtures);
            // Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SortIntoDateGroups_RangeCollectionShouldContainJan()
        {
            var fvm = new FixturesViewModel();
            GroupedFixtures expected = new GroupedFixtures() { LongName = "January", ShortName = "Jan" };
            expected.Add(fixtures[0]);

            var actual = fvm.SortIntoDateGroups(fixtures);

            Assert.Contains(expected, actual); 
        }

        [Fact]
        public void SortIntoDateGroups_GroupedfixturesCountShouldBeNonZero()
        {
            var fvm = new FixturesViewModel();

            var actual = fvm.SortIntoDateGroups(fixtures);

            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void AddFixturesToDateGroup_ShouldAddFixtures()
        {
            var fvm = new FixturesViewModel();
            var actual = fvm.SortIntoDateGroups(fixtures);

            Assert.Equal(3, actual.Count());
        }

        [Fact]
        public void AddFixturesToDateGroup_ShouldIncludeCorrectFixtureGroups()
        {
            var fvm = new FixturesViewModel();
            List<GroupedFixtures> groupedList = new List<GroupedFixtures>();
            var Jan = new GroupedFixtures() { LongName = "January", ShortName = "Jan" };
            var Feb = new GroupedFixtures() { LongName = "February", ShortName = "Feb" };
            var Mar = new GroupedFixtures() { LongName = "March", ShortName = "Mar" };

            Jan.Add(fixtures[0]);
            Feb.Add(fixtures[1]);
            Feb.Add(fixtures[2]);
            Mar.Add(fixtures[3]);
            Mar.Add(fixtures[4]);

            groupedList.Add(Jan);
            groupedList.Add(Feb);
            groupedList.Add(Mar);

            var expected = groupedList; 
            var actual = fvm.SortIntoDateGroups(fixtures);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPostJson_ShouldReturnCorectlyFormattedString()
        {

            string expected = "{\"type\":\"fixtures-request\"}";

            object obj = new { type = "fixtures-request" };
            // Actual
            string actual = Utilities.GetJsonString(obj);
            // Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveOldFixtures_ShouldRemoveCorrectly()
        {
            Fixture[] expected = new Fixture[]
            {
                new Fixture
                {
                    ID = "abbb-bbcc",
                    League = "Brighton Division 3",
                    BatsTeam = "Men's Combination",
                    TeamVs = "Chanctonbury",
                    Venue = "Home",
                    Date = "2019-03-23",
                    Time = "20:00:00"
                },
                new Fixture
                {
                    ID = "abbb-bccc",
                    League = "Brighton Division 1",
                    BatsTeam = "Mixed Doubles",
                    TeamVs = "Chanctonbury",
                    Venue = "Home",
                    Date = "2019-03-25",
                    Time = "20:00:00"
                }
            };

            var fvm = new FixturesViewModel();
            var actual = fvm.RemoveOldFixtures(fixtures);

            Assert.Equal(2, actual.Length);
            //Assert.Equal(expected, actual);
        }

        [Fact]
        public void CacheAvailable_ShouldCorrectlyIdentifyIfCacheIsAvailable()
        {
            var fvm = new FixturesViewModel();
            Application.Current.Properties.Add("Fixture", string.Empty);
            Application.Current.Properties.Add("fixCacheDate", DateTime.Now);

            bool expected = true;

            var actual = fvm.CacheAvailable();

            Assert.Equal(expected, actual);

        }
    }
}
