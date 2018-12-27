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
    public partial class Microfono : Form
    {
        private string tipo;
        private string estado;
        private string marca;
        private string serie;
        private string modelo;
        private string inventario;
        public Microfono()
        {
            InitializeComponent();
        }

        private void Microfono_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemasFCNMDataSet.Microfono' Puede moverla o quitarla según sea necesario.
            this.microfonoTableAdapter.Fill(this.sistemasFCNMDataSet.Microfono);
            if (FuncionesUtiles.masdetallesActiva || FuncionesUtiles.siguienteActiva)
            {
                FuncionesUtiles.masdetallesActiva = false;
            }
            

        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.abrirVentanas(new Telefono(), mainPrincipal.contenedor);
        }

        private void btnPrevius_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.abrirVentanas(new Proyector(), mainPrincipal.contenedor);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void CerrarSesion_Click(object sender, EventArgs e)
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

        private void guardar()
        {
            string sql = "update va set va.Inventario_Microfono='" + txtMicro.Text + "', va.tipo = '" + txtTipo.Text + "'" +
              " from Equipo e, Microfonos va where e.Microfono = va.ID and e.id_Equipo = '" + txtEquipo.Text + "';";


            Datos.Insertar(sql);

            sql = "update car set car.estado='" + txtEstado.Text + "',car.marca = '" + txtMarca.Text + "',car.modelo='" + txtModelo.Text + "',car.serie='" + txtSerie.Text + "' " +
                  " from Equipo e,Microfonos va, Caracteristicas car " +
                  " where e.Microfono = va.ID and car.id_caracteristica = va.Caracteristicas and e.id_Equipo = '" + txtEquipo.Text + "'; ";


            Datos.Insertar(sql);
        }

        private void guardarMenuItem_Click(object sender, EventArgs e)
        {
            guardar();
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.INVENTARIO_EQUIPO = Microsoft.VisualBasic.Interaction.InputBox("Inventario Equipo", "Registrar Busqueda", "", 600);
            string sql = "select e.id_Equipo, pro.Inventario_Microfono,pro.tipo,car.estado,car.marca," +
               "car.modelo,car.serie from  Equipo e, Microfonos pro," +
               "Caracteristicas car where " +
               "e.Microfono = pro.ID and car.id_caracteristica = pro.caracteristicas and e.id_Equipo = '" + FuncionesUtiles.INVENTARIO_EQUIPO + "'; ";

            Datos.llenarGrid(sql, gridMicrofono);
      

            sql = "select e.id_Equipo, pro.Inventario_Microfono,pro.tipo,car.estado,car.marca," +
              "car.modelo,car.serie from  Equipo e, Microfonos pro," +
              "Caracteristicas car where " +
              "e.Microfono = pro.ID and car.id_caracteristica = pro.caracteristicas;";

            Datos.llenarGrid(sql, gridMicrofono);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ventanaNuevoRegistro registro;
            switch (FuncionesUtiles.ventanaDialogo())
            {
                case "Yes":

                    guardar();
                    this.Close();
                    registro = new ventanaNuevoRegistro();
                    registro.Show();
                    FuncionesUtiles.form1.Visible = false;
                    FuncionesUtiles.siguienteActiva = true;
                    FuncionesUtiles.desactivarMenu();
                    FuncionesUtiles.abrirVentanas(new CPU(), mainPrincipal.contenedor);
                    return;

                case "No":
                    this.Close();
                    registro = new ventanaNuevoRegistro();
                    registro.Show();
                    FuncionesUtiles.form1.Visible = false;
                    FuncionesUtiles.siguienteActiva = true;
                    FuncionesUtiles.desactivarMenu();
                    FuncionesUtiles.abrirVentanas(new CPU(), mainPrincipal.contenedor);
                    return;

                case "Cancel":
                    return;

                default:
                    return;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            habilitarBotones();
        }
        private void habilitarBotones()
        {
                   
            txtTipo.Enabled = true;
            txtMicro.Enabled = true;
            txtEstado.Enabled = true;
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtSerie.Enabled = true;
        }

        private void microfonoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.microfonoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.sistemasFCNMDataSet);

        }
    }
}
