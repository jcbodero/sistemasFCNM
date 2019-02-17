﻿using sistemaFCNM.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemaFCNM.Vistas
{
    public partial class Teclado : Form
    {
        private string InventarioAnterior;
        private string SerieAnterior;

        public Teclado()
        {
            InitializeComponent();
        }

        private void Teclado_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemasFCNMDataSet.Teclado' Puede moverla o quitarla según sea necesario.
            this.tecladoTableAdapter.Fill(this.sistemasFCNMDataSet.Teclado);
            if (FuncionesUtiles.masdetallesActiva || FuncionesUtiles.siguienteActiva)
            {
                FuncionesUtiles.masdetallesActiva = false;
                this.tecladoTableAdapter.FillBy(this.sistemasFCNMDataSet.Teclado, FuncionesUtiles.ID_TECLADO);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.abrirVentanas(new Mouse(), mainPrincipal.contenedor);
        }

        private void btnPrevius_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.abrirVentanas(new Pantalla(), mainPrincipal.contenedor);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void guardarCsvMenuItem_Click(object sender, EventArgs e)
        {
            login log;
            switch (FuncionesUtiles.ventanaDialogo())
            {

                case "Yes":

                    guardar();
                    log = new login();
                    log.Show();
                    this.Visible = false;
                    FuncionesUtiles.form1.Visible = false;
                    FuncionesUtiles.INVENTARIO_EQUIPO = "";
                    return;

                case "No":
                    log = new login();
                    log.Show();
                    this.Visible = false;
                    FuncionesUtiles.form1.Visible = false;
                    FuncionesUtiles.INVENTARIO_EQUIPO = "";
                    return;

                case "Cancel":
                    return;

                default:
                    return;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            switch (FuncionesUtiles.ventanaDialogo())
            {
                case "Yes":
                    FuncionesUtiles.siguienteActiva = false;
                    FuncionesUtiles.activarMenu();
                    FuncionesUtiles.INVENTARIO_EQUIPO = "";
                    guardar();
                    this.Close();
                    return;

                case "No":
                    FuncionesUtiles.siguienteActiva = false;
                    FuncionesUtiles.activarMenu();
                    FuncionesUtiles.INVENTARIO_EQUIPO = "";
                    this.Close();
                    return;

                case "Cancel":
                    return;

                default:
                    return;
            }
        }

        private void guardar()
        {
           
        }

        private void guardarMenuItem_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
          
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.INVENTARIO_EQUIPO = Microsoft.VisualBasic.Interaction.InputBox("Inventario Equipo", "Registrar Busqueda", "", 600);
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            habilitarBotones();
        }
        private void habilitarBotones()
        {
            
            txtTeclado.Enabled = true;
            comboEstado.Enabled = true;
            comboMarca.Enabled = true;
            comboModelo.Enabled = true;
            txtSerie.Enabled = true;
        }

        private void tecladoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //Update Inventario
            try
            {
                if (this.tecladoTableAdapter.ObtenerIdTeclado(txtTeclado.Text.Trim()).ToString().Length != 0 && this.InventarioAnterior != txtTeclado.Text.Trim())
                {
                    MessageBox.Show("Inventario Repetido ");
                }
            }
            catch (NullReferenceException)
            {
                this.tecladoTableAdapter.UpdateTeclado(txtTeclado.Text.Trim(), this.InventarioAnterior);


            }
            //Update Serie
            try
            {
                if (txtSerie.Text.Trim() != "N/A")
                {
                    if (this.tecladoTableAdapter.ObtenerSerie(txtSerie.Text.Trim()).ToString().Length != 0 && this.SerieAnterior != "N/A")
                    {
                        this.tecladoTableAdapter.UpdateSerie(txtSerie.Text.Trim(), txtTeclado.Text.Trim());
                    }
                    else
                    {
                        try
                        {
                            this.tecladoTableAdapter.UpdateTablaTecladoSerie((int)this.tecladoTableAdapter.ObtenerSerie(txtSerie.Text), txtTeclado.Text.Trim());
                        }
                        catch (InvalidOperationException)
                        {

                            this.tecladoTableAdapter.InsertSerie(txtSerie.Text.Trim());
                            this.tecladoTableAdapter.UpdateTablaTecladoSerie((int)this.tecladoTableAdapter.ObtenerSerie(txtSerie.Text.Trim()), txtTeclado.Text.Trim());
                        }

                    }

                }
                else
                {

                    this.tecladoTableAdapter.UpdateTablaTecladoSerie((int)this.tecladoTableAdapter.ObtenerSerie(txtSerie.Text.Trim()), txtTeclado.Text.Trim());
                }
            }
            catch (NullReferenceException)
            {

                this.tecladoTableAdapter.InsertSerie(txtSerie.Text.Trim());
                this.tecladoTableAdapter.UpdateTablaTecladoSerie((int)this.tecladoTableAdapter.ObtenerSerie(txtSerie.Text.Trim()), txtTeclado.Text.Trim());
            }
            //Update Combos
            try
            {
                this.tecladoTableAdapter.UpdateTablaTeclado(this.tecladoTableAdapter.ObtenerMarca(comboMarca.SelectedItem.ToString()),
                this.tecladoTableAdapter.ObtenerModelo(comboModelo.SelectedItem.ToString()),
                this.tecladoTableAdapter.ObtenerEstado(comboEstado.SelectedItem.ToString()), txtTeclado.Text.Trim());
            }
            catch (NullReferenceException)
            {

                this.tecladoTableAdapter.UpdateTablaTeclado(this.tecladoTableAdapter.ObtenerMarca(comboMarca.Text),
                 this.tecladoTableAdapter.ObtenerModelo(comboModelo.Text),
                 this.tecladoTableAdapter.ObtenerEstado(comboEstado.Text), txtTeclado.Text.Trim());
            }



            this.tecladoTableAdapter.Fill(this.sistemasFCNMDataSet.Teclado);
            ApagarBotones();
            gridTeclado.Enabled = true;

        }

        private void ApagarBotones()
        {
            txtTeclado.Enabled = false;
            comboEstado.Enabled = false;
            comboMarca.Enabled = false;
            comboModelo.Enabled = false;
            txtSerie.Enabled = false;
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            habilitarBotones();
            comboEstado.Items.AddRange(Datos._obtenerEstado());
            comboMarca.Items.AddRange(Datos._obtenerMarcaTeclado());
            comboModelo.Items.AddRange(Datos._obtenerModeloTeclado());

            this.InventarioAnterior = txtTeclado.Text.Trim();
            this.SerieAnterior = txtSerie.Text.Trim();

            gridTeclado.Enabled = false;
        }
    }
}
