using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xunit;
using Xamarin.Forms;
using Newtonsoft.Json;

using BatsBadmintonFixtures.ViewModels;
using BatsBadmintonFixtures.Models;

namespace BatsBadmintonFixtures.Tests
{ 
    public class FixturesViewModelTests
    {
        #region DATA
        Fixture[] fixtures =
        {
            new Fixture{
                ID = 6,
                League = "Brighton Division 3",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-01-16",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = 7,
                League = "Brighton Division 2",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-02-20",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = 10,
                League = "Brighton Division 3",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-02-20",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = 11,
                League = "Brighton Division 3",
                BatsTeam = "Men's Combination",
                TeamVs = "Chanctonbury",
                Venue = "Home",
                Date = "2019-03-23",
                Time = "20:00:00"
            },
            new Fixture
            {
                ID = 20,
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

    }
}
