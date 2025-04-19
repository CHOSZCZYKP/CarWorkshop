using Xunit;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CarWorkshop.Domain.Entities.Tests
{
    public class CarWorkshopTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            //arrange
            var carWorkshopp = new CarWorkshop();
            carWorkshopp.Name = "Test Workshop";

            //act
            carWorkshopp.EncodeName();

            //assert
            carWorkshopp.EncodedName.Should().Be("test-workshop");
        }

        [Fact()]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            //arrange
            var carWorkshopp = new CarWorkshop();

            //act
            Action action = () => carWorkshopp.EncodeName();

            //assert
            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}