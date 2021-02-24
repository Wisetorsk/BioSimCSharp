# BioSim Project 


## <u>What tf is it.</u>
<p>Simulates an island and some basic animals in it. Animals can interact with each other and the enviroment they occupy. This impacts the fitness of the animals.</p>

## <u>Dependencies</u>
<p>Animal generation uses <a href="https://www.nuget.org/packages/MathNet.Numerics"/>MathNet.Numerics</a> (NuGet)</br>
Data post processing and visualization uses ffmpeg & python 3 with numpy and matplotlib </br>
FFmpeg is available through Chocolatey or directly from <a href="https://www.ffmpeg.org">ffmpeg.org</a></br>
Automatic installation of all depenencies may be available through install script in the future.</p>


## <u>Features</u>
<p>Main simulation runs as a console app, with possibilities to control and alter parameters through a MVC web interface</p>

### **Results** 
<p>Simulation object can generate output graphs as png through python. Data is saved as csv files available in the <em><b>Results</b></em> folder <br/></p>

Current output csv files: </br>
* <em>simResult.csv</em> - Global overview of island population and average "life" parameters.
* <em>HerbivorePopulation.csv</em> - Cell by cell overview of Herbivore population by year.
* <em>CarnivorePopulation.csv</em> - Cell by cell overview of Carnivore population by year.


## <u>Classes</u>
> ### **Sim**
> Main simulation class can be instanced using this constructor
> ```cs
> Sim(int yearsToSimulate, string template, bool noMigration);
> ```
> Simulation can be started by invoking the method ```Sim.Simulate();``` <br/>
> The simulation will run until one of two arguments is met: 
> - All animals are dead
> - Current simulated year == yearsToSimulate

>### **Animal**
> <p>Main Animal class</p>

>### **Enviroment**
> <p>Main Enviroment class</p>

>### **Parameters**
> <p>Parameter class</p>


## <u>WebInterface</u>