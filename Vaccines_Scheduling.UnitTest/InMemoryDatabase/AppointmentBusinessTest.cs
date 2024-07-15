using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Vaccines_Scheduling.Business.Businesses;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Repository.Interface.IRepositories;
using Vaccines_Scheduling.Repository.Repositories;
using Vaccines_Scheduling.UnitTest.Builders;
using Vaccines_Scheduling.Utility.Exceptions;
using Vaccines_Scheduling.Utility.Messages;
using Vaccines_Scheduling.Validators.Fluent;

namespace Vaccines_Scheduling.UnitTest.InMemoryDatabase
{
    public class AppointmentBusinessTest : BaseTest
    {
        private IAppointmentSignUpBusiness _appointmentBusiness;
        private IAppointmentSignUpRepository _appointmentRepository;
        private IPatientSignUpRepository _patientRepository;
        private IPatientSignUpBusiness _patientBusiness;

        [SetUp]
        public async Task Setup()
        {
            _appointmentRepository = new AppointmentSignUpRepository(_context);
            RegisterObject(typeof(IAppointmentSignUpRepository), _appointmentRepository);

            _patientRepository = new PatientSignUpRepository(_context);
            RegisterObject(typeof(IPatientSignUpRepository), _patientRepository);

            Register<IPatientSignUpBusiness, PatientSignUpBusiness>();
            _patientBusiness = GetService<IPatientSignUpBusiness>();

            Register<IAppointmentSignUpBusiness, AppointmentSignUpBusiness>();
            _appointmentBusiness = GetService<IAppointmentSignUpBusiness>();

            var existingPatient = await _context.Patient.FirstOrDefaultAsync(p => p.Name == "Test");
            if (existingPatient == null)
            {
                var patient = new PatientModelBuilder().Build();
                await _patientBusiness.InsertPatient(patient);
            }

        }
        // Make Appointment
        [Test]
        public void MakeAppointment_Success()
        {
            // Arrange
            var id = "1";

            var appointment = new AppointmentModelBuilder().Build();
            var validator = new AppointmentSignUpValidator();

            var result = validator.Validate(appointment);
            Assert.IsTrue(result.IsValid);
            // Act
            async Task action() => await _appointmentBusiness.InsertAppointment(appointment, id);

            // Assert
            Assert.DoesNotThrowAsync(action);
        }
        
        // Fluent Validation Inputs
        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        public void MakeAppointment_InvalidName(string name)
        {
            // Arrange
            var appointment = new AppointmentModelBuilder().Build();
            appointment.PatientName = name;
            var validator = new AppointmentSignUpValidator();

            // Act
            var result = validator.Validate(appointment);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [TestCase("2025-12-31")]
        public void MakeAppointment_InvalidBirthday(string birthday)
        {
            // Arrange
            var appointment = new AppointmentModelBuilder().Build();
            appointment.Birthday = DateOnly.Parse(birthday);
            var validator = new AppointmentSignUpValidator();

            // Act
            var result = validator.Validate(appointment);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [TestCase("2023-08-14")]
        public static void MakeAppointment_InvalidDate(string date)
        {
            // Arrange
            var appointment = new AppointmentModelBuilder().Build();
            appointment.Date = DateOnly.Parse(date);
            var validator = new AppointmentSignUpValidator();

            // Act
            var result = validator.Validate(appointment);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        // Business Exceptions

        [TestCase("99999")]
        public void MakeAppointment_NotFoundPatient(string id)
        {   
            // Arrange
            var appointment = new AppointmentModelBuilder().Build();
            var validator = new AppointmentSignUpValidator();
            var result = validator.Validate(appointment);

            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.InsertAppointment(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundPatient));

        }

        [TestCase("WrongName")]
        public void MakeAppointment_NotMatchingNames(string name)
        {
            // Arrange
            var id = "1";
            
            var appointment = new AppointmentModelBuilder().Build();
            appointment.PatientName = name;
            var validator = new AppointmentSignUpValidator();

            var result = validator.Validate(appointment);

            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.InsertAppointment(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.InvalidName));

        }
        [TestCase("1900-01-01")]
        public void MakeAppointment_NotMatchingBirthdays(string date)
        {
            // Arrange 
            var id = "1";

            var appointment = new AppointmentModelBuilder().Build();
            appointment.Birthday = DateOnly.Parse(date);
            var validator = new AppointmentSignUpValidator();

            var result = validator.Validate(appointment);

            Assert.IsTrue(result.IsValid);

            // Act 
            async Task action() => await _appointmentBusiness.InsertAppointment(appointment, id);

            // Assert

            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.InvalidBdayMatch));

        }

