import matplotlib.pyplot as plt
import numpy as np
import sys

try:
	data = np.genfromtxt("../../Results/SimResult.csv", delimiter=",", names=["Year", "Herbivores", "Carnivores", "HFitness", "CFitness", "HerbivoreAvgAge", "CarnivoreAvgAge", "HerbivoreAvgWeight", "CarnivoreAvgWeight"])
	fig = plt.figure(1)
	ax1 = fig.add_subplot(411)
	ax2 = fig.add_subplot(412)
	ax3 = fig.add_subplot(413)
	ax4 = fig.add_subplot(414)

	plt.xlabel('Year')

	ax1.set_title('Population')
	ax1.plot(data["Year"], data["Herbivores"], label='Herbivores')
	ax1.plot(data["Year"], data["Carnivores"], label='Carnivores')
	ax1.legend()

	ax2.set_title('Fitness')
	ax2.plot(data["Year"], data["HFitness"], label='Herbivore')
	ax2.plot(data["Year"], data["CFitness"], label='Carnivore')
	ax2.legend()

	ax3.set_title('Average age')
	ax3.plot(data["Year"], data["HerbivoreAvgAge"], label='Herbivore')
	ax3.plot(data["Year"], data["CarnivoreAvgAge"], label='Carnivore')
	ax3.legend()

	ax4.set_title('Average Weight')
	ax4.plot(data["Year"], data["HerbivoreAvgWeight"], label='Herbivore')
	ax4.plot(data["Year"], data["CarnivoreAvgWeight"], label='Carnivore')
	ax4.legend()

	plt.subplots_adjust(left=0.1, 
                    bottom=0.1,  
                    right=0.9,  
                    top=0.9,  
                    wspace=0,  
                    hspace=.8) 
	plt.show()
except Exception as e:
	print(e) #To be able to see error messages from external view
	input()

