namespace ProyectoFinal
{
    partial class frmBoletos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoletos));
            this.panelBoletos = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImprimirBoletos = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelBoletos
            // 
            this.panelBoletos.BackColor = System.Drawing.Color.Transparent;
            this.panelBoletos.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelBoletos.Location = new System.Drawing.Point(517, 77);
            this.panelBoletos.Margin = new System.Windows.Forms.Padding(2);
            this.panelBoletos.Name = "panelBoletos";
            this.panelBoletos.Size = new System.Drawing.Size(429, 307);
            this.panelBoletos.TabIndex = 0;
            // 
            // btnImprimirBoletos
            // 
            this.btnImprimirBoletos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnImprimirBoletos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirBoletos.Location = new System.Drawing.Point(728, 400);
            this.btnImprimirBoletos.Margin = new System.Windows.Forms.Padding(2);
            this.btnImprimirBoletos.Name = "btnImprimirBoletos";
            this.btnImprimirBoletos.Size = new System.Drawing.Size(101, 31);
            this.btnImprimirBoletos.TabIndex = 2;
            this.btnImprimirBoletos.Tag = "Imprimir Boleto";
            this.btnImprimirBoletos.Text = "Imprimir";
            this.btnImprimirBoletos.UseVisualStyleBackColor = false;
            this.btnImprimirBoletos.Click += new System.EventHandler(this.btnImprimirBoletos_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCerrar.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(634, 400);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 31);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // frmBoletos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(986, 442);
            this.Controls.Add(this.btnImprimirBoletos);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.panelBoletos);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmBoletos";
            this.Text = "frmBoletos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelBoletos;
        private System.Windows.Forms.Button btnImprimirBoletos;
        private System.Windows.Forms.Button btnCerrar;
    }
}