using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using NUnit.Framework;
using ValidationAttributesPlayground.Infrastructure;

namespace ValidationAttributesPlayground
{
    public class MinLengthAttributeTests
    {
        private const int MinLength = 5;

        [Test]
        public void when_string_is_equal_to_min_validation_passes()
        {
            //given
            var sut = new StringWithMinLength
            {
                String = GenerateString.WithLength(MinLength)
            };

            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        [Test]
        public void when_string_is_longer_than_min_validation_passes()
        {
            //given
            var sut = new StringWithMinLength
            {
                String = GenerateString.WithLength(MinLength + 1)
            };

            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        [Test]
        public void when_string_is_shorter_than_min_validation_fails()
        {
            //given
            var sut = new StringWithMinLength
            {
                String = GenerateString.WithLength(MinLength - 1)
            };

            //then
            Validate(sut).Should().Throw<ValidationException>();
        }

        public class StringWithMinLength
        {
            [MinLength(MinLength)]
            public string String { get; set; }
        }

        private static Action Validate(object sut)
        {
            return () => Validator.ValidateObject(sut, new ValidationContext(sut), true);
        }
    }
}