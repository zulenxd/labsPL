// Задание 1. Вариант 4.
// Получить список из длин строк, содержащихся в исходном списке.

open System

/// Ввод целого положительного числа с повторением при ошибке
let rec readInt prompt =
    printf "%s" prompt
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | true, value when value > 0 -> value
    | _ ->
        printfn "Ошибка: введите положительное целое число."
        readInt prompt

/// Запрашивает у пользователя заданное количество строк
let readStrings count =
    seq {
        for i in 1 .. count do
            printf "Введите строку %d: " i
            yield Console.ReadLine()
    }

/// Получает длины строк из последовательности
let getStringLengths strings =
    // Используем Seq.map, чтобы преобразовать каждую строку (s) в её длину
    strings |> Seq.map (fun s -> String.length s)

[<EntryPoint>]
let main args =
    let count = readInt "Введите количество строк: "
    
    // Получаем исходную последовательность строк
    let sequence = readStrings count
    
    // Получаем последовательность из длин этих строк
    let result = getStringLengths sequence

    // Выводим результат, предварительно преобразовав последовательность в список
    printfn "Длины введенных строк: %A" (result |> Seq.toList)
    
    0
