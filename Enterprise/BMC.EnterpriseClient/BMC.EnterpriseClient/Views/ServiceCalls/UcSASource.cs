﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Business;

namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    public partial class UcSASource : UserControlBase
    {
        private IServiceAdmin _admin = null;

        public UcSASource(IServiceAdmin admin)
        {
            _admin = admin;
            InitializeComponent();
        }
    }
}