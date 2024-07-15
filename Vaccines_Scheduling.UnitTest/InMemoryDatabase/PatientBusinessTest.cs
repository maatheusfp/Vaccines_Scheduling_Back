using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Business.Businesses;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Repository.Interface.IRepositories;
using Vaccines_Scheduling.Repository.Repositories;
using Vaccines_Scheduling.UnitTest.Builders;
using Vaccines_Scheduling.Utility.Exceptions;
using Vaccines_Scheduling.Utility.Messages;
using Vaccines_Scheduling.Validators.Fluent;
using static System.Collections.Specialized.BitVector32;

namespace Vaccines_Scheduling.UnitTest.InMemoryDatabase
{
    public class PatientBusinessTest : BaseTest
    {
        private IPatientSignUpBusiness _patientBusiness;
        private IPatientSignUpRepository _patientRepository;

        [SetUp]
        public async Task Setup()
        {
            _patientRepository = new PatientSignUpRepository(_context);
            RegisterObject(typeof(IPatientSignUpRepository), _patientRepository);

            Register<IPatientSignUpBusiness, PatientSignUpBusiness>();
            _patientBusiness = GetService<IPatientSignUpBusiness>();
        }

        // Patient SignUp

        [Test]
        public void PatientSignUp_success()
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            var validator = new PatientSignUpValidator();

            var result = validator.Validate(patient);
            Assert.IsTrue(result.IsValid);

            // Act 
            async Task action() => await _patientBusiness.InsertPatient(patient);

            // Assert

            Assert.DoesNotThrowAsync(action);
        }

        // Fluent Validation inputs

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        public void PatientSignUp_InvalidName(string name)
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            patient.Name = name;
            var validator = new PatientSignUpValidator();
            var result = validator.Validate(patient);

            Assert.IsFalse(result.IsValid);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        public void PatientSignUp_InvalidLogin(string login)
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            patient.Login = login;
            var validator = new PatientSignUpValidator();
            var result = validator.Validate(patient);

            Assert.IsFalse(result.IsValid);
        }

        [TestCase("2025-11-11")]
        public void PatientSignUp_InvalidBirthday(string birthday)
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            patient.Birthday = DateOnly.Parse(birthday);
            var validator = new PatientSignUpValidator();
            var result = validator.Validate(patient);

            Assert.IsFalse(result.IsValid);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        [TestCase("abcd")]
        public void PatientSignUp_InvalidPassword(string password)
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            patient.Password = password;
            var validator = new PatientSignUpValidator();
            var result = validator.Validate(patient);

            Assert.IsFalse(result.IsValid);
        }

        // Business rules 

        [Test]
        public async Task PatientSignUp_ExistingPatient()
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            var validator = new PatientSignUpValidator();

            var result = validator.Validate(patient);
            Assert.IsTrue(result.IsValid);

            await _patientBusiness.InsertPatient(patient);

            // Act 
            async Task action() => await _patientBusiness.InsertPatient(patient);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.RegisteredPatient));
        }

        // Patient Find
        [Test]
        public async Task PatientFind_success()
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            var validator = new PatientSignUpValidator();

            var result = validator.Validate(patient);
            Assert.IsTrue(result.IsValid);

            await _patientBusiness.InsertPatient(patient);

            // Act 
            async Task action() => await _patientBusiness.FindPatient(patient.Login);

            // Assert
            Assert.DoesNotThrowAsync(action);
        }

        [TestCase("123123")]
        public async Task PatientFind_NotFound(string login)
        {
            // Arrange
            
            // Act 
            async Task action() => await _patientBusiness.FindPatient(login);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundPatient));
        }

        // Patient Delete
        [Test]
        public async Task PatientDelete_success()
        {
            // Arrange
            var patient = new PatientModelBuilder().Build();
            var validator = new PatientSignUpValidator();

            var result = validator.Validate(patient);
            Assert.IsTrue(result.IsValid);

            await _patientBusiness.InsertPatient(patient);

            // Act 
            async Task action() => await _patientBusiness.DeletePatient(patient.Login);

            // Assert
            Assert.DoesNotThrowAsync(action);
        }

        [TestCase("123123")]
        public async Task PatientDelete_NotFound(string login)
        {
            // Arrange

            // Act 
            async Task action() => await _patientBusiness.DeletePatient(login);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundPatient));
        }

    }
}
