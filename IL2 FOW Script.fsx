open System
///
/// FOG OF WAR v0.1
/// Where the randomisation is done via replacement of placeholder objects in a Mission file with
/// a random object from a list (equal probability).
/// 

//
//  FILE & FOLDER PATHS
//               
let missionFileName = "For FOW Vehicle Testing"
let dirPathMissions = @"C:\p\1C Game Studios\IL-2 Sturmovik Great Battles\data\Missions\FN\"

let filePathMission = 
    dirPathMissions + @"\" + missionFileName + ".Mission"

let writeFilePath = 
    dirPathMissions + missionFileName + "-fow.Mission"

(*
//  check paths
printfn "Read path: %s" filePathMission
printfn "Write path: %s" writeFilePath
*)

//
//  1. READ the mission file
//
let missionText = 
    System.IO.File.ReadAllText(filePathMission)


//
//  2. PARSE the entries from the Mission file
//      - the  Mission file will reconstructed from these entries later to create the new, randomized mission
//  

//  2A As Regex Matches
let getMatchCollection regexPattern textToParse =
    let r =  System.Text.RegularExpressions.Regex(regexPattern)
    r.Matches textToParse

let sectionEditorVersionPattern = "\s*# Mission File Version = \d+\.\d+;" // "\s.#\s+Mission File Version = \d+\.\d+;"
let editorVersionMatches = getMatchCollection sectionEditorVersionPattern missionText

let sectionOptionsPattern = """Options\s+{[\s\w=;\"\\\.:-]+\s+{[\s\d:;]+}\s+Countries\s+{[\s\d:;]+}\s+}"""
let sectionOptionsMatches = getMatchCollection sectionOptionsPattern missionText

let planeEntryPattern = """Plane\s+{\s+[\w =\"";_\.\s\\-]+}"""
let planeMatches = getMatchCollection planeEntryPattern missionText

let vehicleEntryPattern = """Vehicle\s+{\s+[\w =\"";_\.\s\\-]+}"""
let vehicleMatches = getMatchCollection  vehicleEntryPattern missionText

let blockEntryPattern = """Block\s+{\s+[\w =\"";_\.\s\\-]+}"""
let blockMatches = getMatchCollection blockEntryPattern missionText

let mcuEntryPattern = """MCU_TR_Entity\s+{\s+[]\[\w\s=;\"\.]+}"""  //  """MCU_TR_Entity\s+{\s+[\w \[]=\"";_\.\s\\-]+}"""
let mcuMatches = getMatchCollection mcuEntryPattern missionText

//  simple count of matches for each entry type
printfn "missionVersionMatches.Count = %i"  editorVersionMatches.Count
printfn "sectionOptionsMatches.Count = %i"  sectionOptionsMatches.Count
printfn "planeMatches.Count = %i"  planeMatches.Count
printfn "vehicleMatches.Count = %i"  vehicleMatches.Count
printfn "blockMatches.Count = %i" blockMatches.Count
printfn "mcuMatches.Count = %i"  mcuMatches.Count

for i in 0..blockMatches.Count - 1 do
    printfn "blockMatches.Item.[%i] = %A" i <| blockMatches.Item i


//  2B Convert the Matches to Lists
let getMatchValue (m: System.Text.RegularExpressions.Match) = m.Value

let missionVersionList = editorVersionMatches |> Seq.cast |> Seq.map getMatchValue |> Seq.toList
let missionOptionsList = sectionOptionsMatches |> Seq.cast |> Seq.map getMatchValue |> Seq.toList
let planeEntriesList = planeMatches |> Seq.cast |> Seq.map getMatchValue |> Seq.toList
let vehicleEntriesList = vehicleMatches |> Seq.cast |> Seq.map getMatchValue |> Seq.toList
let blockEntriesLIst = blockMatches |> Seq.cast |> Seq.map getMatchValue |> Seq.toList
let mcuEntriesList = mcuMatches |> Seq.cast |> Seq.map getMatchValue |> Seq.toList

//
// The random object generator
//
let myRandGen = System.Random()
let getRandomObject (possibles: string list) = 
  let randIndex  = myRandGen.Next(possibles.Length) 
  possibles.[randIndex] 

//
//  The placeholder and list of possible replacements
//
let placeholderObject = "ba10m"
let possibleObjects = ["_t34-76stz"; "ba64"; "ford-g917";"gaz-aa-m4-aa"]

(*
//  test the random object generator 
printfn "Random: %s" (getRandomObject possibleObjects )
printfn "Random: %s" (getRandomObject possibleObjects )
printfn "Random: %s" (getRandomObject possibleObjects )
*)

//
//  3. RANDOMIZE - Replace entries containing the placeholder with randome objects from the possible
//      objects list, 
//
let hasPlaceholderObjectInText (text: string) = ( text.IndexOf(placeholderObject) > 0 )

let randomisedEntriesList (entriesList: List<string>) = 
    entriesList
    |> List.map (fun entry -> 
                    match entry with 
                        | entry when hasPlaceholderObjectInText entry ->
                            let newVehicle = (getRandomObject possibleObjects)
                            printfn "New Vehicle: %s" newVehicle
                            entry.Replace( placeholderObject, newVehicle )
                        | _ -> printfn "NO placeholder entry"  
                               entry 
    )                                          

//  get the randomized entries list
let randomizedPlaneEntriesList = randomisedEntriesList planeEntriesList
let randmizedVehicleEntriesList = randomisedEntriesList vehicleEntriesList
let randomizedBlockEntriesList = randomisedEntriesList blockEntriesLIst

printfn "randomizedPlaneEntriesList.Count: %i" randomizedPlaneEntriesList.Length
printfn "randmizedVehicleEntriesList.Count: %i" randmizedVehicleEntriesList.Length
printfn "randomizedBlockEntriesList.Count: %i" randomizedBlockEntriesList.Length

//
//  4. RECONSTRUCT the Mission file text
//
let allMissionElements = missionVersionList @ missionOptionsList @ planeEntriesList @ randmizedVehicleEntriesList @ randomizedBlockEntriesList @ mcuEntriesList @ ["# end of file"]

let newFileContents = 
    allMissionElements
    |> String.concat (System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine)

//
//  5. WRITE the new Mission file
//
System.IO.File.WriteAllText(writeFilePath, newFileContents)
