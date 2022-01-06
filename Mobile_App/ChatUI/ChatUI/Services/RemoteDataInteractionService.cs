﻿using ChatUI.Models;
using ServerHandler.Services;

namespace ChatUI.Services
{
    public class RemoteDataInteractionService
    {
        private readonly APIWorkerService _workerService;

        public RemoteDataInteractionService(RemoteSettings remoteSettings)
        {
            _workerService = new APIWorkerService(remoteSettings.BaseAdress);
        }

    }
}