namespace AutoKey
{
    partial class FormHomeAutoKey
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHomeAutoKey));
            buttonIniciarAutoKey = new Button();
            checkBoxVerSubprocesos = new CheckBox();
            textBoxAsignacionTeclas = new TextBox();
            buttonLimpiarTexto = new Button();
            checkBoxEnviarToAppsEnSegundoPlano = new CheckBox();
            label2 = new Label();
            listViewProcesos = new ListView();
            groupBoxAsignacionTeclas = new GroupBox();
            groupBoxListaProcesos = new GroupBox();
            groupBoxEmpezar = new GroupBox();
            buttonHelp_Iniciar = new Button();
            buttonMenosVelocidad = new Button();
            buttonMasVelocidad = new Button();
            groupBoxVelocidad = new GroupBox();
            labelIntervalos = new Label();
            labelEstado = new Label();
            toolTip_ButtonHelp_Iniciar = new ToolTip(components);
            pictureBox1 = new PictureBox();
            groupBoxAsignacionTeclas.SuspendLayout();
            groupBoxListaProcesos.SuspendLayout();
            groupBoxEmpezar.SuspendLayout();
            groupBoxVelocidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonIniciarAutoKey
            // 
            buttonIniciarAutoKey.Cursor = Cursors.Hand;
            buttonIniciarAutoKey.Location = new Point(290, 22);
            buttonIniciarAutoKey.Name = "buttonIniciarAutoKey";
            buttonIniciarAutoKey.Size = new Size(95, 23);
            buttonIniciarAutoKey.TabIndex = 0;
            buttonIniciarAutoKey.Text = "Iniciar";
            buttonIniciarAutoKey.UseVisualStyleBackColor = true;
            buttonIniciarAutoKey.Click += buttonIniciarAutoKey_Click;
            // 
            // checkBoxVerSubprocesos
            // 
            checkBoxVerSubprocesos.AutoSize = true;
            checkBoxVerSubprocesos.Cursor = Cursors.Hand;
            checkBoxVerSubprocesos.Location = new Point(303, 173);
            checkBoxVerSubprocesos.Name = "checkBoxVerSubprocesos";
            checkBoxVerSubprocesos.Size = new Size(111, 19);
            checkBoxVerSubprocesos.TabIndex = 1;
            checkBoxVerSubprocesos.Text = "Ver subprocesos";
            checkBoxVerSubprocesos.UseVisualStyleBackColor = true;
            // 
            // textBoxAsignacionTeclas
            // 
            textBoxAsignacionTeclas.Cursor = Cursors.IBeam;
            textBoxAsignacionTeclas.Location = new Point(6, 24);
            textBoxAsignacionTeclas.Name = "textBoxAsignacionTeclas";
            textBoxAsignacionTeclas.Size = new Size(307, 23);
            textBoxAsignacionTeclas.TabIndex = 7;
            // 
            // buttonLimpiarTexto
            // 
            buttonLimpiarTexto.Cursor = Cursors.Hand;
            buttonLimpiarTexto.Location = new Point(319, 23);
            buttonLimpiarTexto.Name = "buttonLimpiarTexto";
            buttonLimpiarTexto.Size = new Size(95, 23);
            buttonLimpiarTexto.TabIndex = 8;
            buttonLimpiarTexto.Text = "Limpiar";
            buttonLimpiarTexto.UseVisualStyleBackColor = true;
            buttonLimpiarTexto.Click += buttonLimpiarTexto_Click;
            // 
            // checkBoxEnviarToAppsEnSegundoPlano
            // 
            checkBoxEnviarToAppsEnSegundoPlano.AutoSize = true;
            checkBoxEnviarToAppsEnSegundoPlano.Enabled = false;
            checkBoxEnviarToAppsEnSegundoPlano.Location = new Point(16, 26);
            checkBoxEnviarToAppsEnSegundoPlano.Name = "checkBoxEnviarToAppsEnSegundoPlano";
            checkBoxEnviarToAppsEnSegundoPlano.Size = new Size(215, 19);
            checkBoxEnviarToAppsEnSegundoPlano.TabIndex = 9;
            checkBoxEnviarToAppsEnSegundoPlano.Text = "Enviar a procesos en segundo plano";
            checkBoxEnviarToAppsEnSegundoPlano.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(327, 473);
            label2.Name = "label2";
            label2.Size = new Size(105, 15);
            label2.TabIndex = 10;
            label2.Text = "By: Mateo Noriega";
            label2.TextAlign = ContentAlignment.MiddleRight;
            label2.Click += label2_Click;
            // 
            // listViewProcesos
            // 
            listViewProcesos.Location = new Point(6, 22);
            listViewProcesos.Name = "listViewProcesos";
            listViewProcesos.Size = new Size(408, 145);
            listViewProcesos.Sorting = SortOrder.Ascending;
            listViewProcesos.TabIndex = 11;
            listViewProcesos.UseCompatibleStateImageBehavior = false;
            // 
            // groupBoxAsignacionTeclas
            // 
            groupBoxAsignacionTeclas.Controls.Add(buttonLimpiarTexto);
            groupBoxAsignacionTeclas.Controls.Add(textBoxAsignacionTeclas);
            groupBoxAsignacionTeclas.Location = new Point(12, 261);
            groupBoxAsignacionTeclas.Name = "groupBoxAsignacionTeclas";
            groupBoxAsignacionTeclas.Size = new Size(420, 61);
            groupBoxAsignacionTeclas.TabIndex = 12;
            groupBoxAsignacionTeclas.TabStop = false;
            groupBoxAsignacionTeclas.Text = "Asignar combinación de teclas";
            // 
            // groupBoxListaProcesos
            // 
            groupBoxListaProcesos.Controls.Add(listViewProcesos);
            groupBoxListaProcesos.Controls.Add(checkBoxVerSubprocesos);
            groupBoxListaProcesos.Location = new Point(12, 57);
            groupBoxListaProcesos.Name = "groupBoxListaProcesos";
            groupBoxListaProcesos.Size = new Size(420, 198);
            groupBoxListaProcesos.TabIndex = 13;
            groupBoxListaProcesos.TabStop = false;
            groupBoxListaProcesos.Text = "Lista de procesos";
            // 
            // groupBoxEmpezar
            // 
            groupBoxEmpezar.Controls.Add(buttonHelp_Iniciar);
            groupBoxEmpezar.Controls.Add(buttonIniciarAutoKey);
            groupBoxEmpezar.Controls.Add(checkBoxEnviarToAppsEnSegundoPlano);
            groupBoxEmpezar.Location = new Point(12, 404);
            groupBoxEmpezar.Name = "groupBoxEmpezar";
            groupBoxEmpezar.Size = new Size(420, 59);
            groupBoxEmpezar.TabIndex = 14;
            groupBoxEmpezar.TabStop = false;
            groupBoxEmpezar.Text = "Empezar";
            // 
            // buttonHelp_Iniciar
            // 
            buttonHelp_Iniciar.Cursor = Cursors.Help;
            buttonHelp_Iniciar.FlatAppearance.BorderSize = 0;
            buttonHelp_Iniciar.FlatStyle = FlatStyle.Flat;
            buttonHelp_Iniciar.Image = Properties.Resources.Help_icon;
            buttonHelp_Iniciar.Location = new Point(391, 22);
            buttonHelp_Iniciar.Name = "buttonHelp_Iniciar";
            buttonHelp_Iniciar.Size = new Size(23, 23);
            buttonHelp_Iniciar.TabIndex = 10;
            toolTip_ButtonHelp_Iniciar.SetToolTip(buttonHelp_Iniciar, "¡Ayuda!");
            buttonHelp_Iniciar.UseVisualStyleBackColor = true;
            buttonHelp_Iniciar.Click += buttonHelp_Iniciar_Click;
            // 
            // buttonMenosVelocidad
            // 
            buttonMenosVelocidad.Cursor = Cursors.Hand;
            buttonMenosVelocidad.Location = new Point(19, 27);
            buttonMenosVelocidad.Name = "buttonMenosVelocidad";
            buttonMenosVelocidad.Size = new Size(75, 23);
            buttonMenosVelocidad.TabIndex = 17;
            buttonMenosVelocidad.Text = "-";
            buttonMenosVelocidad.UseVisualStyleBackColor = true;
            buttonMenosVelocidad.Click += buttonMenosVelocidad_Click;
            // 
            // buttonMasVelocidad
            // 
            buttonMasVelocidad.Cursor = Cursors.Hand;
            buttonMasVelocidad.Location = new Point(327, 27);
            buttonMasVelocidad.Name = "buttonMasVelocidad";
            buttonMasVelocidad.Size = new Size(75, 23);
            buttonMasVelocidad.TabIndex = 18;
            buttonMasVelocidad.Text = "+";
            buttonMasVelocidad.UseVisualStyleBackColor = true;
            buttonMasVelocidad.Click += buttonMasVelocidad_Click;
            // 
            // groupBoxVelocidad
            // 
            groupBoxVelocidad.Controls.Add(labelIntervalos);
            groupBoxVelocidad.Controls.Add(buttonMenosVelocidad);
            groupBoxVelocidad.Controls.Add(buttonMasVelocidad);
            groupBoxVelocidad.Location = new Point(12, 328);
            groupBoxVelocidad.Name = "groupBoxVelocidad";
            groupBoxVelocidad.Size = new Size(420, 70);
            groupBoxVelocidad.TabIndex = 19;
            groupBoxVelocidad.TabStop = false;
            groupBoxVelocidad.Text = "Asignar intervalo de velocidad";
            // 
            // labelIntervalos
            // 
            labelIntervalos.AutoSize = true;
            labelIntervalos.Location = new Point(189, 31);
            labelIntervalos.Name = "labelIntervalos";
            labelIntervalos.Size = new Size(38, 15);
            labelIntervalos.TabIndex = 19;
            labelIntervalos.Text = "label1";
            labelIntervalos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEstado
            // 
            labelEstado.AutoSize = true;
            labelEstado.ForeColor = Color.Red;
            labelEstado.Location = new Point(341, 9);
            labelEstado.Name = "labelEstado";
            labelEstado.Size = new Size(32, 15);
            labelEstado.TabIndex = 20;
            labelEstado.Text = "label";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.AutoKey_Logo;
            pictureBox1.Location = new Point(135, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(176, 42);
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // FormHomeAutoKey
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(444, 500);
            Controls.Add(pictureBox1);
            Controls.Add(labelEstado);
            Controls.Add(groupBoxEmpezar);
            Controls.Add(label2);
            Controls.Add(groupBoxListaProcesos);
            Controls.Add(groupBoxAsignacionTeclas);
            Controls.Add(groupBoxVelocidad);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormHomeAutoKey";
            Text = "AutoKey";
            groupBoxAsignacionTeclas.ResumeLayout(false);
            groupBoxAsignacionTeclas.PerformLayout();
            groupBoxListaProcesos.ResumeLayout(false);
            groupBoxListaProcesos.PerformLayout();
            groupBoxEmpezar.ResumeLayout(false);
            groupBoxEmpezar.PerformLayout();
            groupBoxVelocidad.ResumeLayout(false);
            groupBoxVelocidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonIniciarAutoKey;
        private CheckBox checkBoxVerSubprocesos;
        private TextBox textBoxAsignacionTeclas;
        private Button buttonLimpiarTexto;
        private CheckBox checkBoxEnviarToAppsEnSegundoPlano;
        private Label label2;
        private ListView listViewProcesos;
        private GroupBox groupBoxAsignacionTeclas;
        private GroupBox groupBoxListaProcesos;
        private GroupBox groupBoxEmpezar;
        private Button buttonMenosVelocidad;
        private Button buttonMasVelocidad;
        private GroupBox groupBoxVelocidad;
        private Label labelEstado;
        private Label labelIntervalos;
        private Button buttonHelp_Iniciar;
        private ToolTip toolTip_ButtonHelp_Iniciar;
        private PictureBox pictureBox1;
    }
}
