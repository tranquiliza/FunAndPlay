using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Genetics.Genetics;

namespace GeneticsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int PopSize = 1000;
            double MutationRate = 0.01;
            string Target = "Buggi loves playing FortressCraft Evolved!";
            Population pop = new Population(PopSize, Target.ToCharArray(), MutationRate);
            while (pop.Evolving)
            {
                pop.EvolvePopulation();
                foreach (var item in pop.GetPopulation())
                {
                    Console.WriteLine(item.GetGenes());
                }
                Console.WriteLine(pop.BestDna.GetGenes() + " Fitness: " + pop.BestFitness + " gens: " + pop.Generations);
            }

            Console.WriteLine("REACHED TARGET!");
            Console.WriteLine("Took " + pop.Generations + " Generations");
            Console.WriteLine(pop.BestDna.GetGenes());
            Console.Read();

        }
    }
}
