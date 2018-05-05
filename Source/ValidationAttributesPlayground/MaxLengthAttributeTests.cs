using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using NUnit.Framework;
using ValidationAttributesPlayground.Infrastructure;

namespace ValidationAttributesPlayground
{
    public class MaxLengthAttributeTests
    {
        private const int MaximumLength = 5;

        [Test]
        public void when_string_is_equal_to_maximum_validation_passes()
        {
            //given
            var sut = new StringWithMaximumLength
            {
                String = GenerateString.WithLength(MaximumLength)
            };

            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        [Test]
        public void when_string_is_shorter_than_maximum_validation_passes()
        {
            //given
            var sut = new StringWithMaximumLength
            {
                String = GenerateString.WithLength(MaximumLength - 1)
            };

            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        [Test]
        public void when_string_is_longer_than_maximum_validation_fails()
        {
            //given
            var sut = new StringWithMaximumLength
            {
                String = GenerateString.WithLength(MaximumLength + 1)
            };

            //then
            Validate(sut).Should().Throw<ValidationException>();
        }
        
        public class StringWithMaximumLength
        {
            [MaxLength(MaximumLength)]
            public string String { get; set; }
        }

        private static Action Validate(object sut)
        {
            return () => Validator.ValidateObject(sut, new ValidationContext(sut), true);
        }
    }
}