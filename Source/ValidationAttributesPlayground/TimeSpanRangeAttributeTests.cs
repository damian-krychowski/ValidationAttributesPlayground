using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using NUnit.Framework;

namespace ValidationAttributesPlayground
{
    public class TimeSpanRangeAttributeTests
    {
        [Test]
        public void when_timespan_is_less_then_range_minimum_validation_fails()
        {
            //given
            var sut = new TimeSpanRangedWithStrings
            {
                TimeSpan = TimeSpan.Zero
            };

            //then
            Validate(sut).Should().Throw<ValidationException>();
        }

        [Test]
        public void when_timespan_is_greater_then_range_maximum_validation_fails()
        {
            //given
            var sut = new TimeSpanRangedWithStrings
            {
                TimeSpan = TimeSpan.FromHours(13)
            };
            
            //then
            Validate(sut).Should().Throw<ValidationException>();
        }

        [Test]
        public void when_timespan_is_within_the_range_validation_passes()
        {
            //given
            var sut = new TimeSpanRangedWithStrings
            {
                TimeSpan = TimeSpan.FromHours(11)
            };
            
            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        private class TimeSpanRangedWithStrings
        {
            [System.ComponentModel.DataAnnotations.Range(typeof(TimeSpan), "01:00:00", "12:00:00")]
            public TimeSpan TimeSpan { get; set; }
        }


        [Test]
        public void when_range_attribute_is_defined_as_integers_they_are_not_treated_as_milliseconds()
        {
            //given
            var sut = new TimeSpanRangedWithInts
            {
                TimeSpan = TimeSpan.FromMilliseconds(150)
            };
            
            //then
            Validate(sut).Should().Throw<ValidationException>();
        }

        private class TimeSpanRangedWithInts
        {
            [System.ComponentModel.DataAnnotations.Range(100,200)]
            public TimeSpan TimeSpan { get; set; }
        }

        private static Action Validate(object sut)
        {
            return () => Validator.ValidateObject(sut, new ValidationContext(sut), true);
        }
    }
}