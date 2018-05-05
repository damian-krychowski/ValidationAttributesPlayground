using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using NUnit.Framework;
using ValidationAttributesPlayground.Infrastructure;

namespace ValidationAttributesPlayground
{
    public class MinAndMaxLengthAttributesTests
    {
        private const int MinLength = 5;
        private const int MaxLength = 10;

        [Test]
        public void when_string_is_within_range_validation_passes()
        {
            //given
            var sut = new StringWithMinAndMaxLength
            {
                String = GenerateString.WithLength((MinLength + MaxLength) / 2)
            };

            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        [Test]
        public void when_string_is_longer_than_max_validation_faile()
        {
            //given
            var sut = new StringWithMinAndMaxLength
            {
                String = GenerateString.WithLength(MaxLength + 1)
            };

            //then
            Validate(sut).Should().Throw<ValidationException>();
        }

        [Test]
        public void when_string_is_shorter_than_min_validation_fails()
        {
            //given
            var sut = new StringWithMinAndMaxLength
            {
                String = GenerateString.WithLength(MinLength - 1)
            };

            //then
            Validate(sut).Should().Throw<ValidationException>();
        }

        public class StringWithMinAndMaxLength
        {
            [MinLength(MinLength)]
            [MaxLength(MaxLength)]
            public string String { get; set; }
        }

        private static Action Validate(object sut)
        {
            return () => Validator.ValidateObject(sut, new ValidationContext(sut), true);
        }
    }
}