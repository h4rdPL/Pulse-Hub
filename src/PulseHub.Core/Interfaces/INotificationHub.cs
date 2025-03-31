namespace PulseHub.Core.Interfaces
{
        public interface INotificationHub
        {
            Task SendNotificationAsync(string userEmail, string message);
        }
}
