using Api.Core.Configuration;
using Api.Core.Models;
using Api.Core.Services;
using Api.Core.Services.interfaces;
using Api.External.Consumer.Model;
using Api.External.Consumer.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Api.Core.Tests.Services
{
    public class SlotsServiceTest
    {
        private ISlotsService _service;

        private Mock<IExternalApiService> _externalApiServiceMock;

        private Mock<IOptions<ExternalApiConfig>> _iOptExternalConfigMock;
        private Mock<ExternalApiConfig> _externalApiConfig;

        [SetUp]
        public void SetUp()
        {
            _externalApiServiceMock = new Mock<IExternalApiService>();

            _iOptExternalConfigMock = new Mock<IOptions<ExternalApiConfig>>();
            _externalApiConfig = new Mock<ExternalApiConfig>();
            _iOptExternalConfigMock.Setup(opt => opt.Value).Returns(_externalApiConfig.Object);

            _service = new SlotsService(_externalApiServiceMock.Object, _iOptExternalConfigMock.Object);
        }

        [Test]
        public async Task GivenBasicExample_WhenGetAvailability_ThenOnlyCorrectDaysAreFilledAndRestAreNotPresent()
        {
            // given
            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 10,
                Monday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    }
                },
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 8,
                        EndHour = 14,
                        LunchStartHour = 12,
                        LunchEndHour = 13,
                    },
                    BusySlots = new List<BusySlotDTO>()
                    {
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 4, 8, 0, 0),
                            End = new DateTime(2024, 11, 4, 8, 10, 0)
                        }
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Monday.Should().NotBeNull();
            result.Thursday.Should().NotBeNull();

            result.Tuesday.Should().BeNull();
            result.Wednesday.Should().BeNull();
            result.Friday.Should().BeNull();
            result.Saturday.Should().BeNull();
            result.Sunday.Should().BeNull();
        }

        [Test]
        public async Task GivenOneDayWithNoReservedSlots_WhenGetAvailability_ThenResultHasCorrectNumberOfFreeSlots()
        {
            // given
            int FreeSlotsForThisExample = 30;

            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 10,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();
            result.Thursday.AvailableSlots.Should().HaveCount(FreeSlotsForThisExample);
        }

        [Test]
        public async Task GivenOneDayWithThreeReservedSlots_WhenGetAvailability_ThenResultHasCorrectNumberOfFreeSlots()
        {
            // given
            int FreeSlotsForThisExample = 27;

            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 10,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    },
                    BusySlots = new List<BusySlotDTO>()
                    {
                        // open time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 9, 0, 0),
                            End = new DateTime(2024, 11, 7, 9, 10, 0)
                        },
                        // right before lunchtime
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 12, 50, 0),
                            End = new DateTime(2024, 11, 7, 13, 0, 0)
                        },
                        // close time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 14, 50, 0),
                            End = new DateTime(2024, 11, 7, 15, 0, 0)
                        }
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();
            result.Thursday.AvailableSlots.Should().HaveCount(FreeSlotsForThisExample);
        }


        [Test]
        public async Task GivenOneDayWithSomeReservedSlots_WhenGetAvailability_ThenResultDoesntContainReservedSlots()
        {
            // given
            int FreeSlotsForThisExample = 27;

            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 10,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    },
                    BusySlots = new List<BusySlotDTO>()
                    {
                        // open time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 9, 0, 0),
                            End = new DateTime(2024, 11, 7, 9, 10, 0)
                        },
                        // right before lunchtime
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 12, 50, 0),
                            End = new DateTime(2024, 11, 7, 13, 0, 0)
                        },
                        // close time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 14, 50, 0),
                            End = new DateTime(2024, 11, 7, 15, 0, 0)
                        }
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 9, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 12, 50, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 14, 50, 0));
        }


        [Test]
        public async Task GivenOneDayWithSomeReservedSlots_WhenGetAvailability_ThenResultDoesntContainBeforeOpenTimeOrAfterCloseTime()
        {
            // given
            int FreeSlotsForThisExample = 27;

            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 10,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    },
                    BusySlots = new List<BusySlotDTO>()
                    {
                        // open time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 9, 0, 0),
                            End = new DateTime(2024, 11, 7, 9, 10, 0)
                        },
                        // right before lunchtime
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 12, 50, 0),
                            End = new DateTime(2024, 11, 7, 13, 0, 0)
                        },
                        // close time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 14, 50, 0),
                            End = new DateTime(2024, 11, 7, 15, 0, 0)
                        }
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();

            // before open time
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 8, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 8, 10, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 8, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 8, 30, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 8, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 8, 50, 0));

            // after (including) close time
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 10, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 30, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 50, 0));
        }


        [Test]
        public async Task GivenOneDayWithSomeReservedSlots_WhenGetAvailability_ThenResultDoesntContainLunchTime()
        {
            // given
            int FreeSlotsForThisExample = 27;

            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 10,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    },
                    BusySlots = new List<BusySlotDTO>()
                    {
                        // open time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 9, 0, 0),
                            End = new DateTime(2024, 11, 7, 9, 10, 0)
                        },
                        // right before lunchtime
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 12, 50, 0),
                            End = new DateTime(2024, 11, 7, 13, 0, 0)
                        },
                        // close time
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 7, 14, 50, 0),
                            End = new DateTime(2024, 11, 7, 15, 0, 0)
                        }
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 10, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 30, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 50, 0));
        }


        [Test]
        public async Task GivenReallyLongLunchtime_WhenGetAvailability_ThenResultDoesntContainLunchTime()
        {
            // given
            int FreeSlotsForThisExample = 27;

            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 20,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 17,
                        LunchStartHour = 10,
                        LunchEndHour = 16,
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 10, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 10, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 10, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 11, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 11, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 11, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 12, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 12, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 12, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 13, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 14, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 14, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 14, 40, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 0, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 20, 0));
            result.Thursday.AvailableSlots.Should().NotContain(slot => slot.StartTime == new DateTime(2024, 11, 7, 15, 40, 0));
        }

        [Test]
        public async Task GivenReallyLongLunchtime_WhenGetAvailability_ThenResultContainsEdgeCasesAsFreeSlots()
        {
            // given
            int FreeSlotsForThisExample = 27;

            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 20,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 17,
                        LunchStartHour = 10,
                        LunchEndHour = 16,
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 7, 9, 0, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 7, 9, 40, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 7, 16, 0, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 7, 16, 40, 0));

            result.Thursday.AvailableSlots.Should().Contain(slot => slot.EndTime == new DateTime(2024, 11, 7, 9, 20, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.EndTime == new DateTime(2024, 11, 7, 9, 40, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.EndTime == new DateTime(2024, 11, 7, 10, 0, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.EndTime == new DateTime(2024, 11, 7, 16, 20, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.EndTime == new DateTime(2024, 11, 7, 16, 40, 0));
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.EndTime == new DateTime(2024, 11, 7, 17, 0, 0));
        }

        [Test]
        public async Task GivenOneDayWithNoReservedSlots_WhenGetAvailability_ThenResultHasAvailableEdgeTimeCases()
        {
            // given
            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var rightAfterOpeningTime = new DateTime(2024, 11, 7, 9, 0, 0);
            var rightBeforeLunchTime = new DateTime(2024, 11, 7, 12, 50, 0);
            var rightAfterLunchTime = new DateTime(2024, 11, 7, 14, 0, 0);
            var rightBeforeCloseTime = new DateTime(2024, 11, 7, 14, 50, 0);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 10,
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Thursday.Should().NotBeNull();
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == rightAfterOpeningTime);
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == rightBeforeLunchTime);
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == rightAfterLunchTime);
            result.Thursday.AvailableSlots.Should().Contain(slot => slot.StartTime == rightBeforeCloseTime);
        }

        [Test]
        public async Task GivenSlotDurationMultipleOfFive_WhenGetAvailability_ThenResultStillHasCorrectAvailableTimes()
        {
            // given
            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = "We have the best doctors clinic",
                    Address = "Calle falsa 123"
                },
                SlotDurationMinutes = 45,
                Wednesday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    }
                }
            };

            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Wednesday.Should().NotBeNull();
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 9, 0, 0));
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 9, 45, 0));
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 10, 30, 0));
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 11, 15, 0));
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 12, 0, 0));
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 12, 45, 0));
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 14, 0, 0));
            result.Wednesday.AvailableSlots.Should().Contain(slot => slot.StartTime == new DateTime(2024, 11, 6, 14, 45, 0));
        }

        [Test]
        public async Task GivenFacilityData_WhenGetAvailability_ThenResultContainsFacilityData()
        {
            // given
            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            string facilityName = "We have the best doctors clinic";
            string facilityAddress = "Calle falsa 123";

            var weekAvailabilityStub = new WeekAvailabilityDTO
            {
                Facility = new FacilityDTO
                {
                    FacilityId = "this-is-some-facility-id",
                    Name = facilityName,
                    Address = facilityAddress
                },
                SlotDurationMinutes = 10,
                Monday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 9,
                        EndHour = 15,
                        LunchStartHour = 13,
                        LunchEndHour = 14,
                    }
                },
                Thursday = new DayDTO
                {
                    WorkPeriod = new WorkPeriodDTO
                    {
                        StartHour = 8,
                        EndHour = 14,
                        LunchStartHour = 12,
                        LunchEndHour = 13,
                    },
                    BusySlots = new List<BusySlotDTO>()
                    {
                        new BusySlotDTO()
                        {
                            Start = new DateTime(2024, 11, 4, 8, 0, 0),
                            End = new DateTime(2024, 11, 4, 8, 10, 0)
                        }
                    }
                }
            };
            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityStub);

            // when
            var result = await _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            result.Facility.Should().NotBeNull();
            result.Facility.Name.Should().Be(facilityName);
            result.Facility.Address.Should().Be(facilityAddress);
        }

        [Test]
        public async Task GivenInvalidDataFromExternalApi_WhenGetAvailability_ThenExceptionWithMessageThrown()
        {
            // given
            DateOnly mondayDate = new DateOnly(2024, 11, 4);

            string facilityName = "We have the best doctors clinic";
            string facilityAddress = "Calle falsa 123";

            WeekAvailabilityDTO? weekAvailabilityNullValue = null;
            _externalApiServiceMock.Setup(api => api.GetWeeklyAvailabilityAsync(mondayDate)).ReturnsAsync(weekAvailabilityNullValue);

            string errorMessage = "oh no, something went wrong!";
            _externalApiConfig.Setup(conf => conf.InvalidDataFromExternalApiError).Returns(errorMessage);

            // when
            Func<Task> result = () => _service.GetWeekFreeSlotsAsync(mondayDate);

            // then
            await result.Should().ThrowAsync<InvalidOperationException>().WithMessage($"*{errorMessage}*");
        }

        [Test]
        public async Task GivenSlotRequest_WhenReserveSlot_ThenAssertReturnedValueIsExpected()
        {
            // given
            var requestDTO = new ReserveSlotRequest
            {
                FacilityId = "c015550a-7dac-4904-bd83-ef6b48756bb8",
                Start = "2024-11-04 09:00:00",
                End = "2024-11-04 09:10:00",
                Comments = "my knee hurts sometimes when it's about to rain",
                Patient = new Patient
                {
                    Name = "Mario",
                    SecondName = "Neta",
                    Email = "mario.neta@example.com",
                    Phone = "+34617829923"
                }
            };

            string response = "everything went okay!";
            _externalApiServiceMock.Setup(api => api.ReserveSlotAsync(It.IsAny<ReserveSlotDTO>())).ReturnsAsync(response);

            // when
            var result = await _service.ReserveSlotAsync(requestDTO);

            // then
            result.Should().NotBeNull();
            result.Should().Be(response);
        }

        [Test]
        public async Task GivenSlotRequest_WhenReserveSlot_ThenVerifyCorrectMethodIsCalled()
        {
            // given
            var requestDTO = new ReserveSlotRequest
            {
                FacilityId = "c015550a-7dac-4904-bd83-ef6b48756bb8",
                Start = "2024-11-04 09:00:00",
                End = "2024-11-04 09:10:00",
                Comments = "my knee hurts sometimes when it's about to rain",
                Patient = new Patient
                {
                    Name = "Mario",
                    SecondName = "Neta",
                    Email = "mario.neta@example.com",
                    Phone = "+34617829923"
                }
            };

            string response = "everything went okay!";
            _externalApiServiceMock.Setup(api => api.ReserveSlotAsync(It.IsAny<ReserveSlotDTO>())).ReturnsAsync(response);

            // when
            var result = await _service.ReserveSlotAsync(requestDTO);

            // then
            _externalApiServiceMock.Verify(api => api.ReserveSlotAsync(It.IsAny<ReserveSlotDTO>()), Times.Once());
        }
    }
}
