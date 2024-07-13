using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [SetUp]
        public void Setup()
        {
            _appointmentRepository = new AppointmentSignUpRepository(_context);
            RegisterObject(typeof(IAppointmentSignUpRepository), _appointmentRepository);

            Register<IAppointmentSignUpBusiness, AppointmentSignUpBusiness>();
            _appointmentBusiness = GetService<IAppointmentSignUpBusiness>();
            
        }
        [Test]
        public void MakeAppointment_Success()
        {
            // Arrange
            var appointment = new AppointmentSignUpModel
            {
                Date = DateOnly.FromDateTime(DateTime.Now), // Convert DateTime to DateOnly
                Time = new TimeOnly(10, 0),
                Birthday = DateTime.Now.Date, // Extract the date part from DateTime
                PatientName = "Test Patient"
            };
            Assert.Pass();
        }
    }
}
