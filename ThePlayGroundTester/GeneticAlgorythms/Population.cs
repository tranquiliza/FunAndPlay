using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlayGroundTester.Extensions;

namespace ThePlayGroundTester.GeneticAlgorytmns
{
    public class Population
    {
        private int PopulationSize { get; set; }
        private string TargetGenes { get; set; }
        private int PerfectScore { get; set; }
        private double MutationRate { get; set; }
        public bool Evolved { get; private set; }
        private DNA[] ListOfEntities { get; set; }
        public int Generations { get; private set; }

        public Population(int populationSize, string targetGenes, double mutationRate = 0.1)
        {
            PopulationSize = populationSize;
            TargetGenes = targetGenes;
            PerfectScore = 1;
            MutationRate = mutationRate;

            Evolved = false;
            Generations = 0;

            ListOfEntities = new DNA[PopulationSize];
            CreatePopulation();
        }

        public List<DNA> ListAllMembersOfPopulation()
        {
            var list = new List<DNA>();
            foreach (var item in ListOfEntities)
            {
                list.Add(item);
            }
            return list;
        }

        private void CreatePopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                ListOfEntities[i] = new DNA(TargetGenes, MutationRate);
            }
        }

        private DNA best;
        public DNA GetBestDna
        {
            get
            {
                return best;
            }
        }

        public void NaturalSelection()
        {
            List<DNA> Parents = new List<DNA>();
            float maxFitness = 0;
            float minFitness = 1;
            for (int i = 0; i < ListOfEntities.Length; i++)
            {
                if (ListOfEntities[i].Fitness > maxFitness)
                {
                    maxFitness = ListOfEntities[i].Fitness;
                    best = ListOfEntities[i];
                }
                if (ListOfEntities[i].Fitness < minFitness)
                {
                    minFitness = ListOfEntities[i].Fitness;
                }
            }

            if (maxFitness == PerfectScore)
            {
                Evolved = true;
            }

            for (int i = 0; i < ListOfEntities.Length; i++)
            {
                float fitness = ListOfEntities[i].Fitness;
                float floor = maxFitness / 4;
                float percentage = ((float)fitness / (float)maxFitness) * 100;
                if (fitness > floor)
                {
                    for (int j = 0; j < percentage; j++)
                    {
                        Parents.Add(ListOfEntities[i]);
                    }
                }
            }
            
            Repopulate(Parents);
        }
        
        private static Random rnd = new Random((int)DateTime.Now.Ticks);
        private void Repopulate(List<DNA> MatingPool)
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                var indexA = rnd.Next(MatingPool.Count);
                var indexB = rnd.Next(MatingPool.Count);
                var parentA = MatingPool[indexA];
                var parentB = MatingPool[indexB];

                var child = DNA.CrossOver(parentA, parentB);
                ListOfEntities[i] = child;
            }
            Generations++;
        }
    }
}