        [Test]
        public async Task MakeAppointment_FullDay()
        {
            // Arrange
            var id = "1";
            AppointmentSignUpModel appointment;
            string time;

            // Insert the appointments
            for (int i = 0;  i < 20; i++) 
            {
                if (i < 10)
                {
                    time = "0" + i.ToString() + ":00";
                }
                else
                {
                    time = i.ToString() + ":00";
                }
                appointment = new AppointmentModelBuilder().Build();
                appointment.Time = TimeOnly.Parse(time);
                await _appointmentBusiness.InsertAppointment(appointment, id);
            }

            // Act
            appointment = new AppointmentModelBuilder().Build();
            async Task action() => await _appointmentBusiness.InsertAppointment(appointment, id);
            
            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.FullDay));
        }

        [Test]
        public async Task MakeAppointment_FullTime()
        {   
            //Arrange
            var id = "1";
            AppointmentSignUpModel appointment;

            for (int i = 0; i < 2; i++)
            {
                appointment = new AppointmentModelBuilder().Build();
                await _appointmentBusiness.InsertAppointment(appointment, id);
            }

            // Act
            appointment = new AppointmentModelBuilder().Build();
            async Task action() => await _appointmentBusiness.InsertAppointment(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.FullTime, appointment.Time));

        }

        // Delete Appointment
        // Fluent Validation Inputs

        [TestCase("")]
        [TestCase(null)]
        public static void DeleteAppointment_InvalidStatus(string status)
        {
            // Arrange
            var appointment = new ChangeAppointmentBuilder().Build();
            appointment.Status = status;
            var validator = new ChangeAppointmentValidator();

            // Act
            var result = validator.Validate(appointment);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        // Business Exceptions

        [TestCase("123123")]
        public void DeleteAppointment_NotFoundPatient(string id)
        {
            // Arrange
            var appointment = new ChangeAppointmentBuilder().Build();
            var validator = new ChangeAppointmentValidator();
            var result = validator.Validate(appointment);

            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.DeleteAppointment(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundPatient));
        }

        [Test]
        public void DeleteAppointment_NoPatientAppointments()
        {
            // Arrange
            var id = "1";
            var appointment = new ChangeAppointmentBuilder().Build();
            var validator = new ChangeAppointmentValidator();
            var result = validator.Validate(appointment);

            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.DeleteAppointment(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundAppointment));
        }

        [Test]
        public async Task DeleteAppointment_NotMatchingAppointment()
        {
            // Arrange
            var id = "1";
            var appointment = new ChangeAppointmentBuilder().Build();
            var validator = new ChangeAppointmentValidator();
            appointment.Date = DateOnly.Parse("2024-12-15");

            var newAppointment = new AppointmentModelBuilder().Build();
            await _appointmentBusiness.InsertAppointment(newAppointment, id);

            var result = validator.Validate(appointment);
            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.DeleteAppointment(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundAppointment));
        }

        // Change Appointment Status
        //Fluent Validation Inputs

        [TestCase("")]
        [TestCase(null)]
        public static void ChangeAppointmentStatus_InvalidStatus(string status)
        {
            // Arrange
            var appointment = new ChangeAppointmentBuilder().Build();
            appointment.Status = status;
            var validator = new ChangeAppointmentValidator();

            // Act
            var result = validator.Validate(appointment);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        // Business Exceptions

        [TestCase("123123")]
        public void ChangeAppointmentStatus_NotFoundPatient(string id)
        {
            // Arrange
            var appointment = new ChangeAppointmentBuilder().Build();
            var validator = new ChangeAppointmentValidator();
            var result = validator.Validate(appointment);

            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.ChangeAppointmentStatus(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundPatient));
        }

        [Test]
        public void ChangeAppointmentStatus_NoPatientAppointments()
        {
            // Arrange
            var id = "1";
            var appointment = new ChangeAppointmentBuilder().Build();
            var validator = new ChangeAppointmentValidator();
            var result = validator.Validate(appointment);

            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.ChangeAppointmentStatus(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundAppointment));
        }

        [Test]
        public async Task ChangeAppointmentStatus_NotMatchingAppointment()
        {
            // Arrange
            var id = "1";
            var appointment = new ChangeAppointmentBuilder().Build();
            var validator = new ChangeAppointmentValidator();
            appointment.Date = DateOnly.Parse("2024-12-15");

            var newAppointment = new AppointmentModelBuilder().Build();
            await _appointmentBusiness.InsertAppointment(newAppointment, id);

            var result = validator.Validate(appointment);
            Assert.IsTrue(result.IsValid);

            // Act
            async Task action() => await _appointmentBusiness.ChangeAppointmentStatus(appointment, id);

            // Assert
            var exception = Assert.ThrowsAsync<BusinessException>(action);
            Assert.IsTrue(exception.Message == string.Format(InfraMessages.NotFoundAppointment));
        }
    }
}
