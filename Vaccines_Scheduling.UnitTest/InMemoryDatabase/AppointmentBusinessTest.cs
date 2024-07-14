using NUnit.Framework;
using Vaccines_Scheduling.Business.Businesses;
using Vaccines_Scheduling.Business.Interface.IBusiness;
using Vaccines_Scheduling.Entity.Model;
using Vaccines_Scheduling.Repository.Interface.IRepositories;
using Vaccines_Scheduling.Repository.Repositories;

namespace Vaccines_Scheduling.UnitTest.InMemoryDatabase
{
    public class AppointmentBusinessTest : BaseTest
    {
        private IAppointmentSignUpBusiness _appointmentBusiness;
        private IAppointmentSignUpRepository _appointmentRepository;
        private IPatientSignUpRepository _patientRepository;

        [SetUp]
        public void Setup()
        {
            _appointmentRepository = new AppointmentSignUpRepository(_context);
            RegisterObject(typeof(IAppointmentSignUpRepository), _appointmentRepository);

            _patientRepository = new PatientSignUpRepository(_context);
            RegisterObject(typeof(IPatientSignUpRepository), _patientRepository);

            Register<IPatientSignUpRepository, PatientSignUpRepository>();
            _patientRepository = GetService<IPatientSignUpRepository>();

            Register<IAppointmentSignUpBusiness, AppointmentSignUpBusiness>();
            _appointmentBusiness = GetService<IAppointmentSignUpBusiness>();
            
        }
        [Test]
        public void MakeAppointment_Success()
        {
            var id = "1";
            // Arrange
            var appointment = new AppointmentSignUpModel
            {
                PatientName = "Test",
                Birthday = DateOnly.Parse("2024-05-21"),
                Date = DateOnly.Parse("2024-07-13"),
                Time = TimeOnly.Parse("18:00")
            };
            // Act
            async Task action() => await _appointmentBusiness.InsertAppointment(appointment, id);

            // Assert
            Assert.DoesNotThrowAsync(action);
        }
    }
}
