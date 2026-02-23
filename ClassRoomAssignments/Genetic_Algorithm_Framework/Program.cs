using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Chromosome interface
public interface IChromosome<TGene, TFitness> : IComparable<IChromosome<TGene, TFitness>>
    where TFitness : IComparable<TFitness>
{
    IReadOnlyList<TGene> Genes { get; }
    TFitness Fitness { get; set; }
    IChromosome<TGene, TFitness> Crossover(IChromosome<TGene, TFitness> other);
    void Mutate(double mutationRate);
    IChromosome<TGene, TFitness> Clone();
}

// Binary chromosome implementation
public class BinaryChromosome : IChromosome<int, double>
{
    private List<int> genes;
    private Random random = new Random();

    public IReadOnlyList<int> Genes => genes.AsReadOnly();
    public double Fitness { get; set; }

    public BinaryChromosome(int length)
    {
        genes = new List<int>();
        for (int i = 0; i < length; i++)
        {
            genes.Add(random.Next(2));
        }
    }

    public BinaryChromosome(List<int> genes)
    {
        this.genes = new List<int>(genes);
    }

    public int CompareTo(IChromosome<int, double> other)
    {
        return other.Fitness.CompareTo(Fitness);
    }

    public IChromosome<int, double> Crossover(IChromosome<int, double> other)
    {
        int crossPoint = random.Next(1, genes.Count);
        List<int> childGenes = new List<int>();

        for (int i = 0; i < crossPoint; i++)
        {
            childGenes.Add(genes[i]);
        }
        for (int i = crossPoint; i < other.Genes.Count; i++)
        {
            childGenes.Add(other.Genes[i]);
        }

        return new BinaryChromosome(childGenes);
    }

    public void Mutate(double mutationRate)
    {
        for (int i = 0; i < genes.Count; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                genes[i] = 1 - genes[i];
            }
        }
    }

    public IChromosome<int, double> Clone()
    {
        BinaryChromosome clone = new BinaryChromosome(new List<int>(genes));
        clone.Fitness = Fitness;
        return clone;
    }

    public override string ToString()
    {
        return string.Join("", genes) + $" (Fitness: {Fitness:F4})";
    }
}

// Real-valued chromosome
public class RealChromosome : IChromosome<double, double>
{
    private List<double> genes;
    private Random random = new Random();
    private double minValue;
    private double maxValue;

    public IReadOnlyList<double> Genes => genes.AsReadOnly();
    public double Fitness { get; set; }

    public RealChromosome(int length, double min, double max)
    {
        minValue = min;
        maxValue = max;
        genes = new List<double>();
        for (int i = 0; i < length; i++)
        {
            genes.Add(min + random.NextDouble() * (max - min));
        }
    }

    public RealChromosome(List<double> genes, double min, double max)
    {
        this.genes = new List<double>(genes);
        minValue = min;
        maxValue = max;
    }

    public int CompareTo(IChromosome<double, double> other)
    {
        return other.Fitness.CompareTo(Fitness);
    }

    public IChromosome<double, double> Crossover(IChromosome<double, double> other)
    {
        List<double> childGenes = new List<double>();
        double alpha = random.NextDouble();

        for (int i = 0; i < genes.Count; i++)
        {
            double childGene = alpha * genes[i] + (1 - alpha) * other.Genes[i];
            childGenes.Add(childGene);
        }

        return new RealChromosome(childGenes, minValue, maxValue);
    }

    public void Mutate(double mutationRate)
    {
        for (int i = 0; i < genes.Count; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                double mutation = (random.NextDouble() - 0.5) * (maxValue - minValue) * 0.1;
                genes[i] = Math.Clamp(genes[i] + mutation, minValue, maxValue);
            }
        }
    }

    public IChromosome<double, double> Clone()
    {
        RealChromosome clone = new RealChromosome(new List<double>(genes), minValue, maxValue);
        clone.Fitness = Fitness;
        return clone;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", genes.Select(g => g.ToString("F2")))}] (Fitness: {Fitness:F4})";
    }
}

// Selection strategy interface (contravariant)
public interface ISelectionStrategy<in TChromosome>
{
    List<object> Select(IEnumerable<TChromosome> population, int count);
}

