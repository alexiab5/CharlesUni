import random
import matplotlib.pyplot as plt
import numpy as np
from matplotlib.lines import Line2D

NO_GENERATIONS = 100
POP_SIZE = 1000
IND_LEN = 25

def random_individual():
    arr = []
    for i in range(IND_LEN):
        if random.random() < 0.5:
            arr.append(0)
        else:
            arr.append(1)
    return arr

def random_initial_population(population_size):
    return [random_individual() for _ in range(population_size)]

def fitness_one_max(individual):
    sum = 0
    for bit in individual:
        sum += bit
    return sum

def fitness_alternating(individual):
    sum = 0
    for i in range(IND_LEN - 1):
        if (individual[i] == 0 and individual[i + 1] == 1) or (individual[i] == 1 and individual[i + 1] == 0):
            sum += 1
    return sum

def select(pop, fits): # roulette wheel selection, the probability of selecting an individual is proportionate with its fitness
    return random.choices(pop, fits, k=POP_SIZE)

def cross(p1, p2): # one point crossover
    p = random.randrange(1, IND_LEN) # selecting a random split point for the xover
    return p1[:p] + p2[p:], p2[:p] + p1[p:]

def crossover(pop, xover_prob):
    offsprings = []
    for p1, p2 in zip(pop[::2], pop[1::2]): # zip creates the pairs (0,1), (2,3) ...
        o1, o2 = p1, p2
        if random.random() < xover_prob:
            o1, o2 = cross(p1, p2)
        offsprings.append(o1[:])
        offsprings.append(o2[:])
    return offsprings

def mutate(individual, mut_flip_prob):
    length = len(individual)
    for i in range(length):
        if random.random() < mut_flip_prob:
            individual[i] = 1 - individual[i]
    return individual

def mutation(population, mut_prob):
    for ind in population:
        if random.random() < mut_prob:
            ind = mutate(ind, 1/len(ind))
    return population

def evolutionary_algorithm(pop, fitness_function, mut_prob, xover_prob):
    log = [] # log keeps track of the best fitness of an individual from each generation
    for _ in range(NO_GENERATIONS):
        fits = [fitness_function(ind) for ind in pop] # create an array containing the fitnesses of all individuals in the population
        log.append(max(fits))
        mating_pool = select(pop, fits) #  mating pool will contain, for the most part, the best fitted individuals, with the worst ones having a much lower probability of being included
        o = crossover(mating_pool, xover_prob)
        offspring = mutation(o, mut_prob)
        pop = offspring[:]
    return pop, log

logs = []
for _ in range(10):
    pop = random_initial_population(POP_SIZE)
    pop, log = evolutionary_algorithm(pop, fitness_one_max, 0.3, 0.7)
    logs.append(log)
logs = np.array(logs)
plt.plot(logs.mean(axis=0))
plt.fill_between(list(range(NO_GENERATIONS)), np.percentile(logs, axis=0, q=25),
                 np.percentile(logs, axis=0, q=75), alpha=0.5)

logs2 = []
for _ in range(10):
    pop = random_initial_population(POP_SIZE)
    pop, log = evolutionary_algorithm(pop, fitness_one_max, 0.8, 0.02)
    logs2.append(log)
logs = np.array(logs2)
plt.plot(logs.mean(axis=0))
plt.fill_between(list(range(NO_GENERATIONS)), np.percentile(logs, axis=0, q=25),
                 np.percentile(logs, axis=0, q=75), alpha=0.5)

plt.show()

""""
1, 2, 3. I implemented the simple genetic algorithm to find an individual with all 1s, as well as an individual with alternating 1s and 0s.
For the second problem, the fitness function simply counts the number of 'pairs' 01 or 10 and returns this number, representing the fitness of the individual.

The genetic algorithm works as described in the lecture: starting with a random population of individuals, at each iteration we perform 
the roulette wheel selection and establish a mating pool containing mostly the 'best fitted' individuals. Then, the genetic operators 
(one point crossover and bit flip mutation) are applied to the individuals in the mating pool in order to obtain the offsprings. The process
continues for NO_GENERATIONS iterations, at each step, the new population of offsprings replacing the old one.

4. Tweaking the xover and mutation probabilities:
As the xover probability increases, the performance of the algorithm seems to be increasing as well - the best fitted individuals 
are found more quickly as the chances of xover are higher and, if the chances are lower, they are found after a bigger number of generations, or not even at all.
If the xover probability is too high however, the population becomes too similar too early, reducing diversity and potentially causing the algorithm to get stuck in local optima.

For mutation, the opposite holds true: the higher the chances of mutation, the lower the performance of the algorithm -
if the mutation probability is too high, the algorithm becomes overly random, resembling more a random search
With too little mutation, the algorithm may not explore enough of the solution space, leading to premature convergence.

5. The plots showcase the performance of the evolutionary algorithm over multiple generations, averaged over 10 runs.
The X-axis represents the number of generations in the evolutionary algorithm. Each point on the X-axis corresponds to a specific generation.
The Y-axis represents the performance measure of the population in the EA. Higher values generally indicate better performance.
The solid line in the plot shows the trend of how the performance changes over time.
The shaded region gives an idea of the variability or uncertainty in the performance across runs. Narrow shaded regions indicates more consistent results across runs.
"""