open System

// 1. Указываем, что возвращаем seq<string> вместо string list
let rec getWords count : seq<string> = 
    seq {
        if count > 0 then
            printf "Введите слово (осталось %d): " count
            let input = Console.ReadLine()
            
            // Выдаем текущий введенный элемент в последовательность
            yield input
            
            // Рекурсивно выдаем все остальные элементы
            yield! getWords (count - 1)
    }

[<EntryPoint>]
let main args = 
    printf "Сколько слов хотите ввести? "
    let count = int (Console.ReadLine())

    // 2. Получаем последовательность введенных слов
    let wordSeq = getWords count 
    
    // 3. Меняем List.map на Seq.map
    let lengths = Seq.map String.length wordSeq
    
    // Выводим результат. Используем Seq.toList чисто для наглядного вывода в консоль,
    // чтобы увидеть все элементы (иначе ленивая последовательность выведет seq [ ... ])
    printfn "Длины введенных слов: %A" (Seq.toList lengths)
    0