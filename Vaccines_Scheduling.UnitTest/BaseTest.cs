using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Vaccines_Scheduling.Repository;

namespace Vaccines_Scheduling.UnitTest
{
    public class BaseTest
    {
        private readonly IServiceCollection ServiceCollection = new ServiceCollection();
        protected Context _context;

        protected void Register<I,T>() where I : class where T : class, I
               => ServiceCollection.AddScoped<I, T>();

        protected I GetService<I>() where I : class
                        => ServiceCollection.BuildServiceProvider().GetService<I>();

        protected void RegisterObject<Tp, T>(Tp type, T obj) where Tp : Type where T : class
            => ServiceCollection.AddSingleton(type, obj);

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            ConfigureInMemoryDataBase();
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            _context.Dispose();
        }

        private void ConfigureInMemoryDataBase()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("InMemoryDatabase")
                .Options;

            _context = new Context(options);

            if (_context.Database.IsInMemory())
            {
                _context.Database.EnsureCreated();
            }
        }

    }
}
