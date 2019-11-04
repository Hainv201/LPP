using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPP
{
    public partial class Form1 : Form
    {
        Formula infix;
        public Form1()
        {
            InitializeComponent();
        }

        private void read_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
                List<string> testPrefixAndresults = new List<string>();

                infix = new Formula(inputprefix.Text);
                infix_listBox.Items.Add(infix.RootProposition);
                if (!infix.IsPredicate)
                {
                    foreach (Variable variable in infix.Variables)
                    {
                        variables.Items.Add(variable);
                    }
                    // Show truth table
                    TruthTable table = infix.RootProposition.CreateTruthTable(infix.Variables);

                    foreach (Variable variable in table.ListVariables)
                    {
                        dataGridView1.Columns.Add(variable.ToString(), variable.ToString());
                    }
                    dataGridView1.Columns.Add("R", "R");
                    foreach (string[] row in table.Data)
                    {
                        dataGridView1.Rows.Add(row);
                    }
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    dataGridView1.Columns[dataGridView1.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    // Show simplified truth table
                    TruthTable simplifiedTable = table.Simplify();
                    foreach (Variable variable in simplifiedTable.ListVariables)
                    {
                        dataGridView2.Columns.Add(variable.ToString(), variable.ToString());
                    }
                    dataGridView2.Columns.Add("R", "R");
                    foreach (string[] row in simplifiedTable.Data)
                    {
                        dataGridView2.Rows.Add(row);
                    }

                    foreach (DataGridViewColumn col in dataGridView2.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    // Hash Code
                    hash_Code.Items.Add("Infix: " + table.GetTruthTableHashCode());

                    // Show disjunctive normal formula
                    Logic disjuncNormal = table.CreateDisjunctiveFormula();

                    if (disjuncNormal != null)
                    {
                        disjunc_Normal.Items.Add(disjuncNormal);
                        TruthTable disjunctable = disjuncNormal.CreateTruthTable(infix.Variables);
                        hash_Code.Items.Add("Disjunc Normal: " + disjunctable.GetTruthTableHashCode());
                    }

                    // Show disjunctive simplified formula
                    Logic disjuncSimplified = simplifiedTable.CreateDisjunctiveFormula();
                    if (disjuncSimplified != null)
                    {
                        disjunc_Simplified.Items.Add(disjuncSimplified);
                        TruthTable disjunc_simplifiedtable = disjuncSimplified.CreateTruthTable(infix.Variables);
                        hash_Code.Items.Add("Disjunc Simplified: " + disjunc_simplifiedtable.GetTruthTableHashCode());
                    }
                    // Show NAND formula
                    Logic nand = null;
                    Thread thread = new Thread(() => Nandify(ref nand));
                    thread.Start();

                    if (!thread.Join(7000))
                    {
                        nand = null;
                        thread.Abort();
                    }
                    if (nand!= null)
                    {
                        TruthTable nandtable = nand.CreateTruthTable(infix.Variables);
                        nand_ListBox.Items.Add(nand);
                        hash_Code.Items.Add("NAND: " + nandtable.GetTruthTableHashCode());
                    }
                }
                else
                {
                    // Show bound variables;
                    foreach (Variable variable in infix.BoundVariables)
                    {
                        listBoxBoundVariables.Items.Add(variable);
                    }

                    // Show unbound variables
                    List<Variable> unboundVariables = infix.Variables.Except(infix.BoundVariables).ToList();
                    unboundVariables.Sort();
                    foreach (Variable variable in unboundVariables)
                    {
                        listBoxUnboundVariables.Items.Add(variable);
                    }
                }
                graph.Enabled = true;
                btSemanticTableaux.Enabled = true;
                //Print TESTED prefix
                testPrefixAndresults.Add(inputprefix.Text);
                PrintFormula(testPrefixAndresults);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void graph_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 1;
                string str = "graph logic { \r\nnode [] " + infix.RootProposition.CreateGraph(ref index) + "\r\n}";
                File.WriteAllText(@"graph.dot", str);

                Process dot = new Process();
                dot.StartInfo.FileName = @"dot.exe";
                dot.StartInfo.Arguments = "-Tpng -ograph.png graph.dot";
                dot.Start();
                dot.WaitForExit();

                Process.Start(@"graph.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void PrintFormula(List<string> content)
        {
            string path = Environment.CurrentDirectory.ToString() + @"\TestedFormula.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("------- Tested Formula -------" + Environment.NewLine);
                }
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                foreach (String item in content)
                {
                    sw.WriteLine($"{item}" + Environment.NewLine);
                }
            }
        }

        private void ClearForm()
        {
            infix_listBox.Items.Clear();
            variables.Items.Clear();
            hash_Code.Items.Clear();
            disjunc_Normal.Items.Clear();
            disjunc_Simplified.Items.Clear();
            nand_ListBox.Items.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            listBoxBoundVariables.Items.Clear();
            listBoxUnboundVariables.Items.Clear();
            textBoxReplaceVariable.Text = "";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            inputprefix.Text = "";
            btSemanticTableaux.Enabled = false;
            graph.Enabled = false;
            ClearForm();
        }

        private void btSemanticTableau_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 1;
                Negation negation = new Negation();
                negation.LeftOperand = infix.RootProposition;
                SemanticTableaux tableaux = new SemanticTableaux(negation);
                tableaux.ApplyRule();
                bool result = tableaux.GetResult();
                if (!result)
                {
                    MessageBox.Show("The formula is not a tautology!!!");
                }
                else if (result)
                {
                    MessageBox.Show("The formula is a tautology!!!");
                }
                string str = "graph logic { \r\nnode [shape=box] " + tableaux.CreateGraph(ref index) + "\r\n}";
                File.WriteAllText(@"tableauxgraph.dot", str);

                Process dot_process = new Process();
                dot_process.StartInfo.FileName = @"dot.exe";
                dot_process.StartInfo.Arguments = "-Tpng -otableaux.png tableauxgraph.dot";
                dot_process.Start();
                dot_process.WaitForExit();

                Process.Start(@"tableaux.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Nandify(ref Logic nand)
        {
            nand = infix.RootProposition.Nandify();
        }

        private void replace_Click(object sender, EventArgs e)
        {
            if (listBoxUnboundVariables.SelectedItem != null && !String.IsNullOrWhiteSpace(textBoxReplaceVariable.Text))
            {
                Variable unboundVariable = (Variable)listBoxUnboundVariables.SelectedItem;
                unboundVariable.Letter = textBoxReplaceVariable.Text;
                // Clear old fields
                listBoxBoundVariables.Items.Clear();
                listBoxUnboundVariables.Items.Clear();
                listBoxUnboundVariables.SelectedIndex = -1;
                textBoxReplaceVariable.Text = "";

                // Show new infix formula
                infix_listBox.Items.Add(infix.RootProposition.ToString());

                // Show new bound variables
                infix.BoundVariables.Sort();
                foreach (Variable variable in infix.BoundVariables)
                {
                    listBoxBoundVariables.Items.Add(variable);
                }

                // Show new unbound variables
                List<Variable> unboundVariables = infix.Variables.Except(infix.BoundVariables).ToList();
                unboundVariables.Sort();
                foreach (Variable variable in unboundVariables)
                {
                    listBoxUnboundVariables.Items.Add(variable);
                }
            }
        }
    }
}
