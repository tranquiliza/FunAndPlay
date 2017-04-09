using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Genetics.Genetics
{
    public class Population
    {
        private static Random Random = new Random();
        private int PopulationSize { get; set; }
        private DNA[] CurrentGeneration { get; set; }
        private char[] TargetGenes { get; set; }
        private double MutationRate { get; set; }
        public bool Evolving { get; set; }
        public int Generations { get; private set; }


        public Population(int populationSize, char[] targetGenes, double mutationRate)
        {
            PopulationSize = populationSize;
            TargetGenes = targetGenes;
            MutationRate = mutationRate;
            Evolving = true;
            Generations = 0;
            CreatePopulation();
        }

        private void CreatePopulation()
        {
            CurrentGeneration = new DNA[PopulationSize];
            for (int i = 0; i < PopulationSize; i++)
            {
                CurrentGeneration[i] = new DNA(TargetGenes, MutationRate);
            }
        }

        public void EvolvePopulation()
        {
            //Calculate Fitness and Create evaluations.
            EvaluatePopulation();
            if (Evolving == false)
            {
                return;
            }
            //var matingPool = GenerateMatingPool();

            var newPopulation = new DNA[PopulationSize];
            for (int i = 0; i < CurrentGeneration.Length; i++)
            {
                var parentA = AcceptOrReject();
                //var parentB = AcceptOrReject();
                var child = parentA.CrossOver(AcceptOrReject());
                child.Mutate();
                newPopulation[i] = child;
            }
            CurrentGeneration = null;
            CurrentGeneration = newPopulation;

            Generations++;
        }

        
        private DNA AcceptOrReject()
        {
            int preventInf = 0;
            while (true)
            {
                var index = Random.Next(CurrentGeneration.Length);
                var partner = CurrentGeneration[index];

                var tolerance = Random.Next((int)(BestFitness / 2), (int)BestFitness);
                if (tolerance < partner.Fitness)
                {
                    return partner;
                }
                preventInf++;

                if (preventInf > 10000)
                {
                    return partner;
                }
            }
        }


        public DNA BestDna { get; private set; }
        public double BestFitness { get; set; } 

        private void EvaluatePopulation()
        {
            for (int i = 0; i < CurrentGeneration.Length; i++)
            {
                CurrentGeneration[i].CalculateFitness();
                if (CurrentGeneration[i].Fitness > BestFitness)
                {
                    BestDna = CurrentGeneration[i];
                    BestFitness = CurrentGeneration[i].Fitness;
                }
                if (CurrentGeneration[i].IsEvolved)
                {
                    Evolving = false;
                }
            }
        }

        private List<DNA> GenerateMatingPool()
        {
            var listOfParents = new List<DNA>();
            for (int i = 0; i < CurrentGeneration.Length; i++)
            {
                var ReproMultiplier = Math.Floor(CurrentGeneration[i].Fitness * 100);
                for (int j = 0; j < ReproMultiplier; j++)
                {
                    listOfParents.Add(CurrentGeneration[i]);
                }
            }
            return listOfParents;
        }

        public List<DNA> GetPopulation()
        {
            return CurrentGeneration.ToList();
        }
    }
}
