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
    public partial class PProyeccion : Form
    {
       
        public PProyeccion()
        {
            InitializeComponent();
        }

        private void PProyeccion_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemasFCNMDataSet.PantallaProyeccion' Puede moverla o quitarla según sea necesario.
            this.pantallaProyeccionTableAdapter.Fill(this.sistemasFCNMDataSet.PantallaProyeccion);
            if (FuncionesUtiles.masdetallesActiva || FuncionesUtiles.siguienteActiva)
            {
                FuncionesUtiles.masdetallesActiva = false;
                this.pantallaProyeccionTableAdapter.FillBy(this.sistemasFCNMDataSet.PantallaProyeccion, FuncionesUtiles.ID_PANTALLAPROY);
            }
          
        }

      

        private void btnNext_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.abrirVentanas(new Radio(), mainPrincipal.contenedor);
        }

        private void btnPrevius_Click(object sender, EventArgs e)
        {
            FuncionesUtiles.abrirVentanas(new Telefono(), mainPrincipal.contenedor);
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
            string sql = "update va set va.Inventario_PantallaProyeccion='" + txtPproyeccion.Text + "', va.Dimensiones = '" + txtDimensiones.Text + "'" +
              " from Equipo e,Pantalla_Proyeccion va where e.PantallaProyeccion = va.ID and e.id_Equipo = '" + txtEquipo.Text + "';";


            Datos.Insertar(sql);

            sql = "update car set car.estado='" + txtEstado.Text + "',car.marca = '" + txtMarca.Text + "',car.modelo='" + txtModelo.Text + "',car.serie='" + txtSerie.Text + "' " +
                  " from Equipo e,Pantalla_Proyeccion va, Caracteristicas car " +
                  " where e.PantallaProyeccion = va.ID and car.id_caracteristica = va.Caracteristicas and e.id_Equipo = '" + txtEquipo.Text + "'; ";


            Datos.Insertar(sql);
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
            string sql = "select e.id_Equipo, pro.Inventario_PantallaProyeccion,pro.Dimensiones,car.estado,car.marca," +
                "car.modelo,car.serie from  Equipo e, Pantalla_Proyeccion pro," +
                "Caracteristicas car where " +
                " e.PantallaProyeccion=pro.ID and car.id_caracteristica = pro.caracteristicas and e.id_Equipo = '" + FuncionesUtiles.INVENTARIO_EQUIPO + "'; ";

            Datos.llenarGrid(sql, gridPP);
            

            sql = "select e.id_Equipo, pro.Inventario_PantallaProyeccion,pro.Dimensiones,car.estado,car.marca," +
                "car.modelo,car.serie from  Equipo e, Pantalla_Proyeccion pro," +
                "Caracteristicas car where " +
                " e.PantallaProyeccion=pro.ID and car.id_caracteristica = pro.caracteristicas ; ";

            Datos.llenarGrid(sql, gridPP);

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            habilitarBotones();
        }

        private void habilitarBotones()
        {
            
            txtPproyeccion.Enabled = true;
            txtDimensiones.Enabled = true;
            txtEstado.Enabled = true;
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtSerie.Enabled = true;
        }

        private void pantallaProyeccionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pantallaProyeccionBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.sistemasFCNMDataSet);

        }
    }
}
