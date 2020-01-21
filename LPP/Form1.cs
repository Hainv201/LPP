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
        CNF cNF;
        const int range = 10;
        Random random;
        public Form1()
        {
            InitializeComponent();
        }

        private void read_Click(object sender, EventArgs e)
        {
            try
            {
                ReadPrefix();
                genarate_graph.Enabled = true;
                btSemanticTableaux.Enabled = true;
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
                string str = "graph logic {" + infix.RootProposition.CreateGraph(ref index) + Environment.NewLine + "}";
                File.WriteAllText(@"logicgraph.dot", str);

                Process dot = new Process();
                dot.StartInfo.FileName = @"dot.exe";
                dot.StartInfo.Arguments = "-Tpng -ologicgraph.png logicgraph.dot";
                dot.Start();
                dot.WaitForExit();

                Process.Start(@"logicgraph.png");
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
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView5.Rows.Clear();
            dataGridView5.Columns.Clear();
            listBoxBoundVariables.Items.Clear();
            listBoxUnboundVariables.Items.Clear();
            Cnf_listBox.Items.Clear();
            tseitin_listBox.Items.Clear();
            textBoxReplaceVariable.Text = "";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            inputprefix.Text = "";
            btSemanticTableaux.Enabled = false;
            genarate_graph.Enabled = false;
            cnf_Graph.Enabled = false;
            David_Putnam.Enabled = false;
            Tseitin.Enabled = false;
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
                string str = "graph tableaux {" + Environment.NewLine + "node[shape=box]" + tableaux.CreateTableauxTree(ref index) + Environment.NewLine + "}";
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

        private void cnf_Graph_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 1;
                string str = "graph cnf {" + cNF.CreateCNFGraph(ref index) + Environment.NewLine + "}";
                File.WriteAllText(@"cnf.dot", str);

                Process dot = new Process();
                dot.StartInfo.FileName = @"dot.exe";
                dot.StartInfo.Arguments = "-Tpng -ographcnf.png cnf.dot";
                dot.Start();
                dot.WaitForExit();

                Process.Start(@"graphcnf.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void David_Putnam_Click(object sender, EventArgs e)
        {
            string step = "";
            if (!RunDavidPutNam(ref step,cNF,dataGridView3))
            {
                MessageBox.Show("The proposition formula is not satisfiable!!!");
            }
            MessageBox.Show(step, "Algorithm:");

        }

        private void ShowPropositionFormula()
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
            dataGridView1.Columns.Add("Result", "Result");
            foreach (string[] row in table.Data)
            {
                dataGridView1.Rows.Add(row);
            }
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Show simplified truth table
            TruthTable simplifiedTable = table.Simplify();
            foreach (Variable variable in simplifiedTable.ListVariables)
            {
                dataGridView2.Columns.Add(variable.ToString(), variable.ToString());
            }
            dataGridView2.Columns.Add("Result", "Result");
            foreach (string[] row in simplifiedTable.Data)
            {
                dataGridView2.Rows.Add(row);
            }

            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

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
            Logic nand = infix.RootProposition.Nandify();

            if (nand != null)
            {
                TruthTable nandtable = nand.CreateTruthTable(infix.Variables);
                hash_Code.Items.Add("NAND: " + nandtable.GetTruthTableHashCode());
                //Show nand
                //nand_ListBox.Items.Add(nand);
            }
        }

        // Convert Proposition formula to CNF
        private void ConvertCNF(ref CNF cnf, Logic logicRoot, List<Variable> variables)
        {
            Logic clone_root = ObjectExtension.CopyObject<Logic>(logicRoot);
            Logic logic = clone_root.ConvertToCNF();
            Console.WriteLine(logic);
            logic = logic.ApplyDistributiveLaw();
            string cnf_form = "[" + logic.GetCNFForm() + "]";
            cnf = new CNF(cnf_form);
            cnf.Cnf_List_Variables = variables;
        }

        private void randomPrefix_Click(object sender, EventArgs e)
        {
            random = new Random();
            int i = 1;
            var a = RandomPrefix(i);
            inputprefix.Text = a.GetRandomPrefix();
            ReadPrefix();
            string step = "";
            RunDavidPutNam(ref step,cNF,dataGridView3);
            genarate_graph.Enabled = true;
            btSemanticTableaux.Enabled = true;
        }

        private void Tseitin_Click(object sender, EventArgs e)
        {
            bool tseitin_HasJanus = false;
            string step = "";
            TseitinEvent(ref step, ref tseitin_HasJanus);
            if (!tseitin_HasJanus)
            {
                MessageBox.Show("The proposition formula is not satisfiable!!!");
            }
            MessageBox.Show(step, "Algorithm:");
        }

        private void TseitinEvent(ref string step, ref bool tseitin_HasJanus)
        {
            tseitin_listBox.Items.Clear();
            char tseitin = 'K';
            List<Variable> Tseitin_Variables = infix.Variables.ToList();
            infix.RootProposition.GetTseitinVariable(ref tseitin, Tseitin_Variables);
            var tsetinlogic = infix.GetTseitin();
            tseitin_listBox.Items.Add(tsetinlogic);
            // Show truth table
            TruthTable table = tsetinlogic.CreateTruthTable(Tseitin_Variables);

            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            foreach (Variable variable in Tseitin_Variables)
            {
                dataGridView4.Columns.Add(variable.ToString(), variable.ToString());
            }
            dataGridView4.Columns.Add("Result", "Result");
            foreach (string[] row in table.Data)
            {
                dataGridView4.Rows.Add(row);
            }
            foreach (DataGridViewColumn col in dataGridView4.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //Show CNF
            CNF cnf = null;
            Thread thread2 = new Thread(() => ConvertCNF(ref cnf, tsetinlogic, Tseitin_Variables));
            thread2.Start();

            if (!thread2.Join(55000))
            {
                cnf = null;
                thread2.Abort();
            }
            if (cnf != null)
            {
                Cnf_listBox.Items.Add(cnf.ToString());
                tseitin_HasJanus = RunDavidPutNam(ref step, cnf, dataGridView5);
            }
        }

        private async void test_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    random = new Random();
                    int j = 1;
                    var a = RandomPrefix(j);
                    inputprefix.Text = a.GetRandomPrefix();
                    ReadPrefix();
                    string tseitin_step = "";
                    bool tseitin_davidputnam = false;
                    TseitinEvent(ref tseitin_step,ref tseitin_davidputnam);
                    string step = "";
                    RunDavidPutNam(ref step,cNF,dataGridView3);
                    await Task.Delay(55000);
                }
                MessageBox.Show("The test is completed successfully!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.ToString());
            }
        }

        private Logic RandomPrefix(int i)
        {
            int value = random.Next(1, 100);
            int max = range * i;
            int avgrange = (100 - max) / 7;
            if (value>=1 && value< max)
            {
                int a = random.Next(0, 4);
                string character = ((char)(((int)'A') + a)).ToString();
                return new Variable(character);
            }
            else if (value >= max && value < max + avgrange)
            {
                Logic logic = new BiImplication();
                i++;
                logic.LeftOperand = RandomPrefix(i);
                logic.RightOperand = RandomPrefix(i);
                return logic;
            }
            else if (value>= max+avgrange && value<max+avgrange*2)
            {
                Logic logic = new Conjunction();
                i++;
                logic.LeftOperand = RandomPrefix(i);
                logic.RightOperand = RandomPrefix(i);
                return logic;
            }
            else if (value >= max + avgrange*2 && value < max + avgrange * 3)
            {
                Logic logic = new Disjunction();
                i++;
                logic.LeftOperand = RandomPrefix(i);
                logic.RightOperand = RandomPrefix(i);
                return logic;
            }
            else if (value >= max + avgrange * 3 && value < max + avgrange * 4)
            {
                Logic logic = new Implication();
                i++;
                logic.LeftOperand = RandomPrefix(i);
                logic.RightOperand = RandomPrefix(i);
                return logic;
            }
            else if (value >= max + avgrange * 4 && value < max + avgrange * 5)
            {
                Logic logic = new Negation();
                i++;
                logic.LeftOperand = RandomPrefix(i);
                return logic;
            }
            else if (value >= max + avgrange * 5 && value < max + avgrange * 6)
            {
                Logic logic = new NotAnd();
                i++;
                logic.LeftOperand = RandomPrefix(i);
                logic.RightOperand = RandomPrefix(i);
                return logic;
            }
            else if (value >= max + avgrange * 6 && value < max + (avgrange * 13/2))
            {
                return new False();
            }
            else
            {
                return new True();
            }
        }
        
        private void ReadPrefix()
        {
            ClearForm();
            List<string> testPrefixAndresults = new List<string>();
            string input = "";
            for (int i = 0; i < inputprefix.Lines.Length; i++)
            {
                input += inputprefix.Lines[i];
            }
            input = input.Replace(" ", "");
            if (input[0] == '[')
            {
                cNF = new CNF(input);
                infix = new Formula();
                //Convert CNF to Logic
                infix.RootProposition = cNF.ConvertToLogic();
                if (infix.RootProposition != null)
                {
                    infix_listBox.Items.Add(infix.RootProposition);
                    infix.Variables = cNF.Cnf_List_Variables;
                    ShowPropositionFormula();
                }
                //Show CNF
                Cnf_listBox.Items.Add(cNF.ToString());
                cnf_Graph.Enabled = true;
                David_Putnam.Enabled = true;
            }
            else
            {
                infix = new Formula(input);
                infix_listBox.Items.Add(infix.RootProposition);
                if (!infix.IsPredicate)
                {
                    ShowPropositionFormula();
                    //Show CNF
                    cNF = null;
                    Thread thread2 = new Thread(() => ConvertCNF(ref cNF, infix.RootProposition, infix.Variables));
                    thread2.Start();

                    if (!thread2.Join(55000))
                    {
                        cNF = null;
                        thread2.Abort();
                    }
                    if (cNF != null)
                    {
                        Cnf_listBox.Items.Add(cNF.ToString());
                        cnf_Graph.Enabled = true;
                        David_Putnam.Enabled = true;
                    }
                }
                else
                {
                    cnf_Graph.Enabled = false;
                    David_Putnam.Enabled = false;
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
            }
            Tseitin.Enabled = true;
            //Print TESTED prefix
            testPrefixAndresults.Add(inputprefix.Text);
            PrintFormula(testPrefixAndresults);
        }
        
        private bool RunDavidPutNam(ref string step, CNF cnf, DataGridView dataGridView)
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            CNF clone_cnf = ObjectExtension.CopyObject<CNF>(cnf);
            cnf.DavisPutnan(clone_cnf);
            if (!cnf.GetHasJanusValue())
            {
                dataGridView.Columns.Add("Variable", "Variable");
                dataGridView.Columns.Add("Value", "Value");
                foreach (var item in cnf.GetAppropriateValue())
                {
                    dataGridView.Rows.Add(item.Key.ToUpper(), item.Value);
                }
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                step = cnf.ShowStep();
                return true;
            }
            step = cnf.ShowStep();
            return false;
        }
    }
}
