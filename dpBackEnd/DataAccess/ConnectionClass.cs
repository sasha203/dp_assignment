﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConnectionClass
    {
        public dpWeatherEntities Entity { get; set; }

        public ConnectionClass() {
            Entity = new dpWeatherEntities();
        }
    }
}
