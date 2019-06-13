﻿using OmniCore.Model.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniCore.Model.Interfaces.Data
{
    public interface IMessageExchangeResult
    {
        long? Id { get; set; }
        Guid PodId { get; set; }

        DateTime? RequestTime { get; set; }
        RequestSource Source { get; set; }
        RequestType Type { get; set; }
        string Parameters { get; set; }

        DateTime? ResultTime { get; set; }

        bool Success { get; set; }
        FailureType Failure { get; set;  }

        Exception Exception { get; set;  }

        IMessageExchangeStatistics Statistics { get; set; }
        IMessageExchangeParameters ExchangeParameters { get; set; }
        IAlertStates AlertStates { get; set; }
        IBasalSchedule BasalSchedule { get; set; }
        IFault Fault { get; set; }
        IStatus Status { get; set; }
        IUserSettings UserSettings { get; set; }

        long? StatisticsId { get; set; }
        long? ParametersId { get; set; }
        long? AlertStatesId { get; set; }
        long? BasalScheduleId { get; set; }
        long? FaultId { get; set; }
        long? StatusId { get; set; }
        long? UserSettingsId { get; set; }

    }
}