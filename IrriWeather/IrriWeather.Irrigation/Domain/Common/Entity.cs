﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Irrigation.Domain.Common
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        public int Id { get; private set; }
    }
}