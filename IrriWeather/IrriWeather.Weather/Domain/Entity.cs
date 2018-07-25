using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Weather.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        public int Id { get; private set; }
    }
}
