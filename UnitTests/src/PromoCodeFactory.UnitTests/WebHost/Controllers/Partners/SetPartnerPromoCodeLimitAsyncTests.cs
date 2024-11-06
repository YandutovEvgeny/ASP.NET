using AutoFixture.AutoMoq;
using AutoFixture;
using Moq;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Controllers;
using Xunit;
using System;
using PromoCodeFactory.WebHost.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.UnitTests.WebHost.Controllers.Partners
{
    public class SetPartnerPromoCodeLimitAsyncTests
    {
        private readonly Mock<IRepository<Partner>> _partnersRepositoryMock;
        private readonly PartnersController _controller;

        public SetPartnerPromoCodeLimitAsyncTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            _partnersRepositoryMock = fixture.Freeze<Mock<IRepository<Partner>>>();
            _controller = fixture.Build<PartnersController>().OmitAutoProperties().Create();
        }

        public SetPartnerPromoCodeLimitRequest CreateRequest()
        {
            return new Fixture()
                .Build<SetPartnerPromoCodeLimitRequest>()
                .With(e => e.EndDate, DateTime.UtcNow)
                .With(e => e.Limit, 10)
                .Create();
        }

        public Partner CreateBasePartner()
        {
            return new Partner()
            {
                Id = Guid.Parse("7d994823-8226-4273-b063-1a95f3cc1df8"),
                Name = "Суперигрушки",
                IsActive = true,
                NumberIssuedPromoCodes = 5,
                PartnerLimits = new List<PartnerPromoCodeLimit>()
                {
                    new PartnerPromoCodeLimit()
                    {
                        Id = Guid.Parse("e00633a5-978a-420e-a7d6-3e1dab116393"),
                        CreateDate = new DateTime(2020, 07, 9),
                        EndDate = new DateTime(2020, 10, 9),
                        Limit = 100
                    }
                }
            };
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_PartnerIsNotFound_ReturnStatus404NotFound()
        {
            //Arrange
            var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
            var limitRequest = CreateRequest();
            Partner partner = null;
            
            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId))
                .ReturnsAsync(partner);

            //Act
            var result = await _controller.SetPartnerPromoCodeLimitAsync(partnerId, limitRequest);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_PartnerIsBlocked_ReturnStatus400BadRequest()
        {
            //Arrange
            var partnerId = Guid.Parse("0da65561-cf56-4942-bff2-22f50cf70d43");
            var limitRequest = CreateRequest();
            Partner partner = null;

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId))
                .ReturnsAsync(partner);

            //Act
            var result = await _controller.SetPartnerPromoCodeLimitAsync(partnerId, limitRequest);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_LimitExists_ZeroPromocodes()
        {
            //Arrange
            var partner = CreateBasePartner();
            var partnerId = partner.Id;
            var limitRequest = CreateRequest();

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId))
                .ReturnsAsync(partner);

            //Act
            var result = await _controller.SetPartnerPromoCodeLimitAsync(partnerId, limitRequest);

            //Assert
            partner.NumberIssuedPromoCodes.Should().Be(0);
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_LimitNotExists_MoreThanZeroPromocodes()
        {
            //Arrange
            Partner partner = CreateBasePartner();
            var limit = partner.PartnerLimits.First();
            limit.CancelDate = new DateTime(2020,06,16);
            var partnerId = partner.Id;
            var limitRequest = CreateRequest();

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId))
                .ReturnsAsync(partner);

            //Act
            var result = await _controller.SetPartnerPromoCodeLimitAsync(partnerId, limitRequest);

            //Assert
            partner.NumberIssuedPromoCodes.Should().NotBe(0);
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_CancelDateSeed_DisablePreviousLimit()
        {
            //Arrange
            var partner = CreateBasePartner();
            var partnerId = partner.Id;
            var limitRequest = CreateRequest();

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId))
                .ReturnsAsync(partner);

            //Act
            var result = await _controller.SetPartnerPromoCodeLimitAsync(partnerId, limitRequest);

            //Assert
            var activeLimit = partner.PartnerLimits.FirstOrDefault(x => x.CancelDate.HasValue);
            activeLimit.CancelDate.Should().BeCloseTo(DateTime.Now, new TimeSpan(0,1,0));
            partner.NumberIssuedPromoCodes.Should().Be(0);
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_LimitMoreThanZero_True()
        {
            //Arrange
            var partner = CreateBasePartner();
            var partnerId = partner.Id;
            var limitRequest = CreateRequest();

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId))
                .ReturnsAsync(partner);

            //Act
            var result = await _controller.SetPartnerPromoCodeLimitAsync(partnerId, limitRequest);

            //Assert
            limitRequest.Limit.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_SaveLimitInDataBase_SavedLimit()
        {
            //Arrange
            Partner partner = CreateBasePartner();
            var limit = partner.PartnerLimits.First();
            limit.CancelDate = new DateTime(2020,06,16);
            var partnerId = partner.Id;
            var limitRequest = CreateRequest();

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId))
                .ReturnsAsync(partner);

            //Act
            var result = await _controller.SetPartnerPromoCodeLimitAsync(partnerId, limitRequest);

            //Assert
            var addedLimit = partner.PartnerLimits
                .FirstOrDefault(x => x.Limit == limitRequest.Limit && x.EndDate == limitRequest.EndDate);
            addedLimit.Should().NotBeNull();
        }
    }
}