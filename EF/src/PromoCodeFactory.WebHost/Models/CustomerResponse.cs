﻿using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.WebHost.Models
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<PreferenceResponse> Preferences { get; set; }
        public List<PromoCodeShortResponse> PromoCodes { get; set; }

        public CustomerResponse(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
            Preferences = customer.Preferences.Select(x => new PreferenceResponse
            {
                Id = x.PreferenceId,
                Name = x.Preference.Name
            }).ToList();
        }
    }
}