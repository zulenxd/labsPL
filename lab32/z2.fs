// Задание 2. Вариант 4.
// Найти сумму тех элементов списка, в которых встречается заданная цифра.

open System

/// Ввод целого положительного числа (для количества элементов)
let rec readInt prompt =
    printf "%s" prompt
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | true, value when value > 0 -> value
    | _ ->
        printfn "Ошибка: введите положительное целое число."
        readInt prompt

/// Ввод любого целого числа (для самих элементов списка)
let rec readElement prompt =
    printf "%s" prompt
    let input = Console.ReadLine()
    match Int32.TryParse input with
    | true, value -> value
    | _ ->
        printfn "Ошибка: введите целое число."
        readElement prompt

/// Ввод одной цифры для поиска
let rec readDigit prompt =
    printf "%s" prompt
    let input = Console.ReadLine()
    // Проверяем, что введена ровно одна цифра от 0 до 9
    if input.Length = 1 && Char.IsDigit(input.[0]) then
        input.[0]
    else
        printfn "Ошибка: введите ровно одну цифру (от 0 до 9)."
        readDigit prompt

/// Создаёт последовательность введённых чисел
let readNumbers count =
    seq {
        for i in 1 .. count do
            yield readElement (sprintf "Введите число %d: " i)
    }

[<EntryPoint>]
let main _ =
    let count = readInt "Введите количество элементов в списке: "
    let numbers = readNumbers count
    let targetDigit = readDigit "Введите цифру для поиска: "

    // Вычисляем сумму элементов с помощью Seq.fold и аккумулятора
    let targetSum =
        numbers
        |> Seq.fold (fun acc num ->
            // Проверяем, содержит ли текущее число нужную цифру
            if num.ToString().Contains(string targetDigit) then
                acc + num // Если содержит, "прилепляем" число к нашему кому (аккумулятору)
            else
                acc       // Если нет, передаем аккумулятор дальше без изменений
        ) 0 // 0 — это стартовое значение нашего аккумулятора

    printfn "\nСумма элементов, содержащих цифру '%c': %d" targetDigit targetSum

    0
