open System

let rec getNumbers count : seq<int> = 
    seq {
        if count > 0 then 
            printf "Введите число (осталось %d): " count
            let input = Console.ReadLine()
            yield int input
            yield! getNumbers (count - 1)
    }

[<EntryPoint>]
let main args =
    printf "Сколько элементов вы хотите ввести? "
    let count = int (Console.ReadLine())
   
    let numSeq = getNumbers count |> Seq.cache
    
    printf "Введите цифру для поиска: "
    let numSearch = Console.ReadLine().[0]
    
    let result = 
        Seq.fold (fun acc x -> 
            if (string x).Contains(numSearch) then acc + x else acc
        ) 0 numSeq

    printfn "Введенные числа: %A" (Seq.toList numSeq)
    printfn "Сумма чисел, содержащих цифру %c: %i" numSearch result
    0
