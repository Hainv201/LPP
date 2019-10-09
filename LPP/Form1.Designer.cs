namespace LPP
{
    partial class Form1
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
            this.labelUnboundVariables = new System.Windows.Forms.Label();
            this.labelBoundVariables = new System.Windows.Forms.Label();
            this.replace = new System.Windows.Forms.Button();
            this.textBoxReplaceVariable = new System.Windows.Forms.TextBox();
            this.listBoxUnboundVariables = new System.Windows.Forms.ListBox();
            this.listBoxBoundVariables = new System.Windows.Forms.ListBox();
            this.btSemanticTableau = new System.Windows.Forms.Button();
            this.hash_Code = new System.Windows.Forms.ListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.nand = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.disjunc_Simple = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.disjunc_Normal = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.variables = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.inputprefix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Prefix = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.graph = new System.Windows.Forms.Button();
            this.read = new System.Windows.Forms.Button();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.listBox6 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUnboundVariables
            // 
            this.labelUnboundVariables.AutoSize = true;
            this.labelUnboundVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUnboundVariables.Location = new System.Drawing.Point(613, 231);
            this.labelUnboundVariables.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUnboundVariables.Name = "labelUnboundVariables";
            this.labelUnboundVariables.Size = new System.Drawing.Size(99, 13);
            this.labelUnboundVariables.TabIndex = 57;
            this.labelUnboundVariables.Text = "Unbound variables:";
            // 
            // labelBoundVariables
            // 
            this.labelBoundVariables.AutoSize = true;
            this.labelBoundVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBoundVariables.Location = new System.Drawing.Point(615, 81);
            this.labelBoundVariables.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBoundVariables.Name = "labelBoundVariables";
            this.labelBoundVariables.Size = new System.Drawing.Size(86, 13);
            this.labelBoundVariables.TabIndex = 56;
            this.labelBoundVariables.Text = "Bound variables:";
            // 
            // replace
            // 
            this.replace.Location = new System.Drawing.Point(745, 352);
            this.replace.Name = "replace";
            this.replace.Size = new System.Drawing.Size(75, 23);
            this.replace.TabIndex = 55;
            this.replace.Text = "Replace";
            this.replace.UseVisualStyleBackColor = true;
            // 
            // textBoxReplaceVariable
            // 
            this.textBoxReplaceVariable.Location = new System.Drawing.Point(616, 352);
            this.textBoxReplaceVariable.Name = "textBoxReplaceVariable";
            this.textBoxReplaceVariable.Size = new System.Drawing.Size(100, 20);
            this.textBoxReplaceVariable.TabIndex = 54;
            // 
            // listBoxUnboundVariables
            // 
            this.listBoxUnboundVariables.FormattingEnabled = true;
            this.listBoxUnboundVariables.Location = new System.Drawing.Point(616, 247);
            this.listBoxUnboundVariables.Name = "listBoxUnboundVariables";
            this.listBoxUnboundVariables.Size = new System.Drawing.Size(204, 95);
            this.listBoxUnboundVariables.TabIndex = 53;
            // 
            // listBoxBoundVariables
            // 
            this.listBoxBoundVariables.FormattingEnabled = true;
            this.listBoxBoundVariables.Location = new System.Drawing.Point(616, 97);
            this.listBoxBoundVariables.Name = "listBoxBoundVariables";
            this.listBoxBoundVariables.Size = new System.Drawing.Size(204, 108);
            this.listBoxBoundVariables.TabIndex = 52;
            // 
            // btSemanticTableau
            // 
            this.btSemanticTableau.Enabled = false;
            this.btSemanticTableau.Location = new System.Drawing.Point(722, 46);
            this.btSemanticTableau.Name = "btSemanticTableau";
            this.btSemanticTableau.Size = new System.Drawing.Size(100, 25);
            this.btSemanticTableau.TabIndex = 51;
            this.btSemanticTableau.Text = "SemanticTableau";
            this.btSemanticTableau.UseVisualStyleBackColor = true;
            // 
            // hash_Code
            // 
            this.hash_Code.FormattingEnabled = true;
            this.hash_Code.Location = new System.Drawing.Point(364, 81);
            this.hash_Code.Name = "hash_Code";
            this.hash_Code.Size = new System.Drawing.Size(246, 95);
            this.hash_Code.TabIndex = 50;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(370, 404);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(240, 214);
            this.dataGridView2.TabIndex = 49;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 404);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 214);
            this.dataGridView1.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(367, 376);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Simplified Table";
            // 
            // nand
            // 
            this.nand.FormattingEnabled = true;
            this.nand.HorizontalScrollbar = true;
            this.nand.Location = new System.Drawing.Point(96, 280);
            this.nand.Name = "nand";
            this.nand.Size = new System.Drawing.Size(514, 43);
            this.nand.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 289);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "NAND";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 376);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Truth Table";
            // 
            // disjunc_Simple
            // 
            this.disjunc_Simple.FormattingEnabled = true;
            this.disjunc_Simple.HorizontalScrollbar = true;
            this.disjunc_Simple.Location = new System.Drawing.Point(96, 231);
            this.disjunc_Simple.Name = "disjunc_Simple";
            this.disjunc_Simple.Size = new System.Drawing.Size(514, 43);
            this.disjunc_Simple.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Disjunc Simple";
            // 
            // disjunc_Normal
            // 
            this.disjunc_Normal.FormattingEnabled = true;
            this.disjunc_Normal.HorizontalScrollbar = true;
            this.disjunc_Normal.Location = new System.Drawing.Point(96, 182);
            this.disjunc_Normal.Name = "disjunc_Normal";
            this.disjunc_Normal.Size = new System.Drawing.Size(514, 43);
            this.disjunc_Normal.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Disjunc Normal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Hash Code";
            // 
            // variables
            // 
            this.variables.FormattingEnabled = true;
            this.variables.Location = new System.Drawing.Point(96, 81);
            this.variables.Name = "variables";
            this.variables.Size = new System.Drawing.Size(169, 95);
            this.variables.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Variables";
            // 
            // inputprefix
            // 
            this.inputprefix.Location = new System.Drawing.Point(96, 6);
            this.inputprefix.Name = "inputprefix";
            this.inputprefix.Size = new System.Drawing.Size(514, 20);
            this.inputprefix.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Infix";
            // 
            // Prefix
            // 
            this.Prefix.AutoSize = true;
            this.Prefix.Location = new System.Drawing.Point(12, 9);
            this.Prefix.Name = "Prefix";
            this.Prefix.Size = new System.Drawing.Size(33, 13);
            this.Prefix.TabIndex = 34;
            this.Prefix.Text = "Prefix";
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(722, 9);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(100, 25);
            this.clear.TabIndex = 33;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            // 
            // graph
            // 
            this.graph.Enabled = false;
            this.graph.Location = new System.Drawing.Point(616, 46);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(100, 25);
            this.graph.TabIndex = 32;
            this.graph.Text = "Graph";
            this.graph.UseVisualStyleBackColor = true;
            // 
            // read
            // 
            this.read.Location = new System.Drawing.Point(616, 9);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(100, 25);
            this.read.TabIndex = 31;
            this.read.Text = "Read";
            this.read.UseVisualStyleBackColor = true;
            // 
            // listBox5
            // 
            this.listBox5.FormattingEnabled = true;
            this.listBox5.HorizontalScrollbar = true;
            this.listBox5.Location = new System.Drawing.Point(96, 32);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(514, 43);
            this.listBox5.TabIndex = 58;
            // 
            // listBox6
            // 
            this.listBox6.FormattingEnabled = true;
            this.listBox6.HorizontalScrollbar = true;
            this.listBox6.Location = new System.Drawing.Point(96, 329);
            this.listBox6.Name = "listBox6";
            this.listBox6.Size = new System.Drawing.Size(514, 43);
            this.listBox6.TabIndex = 59;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 630);
            this.Controls.Add(this.listBox6);
            this.Controls.Add(this.listBox5);
            this.Controls.Add(this.labelUnboundVariables);
            this.Controls.Add(this.labelBoundVariables);
            this.Controls.Add(this.replace);
            this.Controls.Add(this.textBoxReplaceVariable);
            this.Controls.Add(this.listBoxUnboundVariables);
            this.Controls.Add(this.listBoxBoundVariables);
            this.Controls.Add(this.btSemanticTableau);
            this.Controls.Add(this.hash_Code);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nand);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.disjunc_Simple);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.disjunc_Normal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.variables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputprefix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Prefix);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.read);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUnboundVariables;
        private System.Windows.Forms.Label labelBoundVariables;
        private System.Windows.Forms.Button replace;
        private System.Windows.Forms.TextBox textBoxReplaceVariable;
        private System.Windows.Forms.ListBox listBoxUnboundVariables;
        private System.Windows.Forms.ListBox listBoxBoundVariables;
        private System.Windows.Forms.Button btSemanticTableau;
        private System.Windows.Forms.ListBox hash_Code;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox nand;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox disjunc_Simple;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox disjunc_Normal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox variables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputprefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Prefix;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button graph;
        private System.Windows.Forms.Button read;
        private System.Windows.Forms.ListBox listBox5;
        private System.Windows.Forms.ListBox listBox6;
    }
}

