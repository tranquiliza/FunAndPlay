using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlayGroundTester.GeneticAlgorytmns
{
    public class DNA
    {
        private char[] Genes { get; set; }
        public float Fitness
        {
            get
            {
                return CalculateFitness();
            }
        }
        private string TargetGenes { get; set; }
        private double MutationRate { get; set; }

        public DNA(string targetGenes, double mutationRate)
        {
            TargetGenes = targetGenes;
            var amountOfGenes = TargetGenes.Length;
            Genes = new char[amountOfGenes];
            MutationRate = mutationRate;
            for (int i = 0; i < amountOfGenes; i++)
            {
                Genes[i] = GenerateRandomChar();
            }
        }

        public string GetGenes()
        {
            string genes = string.Empty;
            for (int i = 0; i < Genes.Length; i++)
            {
                genes += Genes[i];
            }
            return genes;
        }

        private float CalculateFitness()
        {
            float score = 0;
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Genes[i] == TargetGenes[i])
                {
                    score += 1;
                }
            }
            var percentScore = (float)score / (float)TargetGenes.Length;
            return percentScore;
        }
        
        public static DNA Mutate(DNA child)
        {
            var rnd = new Random();
            for (int i = 0; i < child.Genes.Length; i++)
            {
                if (child.MutationRate < rnd.NextDouble())
                {
                    child.Genes[i] = child.GenerateRandomChar();
                }
            }
            return child;
        }
        
        public static DNA CrossOver(DNA parentOne, DNA parentTwo)
        {
            Random rnd = new Random();
            var child = new DNA(parentOne.TargetGenes, parentOne.MutationRate);
            var midPoint = rnd.Next(0, parentOne.Genes.Length);

            for (int i = 0; i < child.Genes.Length; i++)
            {
                if (i > midPoint)
                {
                    child.Genes[i] = parentOne.Genes[i];
                }
                else
                {
                    child.Genes[i] = parentTwo.Genes[i];
                }
            }
            return Mutate(child);
        }

        private static Random rnd = new Random((int)DateTime.Now.Ticks);
        private char GenerateRandomChar()
        {
            string chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789 123456789";
            int num = rnd.Next(0, chars.Length - 1);
            return chars[num];
        }
    }
}
