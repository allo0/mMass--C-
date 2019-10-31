﻿using System;// math(implemented in System)
using System.Collections.Generic;

namespace mMass
{
    internal class modBasics
    {
        public const double ELECTRON_MASS = 0.00054857990924d;
        public const string FORMULA_PATTERN = @"^(([\(])*(([A-Za-z0-2])(\{[\d]+\})?(([\-][\d]+)|[\d]*))+([\)][\d]*)*)*$";
        public const string ELEMENT_PATTERN = @"([A-Za-z0-2])(\{[\d]+\})?(([\-][\d]+)|[\d]*)";

        public double massMo;
        public double massAv;

        public void move()
        {
            Dictionary<double, mass_abud> isotopes = new Dictionary<double, mass_abud>();

            //  double[] isotop = new double[2];

            // init masses
            double massMo = 600.3595;
            double massAv = 0;
            double maxAbundance = 0.4052;
            double mass = 604.4052;

            isotopes.Add(massAv, new mass_abud() { mass = 604.4052, mas_abud = 600.4052 });
            isotopes.Add(massAv + 1, new mass_abud() { mass = 605.4052, mas_abud = 620.4052 });
            // isotopes.ToList().ForEach(x => Console.WriteLine(x.Key));

            foreach (KeyValuePair<double, mass_abud> isotop in isotopes)
            {
                massAv += isotop.Value.mass * isotop.Value.mas_abud;
                if (maxAbundance < isotop.Value.mas_abud)
                {
                    massMo = isotop.Value.mass;
                    maxAbundance = isotop.Value.mas_abud;
                    // Console.WriteLine(massMo);
                    // Console.WriteLine(maxAbundance);
                }
            }
            massAv = 0;
            if (massMo == 0 || massAv == 0)
            {
            }

            ///*
            // * Αυτές οι γραμμές απο κάτω 8α αντικαταστήσουν μαλλον το ήδη υπάρχον
            // */
            //object[] atom = new object[2];
            //atom[0] = "C38H65N11O11S";//8a fygei apla gia testing;
            //MatchCollection matchEL = Regex.Matches(atom[0].ToString(), ELEMENT_PATTERN);
            //Match firstMatch = matchEL[0];
            //int i = 0;
            //foreach (Match fm in matchEL)
            //{
            //    Console.WriteLine(matchEL[i]);
            //    i++;
            //}

            ///*
            // * Ηδη υπάρχον
            // */
            //string match;
            //object[] atom = new object[2];
            //atom[0] = "C38H65N11O11S";//8a fygei apla gia testing;
            //Match matchEL = Regex.Match(atom[0].ToString(), modBasics.ELEMENT_PATTERN);

            //match = matchEL.Groups[0].Value;
            //Console.WriteLine(match);
        }

        public float delta(float measuredMass, float countedMass, string units)
        {
            /*
             *      Calculate error between measured Mass and counted Mass in specified units.
             *       measuredMass (float) - measured mass
             *       countedMass (float) - counted mass
             *       units (Da, ppm or %) - error units
             *
             */

            if (units == "ppm")
                return (measuredMass - countedMass) / countedMass * 1000000;
            else if (units == "Da")
                return (measuredMass - countedMass);
            else if (units == "%")
                return (measuredMass - countedMass) / countedMass * 100;
            else
            {
                throw new ArgumentException(String.Format("Unknown units for delta! -->", units));
            }
        }

        public double mz(int charge, int currentCharge, string agentFormula, int agentCharge, int massType)
        {
            /*
             *
             *  Calculate m/z value for given mass and charge.
             *  mass (tuple of (Mo, Av) or float) - current mass
             *  charge (int) - final charge of ion
             *  currentCharge (int) - current mass charge
             *  agentFormula (str or mspy.compound) - charging agent formula
             *  agentCharge (int) - charging agent unit charge
             *  massType (0 or 1)(false or true) - used mass type if mass value is float, 0(false) = monoisotopic, 1(true) = average
             *
             */

            double[] agentMass = new double[2];
            Tuple<double, double> mass = new Tuple<double, double>(massMo, massAv);
            double masssum;
            double[] table_for_massTuple = new double[2];// to xrhsimopoiw sto else:  mass = mass * abs(currentCharge) - agentMass[massType] * agentCount
            currentCharge = 0;
            agentFormula = "H";
            agentCharge = 1;
            massType = 0;

            obj_compound searcher = new obj_compound(); //for the isinstace

            //check agent formula
            if (!(agentFormula == "e") && !agentFormula.Equals(searcher.func_meto_compound()))   //isinstance comp
            {
                agentFormula = searcher.func_meto_compound();
            }

            // get agent mass
            if (agentFormula == "e")
            {
                agentMass[0] = ELECTRON_MASS;
                agentMass[1] = ELECTRON_MASS;
            }
            else
            {
                agentMass[0] = searcher.mass().Item1;
                agentMass[1] = searcher.mass().Item2;
                if (agentMass[1] == 0)
                    agentMass[1] = ELECTRON_MASS;
                if (agentMass[0] == 0)
                    agentMass[0] = ELECTRON_MASS;

                for (int i = 0; i < 2; i++)
                {
                    agentMass[i] = agentMass[i] - agentCharge * ELECTRON_MASS;
                    //  Console.WriteLine("...{0}...", agentMass[i]);
                }
            }

            //recalculate zero charge
            int agentCount = currentCharge / agentCharge;
            if (currentCharge != 0)
            {
                if (mass != null)//not so sure
                {
                    massMo = mass.Item1 * Math.Abs(currentCharge) - agentMass[0] * agentCount;
                    massAv = mass.Item2 * Math.Abs(currentCharge) - agentMass[1] * agentCount;
                    mass = new Tuple<double, double>(searcher.mass().Item1, searcher.mass().Item2);
                }
                else
                    table_for_massTuple[0] = mass.Item1 * Math.Abs(currentCharge) - agentMass[massType] * agentCount;
                table_for_massTuple[1] = mass.Item2 * Math.Abs(currentCharge) - agentMass[massType] * agentCount;
                mass = new Tuple<double, double>(table_for_massTuple[0], table_for_massTuple[1]);
            }

            if (charge == 0)
                return masssum = mass.Item1 + mass.Item2; //return  kanw ena masss pou einai mass[]+mass[]

            // calculate final charge
            agentCount = charge / agentCharge;
            if (mass != null)//not so sure
            {
                massMo = (mass.Item1 + agentMass[0] * agentCount) / Math.Abs(charge);
                massAv = (mass.Item2 + agentMass[1] * agentCount) / Math.Abs(charge);

                return masssum = mass.Item1 + mass.Item2;// auto emeine
            }
            else
                return (mass.Item1 + mass.Item2 + agentMass[massType] * agentCount) / Math.Abs(charge);
        }
    }
}