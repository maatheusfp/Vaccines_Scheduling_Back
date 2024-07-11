using Microsoft.EntityFrameworkCore;
using System.Data;
using Vaccines_Scheduling.Repository.Interface;

namespace Vaccines_Scheduling.Repository
{
    public class MandatoryTransaction : IMandatoryTransaction
    {
        private readonly Context _context;

        public MandatoryTransaction(Context context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            var activeTransaction = _context.Database.CurrentTransaction;
            if (activeTransaction == null)
            {
                var connection = _context.Database.GetDbConnection();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var transaction = await connection.BeginTransactionAsync(isolationLevel);
                await _context.Database.UseTransactionAsync(transaction);
            }
        }

        public async Task CommitTransactionsAsync()
        {
            var contextHasChanges = _context.ChangeTracker.HasChanges();

            if (contextHasChanges)
                await _context.SaveChangesAsync();

            var activeTransaction = _context.Database.CurrentTransaction;
            await activeTransaction.CommitAsync();
            await activeTransaction.DisposeAsync();
        }

        public async Task RollbackTransactionsAsync()
        {
            var activeTransaction = _context.Database.CurrentTransaction;
            if (activeTransaction != null)
            {
                await activeTransaction.RollbackAsync();
                await activeTransaction.DisposeAsync();
            }
        }
    }
}
