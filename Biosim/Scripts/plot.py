import matplotlib.pyplot as plt
import numpy as np
import sys

try:
	data = np.genfromtxt("../../Results/SimResult.csv", delimiter=",", names=["Year", "Herbivores", "Carnivores", "HFitness", "CFitness"])
	fig = plt.figure(1)
	ax1 = fig.add_subplot(211)
	ax2 = fig.add_subplot(212)
	plt.xlabel('Year')

	ax1.set_title('Population')
	ax1.plot(data["Year"], data["Herbivores"], label='Herbivores')
	ax1.plot(data["Year"], data["Carnivores"], label='Carnivores')
	ax1.legend()

	ax2.set_title('Fitness')
	ax2.plot(data["Year"], data["HFitness"], label='Herbivore')
	ax2.plot(data["Year"], data["CFitness"], label='Carnivore')
	ax2.legend()
	plt.show()
except Exception as e:
	print(e)
	input()

