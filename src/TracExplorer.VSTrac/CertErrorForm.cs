using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace TracExplorer.VSTrac
{
    public enum CertErrorFormResult
    {
        AcceptPermanently = 0,
        AcceptTemporary = 1,
        Reject = 2
    }

    public partial class CertErrorForm : Form
    {
        private CertErrorFormResult dialogResult;

        public new CertErrorFormResult DialogResult
        {
            get { return dialogResult; }
            set { dialogResult = value; }
        }

        public CertErrorForm()
        {
            InitializeComponent();
        }

        private void btnAccPerm_Click(object sender, EventArgs e)
        {
            dialogResult = CertErrorFormResult.AcceptPermanently;
            this.Hide();
        }

        private void btnAccTemp_Click(object sender, EventArgs e)
        {
            dialogResult = CertErrorFormResult.AcceptTemporary;
            this.Hide();
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            dialogResult = CertErrorFormResult.Reject;
            this.Hide();
        }

        public CertErrorFormResult ShowDialog(IWin32Window owner, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            lblUrlName.Text = string.Format(lblUrlName.Text, cert.Subject);
            lblFingerprint.Text = cert.GetSerialNumberString();
            lblDistName.Text = cert.Issuer;

            switch (error)
            {
                case SslPolicyErrors.RemoteCertificateChainErrors:
                    lblMessage1.Text = "Chain error";
                    break;
                case SslPolicyErrors.RemoteCertificateNameMismatch:
                    lblMessage1.Text = "Name mismatch";
                    break;
                case SslPolicyErrors.RemoteCertificateNotAvailable:
                    lblMessage1.Text = "Certificate not available";
                    break;
            }

            base.ShowDialog();

            return this.DialogResult;
        }
    }
}
