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
    public partial class AddNewProviderForm : Form
    {
        private string _parameters;

        public string Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private Selection _selection = new Selection();

        public Selection Selection
        {
            get { return _selection; }
        }

        public AddNewProviderForm(ITracConnect tracConnect)
        {
            InitializeComponent();

            tracExplorerControl.TracConnect = tracConnect;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < lstSelectionStatus.Items.Count; i++)
                {
                    lstSelectionStatus.SetItemChecked(i, true);
                }
            }
            else if (chkSelectAll.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i < lstSelectionStatus.Items.Count; i++)
                {
                    lstSelectionStatus.SetItemChecked(i, false);
                }
            }
        }

        private void lstSelectionStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            int count = 0;

            for (int i = 0; i < lstSelectionStatus.Items.Count; i++)
            {
                if (lstSelectionStatus.GetItemChecked(i))
                {
                    count++;
                }
            }
            if (count == 0)
            {
                wizard1.NextEnabled = false;
                chkSelectAll.CheckState = CheckState.Unchecked;
            }
            else if (count == lstSelectionStatus.Items.Count)
            {
                wizard1.NextEnabled = true;
                chkSelectAll.CheckState = CheckState.Checked;
            }
            else
            {
                wizard1.NextEnabled = true;
                chkSelectAll.CheckState = CheckState.Indeterminate;
            }
        }

        private void wizardPage2_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            // Add selected status selection
            _selection.Format = textFormat.Text;

            _selection.Items.Clear();
            foreach (String selection in lstSelectionStatus.CheckedItems)
            {
                _selection.Items.Add(selection);
            }
        }

        private void tracExplorerControl_TicketQueryClick(object sender, TracExplorerControl.TicketQueryArgs e)
        {
            Parameters = string.Format("<TSVNTrac server=\"{0}\" ticketquery=\"{1}\"/>", e.ServerDetails.Server, e.TicketQuery.Name);
                        
            wizard1.NextEnabled = true;
        }

        private void wizard1_Load(object sender, EventArgs e)
        {
            wizard1.NextEnabled = false;

            List<String> selectionList = new List<String>();

            textFormat.Text = "{0} #{1}: {2}\\n";

            selectionList.Add("closed");
            selectionList.Add("closes");
            selectionList.Add("fix");
            selectionList.Add("fixed");
            selectionList.Add("fixes");
            selectionList.Add("addresses");
            selectionList.Add("re");
            selectionList.Add("references");
            selectionList.Add("refs");
            selectionList.Add("see");

            lstSelectionStatus.BeginUpdate();
            lstSelectionStatus.Items.Clear();
            lstSelectionStatus.Items.AddRange(selectionList.ToArray());
            lstSelectionStatus.EndUpdate();
        }

        private void wizardPage2_ShowFromNext(object sender, EventArgs e)
        {
            if (chkSelectAll.CheckState == CheckState.Unchecked)
            {
                wizard1.NextEnabled = false;
            }
        }
    }
}
