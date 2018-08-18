using IrriWeather.Common.Domain;
using System;

namespace IrriWeather.Irrigation.Domain.Control
{
    public class Zone : Entity
    {
        private Zone() { }

        public Zone(string name, string description, int channel, bool isEnabled)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name must be specified", nameof(name));
            if (String.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description must be specified", nameof(description));
            
            Name = name;
            Description = description;
            Channel = channel;
            IsEnabled = isEnabled;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Channel { get; private set; }
        public bool IsEnabled { get; private set; }



        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name must not be empty", nameof(name));
            this.Name = name;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Name must not be empty", nameof(description));
            this.Description = description;
        }


        public void SetNewChannel(IChannelControlService service, int newChannel)
        {
            service.Stop(Channel);
            this.Channel = newChannel;
        }




        public void Start(IChannelControlService service)
        {
            service.Start(Channel);
        }

        public void Stop(IChannelControlService service)
        {
            service.Stop(Channel);
        }

        public bool IsStarted(IChannelControlService service)
        {
            return service.IsStarted(Channel);
        }

        public void SetEnablement(IChannelControlService service, bool isEnabled)
        {
            if (!isEnabled)
            {
                service.Stop(Channel);
            }
            this.IsEnabled = isEnabled;
        }
    }
}
