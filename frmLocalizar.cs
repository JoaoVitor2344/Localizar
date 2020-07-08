using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace CEP
{
    public partial class frmLocalizar : MetroFramework.Forms.MetroForm
    {
        public frmLocalizar()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtCEP.Text))
            {
                using (var ws = new WSCorreios.AtendeClienteClient())
                {
                    try
                    {
                        var endereco = ws.consultaCEP(txtCEP.Text.Trim());
                        txtLogradouro.Text = endereco.end;
                        txtBairro.Text = endereco.bairro;
                        txtCidade.Text = endereco.cidade;
                        txtEstado.Text = endereco.uf;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            string street = string.Empty;
            string city = string.Empty;
            string state = string.Empty;
            string zip = string.Empty;
            string district = string.Empty;

            StringBuilder queryAddress = new StringBuilder();
            queryAddress.Append("http://maps.google.com/maps?q=");

            if (txtLogradouro.Text != string.Empty)
            {
                street = txtLogradouro.Text.Replace(' ', '+');
                queryAddress.Append(street + ',' + '+');
            }

            if (txtBairro.Text != string.Empty)
            {
                district = txtBairro.Text.Replace(' ', '+');
                queryAddress.Append(district + ',' + '+');
            }

            if (txtCidade.Text != string.Empty)
            {
                city = txtCidade.Text.Replace(' ', '+');
                queryAddress.Append(city + ',' + '+');
            }

            if (txtEstado.Text != string.Empty)
            {
                state = txtEstado.Text.Replace(' ', '+');
                queryAddress.Append(state + ',' + '+');
            }

            if (txtCEP.Text != string.Empty)
            {
                zip = txtCEP.Text.Replace(' ', '+');
                queryAddress.Append(zip + ',' + '+');
            }

            wbMaps.Navigate(queryAddress.ToString());
            wbMaps.ScriptErrorsSuppressed = true;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCEP.Text = string.Empty;
            txtLogradouro.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtEstado.Text = string.Empty;
            wbMaps.DocumentText = null;
        }
    }
}
