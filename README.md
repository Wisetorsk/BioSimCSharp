# BioSim Project 


## <u>What tf is it.</u>
<p>Simulates an island and some basic animals in it. Animals can interact with each other and the enviroment they occupy. This impacts the fitness of the animals.</p>
<p><u>Annual cycle:</u></br>
<ol>
    <li>Food Grows in each cell</li>
    <li>Herbivores feed on grown food in cell</li>
    <li>Carnivores hunt and feed on herbiovres</li>
    <li>Dead herbivores are removed from enviroment</li>
    <li>Animals mate and give birth to offspring</li>
    <li>Animals migrate to neighboring cells</li>
    <li>Animals grow older</li>
    <li>Animals have a "chance" to die</li>
    <li>Dead animals are removed from enviroment</li>
</ol>
</p>

<hr/>

## <u>Dependencies</u>
<p>Animal generation uses <a href="https://www.nuget.org/packages/MathNet.Numerics">MathNet.Numerics</a> (NuGet)</br>
Data post processing and visualization uses ffmpeg & python 3 with numpy and matplotlib </br>
FFmpeg is available through Chocolatey or directly from <a href="https://www.ffmpeg.org">ffmpeg.org</a></br>
Automatic installation of all depenencies may be available through install script in the future.</p>

<hr/>

## <u>Features</u>
<p>Main simulation runs as a console app, with possibilities to control and alter parameters through a MVC web interface</p>

### **Results** 
<p>Simulation object can generate output graphs as png through python. Data is saved as csv files available in the <em><b>Results</b></em> folder <br/></p>

Current output csv files: </br>
* <em>simResult.csv</em> - Global overview of island population and average "life" parameters.
* <em>HerbivorePopulation.csv</em> - Cell by cell overview of Herbivore population by year.
* <em>CarnivorePopulation.csv</em> - Cell by cell overview of Carnivore population by year.

<hr/>

## <u>Classes</u>
> ### **Sim**
> Main simulation class can be instanced using this constructor
> ```cs
> Sim(int yearsToSimulate, string template, bool noMigration);
> ```
> <u>Parameters:</u> <br/>
> - yearsToSimulate -> `integer` value for the maximum amount of years the simulation is allowed to run.
> - template -> `string` representing the layout of the island to be simulated using "newline" char for linebreak. ex: `"DDD\nSSS\nJJJ"` is a three by three island with three "Desert" cells, three "Savannah" cells, and three "Jungle" cells. </br>
>   ><u>Cell types:</u></br>
>   >Ocean and Mountain cells are impassable and uninhabitable to animals
>   > - `"S"` - Savannah
>   > - `"J"` - Jungle
>   > - `"D"` - Desert
>   > - `"M"` - Mountain
>   > - `"O"` - Ocean </br>
> - noMigration -> `bool` value deciding wether animals are allowed to move between cells 
>
> Simulation can be started by invoking the method ```Sim.Simulate();``` <br/>
> The simulation will run until one of two arguments is met: 
> - All animals are dead
> - Current simulated year == yearsToSimulate

>### **Animal**
> <p>Main Animal class</p>

>### **Enviroment**
> <p>Main Enviroment class</p>

>### **Parameters**
> The parameter class and subclasses decide the behaviour of the animals based on animal class (herbivoreParams/carnivoreParams) and the available food and re-growth rate of food in the island cells.
> 

<hr/>

## <u>WebInterface</u>