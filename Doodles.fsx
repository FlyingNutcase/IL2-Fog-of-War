open System.Windows.Forms.VisualStyles
//
//  From Tutorials Point
//
type website = { Title: string;
                 Url:   string}

let homepage = { Title = "IL2 Full Mission Builder";
                 Url   = "www.il2fullmissionbuilder.com" } 

let google = { Title = Google.com;
               Url = "www.google.com"}    

let url = homepage.Url

type switch =
    | On
    | Off
    


type PlaneEntry = 
    { Name: string
      Index: int
      LinkTrId: int
      XPos: float
      YPos: float
      XOri: float
      YOri: float
      ZOri: float
      Script: string
      Model: string
      Country: int
      Desc: string
      Skin: string
      AILevel: int
      CoopStart: int
      NumberInForamtion: int
      Vulnerable: int
      Engageable: int
      LimitAmmo: 
      }

type entityType = 
    | Plane
    | Vehicle
    | Block;
    | Mcu 

type missionFileEntity = { TypeOfEntity: entityType;
                           Index: int
                           Text: string }

//  simulate reading the entities
let thisEntity = { TypeOfEntity = entityType.Vehicle;
                   Index = 6;
                    Text = "This is the mission text." }

let index = thisEntity.Index                           

                  