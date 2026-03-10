open System

let rec getWords count : string list = 
    if count <= 0 then
        []
    else 
        printf "Введите слово (осталось %d): " count

        let input = Console.ReadLine()
        input :: getWords (count - 1)

[<EntryPoint>]
let main args = 
    printf "Сколько слов хотите ввести? "

    let count = int (Console.ReadLine())
    let wordList = getWords count 
    let lengths = List.map String.length wordList

    printfn "Длины введенных слов: %A" lengths
    0