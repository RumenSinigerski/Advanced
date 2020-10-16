using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTrainer
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split().ToArray();
            Dictionary<string, Trainer> trainers = new Dictionary<string, Trainer>();

            while (input[0] != "Tournament")
            {

                string trainerName = input[0];
                string pokemonName = input[1];
                string pokemonElement = input[2];
                int pokemonHealth = int.Parse(input[3]);

                if (!trainers.ContainsKey(trainerName))
                {
                    trainers.Add(trainerName, new Trainer(trainerName, new List<Pokemon>()));
                }

                trainers[trainerName].Pokemons.Add(new Pokemon(pokemonName,pokemonElement, pokemonHealth));

                input = Console.ReadLine().Split().ToArray();
            }

            string element = Console.ReadLine();

            while (element != "End")
            {
                foreach (var kvp in trainers)
                {
                    bool isTrue = kvp.Value.Pokemons.Any(p => p.Element == element);
                    if (isTrue)
                    {
                        kvp.Value.Badges++;
                    }
                    else
                    {
                        for (int i = 0; i < kvp.Value.Pokemons.Count; i++)
                        {
                            Pokemon pokemon = kvp.Value.Pokemons[i];
                            if (pokemon.Health - 10 > 0)
                            {
                                pokemon.Health -= 10;
                            }
                            else
                            {
                                kvp.Value.Pokemons.Remove(pokemon);
                                i--;
                            }
                        }
                        
                    }
                }

                element = Console.ReadLine();
            }

            trainers = trainers.OrderByDescending(t => t.Value.Badges).ToDictionary(k => k.Key, v => v.Value);

            foreach (var trainer in trainers)
            {
                Console.WriteLine($"{trainer.Value.Name} {trainer.Value.Badges} {trainer.Value.Pokemons.Count}");
            }
        }
    }
}
