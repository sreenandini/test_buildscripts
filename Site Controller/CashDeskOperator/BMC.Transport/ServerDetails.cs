﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class ServerDetails
    {
        public string ServerName
        { get; set; }

        public string Username
        { get; set; }

        public string Password
        { get; set; }

        public string DataBase
        { get; set; }

        public string ConnectionTimeout
        { get; set; }

    }
}
