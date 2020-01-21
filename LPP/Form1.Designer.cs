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
            this.btSemanticTableaux = new System.Windows.Forms.Button();
            this.hash_Code = new System.Windows.Forms.ListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.nand_ListBox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.disjunc_Simplified = new System.Windows.Forms.ListBox();
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
            this.genarate_graph = new System.Windows.Forms.Button();
            this.read = new System.Windows.Forms.Button();
            this.infix_listBox = new System.Windows.Forms.ListBox();
            this.Cnf_listBox = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cnf_Graph = new System.Windows.Forms.Button();
            this.David_Putnam = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.randomPrefix = new System.Windows.Forms.Button();
            this.Tseitin = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tseitin_listBox = new System.Windows.Forms.ListBox();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUnboundVariables
            // 
            this.labelUnboundVariables.AutoSize = true;
            this.labelUnboundVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUnboundVariables.Location = new System.Drawing.Point(622, 236);
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
            this.labelBoundVariables.Location = new System.Drawing.Point(622, 109);
            this.labelBoundVariables.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBoundVariables.Name = "labelBoundVariables";
            this.labelBoundVariables.Size = new System.Drawing.Size(86, 13);
            this.labelBoundVariables.TabIndex = 56;
            this.labelBoundVariables.Text = "Bound variables:";
            // 
            // replace
            // 
            this.replace.Location = new System.Drawing.Point(877, 362);
            this.replace.Name = "replace";
            this.replace.Size = new System.Drawing.Size(75, 23);
            this.replace.TabIndex = 55;
            this.replace.Text = "Replace";
            this.replace.UseVisualStyleBackColor = true;
            this.replace.Click += new System.EventHandler(this.replace_Click);
            // 
            // textBoxReplaceVariable
            // 
            this.textBoxReplaceVariable.Location = new System.Drawing.Point(625, 365);
            this.textBoxReplaceVariable.Name = "textBoxReplaceVariable";
            this.textBoxReplaceVariable.Size = new System.Drawing.Size(245, 20);
            this.textBoxReplaceVariable.TabIndex = 54;
            // 
            // listBoxUnboundVariables
            // 
            this.listBoxUnboundVariables.FormattingEnabled = true;
            this.listBoxUnboundVariables.Location = new System.Drawing.Point(627, 252);
            this.listBoxUnboundVariables.Name = "listBoxUnboundVariables";
            this.listBoxUnboundVariables.Size = new System.Drawing.Size(325, 95);
            this.listBoxUnboundVariables.TabIndex = 53;
            // 
            // listBoxBoundVariables
            // 
            this.listBoxBoundVariables.FormattingEnabled = true;
            this.listBoxBoundVariables.Location = new System.Drawing.Point(626, 125);
            this.listBoxBoundVariables.Name = "listBoxBoundVariables";
            this.listBoxBoundVariables.Size = new System.Drawing.Size(326, 108);
            this.listBoxBoundVariables.TabIndex = 52;
            // 
            // btSemanticTableaux
            // 
            this.btSemanticTableaux.Enabled = false;
            this.btSemanticTableaux.Location = new System.Drawing.Point(731, 46);
            this.btSemanticTableaux.Name = "btSemanticTableaux";
            this.btSemanticTableaux.Size = new System.Drawing.Size(107, 25);
            this.btSemanticTableaux.TabIndex = 51;
            this.btSemanticTableaux.Text = "SemanticTableaux";
            this.btSemanticTableaux.UseVisualStyleBackColor = true;
            this.btSemanticTableaux.Click += new System.EventHandler(this.btSemanticTableau_Click);
            // 
            // hash_Code
            // 
            this.hash_Code.FormattingEnabled = true;
            this.hash_Code.Location = new System.Drawing.Point(374, 81);
            this.hash_Code.Name = "hash_Code";
            this.hash_Code.Size = new System.Drawing.Size(246, 95);
            this.hash_Code.TabIndex = 50;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(211, 450);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(190, 215);
            this.dataGridView2.TabIndex = 49;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 450);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(190, 215);
            this.dataGridView1.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 434);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Simplified Table";
            // 
            // nand_ListBox
            // 
            this.nand_ListBox.FormattingEnabled = true;
            this.nand_ListBox.HorizontalScrollbar = true;
            this.nand_ListBox.Location = new System.Drawing.Point(105, 280);
            this.nand_ListBox.Name = "nand_ListBox";
            this.nand_ListBox.Size = new System.Drawing.Size(515, 43);
            this.nand_ListBox.TabIndex = 46;
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
            this.label6.Location = new System.Drawing.Point(12, 434);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Truth Table";
            // 
            // disjunc_Simplified
            // 
            this.disjunc_Simplified.FormattingEnabled = true;
            this.disjunc_Simplified.HorizontalScrollbar = true;
            this.disjunc_Simplified.Location = new System.Drawing.Point(105, 231);
            this.disjunc_Simplified.Name = "disjunc_Simplified";
            this.disjunc_Simplified.Size = new System.Drawing.Size(515, 43);
            this.disjunc_Simplified.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Disjunc Simplifed";
            // 
            // disjunc_Normal
            // 
            this.disjunc_Normal.FormattingEnabled = true;
            this.disjunc_Normal.HorizontalScrollbar = true;
            this.disjunc_Normal.Location = new System.Drawing.Point(105, 182);
            this.disjunc_Normal.Name = "disjunc_Normal";
            this.disjunc_Normal.Size = new System.Drawing.Size(515, 43);
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
            this.variables.Location = new System.Drawing.Point(105, 81);
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
            this.inputprefix.Location = new System.Drawing.Point(105, 6);
            this.inputprefix.Name = "inputprefix";
            this.inputprefix.Size = new System.Drawing.Size(515, 20);
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
            this.clear.Location = new System.Drawing.Point(731, 9);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(107, 25);
            this.clear.TabIndex = 33;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // genarate_graph
            // 
            this.genarate_graph.Enabled = false;
            this.genarate_graph.Location = new System.Drawing.Point(625, 46);
            this.genarate_graph.Name = "genarate_graph";
            this.genarate_graph.Size = new System.Drawing.Size(100, 25);
            this.genarate_graph.TabIndex = 32;
            this.genarate_graph.Text = "Graph";
            this.genarate_graph.UseVisualStyleBackColor = true;
            this.genarate_graph.Click += new System.EventHandler(this.graph_Click);
            // 
            // read
            // 
            this.read.Location = new System.Drawing.Point(625, 9);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(100, 25);
            this.read.TabIndex = 31;
            this.read.Text = "Read";
            this.read.UseVisualStyleBackColor = true;
            this.read.Click += new System.EventHandler(this.read_Click);
            // 
            // infix_listBox
            // 
            this.infix_listBox.FormattingEnabled = true;
            this.infix_listBox.HorizontalScrollbar = true;
            this.infix_listBox.Location = new System.Drawing.Point(105, 32);
            this.infix_listBox.Name = "infix_listBox";
            this.infix_listBox.Size = new System.Drawing.Size(515, 43);
            this.infix_listBox.TabIndex = 58;
            // 
            // Cnf_listBox
            // 
            this.Cnf_listBox.FormattingEnabled = true;
            this.Cnf_listBox.HorizontalScrollbar = true;
            this.Cnf_listBox.Location = new System.Drawing.Point(105, 329);
            this.Cnf_listBox.Name = "Cnf_listBox";
            this.Cnf_listBox.Size = new System.Drawing.Size(515, 43);
            this.Cnf_listBox.TabIndex = 59;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 338);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "CNF";
            // 
            // cnf_Graph
            // 
            this.cnf_Graph.Enabled = false;
            this.cnf_Graph.Location = new System.Drawing.Point(625, 81);
            this.cnf_Graph.Name = "cnf_Graph";
            this.cnf_Graph.Size = new System.Drawing.Size(100, 25);
            this.cnf_Graph.TabIndex = 62;
            this.cnf_Graph.Text = "CNF Graph";
            this.cnf_Graph.UseVisualStyleBackColor = true;
            this.cnf_Graph.Click += new System.EventHandler(this.cnf_Graph_Click);
            // 
            // David_Putnam
            // 
            this.David_Putnam.Enabled = false;
            this.David_Putnam.Location = new System.Drawing.Point(731, 81);
            this.David_Putnam.Name = "David_Putnam";
            this.David_Putnam.Size = new System.Drawing.Size(107, 25);
            this.David_Putnam.TabIndex = 63;
            this.David_Putnam.Text = "David Putnam";
            this.David_Putnam.UseVisualStyleBackColor = true;
            this.David_Putnam.Click += new System.EventHandler(this.David_Putnam_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToOrderColumns = true;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(655, 450);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(135, 215);
            this.dataGridView3.TabIndex = 64;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(652, 434);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 65;
            this.label10.Text = "Appropriate Values";
            // 
            // randomPrefix
            // 
            this.randomPrefix.Location = new System.Drawing.Point(844, 9);
            this.randomPrefix.Name = "randomPrefix";
            this.randomPrefix.Size = new System.Drawing.Size(107, 25);
            this.randomPrefix.TabIndex = 66;
            this.randomPrefix.Text = "Random";
            this.randomPrefix.UseVisualStyleBackColor = true;
            this.randomPrefix.Click += new System.EventHandler(this.randomPrefix_Click);
            // 
            // Tseitin
            // 
            this.Tseitin.Enabled = false;
            this.Tseitin.Location = new System.Drawing.Point(844, 46);
            this.Tseitin.Name = "Tseitin";
            this.Tseitin.Size = new System.Drawing.Size(107, 25);
            this.Tseitin.TabIndex = 67;
            this.Tseitin.Text = "Tseitin";
            this.Tseitin.UseVisualStyleBackColor = true;
            this.Tseitin.Click += new System.EventHandler(this.Tseitin_Click);
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(845, 81);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(107, 25);
            this.test.TabIndex = 68;
            this.test.Text = "Test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToOrderColumns = true;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(407, 450);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(242, 215);
            this.dataGridView4.TabIndex = 69;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(404, 434);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 70;
            this.label11.Text = "Tseitin Table";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 390);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 71;
            this.label12.Text = "Tseitin:";
            // 
            // tseitin_listBox
            // 
            this.tseitin_listBox.FormattingEnabled = true;
            this.tseitin_listBox.HorizontalScrollbar = true;
            this.tseitin_listBox.Location = new System.Drawing.Point(105, 378);
            this.tseitin_listBox.Name = "tseitin_listBox";
            this.tseitin_listBox.Size = new System.Drawing.Size(515, 43);
            this.tseitin_listBox.TabIndex = 72;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToOrderColumns = true;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(796, 450);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.Size = new System.Drawing.Size(155, 215);
            this.dataGridView5.TabIndex = 73;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(793, 434);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(130, 13);
            this.label13.TabIndex = 74;
            this.label13.Text = "Tseitin Appropriate Values";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 715);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dataGridView5);
            this.Controls.Add(this.tseitin_listBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.test);
            this.Controls.Add(this.Tseitin);
            this.Controls.Add(this.randomPrefix);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.David_Putnam);
            this.Controls.Add(this.cnf_Graph);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Cnf_listBox);
            this.Controls.Add(this.infix_listBox);
            this.Controls.Add(this.labelUnboundVariables);
            this.Controls.Add(this.labelBoundVariables);
            this.Controls.Add(this.replace);
            this.Controls.Add(this.textBoxReplaceVariable);
            this.Controls.Add(this.listBoxUnboundVariables);
            this.Controls.Add(this.listBoxBoundVariables);
            this.Controls.Add(this.btSemanticTableaux);
            this.Controls.Add(this.hash_Code);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nand_ListBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.disjunc_Simplified);
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
            this.Controls.Add(this.genarate_graph);
            this.Controls.Add(this.read);
            this.Name = "Form1";
            this.Text = "VH Nguyen - 3206130";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
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
        private System.Windows.Forms.Button btSemanticTableaux;
        private System.Windows.Forms.ListBox hash_Code;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox nand_ListBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox disjunc_Simplified;
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
        private System.Windows.Forms.Button genarate_graph;
        private System.Windows.Forms.Button read;
        private System.Windows.Forms.ListBox infix_listBox;
        private System.Windows.Forms.ListBox Cnf_listBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button cnf_Graph;
        private System.Windows.Forms.Button David_Putnam;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button randomPrefix;
        private System.Windows.Forms.Button Tseitin;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox tseitin_listBox;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.Label label13;
    }
}

