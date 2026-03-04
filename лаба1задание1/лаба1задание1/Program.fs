open System

let rec spisok() : string list = 
    printf "введите слово (0 - закончить): "
    let input = Console.ReadLine()
    if input = "0" then 
        []
    else 
        input :: spisok()

[<EntryPoint>]
let main args = 
    let list = spisok()
    printf "%A" list
    0
    