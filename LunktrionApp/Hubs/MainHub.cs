using LunktrionApp.Models.DTO;
using LunktrionApp.Models.Entities;
using LunktrionApp.Models.Requests;
using LunktrionApp.Models.Responses;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LunktrionApp.Hubs
{
    public class MainHub
    {
        private readonly HubConnection _connection;

        public event Action<string>? OnErrorReceived;
        public event Action<bool>? ConnectionStatusChanged;

        public event Action<DeviceIdentity>? OnDeviceConnected;

        public event Action<DeviceInfoRequest>? OnDeviceInfoRequestReceived;
        public event Action<DeviceInfoDTO>? OnDeviceInfoReceived;

        public event Action<DeviceExecuteCommandRequest>? OnCommandReceived;
        public event Action<string>? OnCommandResultReceived;

        public bool IsConnected => _connection.State == HubConnectionState.Connected;

        public MainHub()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:50000/mainhub")
                .WithAutomaticReconnect()
                .Build();

            _connection.Closed += async (error) =>
            {
                ConnectionStatusChanged?.Invoke(false);
                Debug.WriteLine($"Connection closed: {error?.Message}");
            };

            _connection.Reconnecting += async (error) =>
            {
                Debug.WriteLine($"Connection reconnecting: {error?.Message}");
            };

            _connection.Reconnected += async (connectionId) =>
            {
                ConnectionStatusChanged?.Invoke(true);
                Debug.WriteLine($"Connection reconnected: {connectionId}");
            };

            // Прослушивание входящих ошибок
            _connection.On<string>("Error", (error) =>
            {
                OnErrorReceived?.Invoke(error);
            });

            // Прослушивание подключение новых девайсов
            _connection.On<DeviceIdentity>("DeviceOnline", (device) =>
            {
                OnDeviceConnected?.Invoke(device);
            });

            // ЗАПРОС ИНФОРМАЦИИ О УСТРЙОСТВЕ
            // Прослушивание входящих запрсов на передачу информации
            _connection.On<DeviceInfoRequest>("CollectAndSendInfo", (request) => 
            {
                OnDeviceInfoRequestReceived?.Invoke(request);
            });

            // Прослушивание входящих результатов информации
            _connection.On<DeviceInfoDTO>("DeviceInfoReceived", (info) =>
            {
                OnDeviceInfoReceived?.Invoke(info);
            });

            // ВЫПОЛНЕНИЕ КОМАНД
            // Прослушивание входящих команд
            _connection.On<DeviceExecuteCommandRequest>("ExecuteCommand", (request) =>
            {
                OnCommandReceived?.Invoke(request);
            });

            // Прослушивание входящих результатов выполнения команд
            _connection.On<string>("CommandResult", (command) =>
            {
                OnCommandResultReceived?.Invoke(command);
            });
        }

        public async Task ConnectAsync(DeviceIdentity currentDevice)
        {
            if (_connection.State is HubConnectionState.Disconnected)
            {
                await _connection.StartAsync();

                ConnectionStatusChanged?.Invoke(IsConnected);

                await _connection.InvokeAsync("RegisterDevice", currentDevice);
            }
        }

        public async Task RequestDeviceInfoAsync(string targetDeviceId, string currentDeviceId)
        {
            if (_connection is not null && _connection.State is HubConnectionState.Connected)
            {
                await _connection.SendAsync(
                    "RequestDeviceInfo",
                    new DeviceInfoRequest(targetDeviceId, currentDeviceId)
                );
            }
        }

        public async Task SendDeviceInfoAsync(DeviceInfoResponse response)
        {
            if (_connection.State is HubConnectionState.Connected)
            {
                await _connection.SendAsync(
                    "ReceiveDeviceInfo", 
                    response
                );
            }
        }

        public async Task ExecuteCommandAsync(string targetDeviceId, string command, string currentDeviceId)
        {
            if (_connection.State is HubConnectionState.Connected)
            {
                await _connection.SendAsync(
                    "RequestDeviceCommand", 
                    new DeviceExecuteCommandRequest(targetDeviceId, currentDeviceId, command)
                );
            }
        }

        public async Task SendCommandResultAsync(DeviceExecuteCommandResponse response)
        {
            if (_connection.State is HubConnectionState.Connected)
            {
                await _connection.SendAsync(
                    "ReceiveCommandResult", response
                );
            }
        }
    }
}
