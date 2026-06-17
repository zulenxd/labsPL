open System

let rec readInt prompt =
    printf "%s" prompt
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | true, value when value > 0 -> value
    | _ ->
        printfn "Ошибка: введите положительное целое число."
        readInt prompt

let readStrings count =
    seq {
        for i in 1 .. count do
            printf "Введите строку %d: " i
            yield Console.ReadLine()
    }

let getStringLengths strings =
    strings |> Seq.map (fun s -> String.length s)

[<EntryPoint>]
let main args =
    let count = readInt "Введите количество строк: "
    
    let sequence = readStrings count
    let result = getStringLengths sequence

    printfn "Длины введенных строк: %A" (result |> Seq.toList)
    
    0
