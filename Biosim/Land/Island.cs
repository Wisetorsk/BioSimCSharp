using System;
using System.Collections.Generic;
using Biosim.Animals;
using Biosim.Land;
using Biosim.Parameters;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosim.Land
{
    class Island
    {
        public string Template { get; set; } = null;
        public List<List<IEnviroment>> Land { get; set; } // Nested List
        private List<Herbivore> Herbivores { get; set; }
        private List<Carnivore> Carnivores { get; set; }
        public Position DefaultDim { get; set; } = new Position { x = 10, y = 10 };

        public Island(string template = null, List<Herbivore> herbivores = null, List<Carnivore> carnivores = null) 
        { 
            if (template is null)
            {
                Template = DefaultParameters.DefaultIsland;
            } else
            {
                Template = template;
            }

            if (herbivores is null)
            {
                //Add herbivores according to pattern.
                Herbivores = DefaultParameters.HerbivorePopulation;
            } else
            {
                Herbivores = herbivores;
            }

            if (carnivores is null)
            {
                //Add herbivores according to pattern.
                Carnivores = DefaultParameters.CarnivorePopulation;
            }
            else
            {
                Carnivores = carnivores;
            }
        }

        public void Display()
        {
            // Displays the island as chr in console
            var lines = Template.Split('\n');
            Console.Clear();
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public void BuildIsland()
        {

            foreach (var line in Template.Split('\n'))
            {
                var lLine = new List<IEnviroment>();
                foreach (var plot in line)
                {
                    switch (plot)
                    {
                        case 'O':
                            lLine.Add(new Ocean());
                            break;

                        case 'D':
                            lLine.Add(new Desert());
                            break;

                        case 'S':
                            lLine.Add(new Savannah());
                            break;

                        case 'M':
                            lLine.Add(new Mountain());
                            break;

                        case 'J':
                            lLine.Add(new Jungle());
                            break;

                        default:
                            break;
                    }
                }
                Land.Add(lLine);
            }
        }
    }
}