// Tournament selection
public class TournamentSelection<TGene, TFitness> : ISelectionStrategy<IChromosome<TGene, TFitness>>
    where TFitness : IComparable<TFitness>
{
    private int tournamentSize;
    private Random random = new Random();

    public TournamentSelection(int size)
    {
        tournamentSize = size;
    }

    public List<object> Select(IEnumerable<IChromosome<TGene, TFitness>> population, int count)
    {
        List<object> selected = new List<object>();
        List<IChromosome<TGene, TFitness>> popList = population.ToList();

        for (int i = 0; i < count; i++)
        {
            IChromosome<TGene, TFitness> best = null;
            for (int j = 0; j < tournamentSize; j++)
            {
                IChromosome<TGene, TFitness> candidate = popList[random.Next(popList.Count)];
                if (best == null || candidate.Fitness.CompareTo(best.Fitness) > 0)
                {
                    best = candidate;
                }
            }
            selected.Add(best);
        }

        return selected;
    }
}

// Roulette wheel selection
public class RouletteSelection<TGene> : ISelectionStrategy<IChromosome<TGene, double>>
{
    private Random random = new Random();

    public List<object> Select(IEnumerable<IChromosome<TGene, double>> population, int count)
    {
        List<object> selected = new List<object>();
        List<IChromosome<TGene, double>> popList = population.ToList();

        double totalFitness = popList.Sum(c => Math.Max(0, c.Fitness));
        if (totalFitness == 0)
        {
            for (int i = 0; i < count; i++)
            {
                selected.Add(popList[random.Next(popList.Count)]);
            }
            return selected;
        }

        for (int i = 0; i < count; i++)
        {
            double pick = random.NextDouble() * totalFitness;
            double current = 0;

            foreach (IChromosome<TGene, double> chromosome in popList)
            {
                current += Math.Max(0, chromosome.Fitness);
                if (current >= pick)
                {
                    selected.Add(chromosome);
                    break;
                }
            }
        }

        return selected;
    }
}

// Population statistics
public class PopulationStatistics
{
    public double BestFitness { get; set; }
    public double AverageFitness { get; set; }
    public double WorstFitness { get; set; }
    public double FitnessStdDev { get; set; }
    public int Generation { get; set; }
    public int PopulationSize { get; set; }
    public double DiversityIndex { get; set; }

    public void Accumulate<TGene>(IChromosome<TGene, double> chromosome)
    {
        PopulationSize++;
        double fitness = chromosome.Fitness;
        AverageFitness += fitness;

        if (PopulationSize == 1 || fitness > BestFitness)
        {
            BestFitness = fitness;
        }
        if (PopulationSize == 1 || fitness < WorstFitness)
        {
            WorstFitness = fitness;
        }
    }

    public PopulationStatistics Combine(PopulationStatistics other)
    {
        PopulationStatistics combined = new PopulationStatistics();
        combined.PopulationSize = PopulationSize + other.PopulationSize;
        combined.AverageFitness = AverageFitness + other.AverageFitness;
        combined.BestFitness = Math.Max(BestFitness, other.BestFitness);
        combined.WorstFitness = Math.Min(WorstFitness, other.WorstFitness);
        combined.Generation = Generation;
        return combined;
    }

    public PopulationStatistics Normalize()
    {
        if (PopulationSize > 0)
        {
            AverageFitness /= PopulationSize;
        }
        return this;
    }

    public override string ToString()
    {
        return $"Gen {Generation}: Best={BestFitness:F4}, Avg={AverageFitness:F4}, Worst={WorstFitness:F4}, Pop={PopulationSize}";
    }
}

// Evolution metrics
public class EvolutionMetrics
{
    public int Generation { get; set; }
    public double BestFitness { get; set; }
    public double AverageFitness { get; set; }
    public TimeSpan ElapsedTime { get; set; }
}

// Evolution configuration
public class EvolutionConfiguration
{
    public int PopulationSize { get; set; } = 100;
    public int MaxGenerations { get; set; } = 100;
    public double MutationRate { get; set; } = 0.01;
    public double CrossoverRate { get; set; } = 0.8;
    public int EliteCount { get; set; } = 2;
    public double TargetFitness { get; set; } = double.MaxValue;
}

