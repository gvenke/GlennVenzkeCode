﻿using System;
using System.Collections.Generic;
using System.Linq;
using Test1.Entities;

namespace Test1
{
    
    public class SalesOrderHelper: IDisposable
    {
        private DeveloperInteriewContext _developerInterviewContext;

        public SalesOrderHelper()
        {
            _developerInterviewContext = new DeveloperInteriewContext();
        }

        public IEnumerable<SalesOrderHeader> GetOrdersByCustomerName(string firstName)
        {
            return _developerInterviewContext.SalesOrderHeaders.Where(o => o.Customer.FirstName == firstName);
        }

        public IEnumerable<SalesOrderDetail> GetSalesOrderDetails()
        {
            return _developerInterviewContext.SalesOrderDetails;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _developerInterviewContext.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SalesOrderHelper() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
