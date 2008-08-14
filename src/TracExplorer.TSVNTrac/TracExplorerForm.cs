using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TracExplorer.Common;

namespace TracExplorer.TSVNTrac
{
    public partial class TracExplorerForm : Form
    {
        public TracExplorerForm(ITracConnect tracConnect)
        {
            InitializeComponent();

            tracExplorerControl.TracConnect = tracConnect;
        }
    }
}
