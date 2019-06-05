# IL2-Fog-of-War
Randomization for the IL2 Battle Over Stalingrad Series

The original IL2 Fog of War was for the IL2 1946 series and written from around 2004. After a lot of intervening years I'm back into the IL2 series, with the new generation, IL2 Battle of Stalingrad series (Battle of Stalingrad, Battle of Moscow, Battle of Kuban, Battle of Bodenplatte).

This version is being written with the core code in F#, which I'm new at and very open to feedback. The GUI will be done in C# as a desktop program.

__Version 0.1__
This is the first version. It enables the random replacement of one placeholder object, and is setup fo Vehicle objects.

__Known Issues__
The entity order is not numeric but is all vehicles then all MCU's. Numerical ordering will come in when each Entity is a Record with an Index number to sort by.

__Known Bugs__ When randomizing planes (with  no placeholder at present), the randmoized list came back empty whereas it was expected to have come back with the single player aircraft in the test mission.

__Next Steps__
1. Resolve the above bug.
2. Set it up for one placeholder for each entity type (planes, vehicles and blocks or now), as required.
3. Then set it up for multiple (0..n) placeholders of each entity type - either with for each entity a List containing a List of Possibles, and a 1-element List of Placeholder, or better a List of Records containing a List of Possibles and Placeholder (string).
4. Have the entities written in numerical order to the FOW mission file.
5. Then perhaps custom probabilities for each Possible.

Cheers,
Flying Nutcase
June 3rd 2019
