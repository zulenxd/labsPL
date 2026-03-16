open System

let rec getWords count : seq<string> = 
    seq {
        if count > 0 then
            printf "Введите слово (осталось %d): " count
            let input = Console.ReadLine()
            yield input
            yield! getWords (count - 1)
    }

[<EntryPoint>]
let main args = 
    printf "Сколько слов хотите ввести? "
    let count = int (Console.ReadLine())

    let wordSeq = getWords count 
    
    let lengths = Seq.map String.length wordSeq
    
    printfn "Длины введенных слов: %A" (Seq.toList lengths)
    0
