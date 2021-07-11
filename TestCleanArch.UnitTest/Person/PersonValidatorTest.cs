using FluentValidation.TestHelper;
using TestCleanArch.Application.Common.Validator.Person;
using TestCleanArch.Application.Persons.Command.CreatePesron;
using Xunit;

namespace TestCleanArch.UnitTest.Person
{
    public class PersonValidatorTest
    {
        private readonly CreatePersonCommandValidator _validator;

        public PersonValidatorTest()
        {
            _validator = new CreatePersonCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_when_FirstName_Is_Null()
        {
            var model = new CreatePersonCommand();

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(p => p.FirstName);
        }



        [Fact]
        public void Should_Have_Error_when_LastName_Is_Null()
        {
            var model = new CreatePersonCommand();

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(p => p.LastName);
        }

        [Fact]
        public void Should_Have_Error_when_Email_Is_NotValid()
        {
            var model = new CreatePersonCommand
            {
                Email = "mamad",
                FirstName = "name",
                LastName = "lanme"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(p => p.Email);
        }

    }
}
