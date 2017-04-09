using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Genetics.Genetics
{
    public class DNA
    {
        private static Random Random = new Random();
        private char[] Genes { get; set; }
        private char[] TargetGenes { get; set; }
        private double MutationRate { get; set; }
        public bool IsEvolved { get; set; }
        public double Fitness { get; private set; }
        
        public DNA(char[] targetGenes, double mutationRate, bool isFirstGeneration = true)
        {
            TargetGenes = targetGenes;
            MutationRate = mutationRate;
            IsEvolved = false;
            Genes = new char[TargetGenes.Length];
            if (isFirstGeneration)
            {
                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = GetNextRandomChar();
                }
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

        public void CalculateFitness()
        {
            double maxScore = Genes.Length;
            double score = 0;
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Genes[i] == TargetGenes[i])
                {
                    score++;
                }
            }
            if (score == maxScore)
            {
                IsEvolved = true;
            }
            var finalScore = Math.Pow(score, 2);
            Fitness = finalScore;
        }

        public void Mutate()
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Random.NextDouble() < MutationRate)
                {
                    Genes[i] = GetNextRandomChar();
                }
            }
        }

        public DNA CrossOver(DNA Partner)
        {
            var child = new DNA(TargetGenes, MutationRate, isFirstGeneration: false);
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Random.NextDouble() < 0.5)
                {
                    child.Genes[i] = Genes[i];
                }
                else
                {
                    child.Genes[i] = Partner.Genes[i];
                }
            }
            return child;
        }
        
        public char GetNextRandomChar()
        {
            char[] chars = "qwertyuiopåasdfghjklæøzxcvbnmQWERTYUIOPÅASDFGHJKLÆØZXCVBNM123456789.-_'*!#¤%&/()=?, ".ToCharArray();    //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ .".ToCharArray();
            int index = Random.Next(0, chars.Length);
            return chars[index];
        }

    }
}
