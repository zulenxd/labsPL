open System

let rec getNumbers count : int list = 
    if count <= 0 then 
        [] 
    else 
        printf "Введите число (осталось %d): " count

        let input = Console.ReadLine()
        (int input) :: getNumbers (count - 1)

[<EntryPoint>]
let main args =
    
    printf "Сколько элементов вы хотите ввести? "

    let count = int (Console.ReadLine())
    let NumList = getNumbers count
    
    printfn "%A" NumList  
    printf "Введите цифру для поиска: "

    let NumSearch = Console.ReadLine().[0]
    let Result = 
        List.fold (fun acc x -> 
            if (string x).Contains(NumSearch) then 
                acc + x 
            else 
                acc
        ) 0 NumList

    printfn "Сумма чисел, содержащих цифру %c: %i" NumSearch Result
    0