// Evolutionary algorithm
public class EvolutionaryAlgorithm<TGene, TChromosome>
    where TChromosome : class, IChromosome<TGene, double>
{
    private List<TChromosome> population = new List<TChromosome>();
    private SortedList<double, TChromosome> elitePool = new SortedList<double, TChromosome>(Comparer<double>.Create((a, b) => b.CompareTo(a)));
    private ConcurrentBag<TChromosome> offspring = new ConcurrentBag<TChromosome>();
    private List<EvolutionMetrics> history = new List<EvolutionMetrics>();
    private Random random = new Random();

    private Func<TChromosome> chromosomeFactory;
    private Func<TChromosome, double> fitnessFunction;

    public EvolutionaryAlgorithm(Func<TChromosome> factory, Func<TChromosome, double> fitness)
    {
        chromosomeFactory = factory;
        fitnessFunction = fitness;
    }

    public (TChromosome BestSolution, IEnumerable<EvolutionMetrics> History) Evolve(
        EvolutionConfiguration config,
        CancellationToken cancellationToken = default)
    {
        DateTime startTime = DateTime.Now;

        // Initialize population
        population.Clear();
        for (int i = 0; i < config.PopulationSize; i++)
        {
            population.Add(chromosomeFactory());
        }

        // Evaluate initial fitness in parallel
        Parallel.ForEach(population, chromosome =>
        {
            chromosome.Fitness = fitnessFunction(chromosome);
        });

        TChromosome bestSolution = null;

        for (int generation = 0; generation < config.MaxGenerations; generation++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            // Sort by fitness
            population.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));

            // Update best solution
            if (bestSolution == null || population[0].Fitness > bestSolution.Fitness)
            {
                bestSolution = (TChromosome)population[0].Clone();
            }

            // Record metrics
            EvolutionMetrics metrics = new EvolutionMetrics
            {
                Generation = generation,
                BestFitness = population[0].Fitness,
                AverageFitness = population.Average(c => c.Fitness),
                ElapsedTime = DateTime.Now - startTime
            };
            history.Add(metrics);

            // Check target fitness
            if (population[0].Fitness >= config.TargetFitness)
            {
                break;
            }

            // Create new population
            List<TChromosome> newPopulation = new List<TChromosome>();

            // Elitism
            for (int i = 0; i < config.EliteCount && i < population.Count; i++)
            {
                newPopulation.Add((TChromosome)population[i].Clone());
            }

            // Tournament selection and crossover
            while (newPopulation.Count < config.PopulationSize)
            {
                TChromosome parent1 = TournamentSelect(population, 3);
                TChromosome parent2 = TournamentSelect(population, 3);

                TChromosome child;
                if (random.NextDouble() < config.CrossoverRate)
                {
                    child = (TChromosome)parent1.Crossover(parent2);
                }
                else
                {
                    child = (TChromosome)parent1.Clone();
                }

                child.Mutate(config.MutationRate);
                child.Fitness = fitnessFunction(child);
                newPopulation.Add(child);
            }

            population = newPopulation;
        }

        return (bestSolution, history);
    }

    private TChromosome TournamentSelect(List<TChromosome> pop, int tournamentSize)
    {
        TChromosome best = null;
        for (int i = 0; i < tournamentSize; i++)
        {
            TChromosome candidate = pop[random.Next(pop.Count)];
            if (best == null || candidate.Fitness > best.Fitness)
            {
                best = candidate;
            }
        }
        return best;
    }

    public PopulationStatistics GetStatistics()
    {
        PopulationStatistics stats = new PopulationStatistics();

        foreach (TChromosome chromosome in population)
        {
            stats.Accumulate(chromosome);
        }

        return stats.Normalize();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Genetic Algorithm Framework ===\n");

        // Problem 1: Maximize OneMax (count of 1s in binary string)
        Console.WriteLine("--- Problem 1: OneMax (Binary Chromosome) ---");
        Console.WriteLine("Objective: Maximize the number of 1s in a 20-bit string\n");

        EvolutionaryAlgorithm<int, BinaryChromosome> binaryGA = new EvolutionaryAlgorithm<int, BinaryChromosome>(
            () => new BinaryChromosome(20),
            chromosome => chromosome.Genes.Sum()
        );

        EvolutionConfiguration binaryConfig = new EvolutionConfiguration
        {
            PopulationSize = 50,
            MaxGenerations = 50,
            MutationRate = 0.02,
            CrossoverRate = 0.8,
            EliteCount = 2,
            TargetFitness = 20
        };

        var (binarySolution, binaryHistory) = binaryGA.Evolve(binaryConfig);

        Console.WriteLine($"Best solution: {binarySolution}");
        Console.WriteLine($"Generations: {binaryHistory.Count()}");

        Console.WriteLine("\nEvolution progress (every 10 generations):");
        foreach (EvolutionMetrics m in binaryHistory.Where((m, i) => i % 10 == 0 || m.Generation == binaryHistory.Count() - 1))
        {
            Console.WriteLine($"  Gen {m.Generation}: Best={m.BestFitness:F2}, Avg={m.AverageFitness:F2}");
        }

        // Problem 2: Sphere function optimization
        Console.WriteLine("\n--- Problem 2: Sphere Function (Real Chromosome) ---");
        Console.WriteLine("Objective: Minimize f(x) = sum(xi^2) for 5 variables in [-10, 10]\n");

        EvolutionaryAlgorithm<double, RealChromosome> realGA = new EvolutionaryAlgorithm<double, RealChromosome>(
            () => new RealChromosome(5, -10, 10),
            chromosome =>
            {
                // Fitness is negative of sphere function (to maximize)
                double sum = chromosome.Genes.Sum(g => g * g);
                return -sum;
            }
        );

        EvolutionConfiguration realConfig = new EvolutionConfiguration
        {
            PopulationSize = 100,
            MaxGenerations = 100,
            MutationRate = 0.1,
            CrossoverRate = 0.9,
            EliteCount = 5
        };

        var (realSolution, realHistory) = realGA.Evolve(realConfig);

        Console.WriteLine($"Best solution: {realSolution}");
        double sphereValue = realSolution.Genes.Sum(g => g * g);
        Console.WriteLine($"Sphere function value: {sphereValue:F6}");

        Console.WriteLine("\nEvolution progress (every 20 generations):");
        foreach (EvolutionMetrics m in realHistory.Where((m, i) => i % 20 == 0 || m.Generation == realHistory.Count() - 1))
        {
            Console.WriteLine($"  Gen {m.Generation}: Best={-m.BestFitness:F6}, Avg={-m.AverageFitness:F6}");
        }

        // Problem 3: Rastrigin function
        Console.WriteLine("\n--- Problem 3: Rastrigin Function ---");
        Console.WriteLine("Objective: Minimize Rastrigin function (global minimum = 0 at origin)\n");

        EvolutionaryAlgorithm<double, RealChromosome> rastriginGA = new EvolutionaryAlgorithm<double, RealChromosome>(
            () => new RealChromosome(3, -5.12, 5.12),
            chromosome =>
            {
                int n = chromosome.Genes.Count;
                double sum = 10 * n;
                foreach (double x in chromosome.Genes)
                {
                    sum += x * x - 10 * Math.Cos(2 * Math.PI * x);
                }
                return -sum; // Negative for maximization
            }
        );

        EvolutionConfiguration rastriginConfig = new EvolutionConfiguration
        {
            PopulationSize = 150,
            MaxGenerations = 200,
            MutationRate = 0.15,
            CrossoverRate = 0.85,
            EliteCount = 5
        };

        var (rastriginSolution, rastriginHistory) = rastriginGA.Evolve(rastriginConfig);

        Console.WriteLine($"Best solution: {rastriginSolution}");
        double rastriginValue = CalculateRastrigin(rastriginSolution.Genes);
        Console.WriteLine($"Rastrigin function value: {rastriginValue:F6}");

        // Test selection strategies
        Console.WriteLine("\n--- Testing Selection Strategies ---");

        List<IChromosome<int, double>> testPop = new List<IChromosome<int, double>>();
        Random rand = new Random();
        for (int i = 0; i < 10; i++)
        {
            BinaryChromosome c = new BinaryChromosome(10);
            c.Fitness = rand.NextDouble() * 100;
            testPop.Add(c);
        }

        Console.WriteLine("\nTournament Selection (size=3):");
        TournamentSelection<int, double> tournament = new TournamentSelection<int, double>(3);
        List<object> selected = tournament.Select(testPop, 5);
        foreach (object s in selected)
        {
            Console.WriteLine($"  Selected: Fitness = {((IChromosome<int, double>)s).Fitness:F2}");
        }

        Console.WriteLine("\nRoulette Selection:");
        RouletteSelection<int> roulette = new RouletteSelection<int>();
        selected = roulette.Select(testPop, 5);
        foreach (object s in selected)
        {
            Console.WriteLine($"  Selected: Fitness = {((IChromosome<int, double>)s).Fitness:F2}");
        }

        // Final statistics
        Console.WriteLine("\n--- Algorithm Statistics ---");
        PopulationStatistics stats = binaryGA.GetStatistics();
        Console.WriteLine($"Binary GA: {stats}");
    }

    static double CalculateRastrigin(IReadOnlyList<double> x)
    {
        int n = x.Count;
        double sum = 10 * n;
        foreach (double xi in x)
        {
            sum += xi * xi - 10 * Math.Cos(2 * Math.PI * xi);
        }
        return sum;
    }
}
