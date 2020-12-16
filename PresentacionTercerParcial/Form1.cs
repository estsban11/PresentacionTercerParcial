using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entity;
using System.Data.SqlClient;
using BLL;
using Entity;

namespace PresentacionTercerParcial
{
    public partial class Form1 : Form
    {
        ArchivoLogService ArchivoLogService;
        ServicioService ServicioService;
        ServicioServiceBD ServicioServiceBD;
        IpsServiceBd IpsServiceBd;

      

        
        public Form1()
        {
            InitializeComponent();
            ServicioServiceBD = new ServicioServiceBD(ExtraerCadena.connectionString);
            IpsServiceBd = new IpsServiceBd(ExtraerCadena.connectionString);
           
            
        }

        public void LlenarCombo()
        {
            var lista = IpsServiceBd.Lista().Lista;
            foreach (var item in lista)
            {
                comboBox1.Items.Add(item);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = "C:\\";
                fileDialog.Filter = "txt files(*.txt)|*.txt|All files (*.*)|*.*";
                fileDialog.FilterIndex = 2;
                fileDialog.RestoreDirectory = true;

                if(fileDialog.ShowDialog()== DialogResult.OK)
                {
                    var file = fileDialog.OpenFile();
                    ServicioService = new ServicioService(file);
                    List<Servicio> servicios = ServicioService.Consultar().Servicios;      
                    dataGridView1.DataSource = ServicioService.Validar(comboBox1.Text).Servicios;
                    var lista = ServicioService.Validar(comboBox1.Text).Servicios;
                     MessageBox.Show( ServicioServiceBD.Guardar(lista));
                    var listanoAptos = ServicioService.Noaptos(comboBox1.Text, Valor(servicios)).Servicios;
                    ArchivoLogService.Guardar(listanoAptos, Valor(servicios));
                }
            }
        }

        public double Valor(List<Servicio> servicios)
        {
             
            double valor = 0;
            foreach (var item in servicios)
            {
                if (item.IdLaboratorio == "100")
                {
                    valor = ServicioServiceBD.Valor("100");
                }else if (item.IdLaboratorio == "101")
                {
                    valor = ServicioServiceBD.Valor("101");
                }else if(item.IdLaboratorio == "102")
                {
                    valor = ServicioServiceBD.Valor("102");
                }else if (item.IdLaboratorio == "103")
                {
                    valor = ServicioServiceBD.Valor("103");
                }

            }
            return valor;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LlenarCombo();
        }
    }
}
