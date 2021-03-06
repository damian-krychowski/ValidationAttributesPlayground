﻿using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using NUnit.Framework;
using ValidationAttributesPlayground.Infrastructure;

namespace ValidationAttributesPlayground
{
    public class MaxLengthAttributeTests
    {
        private const int MaxLength = 5;

        [Test]
        public void when_string_is_equal_to_max_validation_passes()
        {
            //given
            var sut = new StringWithMaxLength
            {
                String = GenerateString.WithLength(MaxLength)
            };

            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        [Test]
        public void when_string_is_shorter_than_max_validation_passes()
        {
            //given
            var sut = new StringWithMaxLength
            {
                String = GenerateString.WithLength(MaxLength - 1)
            };

            //then
            Validate(sut).Should().NotThrow<ValidationException>();
        }

        [Test]
        public void when_string_is_longer_than_max_validation_fails()
        {
            //given
            var sut = new StringWithMaxLength
            {
                String = GenerateString.WithLength(MaxLength + 1)
            };

            //then
            Validate(sut).Should().Throw<ValidationException>();
        }
        
        public class StringWithMaxLength
        {
            [MaxLength(MaxLength)]
            public string String { get; set; }
        }

        private static Action Validate(object sut)
        {
            return () => Validator.ValidateObject(sut, new ValidationContext(sut), true);
        }
    }
}