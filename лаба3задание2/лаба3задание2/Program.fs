open System

// Генератор остается без изменений, он возвращает seq<int>
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
    
    // ВМЕСТО Seq.cache используем Seq.toList
    // Это заставит генератор отработать один раз и сохранить результат в память
    let numList = getNumbers count |> Seq.toList
    
    // Теперь мы печатаем уже сохраненные в памяти числа
    printfn "%A" numList  
    
    printf "Введите цифру для поиска: "
    let numSearch = Console.ReadLine().[0]
    
    // Seq.fold прекрасно принимает список, так как список является последовательностью
    let result = 
        Seq.fold (fun acc x -> 
            if (string x).Contains(numSearch) then 
                acc + x 
            else 
                acc
        ) 0 numList

    printfn "Сумма чисел, содержащих цифру %c: %i" numSearch result
    0