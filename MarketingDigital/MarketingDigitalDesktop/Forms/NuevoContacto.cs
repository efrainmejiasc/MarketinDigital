﻿using MarketingDigitalBCS.Response;
using MarketingDigitalDesktop.Engine;
using MarketingDigitalDesktop.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketingDigitalDesktop.Forms
{
    public partial class NuevoContacto : Form
    {
        private readonly ITool tool;
        public NuevoContacto()
        {
            InitializeComponent();
        }

        public NuevoContacto(ITool _tool)
        {
            InitializeComponent();
            tool = _tool;
        }

        private void NuevoContacto_Load(object sender, EventArgs e)
        {
            SetTableCarpetaAsync();
        }
        public async Task SetTableCarpetaAsync()
        {
            var procesador = new Procesador();
            var listaCarpetas = await procesador.ObtenerListaCarpetasAsync();
            dgv1.DataSource = tool.SetTableFolder(listaCarpetas);
            dgv1 = tool.ColorFila(dgv1, Color.WhiteSmoke, Color.AliceBlue);
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv1.ClearSelection();
        }


        private void dgv1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv1 = tool.ColorFila(dgv1, Color.WhiteSmoke, Color.AliceBlue);
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv1.ClearSelection();
        }

        private void dgv2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv2 = tool.ColorFila(dgv1, Color.WhiteSmoke, Color.AliceBlue);
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv2.ClearSelection();
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgv1.CurrentRow;
            var id = row.Cells["ID"].Value.ToString() ;
            ObtenerListasEnCarpetaAsync(id);
        }

        private async Task ObtenerListasEnCarpetaAsync(string id)
        {
            var procesador = new Procesador();
            SBRecoverListInFolder listInFolder = await procesador.ObtenerListasEnCarpetas(id);
            dgv2.DataSource = tool.SetTableListInFolder(listInFolder);
            dgv2 = tool.ColorFila(dgv2, Color.WhiteSmoke, Color.AliceBlue);
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv2.ClearSelection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv2.SelectedRows.Count == 0 )
            {
                MessageBox.Show("DEBE SELECCIONAR UNA LISTA", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}