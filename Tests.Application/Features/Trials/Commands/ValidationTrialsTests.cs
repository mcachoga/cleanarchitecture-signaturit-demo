using NUnit.Framework;
using Signaturit.Application.Features.Trials.Commands.Create;

namespace Signaturit.Application.UnitTests.Features.Trials.Commands
{
    public class ValidationTrialsTests
    {
        public ValidationTrialsTests()
        {

        }

        [Test]
        public void Trial_is_valid()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "aa", Defense = "KK", Prosecutor = "KK" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 0);
        }

        [Test]
        public void Trial_is_valid_without_signs()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "aa", Defense = "", Prosecutor = "" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 0);
        }

        [Test]
        public void Trial_invalid_sign_with_DEF_no_allowed_chars()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "aa", Defense = "A", Prosecutor = "K" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].ErrorCode == "PredicateValidator");
            Assert.IsTrue(result.Errors[0].PropertyName == "Defense");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Defense should be all charactes as: KNV#");
        }

        [Test]
        public void Trial_invalid_sign_with_PRO_no_allowed_chars()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "aa", Defense = "K", Prosecutor = "A" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].ErrorCode == "PredicateValidator");
            Assert.IsTrue(result.Errors[0].PropertyName == "Prosecutor");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Prosecutor should be all charactes as: KNV#");
        }

        [Test]
        public void Trial_invalid_sign_with_DEF_and_PRO_no_allowed_chars()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "aa", Defense = "A", Prosecutor = "A" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsTrue(result.Errors[0].ErrorCode == "PredicateValidator");
            Assert.IsTrue(result.Errors[0].PropertyName == "Defense");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Defense should be all charactes as: KNV#");
            Assert.IsTrue(result.Errors[1].ErrorCode == "PredicateValidator");
            Assert.IsTrue(result.Errors[1].PropertyName == "Prosecutor");
            Assert.IsTrue(result.Errors[1].ErrorMessage == "Prosecutor should be all charactes as: KNV#");
        }

        [Test]
        public void Trial_invalid_empty_name()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "", Defense = "K", Prosecutor = "K" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].ErrorCode == "NotEmptyValidator");
            Assert.IsTrue(result.Errors[0].PropertyName == "Name");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Name is required.");
        }

        [Test]
        public void Trial_invalid_DEF_so_many_chars()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "KKKKKKKKKKK", Prosecutor = "K" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].PropertyName == "Defense");
            Assert.IsTrue(result.Errors[0].ErrorCode == "MaximumLengthValidator");
        }

        [Test]
        public void Trial_invalid_PRO_so_many_chars()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "K", Prosecutor = "KKKKKKKKKKK" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].PropertyName == "Prosecutor");
            Assert.IsTrue(result.Errors[0].ErrorCode == "MaximumLengthValidator");
        }

        [Test]
        public void Trial_invalid_DEF_and_PRO_so_many_chars()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "KKKKKKKKKKK", Prosecutor = "KKKKKKKKKKK" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsTrue(result.Errors[0].ErrorCode == "MaximumLengthValidator");
            Assert.IsTrue(result.Errors[0].PropertyName == "Defense");
            Assert.IsTrue(result.Errors[1].ErrorCode == "MaximumLengthValidator");
            Assert.IsTrue(result.Errors[1].PropertyName == "Prosecutor");
        }

        [Test]
        public void Trial_valid_DEF_with_undefined_sign()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "KK#", Prosecutor = "KK" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 0);
        }

        [Test]
        public void Trial_valid_PRO_with_undefined_sign()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "K", Prosecutor = "KK#" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 0);
        }

        [Test]
        public void Trial_invalid_DEF_with_several_undefined_sign()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "K##", Prosecutor = "KK" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].ErrorCode == "ValidSignaturePropertyValidator");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Only one of them can have the (#) char. More or 1 (#) char can not be in same sign");
        }

        [Test]
        public void Trial_invalid_PRO_with_several_undefined_sign()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "K", Prosecutor = "KK##" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].ErrorCode == "ValidSignaturePropertyValidator");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Only one of them can have the (#) char. More or 1 (#) char can not be in same sign");
        }

        [Test]
        public void Trial_invalid_DEF_and_PRO_with_several_undefined_sign()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "K##", Prosecutor = "KK##" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].ErrorCode == "ValidSignaturePropertyValidator");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Only one of them can have the (#) char. More or 1 (#) char can not be in same sign");
        }

        [Test]
        public void Trial_invalid_DEF_and_PRO_with_undefined_sign()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "K#", Prosecutor = "KK#" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors[0].ErrorCode == "ValidSignaturePropertyValidator");
            Assert.IsTrue(result.Errors[0].ErrorMessage == "Only one of them can have the (#) char. More or 1 (#) char can not be in same sign");
        }

        [Test]
        public void Trial_valid_any_undefined_sign()
        {
            CreateTrialCommand request = new CreateTrialCommand { Name = "a", Defense = "K#", Prosecutor = "" };

            var validator = new CreateTrialCommandValidator();
            var result = validator.Validate(request);

            Assert.IsTrue(result.Errors.Count ==0);
        }
    }
}

