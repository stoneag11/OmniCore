﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using OmniCore.Model.Enumerations;
using OmniCore.Model.Interfaces.Data.Entities;
using OmniCore.Model.Interfaces;

namespace OmniCore.Model.Interfaces
{
    public interface IPod : IServerResolvable, INotifyPropertyChanged
    {
        IPodEntity Entity { get; set; }
        IPodRequest ActiveRequest { get; }
        Task Archive();
        Task<IList<IPodRequest>> GetActiveRequests();
        Task<IPodRequest> RequestPair(uint radioAddress);
        Task<IPodRequest> RequestPrime();
        Task<IPodRequest> RequestInsert();
        Task<IPodRequest> RequestStatus(StatusRequestType requestType);
        Task<IPodRequest> RequestConfigureAlerts();
        Task<IPodRequest> RequestAcknowledgeAlerts();
        Task<IPodRequest> RequestSetBasalSchedule();
        Task<IPodRequest> RequestCancelBasal();
        Task<IPodRequest> RequestSetTempBasal();
        Task<IPodRequest> RequestCancelTempBasal();
        Task<IPodRequest> RequestBolus();
        Task<IPodRequest> RequestCancelBolus();
        Task<IPodRequest> RequestStartExtendedBolus();
        Task<IPodRequest> RequestCancelExtendedBolus();
        Task<IPodRequest> RequestDeactivate();
        Task StartQueue();
        Task StopQueue();
    }
}