using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Common
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        public Guid Id { get; private set; }
    }
}
