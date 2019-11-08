﻿using System.Collections.Generic;

//using System.IO;
//using System.Xml;

namespace mMass
{
    internal class element
    {
        /*Element object definition.
        name: (str) name
        symbol: (str) symbol
        atomicNumber: (int) atomic number
        isotopes: (dict) dict of isotopes { mass number:(mass, abundance),...}
        valence: (int)
       */

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //***********************************************************************************************
        private static string name { get; set; }

        public string names = name;
        private static string symbol { get; set; }
        public string symbols = symbol;
        private static int atomicNumber { get; set; }
        public int atomicNumbers = atomicNumber;
        private static Dictionary<double, mass_abud> isotopes = new Dictionary<double, mass_abud>();
        public Dictionary<double, mass_abud> isotopess = isotopes;
        private static int valence { get; set; }
        public int valences = valence;
        //***********************************************************************************************

        public double[] mass = new double[2];

        public mass_abud ma = new mass_abud();

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public element(string name, string symbol, int atomicNumber, Dictionary<double, mass_abud> isotopes, int valence) { }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public element()
        {
            // init masses
            double massMo = 0;
            double massAv = 0;
            double maxAbundance = 0;
            double[] mass = new double[2];
            double massMoAv = massMo + massAv;
            //mass = this.mass;

            mass_abud ma = new mass_abud(); //put in mass and mass_abud the vaules mass and max_abudance
            ma.mass = massMoAv;
            ma.mas_abud = maxAbundance;

            foreach (KeyValuePair<double, mass_abud> isotop in isotopes)  //for isotop in self.isotopes.values()
            {
                massAv += isotop.Value.mass * isotop.Value.mas_abud;
                if (maxAbundance < isotop.Value.mas_abud)
                {
                    massMo = isotop.Value.mass;
                    maxAbundance = isotop.Value.mas_abud;
                }
            }
            ma.mass = massMoAv;
            ma.mas_abud = maxAbundance;

            if (massMo == 0 || massAv == 0)
            {
                //line 50-60 pyton
            }
            mass[0] = massMo;
            mass[1] = massAv;   //self.mass = (massMo, massAv)
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // Used in the Dictionary ,Holds the vlues of the mass and the abundance
        public class mass_abud
        {
            public double mass { get; set; }
            public double mas_abud { get; set; }

            public mass_abud(double m, double m_abud)
            {
                mass = m;
                mas_abud = m_abud;
            }

            public mass_abud()
            {; }
        }

        internal static Dictionary<string, element> elements = new Dictionary<string, element>()
        {
              {"Ac",new element( name="Actinium", symbol="Ac",atomicNumber=89,
                  isotopes = new Dictionary<double, mass_abud>()
                  {
                     {227, new mass_abud( 227.02774700000001, 1.0) }
                  }, valence = 3)
              },
              {"Ag",new element( name="Silver", symbol="Ag",atomicNumber=47,
                  isotopes = new Dictionary<double, mass_abud>()
                  {
                     {107, new mass_abud(106.90509299999999, 0.51839000000000002) },
                     {109,new mass_abud (108.90475600000001, 0.48160999999999998) }
                  }, valence = 1)
              },
              {"Al",new element( name="Aluminium", symbol="Al",atomicNumber=13,
                  isotopes = new Dictionary<double, mass_abud>()
                  {
                     {27, new mass_abud( 26.981538440000001, 1.0) }
                  }, valence = 3)
              },
        };
    }

    internal class monomer
    {
        //Monomer object definition.
        //abbr: (str) unique monomer abbreviation
        //formula: (str) molecular formula
        //losses: (list) list of applicable neutral losses
        //name: (str) name
        //category: (str) category name

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //***********************************************************************************************
        private static string abbr { get; set; }

        public string abbrr = abbr;
        private static string formula { get; set; }
        public string formulaa = formula;
        private static List<string> losses = new List<string>();
        public List<string> lossess = losses;
        private static string name { get; set; }
        public string namee = name;
        private static string category { get; set; }
        public string categoryy = category;
        //***********************************************************************************************

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public monomer(string abbr, string formula, List<string> losses, string name, string category) { }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public monomer()
        {
            obj_compound obc = new obj_compound();

            //init masses and composition
            string cmpd = obc.func_meto_compound(formula);
            string composition = obc.composition();
            double mass = obc._mass;

            //check formulae
            foreach (var loss in losses)
            {
                cmpd = obc.func_meto_compound(loss);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        internal static Dictionary<string, monomer> monomers = new Dictionary<string, monomer>()
        {
            {"A",new monomer(abbr="A",formula="C3H5NO",
              losses = new List<string>()
              {""}
              ,name="Alanine", category="_InternalAA")
            },
            {"C",new monomer(abbr="C",formula="C3H5NOS",
              losses = new List<string>()
              {""}
              ,name="Cysteine", category="_InternalAA")
            },
            {"D",new monomer(abbr="D",formula="C4H5NO3",
              losses = new List<string>()
              {"H20"}
              ,name="Aspartic Acid", category="_InternalAA")
            },
            {"E",new monomer(abbr="E",formula="C5H7NO3",
              losses = new List<string>()
              {"H20"}
              ,name="Glutamic Acid", category="_InternalAA")
            },
        };
    }

    internal class enzyme
    {
        //Enzyme object definition.
        //name: (str) name
        //expression: (str) regular expression of cleavage site
        //nTermFormula: (str) molecular formula for new N-terminus
        //cTermFormula: (str) molecular formula for new C-terminus
        //modsBefore: (bool) allow modifications before cleavage site
        //modsAfter: (bool) allow modifications after cleavage site

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //***********************************************************************************************

        private static string name;
        public string namee = name;
        private static string expression;
        public string expressionn = expression;
        private static string nTermFormula;
        public string nTermFromulaa = nTermFormula;
        private static string cTermFormula;
        public string cTermFomrmulaa = cTermFormula;
        private static bool modsBefore;
        public bool modsBeforee = modsBefore;
        private static bool modsAfter;
        public bool modsAfterr = modsAfter;
        //***********************************************************************************************

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public enzyme(string name, string expression, string nTermFormula, string cTermFormula, bool modsBefore, bool modsAfter) { }

        public enzyme()
        {
            obj_compound obc = new obj_compound();
            //check formulae
            string cmpd;
            cmpd = obc.func_meto_compound(nTermFormula);
            cmpd = obc.func_meto_compound(cTermFormula);
        }

        internal static Dictionary<string, enzyme> enzymes = new Dictionary<string, enzyme>()
        {
            {   "Arg-C",new enzyme(name="Arg-C",expression="[R][A-Z]",nTermFormula="H",cTermFormula="OH",modsBefore=false,modsAfter=true)   },
            {   "Asp-N",new enzyme(name="Asp-N",expression="[A-Z][D]",nTermFormula="H",cTermFormula="OH",modsBefore=true,modsAfter=false)   },
        };
    }
}