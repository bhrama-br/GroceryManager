using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GroceryManager.Database
{
    public class DatabaseMigrations
    {
        private static readonly ILogger Logger = Log.ForContext<DatabaseMigrations>();

        private readonly DataContext _context;

        public DatabaseMigrations(DataContext context)
        {
            _context = context;
        }

        public void Migrate()
        {
            try
            {
                var previousTimeout = _context.Database.GetCommandTimeout(); // Save the current timeout

                Logger.Information("Starting database migration...");
                _context.Database.SetCommandTimeout(10000); // Set the timeout to 2 minutes

                //using (var transaction = _context.Database.BeginTransaction())
                //{
                _context.Database.Migrate();

                _context.Database.SetCommandTimeout(previousTimeout); // Revert the timeout back to the original value

                //transaction.Commit();
                //}
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "Error migrating database.");

                throw;
            }
        }
    }
}