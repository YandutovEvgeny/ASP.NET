﻿namespace Pcf.Administration.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly DataContext _dataContext;

        public EfDbInitializer(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void InitializeDb()
        {
            var provider = _dataContext.Database.ProviderName;

            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();
            
            _dataContext.AddRange(FakeDataFactory.Employees);
            _dataContext.SaveChanges();
        }
    }
}