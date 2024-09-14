using PromoCodeFactory.Core.Domain.Administration;
using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.WebHost.Models.Extensions
{
    public static class EmployeeExtension
    {
        public static void FillEmployee(this Employee employee, EmployeeRequest employeeRequest, List<Role> roles)
        {
            employee.FirstName = employeeRequest.FirstName;
            employee.LastName = employeeRequest.LastName;
            employee.Email = employeeRequest.Email;
            
            employee.Roles = roles
                .Where(r => employeeRequest.Roles?.Select(rr => rr.Id).Contains(r.Id) ?? false)
                .ToList();

            employee.AppliedPromocodesCount = employeeRequest.AppliedPromocodesCount;
        }
    }
}