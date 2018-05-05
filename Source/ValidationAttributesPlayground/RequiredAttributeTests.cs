using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ValidationAttributesPlayground
{
    public class RequiredAttributeTests
    {
        private const string ErrorMessage = "Bar is required";

        [Test]
        public void when_bar_is_null_validation_fails()
        {
            //given
            var foo = new Foo
            {
                Bar = null
            };

            //when
            Action validate = () => Validator.ValidateObject(foo, new ValidationContext(foo));

            //then
            validate
                .Should().Throw<ValidationException>()
                .Which.Message
                .Should().Be(ErrorMessage);
        }

        [Test]
        public void when_bar_is_not_null_foo_is_valid()
        {
            //given
            var foo = new Foo
            {
                Bar = new object()
            };

            //when
            Action validate = () => Validator.ValidateObject(foo, new ValidationContext(foo));

            //then
            validate.Should().NotThrow<ValidationException>();
        }

        private class Foo
        {
            [Required(ErrorMessage = ErrorMessage)]
            public object Bar { get; set; }
        }
    }
}
