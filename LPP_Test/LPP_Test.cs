using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LPP;

namespace LPP_Test
{
    [TestClass]
    public class LPP_Test
    {
        [TestMethod]
        public void Test_ConvertToCNF()
        {
            string test = "~(%(>(=(A,B),C),D))";
            Formula infix = new Formula(test);
            Logic logic = infix.RootProposition.ConvertToCNF();
            logic = logic.ApplyDistributiveLaw();
            CNF cnf = new CNF("[" + logic.GetCNFForm() + "]");
            cnf.Cnf_List_Variables = infix.Variables;
            Assert.AreEqual(cnf.ToString(), "[D,CaA,Cab,CBA,CBb]");
        }

        [TestMethod]
        public void Test_ConvertToLogic()
        {
            string test = "[D,CaA,Cab,CBA,CBb]";
            CNF cnf = new CNF(test);
            Formula infix = new Formula();
            infix.RootProposition = cnf.ConvertToLogic();
            Assert.AreEqual(infix.RootProposition.ToString(), "(D & ((C | ((~A) | A)) & ((C | ((~A) | (~B))) & ((C | (B | A)) & (C | (B | (~B)))))))");

            Formula formula2 = new Formula("~(%(>(=(A,B),C),D))");

            TruthTable table1 = formula2.RootProposition.CreateTruthTable(formula2.Variables);

            infix.Variables = cnf.Cnf_List_Variables;
            TruthTable table2 = infix.RootProposition.CreateTruthTable(infix.Variables);

            Assert.AreEqual(table1.GetTruthTableHashCode(), table2.GetTruthTableHashCode());

        }

        [TestMethod]
        public void Test_RemoveUseless()
        {
            string test = "[D,CaA,Cab,CBA,CBb]";
            CNF cnf = new CNF(test);
            cnf = cnf.RemoveUseless(cnf);
            Assert.AreEqual("[D,Cab,CBA]", cnf.ToString());
        }

        [TestMethod]
        public void Test_Resolution()
        {
            string test = "[D,CaA,Cab,CBA,CBb]";
            CNF cnf = new CNF(test);
            cnf = cnf.RemoveUseless(cnf);
            Variable v = cnf.Cnf_List_Variables[0];
            cnf = cnf.Resolution(cnf, v);
            Assert.AreEqual("[D,CBb]", cnf.ToString());
        }

        [TestMethod]
        public void Test_SolveNonJanus()
        {
            string test = "[A,CdD,Cdb,CBA,CBb]";
            CNF cnf = new CNF(test);
            cnf = cnf.RemoveUseless(cnf);
            Variable v = cnf.Cnf_List_Variables[0];
            cnf = cnf.SolveNonJanus(cnf, v);
            Assert.AreEqual("[Cdb]", cnf.ToString());
        }

        [TestMethod]
        public void Test_HasJanus()
        {
            string test = "[A,AC,acd,AD,af,AFa,AFf,CD,Fa,Ff]";
            CNF cNF = new CNF(test);
            CNF clone_cnf = ObjectExtension.CopyObject<CNF>(cNF);
            cNF.DavisPutnan(clone_cnf);
            Assert.AreEqual(true, cNF.GetHasJanusValue());
        }

        [TestMethod]
        public void Test_Contradictions()
        {
            string[] test = new string[]
            {
                "&(&(B,=(>(|(=(A,B),C),>(~(=(A,B)),>(A,>(A,B)))),~(B))),~(D))",
                "~(&(>(A,&(>(>(|(C,C),&(C,|(C,=(B,A)))),|(|(C,C),A)),=(>(B,B),A))),>(~(>(B,A)),|(B,B))))",
                "&(&(~(A),&(~(B),B)),~(>(=(B,B),~(A))))",
                "&(|(C,|(B,=(=(C,C),&(B,>(|(A,B),C))))),=(A,~(A)))",
            };
            foreach (string item in test)
            {
                Formula infix = new Formula(item);
                Logic logic = infix.RootProposition.ConvertToCNF();
                logic = logic.ApplyDistributiveLaw();
                CNF cnf = new CNF("["+logic.GetCNFForm()+"]");
                cnf.Cnf_List_Variables = infix.Variables;
                CNF clone_cnf = ObjectExtension.CopyObject<CNF>(cnf);
                cnf.DavisPutnan(clone_cnf);
                Assert.AreEqual(true, cnf.GetHasJanusValue());
                cnf.ShowStep();
            }
        }

        [TestMethod]
        public void Test_Performance()
        {
            string[] test = new string[]
            {
                ">(P,~(=(0,%(&(=(=(|(&(0,X),%(~(=(=(~(>(=(|(X,R),~(%(1,0))),0)),=(>(>(&(%(=(=(~(P),|(X,P)),P),&(&(%(Q,=(P,=(R,Q))),%(|(=(Q,|(R,>(%(=(&(=(0,~(0)),%(~(P),~(0))),S),Q),&(|(&(Q,~(|(%(=(&(=(>(|(~(X),~(|(P,&(1,>(|(S,Q),Q))))),1),1),P),S),X),X))),X),X)))),S),1)),0)),R),Q),S),X)),X)),R)),1),0),1),R))))",
                "|(>(~(>(|(C,C),B)),|(~(>(&(&(>(=(C,A),B),C),B),>(C,C))),C)),=(=(&(=(~(~(&(A,&(C,C)))),D),|(|(C,B),B)),|(&(=(>(C,C),B),C),&(A,C))),|(|(A,C),B)))",
                ">(~(=(|(=(|(|(|(C,>(%(A,B),C)),C),D),|(%(&(C,~(A)),C),>(A,B))),A),>(C,%(A,C)))),%(C,%(&(|(&(&(A,&(C,C)),|(C,A)),~(~(A))),>(~(|(~(%(A,A)),A)),A)),|(%(A,=(%(%(A,|(D,B)),A),D)),|(~(%(&(B,D),|(B,B))),|(>(C,D),C))))))",
                "%(%(%(%(B,C),%(B,C)),%(%(B,C),%(B,C))),%(%(%(A,%(B,B)),%(A,%(B,B))),%(%(A,%(B,B)),%(A,%(B,B)))))",
                "%(%(%(%(%(A,A),%(A,A)),%(B,B)),%(%(%(A,A),%(A,A)),%(B,B))),%(%(%(C,%(C,C)),%(C,%(C,C))),%(%(C,%(C,C)),%(C,%(C,C)))))"
            };
            foreach (string item in test)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Formula infix = new Formula(item);
                Logic logic = infix.RootProposition.ConvertToCNF();
                logic = logic.ApplyDistributiveLaw();
                CNF cnf = new CNF("[" + logic.GetCNFForm() + "]");
                cnf.ToString();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Assert.AreEqual(true, elapsedMs < 45000);
            }
        }
    }
}
