using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GeoLib.Contracts;
using GeoLib.Data;
using GeoLib.Services;
using NSubstitute;
using NUnit.Framework;

namespace GeoLib.Tests
{
    public class GeoManagerShould
    {
        [Test]
        public void Return_corresponding_zipcode_given_a_zip()
        {
            var zipCode = new ZipCode()
            {
                City = "LINCOLN PARK",
                State = new State() {Abbreviation = "NJ"},
                Zip = "07035"
            };
            var zipRepository = Substitute.For<IZipCodeRepository>();
            zipRepository.GetByZip("07035").Returns(zipCode);

            var manager = new GeoManager(zipCodeRepository: zipRepository);
            var data = manager.GetZipInfo("07035");

            data.City.Should().Be("LINCOLN PARK");
            data.State.Should().Be("NJ");
            data.ZipCode.Should().Be("07035");
        }
    }
}
